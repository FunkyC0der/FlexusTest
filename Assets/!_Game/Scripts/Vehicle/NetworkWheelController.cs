using Unity.Netcode;
using UnityEngine;

namespace FlexusTest.Vehicle
{
  [RequireComponent(typeof(WheelController))]
  public class NetworkWheelController : NetworkBehaviour
  {
    private WheelController _controller;

    private void Awake()
    {
      _controller = GetComponent<WheelController>();
      _controller.OnTrailVFXEmittingChanged += OnTrailVFXEmittingChanged;
    }

    private void OnTrailVFXEmittingChanged(bool isEmitting)
    {
      if(IsOwner)
        ChangeTrailVFXEmittingNotOwnerRpc(isEmitting);
    }

    [Rpc(SendTo.NotOwner, InvokePermission = RpcInvokePermission.Owner)]
    private void ChangeTrailVFXEmittingNotOwnerRpc(bool isEmitting) =>
      _controller.TrailVFX.emitting = isEmitting;
  }
}