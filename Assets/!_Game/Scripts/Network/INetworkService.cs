using System;

namespace FlexusTest.Network
{
  public interface INetworkService
  {
    event Action OnLocalClientStarted;
    public event Action<ulong> OnClientConnected;
    
    void StartHost();
    void StartClient();
  }
}