using System;
using Unity.Netcode;
using UnityEngine;

namespace FlexusTest.Network
{
  public class NetworkService : MonoBehaviour, INetworkService
  {
    public event Action OnLocalClientStarted;
    public event Action<ulong> OnClientConnected;

    private void Start()
    {
      NetworkManager.Singleton.OnClientStarted += OnLocalClientStarted;
      NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    public void StartHost() => 
      NetworkManager.Singleton.StartHost();

    public void StartClient() => 
      NetworkManager.Singleton.StartClient();
  }
}