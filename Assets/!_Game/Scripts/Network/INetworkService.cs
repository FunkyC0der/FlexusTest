using System;

namespace FlexusTest.Network
{
  public interface INetworkService
  {
    event Action OnLocalClientStarted;
    void StartHost();
    void StartClient();
  }
}