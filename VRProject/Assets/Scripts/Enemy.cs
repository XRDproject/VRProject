using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    private Object Handgun;
    [FormerlySerializedAs("Animator")] public Animator animator;
    // Start is called before the first frame update
    
    private static readonly int Shoot = Animator.StringToHash("shoot");
    private static readonly int Cautious = Animator.StringToHash("cautious");

    private void StartShooting()
    {
        animator.SetTrigger(Shoot);
    }
    
    private void PrepareShooting()
    {
        animator.SetTrigger(Cautious);
    }
    
}
