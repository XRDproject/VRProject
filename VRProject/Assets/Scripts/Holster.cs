using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Serialization; // Make sure you are using the TextMeshPro namespace
using static Unity.VisualScripting.Member;

public class Holster : MonoBehaviour
{
    Color ogColor;
    [SerializeField] Material collisionColor;
    [SerializeField] TextMeshPro FirstText; // Changed to TextMeshPro for 3D text
     [SerializeField] AudioClip TickingSound;
     [SerializeField] AudioSource audioSource1;
     [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioSource bgMusic;
    public float fadeTime = 1f; // fade time in seconds
    [SerializeField] AudioClip BellSound;
    KillRedFella fellaDied;
    float timeRemaining = 10;
    bool timerIsRunning = false;
    bool timeHasCome = false;
    public bool playerDead = false;
    [SerializeField] public GameObject redFella;
    [SerializeField] public GameObject pistol;
    private Animator _redFellaAnimator;
    private BoxCollider _redFellaBoxCollider;
    private PistolScript _pistolScript;
    private static readonly int Shoot = Animator.StringToHash("onShootingState");
    private static readonly int Cautious = Animator.StringToHash("onCautiousState");
    private static readonly int IsPlayerAlive = Animator.StringToHash("IsPlayerAlive");
    // [SerializeField] TextMeshPro SecondText; // Uncomment if you have a second TextMeshPro that you want to use later

    void Start()
    {
        _redFellaAnimator = redFella.GetComponent<Animator>();
        _redFellaBoxCollider = redFella.GetComponent<BoxCollider>();
        _pistolScript = pistol.GetComponent<PistolScript>();
        ogColor = gameObject.GetComponent<Renderer>().material.color;
        Debug.Log(ogColor.ToString());
        // Ensure the text is visible at the start if it's supposed to be
        if (FirstText != null)
            FirstText.enabled = true;
        
    }


    void OnTriggerEnter(Collider objectName)
    {
        Debug.Log("Entered collision with " + objectName.gameObject.name);
        if (FirstText != null)
            FirstText.gameObject.SetActive(false); // Changed to SetActive for disabling the GameObject
        audioSource1.Play();
        timerIsRunning = true;
        timeRemaining = UnityEngine.Random.Range(5, 10);
        _redFellaAnimator.SetTrigger(Cautious);
        StartCoroutine(FadeSound());
    }

    void OnTriggerStay(Collider objectName)
    {
        gameObject.GetComponent<Renderer>().material.color = collisionColor.color;   
        if (timerIsRunning && timeRemaining > 0)
        {
            
            timeRemaining -= Time.deltaTime;
            if (objectName.CompareTag("Pistol"))
            {
                objectName.GetComponent<GunShoot>().canShoot = false;
            }
            if (timeRemaining <= 0)
                timeHasCome = true;
        }
        else if (timeHasCome)
        {
            Debug.Log("Time has run out!");
            _redFellaBoxCollider.enabled = true;
            audioSource1.Stop();
            audioSource1.PlayOneShot(BellSound);
            timeRemaining = 0;
            FirstText.SetText("");
            timerIsRunning = false;
            if (objectName.CompareTag("Pistol"))
            {
                objectName.GetComponent<GunShoot>().canShoot = true;
            }
            StartCoroutine(KillThePlayer());
            timeHasCome = false;
        }
    }

    IEnumerator  KillThePlayer(){
        _redFellaAnimator.SetTrigger(Shoot);
        float randomNumberTimer = UnityEngine.Random.Range(0.6f, 1.1f);
        yield return new WaitForSeconds(randomNumberTimer);
        _pistolScript.Shoot();
        if (!fellaDied)
         {
             playerDead = true;
             _redFellaAnimator.SetBool(IsPlayerAlive,false);
             // Handle player's death
             Debug.Log("Player has died.");
         }
    }

    IEnumerator FadeSound()
    {
        if (fadeTime == 0)
        {
            bgMusic.volume = 0;
            yield break;
        }
        float t = fadeTime;
        while (t > 0)
        {
            yield return null;
            t -= Time.deltaTime;
            bgMusic.volume = t / fadeTime;
        }
        yield break;
    }

    IEnumerator IncreaseSound()
    {
        float targetVolume = 1.0f; 
        float startVolume = 0.0f; 

        if (fadeTime == 0)
        {
            bgMusic.volume = targetVolume;
            yield break;
        }

        float t = 0.0f;
        while (t < fadeTime)
        {
            yield return null;
            t += Time.deltaTime;
            bgMusic.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeTime);
        }
        yield break;
    }

    void OnTriggerExit(Collider objectName)
    {
        Debug.Log("Exited collision with " + objectName.gameObject.name);
        gameObject.GetComponent<Renderer>().material.color = ogColor;
        if (objectName.CompareTag("Pistol"))
        {
            if (FirstText != null)
                FirstText.gameObject.SetActive(true);
            // Assuming GunShoot is a script on the pistol that handles shooting logic
            objectName.GetComponent<GunShoot>().canShoot = true;

            // You might want to enable the second text here if needed
            // if (SecondText != null)
            //     SecondText.gameObject.SetActive(true);

        }
        timerIsRunning = false;
        _redFellaAnimator.SetTrigger(Cautious);
        audioSource1.Stop();
        StartCoroutine(IncreaseSound());
    }
}
