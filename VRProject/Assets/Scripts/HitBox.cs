using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool fellaDied = false;
    private Animator _animator;
    private BoxCollider _boxCollider;
    private static readonly int Dying  = Animator.StringToHash("onDyingState");
    private static readonly int IsEnemyAlive  = Animator.StringToHash("IsEnemyAlive");
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
        
        fellaDied = true;
        _boxCollider.enabled = false;
        _animator.SetBool(IsEnemyAlive,false);
        _animator.SetTrigger(Dying);
    }
}
