using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool fellaDied = false;

    public void OnBulletHit()
    {
      Die();
    }

    public void Die()
    {
        fellaDied = true;
        GetComponent<Animator>().enabled = false;
    }
}
