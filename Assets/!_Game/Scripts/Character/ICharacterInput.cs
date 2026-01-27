using UnityEngine;

namespace FlexusTest.Character
{
  public interface ICharacterInput
  {
    Vector2 Move();
    bool SprintIsPressed();
  }
}