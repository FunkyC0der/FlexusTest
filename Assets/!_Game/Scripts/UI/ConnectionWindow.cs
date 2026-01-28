using FlexusTest.Cameras;
using FlexusTest.Network;
using Sisus.Init;
using UnityEngine;
using UnityEngine.UI;
using CameraType = FlexusTest.Cameras.CameraType;

namespace FlexusTest.UI
{
  public class ConnectionWindow : MonoBehaviour<INetworkService, ICameraService>
  {
    [SerializeField]
    private Button _hostButton;
    
    [SerializeField]
    private Button _clientButton;
    
    private INetworkService _networkService;
    private ICameraService _cameraService;
    
    protected override void Init(INetworkService networkService, ICameraService cameraService)
    {
      _networkService = networkService;
      _cameraService = cameraService;
    }

    private void Start()
    {
      _hostButton.onClick.AddListener(() => _networkService.StartHost());
      _clientButton.onClick.AddListener(() => _networkService.StartClient());

      _networkService.OnLocalClientStarted += CloseWindow;
      
      _cameraService.ActivateCamera(CameraType.Menu);
    }

    private void CloseWindow() => 
      Destroy(gameObject);
  }
}