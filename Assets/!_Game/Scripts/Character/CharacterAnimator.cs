using UnityEngine;

namespace FlexusTest.Character
{
  [RequireComponent(typeof(Animator))]
  public class CharacterAnimator : MonoBehaviour, ICharacterAnimator
  {
    [SerializeField]
    private string _speedFloatName = "Speed";
    
    private Animator _animator;
    private int _speedFloatId;

    private void Awake()
    {
      _animator = GetComponent<Animator>();
      _speedFloatId = Animator.StringToHash(_speedFloatName);
    }

    public void SetSpeed(float speed)
    {
      _animator.SetFloat(_speedFloatId, speed);
    }
  }
}