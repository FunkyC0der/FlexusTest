using UnityEngine;

namespace FlexusTest.Character
{
  [RequireComponent(typeof(CharacterController))]
  public class CharacterGravityComponent : MonoBehaviour
  {
    private CharacterController _characterController;
    private float _verticalVelocity;

    private void Awake() =>
      _characterController = GetComponent<CharacterController>();

    private void Update() =>
      ApplyGravity();

    private void ApplyGravity()
    {
      if (_characterController.isGrounded)
      {
        _verticalVelocity = -2f;
      }
      else
      {
        _verticalVelocity += Physics.gravity.y * Time.deltaTime;
      }

      _characterController.Move(Vector3.up * (_verticalVelocity * Time.deltaTime));
    }
  }
}