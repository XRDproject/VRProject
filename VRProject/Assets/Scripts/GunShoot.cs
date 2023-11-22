using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.OpenXR.Input;

public class GunShoot : MonoBehaviour
{
    [Header("Location Refrences")]
    [SerializeField] private Transform barrelLocation;

    [Header("Inputs")]
    [Tooltip("Specify trigger button")][SerializeField] InputActionReference triger;
    [Tooltip("Specify controller haptics")][SerializeField] InputActionReference haptics;
    

    [Header("Gun stats")]
    public float damage = 10f;
    public float headDamage = 35f;
    public float range = 100f;
    public float fireRate = 2f;
    public float impactForce = 6f;
    public int maxAmmo = 12;
    public bool canShoot = true;
    [Range(1.0f, 5.0f)]
    [Tooltip("Changes how fast the gun fires")]public float animFireSpeed = 1f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip gunFireSound;
    public AudioClip gunReloadSound;
    public AudioClip gunNoAmmoSound;

    [Header("Visual effects")]
    [Tooltip("Used for bullet holes and stuff")] public HitEffect hitEffect;

    [Header("Ammo indicator")]
    public TextMeshPro ammoText;

    private int currentAmmo;

    private void Start()
    {
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        triger.action.started += ctx =>
        {
            if (currentAmmo > 0 && canShoot)
            {
                Shoot();
                if (ctx.control.device is XRController device)
                {

                    Rumble(device);//vibrate

                    //OpenXRInput.SendHapticImpulse(haptics, 1f, 1000f, device); //Right Hand Haptic Impulse, not working, found alternative
                    //Debug.Log(device.name);
                }
            }
            else audioSource.PlayOneShot(gunNoAmmoSound);
        };

        //Turn the gun to reload
        if (Vector3.Angle(transform.up, Vector3.up) > 100 && currentAmmo < maxAmmo)
            Reload();
    }

    private void Rumble(InputDevice device)
    {
        //2-thumb area?, 1- trigger, 0 - whole body
        var channel = 0;
        var command = UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand.Create(channel, 1f, 0.5f);
        device.ExecuteCommand(ref command);
        //OpenXRInput.SendHapticImpulse(haptics, 1, 10000, XRController.rightHand); //trying for Oculus
        Debug.Log("tried to vibrate");
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        //ammoText.SetText(currentAmmo.ToString()); //Change ammo indicator
        audioSource.PlayOneShot(gunReloadSound);
    }

    //Note: method is called inside the animation, using an Animation event
    void Shoot()
    {
        audioSource.PlayOneShot(gunFireSound);
        currentAmmo--;
        //ammoText.SetText(currentAmmo.ToString()); //Change ammo indicator

        if (Physics.Raycast(barrelLocation.position, barrelLocation.transform.up, out RaycastHit hit, range))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Player")) return;

            if (hit.transform.TryGetComponent<HitBox>(out var hitBox))
            {  
                    hitBox.OnBulletHit();            
                hitEffect.ShowHitEffect(hit, HitEffect.Effects.Blood, 1f);
            }
            else
            {
                hitEffect.ShowHitEffect(hit, HitEffect.Effects.Hole, 4f);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition(-hit.normal * impactForce, hit.point, ForceMode.Impulse);
            }
        }
    }

}