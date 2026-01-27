using System;
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
    
    [SerializeField]
    private InputActionReference _enterExitVehicleInputAction;
    
    [SerializeField]
    private InputActionReference _brakeInputAction;

    private void Start() => 
      _enterExitVehicleInputAction.action.performed += OnEnterExitVehiclePerformed;

    private void OnDestroy() => 
      _enterExitVehicleInputAction.action.performed -= OnEnterExitVehiclePerformed;

    public event Action OnEnterExitVehicle;
    
    public Vector2 Move() => 
      _moveInputAction.action.ReadValue<Vector2>();

    public bool SprintIsPressed() => 
      _sprintInputAction.action.IsPressed();

    public bool BrakeIsPressed() =>
      _brakeInputAction.action.IsPressed();

    private void OnEnterExitVehiclePerformed(InputAction.CallbackContext ctx) => 
      OnEnterExitVehicle?.Invoke();
  }
}