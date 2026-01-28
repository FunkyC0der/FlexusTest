using FlexusTest.Network;
using FlexusTest.Utils;
using Sisus.Init;
using Unity.Netcode;
using UnityEngine;

namespace FlexusTest.Vehicle
{
  public class NetworkVehicleController : NetworkBehaviour<IVehicle>
  {
    private readonly NetworkVariable<bool> _isFree = new(writePerm: NetworkVariableWritePermission.Owner);
    
    private IVehicle _vehicle;
    
    protected override void Init(IVehicle vehicle) => 
      _vehicle = vehicle;
    
    public override void OnNetworkSpawn()
    {
      _isFree.OnValueChanged += OnIsFreeChanged;

      if (IsOwner)
        _isFree.Value = _vehicle.IsFree();
      else
        _vehicle.SetIsFree(_isFree.Value);

      Debug.Log($"Is Free: {_isFree.Value}");
    }

    public void UpdateIsFreeOnOwner() =>
      this.WaitConditionAndCallAction(condition: () => IsOwner, action: () => _isFree.Value = _vehicle.IsFree());

    private void OnIsFreeChanged(bool previousValue, bool newValue)
    {
      if(!IsOwner)
        _vehicle.SetIsFree(newValue);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
      if (!IsOwner || _isFree.Value)
        return;
    
      var otherNetworkVehicle = collision.collider.attachedRigidbody?.GetComponent<NetworkVehicleController>();
      if (!otherNetworkVehicle)
        return;
    
      if (otherNetworkVehicle._isFree.Value)
      {
        if (!otherNetworkVehicle.IsOwner) 
          otherNetworkVehicle.GetComponent<NetworkOwnershipHelper>()?.RequestOwnershipServerRpc();
      
        return;
      }

      if (otherNetworkVehicle.IsOwner) 
        return;

      otherNetworkVehicle.AddImpulseToVehicleOwnerRpc(-collision.impulse, collision.contacts[0].point);
    }
    
    [Rpc(SendTo.Owner, InvokePermission = RpcInvokePermission.Everyone)]
    private void AddImpulseToVehicleOwnerRpc(Vector3 impulse, Vector3 point) => 
      GetComponent<Rigidbody>().AddForceAtPosition(impulse, point, ForceMode.Impulse);
  }
}