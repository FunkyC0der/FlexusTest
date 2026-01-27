using FlexusTest.Cameras;
using Sisus.Init;
using UnityEngine;

namespace FlexusTest.Character
{
  [RequireComponent(typeof(CharacterController))]
  public class CharacterMover : MonoBehaviour<ICharacterInput, ICameraService, ICharacterAnimator>
  {
    [SerializeField]
    private float RunSpeed = 3;

    [SerializeField]
    private float SprintSpeed = 6;
    
    [SerializeField]
    private float Acceleration = 10f;
    
    [SerializeField]
    private float Deceleration = 20f;
    
    [SerializeField]
    private float DirectionChangeSpeed = 20;

    private float _speed;

    private CharacterController _characterController;
    
    private ICharacterInput _input;
    private ICameraService _cameraService;
    private ICharacterAnimator _animator;
    
    protected override void Init(ICharacterInput characterInput, ICameraService cameraService, ICharacterAnimator animator)
    {
      _input = characterInput;
      _cameraService = cameraService;
      _animator = animator;
    }

    protected override void OnAwake() => 
      _characterController = GetComponent<CharacterController>();

    private void Update()
    {
      Vector2 moveInput = _input.Move();

      float targetSpeed = 0;
        
      if (moveInput.sqrMagnitude > 0)
      {
        targetSpeed = _input.SprintIsPressed() ? SprintSpeed : RunSpeed;
        transform.forward = Vector3.MoveTowards(transform.forward, GetMoveDirection(moveInput), DirectionChangeSpeed * Time.deltaTime);
      }
        
      float speedChangeRate = _speed < targetSpeed ? Acceleration : Deceleration;
      _speed = Mathf.MoveTowards(_speed, targetSpeed, speedChangeRate * Time.deltaTime);

      _characterController.Move(transform.forward * (_speed * Time.deltaTime));
      
      _animator.SetSpeed(_speed);
    }

    private Vector3 GetMoveDirection(Vector2 moveInput)
    {
      Transform cameraTransform = _cameraService.GetMainCameraTransform();
      
      Vector3 cameraForward = cameraTransform.forward;
      Vector3 cameraRight = cameraTransform.right;
            
      cameraForward.y = 0f;
      cameraRight.y = 0f;
            
      cameraForward.Normalize();
      cameraRight.Normalize();
            
      Vector3 moveDirection = cameraForward * moveInput.y + cameraRight * moveInput.x;
      moveDirection = Vector3.ClampMagnitude(moveDirection, maxLength: 1);
      return moveDirection;
    }
  }
}