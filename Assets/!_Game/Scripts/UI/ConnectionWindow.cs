using FlexusTest.Network;
using Sisus.Init;
using UnityEngine;
using UnityEngine.UI;

namespace FlexusTest.UI
{
  public class ConnectionWindow : MonoBehaviour<INetworkService>
  {
    [SerializeField]
    private Button _hostButton;
    
    [SerializeField]
    private Button _clientButton;
    
    private INetworkService _networkService;
    
    protected override void Init(INetworkService networkService) => 
      _networkService = networkService;

    private void Start()
    {
      _hostButton.onClick.AddListener(() => _networkService.StartHost());
      _clientButton.onClick.AddListener(() => _networkService.StartClient());

      _networkService.OnLocalClientStarted += CloseWindow;
    }

    private void CloseWindow() => 
      Destroy(gameObject);
  }
}