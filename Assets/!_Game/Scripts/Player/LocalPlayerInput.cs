using FlexusTest.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlexusTest.Player
{
  public class LocalPlayerInput : MonoBehaviour, ICharacterInput
  {
    [SerializeField]
    private InputActionReference _moveInputAction;
    
    [SerializeField]
    private InputActionReference _sprintInputAction;

    public Vector2 Move() => 
      _moveInputAction.action.ReadValue<Vector2>();

    public bool SprintIsPressed() => 
      _sprintInputAction.action.IsPressed();
  }
}