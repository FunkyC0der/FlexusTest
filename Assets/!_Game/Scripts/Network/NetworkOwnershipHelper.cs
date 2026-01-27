using Unity.Netcode;
using UnityEngine.Events;

namespace FlexusTest.Network
{
  public class NetworkOwnershipHelper : NetworkBehaviour
  {
    public UnityEvent OnOwnershipGained;
    public UnityEvent OnOwnershipLost;

    public override void OnNetworkSpawn()
    {
      if(IsOwner)
        OnOwnershipGained?.Invoke();
      else
        OnOwnershipLost?.Invoke();
    }
    
    public void RequestOwnership() => 
      RequestOwnershipServerRpc();

    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    public void RequestOwnershipServerRpc(RpcParams rpcParams = default)
    {
      ulong senderClientId = rpcParams.Receive.SenderClientId;
      NetworkObject.ChangeOwnership(senderClientId);
    }

    protected override void OnOwnershipChanged(ulong previous, ulong current)
    {
      if (previous == current)
        return;
      
      if(previous == NetworkManager.Singleton.LocalClientId)
        OnOwnershipLost?.Invoke();
      
      else if(current == NetworkManager.Singleton.LocalClientId)
        OnOwnershipGained?.Invoke();
    }
  }
}