using UnityEngine;

namespace FlexusTest.Vehicle
{
  public interface IVehicle
  {
    bool IsFree();
    void SetIsFree(bool isFree);
    void Enter();
    void Exit();
    void SetMoveInput(Vector2 input);
    void SetBrakeIsPressed(bool isPressed);
    Vector3 GetDriverExitPosition();
    Transform GetTransform();
  }
}