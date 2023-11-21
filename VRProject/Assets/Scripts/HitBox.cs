using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool fellaDied = false;
    private Animator _animator;
    private BoxCollider _boxCollider;
    private static readonly int Dying  = Animator.StringToHash("onDyingState");
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void OnBulletHit()
    {
        Die();
    }

    public void Die()
    {   
        _animator.SetTrigger(Dying);
        fellaDied = true;
        _boxCollider.enabled = false;
    }
}
