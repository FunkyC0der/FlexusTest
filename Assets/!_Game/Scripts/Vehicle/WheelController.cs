using System;
using UnityEngine;

namespace FlexusTest.Vehicle
{
  public class WheelController : MonoBehaviour
  {
    public Action<bool> OnTrailVFXEmittingChanged;
    
    public WheelCollider Collider;
    public Transform Model;
    public TrailRenderer TrailVFX;

    private void FixedUpdate()
    {
      UpdateModelTransform();
      UpdateTrailVFX();
    }

    private void UpdateTrailVFX()
    {
      bool needEmit = Collider.isGrounded && Collider.brakeTorque > 0;
      if(needEmit == TrailVFX.emitting)
        return;
      
      TrailVFX.emitting = needEmit;
      OnTrailVFXEmittingChanged?.Invoke(needEmit);
    }

    private void UpdateModelTransform()
    {
      Collider.GetWorldPose(out Vector3 pos, out Quaternion quat);

      Model.position = pos;
      Model.rotation = quat;
    }
  }
}