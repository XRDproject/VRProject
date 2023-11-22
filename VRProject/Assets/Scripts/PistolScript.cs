using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.OpenXR.Input;

public class PistolScript : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip gunFireSound;

    public ParticleSystem muzzleFlash;


    //Note: method is called inside the animation, using an Animation event
   public void Shoot()
    {
        audioSource.PlayOneShot(gunFireSound);
        muzzleFlash.Play();
        
    }

}