using Unity.XR.CoreUtils;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool fellaDied = false;
    private Animator _animator;
    private BoxCollider _boxCollider;
    private static readonly int Dying  = Animator.StringToHash("onDyingState");
    private static readonly int IsEnemyAlive  = Animator.StringToHash("IsEnemyAlive");
    [SerializeField] public GameObject player;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        //player = GetComponent<Holster>(); 
        // GameObject holster = GameObject.FindGameObjectWithTag("Player");
        // player =  holster;  
        // // transform.LookAt(player.transform);
    }

    private void Update(){
        transform.LookAt(player.transform);
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
