using System;
using UnityEngine;

namespace FlexusTest.Vehicle
{
  public interface IVehicle
  {
    event Action OnEnter;
    event Action OnExit;

    bool IsFree();
    void SetIsFree(bool isFree);
    void Enter();
    void Exit();
    void Enable();
    void Disable();
    void SetMoveInput(Vector2 input);
    void SetBrakeIsPressed(bool isPressed);
    Vector3 GetDriverExitPosition();
    Transform GetTransform();
  }
}