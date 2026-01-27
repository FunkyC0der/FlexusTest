using UnityEngine;

namespace FlexusTest.Character
{
  [RequireComponent(typeof(Animator))]
  public class CharacterAnimator : MonoBehaviour, ICharacterAnimator
  {
    [SerializeField]
    private string _speedFloatName = "Speed";
    
    [SerializeField]
    private string _isDrivingBoolName = "IsDriving";
    
    private Animator _animator;
    private int _speedFloatId;
    private int _isDrivingBoolId;

    private void Awake()
    {
      _animator = GetComponent<Animator>();
      _speedFloatId = Animator.StringToHash(_speedFloatName);
      _isDrivingBoolId = Animator.StringToHash(_isDrivingBoolName);
    }

    public void SetSpeed(float speed) => 
      _animator.SetFloat(_speedFloatId, speed);

    public void EnterToVehicle() => 
      _animator.SetBool(_isDrivingBoolId, true);

    public void ExitFromVehicle() => 
      _animator.SetBool(_isDrivingBoolId, false);
  }
}