using System;
using UnityEngine;

namespace FlexusTest.Character
{
  public interface ICharacterInput
  {
    event Action OnEnterExitVehicle;
    
    Vector2 Move();
    bool SprintIsPressed();
    bool BrakeIsPressed();
    
  }
}