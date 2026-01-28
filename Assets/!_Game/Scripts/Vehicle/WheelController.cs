using UnityEngine;

namespace FlexusTest.Vehicle
{
  public class WheelController : MonoBehaviour
  {
    public WheelCollider Collider;
    public Transform Model;
    public TrailRenderer TrailVFX;

    private void FixedUpdate()
    {
      UpdateModelTransform();
      UpdateTrailVFX();
    }

    private void UpdateTrailVFX() => 
      TrailVFX.emitting = Collider.isGrounded && Collider.brakeTorque > 0;

    private void UpdateModelTransform()
    {
      Collider.GetWorldPose(out Vector3 pos, out Quaternion quat);

      Model.position = pos;
      Model.rotation = quat;
    }
  }
}