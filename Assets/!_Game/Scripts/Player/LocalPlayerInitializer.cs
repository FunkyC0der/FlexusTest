using FlexusTest.Cameras;
using Sisus.Init;
using UnityEngine;
using CameraType = FlexusTest.Cameras.CameraType;

namespace FlexusTest.Player
{
  public class LocalPlayerInitializer : MonoBehaviour<ICameraService>
  {
    [SerializeField]
    private bool _autoInit = true;
    
    private ICameraService _cameraService;
    
    protected override void Init(ICameraService cameraService) => 
      _cameraService = cameraService;

    private void Start()
    {
      if (_autoInit)
        Init();
    }

    public void Init()
    {
      _cameraService.SetCameraTarget(CameraType.Character, transform);
      _cameraService.ActivateCamera(CameraType.Character);
    }
  }
}