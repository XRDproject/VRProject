using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool fellaDied = false;
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnBulletHit()
    {
      Die();
    }

    public void Die()
    {
        fellaDied = true;
        _animator.enabled = false;
    }
}
