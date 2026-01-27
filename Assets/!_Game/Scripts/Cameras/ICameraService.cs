using UnityEngine;

namespace FlexusTest.Cameras
{
  public interface ICameraService
  {
    void ActivateCamera(CameraType type);
    void SetCameraTarget(CameraType type, Transform target);
  }
}