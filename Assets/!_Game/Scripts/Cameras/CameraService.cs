using System;
using Unity.Cinemachine;
using UnityEngine;

namespace FlexusTest.Cameras
{
  public class CameraService : MonoBehaviour, ICameraService
  {
    [Serializable]
    private class CameraInfo
    {
      public CameraType Type;
      public CinemachineCamera Camera;
    }

    [SerializeField]
    private CameraInfo[] _cameras;

    private CameraInfo _camera;

    public void ActivateCamera(CameraType type)
    {
      foreach (CameraInfo cameraInfo in _cameras)
      {
        if (cameraInfo.Type == type)
        {
          cameraInfo.Camera.Priority = 1;
          _camera = cameraInfo;
        }
        else
          cameraInfo.Camera.Priority = 0;
      }
    }

    public void SetCameraTarget(CameraType type, Transform target)
    {
      CameraInfo cameraInfo = Array.Find(_cameras, item => item.Type == type);
      if (cameraInfo == null)
      {
        Debug.LogWarning($"Failed to Set Camera Target. Camera not found: {type}");
        return;
      }      
      
      cameraInfo.Camera.Target.TrackingTarget = target;
    }

    public Transform GetMainCameraTransform() => 
      Camera.main.transform;
  }
}