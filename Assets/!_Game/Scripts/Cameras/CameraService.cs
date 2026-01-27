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
      foreach (CameraInfo camera in _cameras)
      {
        if (camera.Type == type)
        {
          camera.Camera.Priority = 1;
          _camera = camera;
        }
        else
          camera.Camera.Priority = 0;
      }
    }

    public void SetCameraTarget(CameraType type, Transform target)
    {
      CameraInfo camera = Array.Find(_cameras, item => item.Type == type);
      if (camera == null)
      {
        Debug.LogWarning($"Failed to Set Camera Target. Camera not found: {type}");
        return;
      }      
      
      camera.Camera.Target.TrackingTarget = target;
    }
  }
}