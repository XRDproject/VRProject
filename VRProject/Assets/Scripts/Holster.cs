using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; // Make sure you are using the TextMeshPro namespace

public class Holster : MonoBehaviour
{
    Color ogColor;
    [SerializeField] Material collisionColor;
    [SerializeField] TextMeshPro FirstText; // Changed to TextMeshPro for 3D text
     [SerializeField] AudioClip TickingSound;
     [SerializeField] AudioSource audioSource1;
     [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioClip BellSound;
    KillRedFella fellaDied;
    float timeRemaining = 10;
    bool timerIsRunning = false;
    bool timeHasCome = false;
    public bool playerDead = false;

    // [SerializeField] TextMeshPro SecondText; // Uncomment if you have a second TextMeshPro that you want to use later

    void Start()
    {
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
            audioSource1.Stop();
            audioSource1.PlayOneShot(BellSound);
            FirstText.SetText("Fire!!!");
            FirstText.gameObject.SetActive(true);
            timeRemaining = 0;
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
         yield return new WaitForSeconds(3);
         if (!fellaDied)
        {
            playerDead = true;
            // Handle player's death
            Debug.Log("Player has died.");
        }
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
        audioSource1.Stop();
    }
}
