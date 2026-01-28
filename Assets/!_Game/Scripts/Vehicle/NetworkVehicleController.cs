using FlexusTest.Utils;
using Sisus.Init;
using Unity.Netcode;

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
      
      if(!IsOwner)
        _vehicle.SetIsFree(_isFree.Value);
    }

    public void UpdateIsFreeOnOwner() =>
      this.WaitConditionAndCallAction(() => IsOwner, () => _isFree.Value = _vehicle.IsFree());

    private void OnIsFreeChanged(bool previousValue, bool newValue)
    {
      if(!IsOwner)
        _vehicle.SetIsFree(newValue);
    }
  }
}