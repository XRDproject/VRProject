using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public KillRedFella KillHim;
   
    public void OnBulletHit()
    {
        KillHim.Die();
    }
}
