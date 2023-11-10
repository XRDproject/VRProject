using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure you are using the TextMeshPro namespace

public class Holster : MonoBehaviour
{
    Color ogColor;
    [SerializeField] Material collisionColor;
    [SerializeField] TextMeshPro FirstText; // Changed to TextMeshPro for 3D text
     [SerializeField] AudioClip TickingSound;
     [SerializeField] AudioSource audioSource1;
     [SerializeField] AudioSource audioSource2;
     [SerializeField] AudioClip BellSound;
     bool check = false;

     private float timeToWait;
    // [SerializeField] TextMeshPro SecondText; // Uncomment if you have a second TextMeshPro that you want to use later

    void Start()
    {
        ogColor = gameObject.GetComponent<Renderer>().material.color;
        Debug.Log(ogColor.ToString());
        // Ensure the text is visible at the start if it's supposed to be
        if (FirstText != null)
            FirstText.enabled = true;
        // If you have a second text, you should initialize it as well.
        // if (SecondText != null)
        //     SecondText.enabled = true;
         StartCoroutine(RandomTimerCoroutine());
    }
     private IEnumerator RandomTimerCoroutine()
    {
        while (true)
        {
            // Set a random time between 3 and 12 seconds
            timeToWait = Random.Range(3f, 12f);
            Debug.Log("Timer set for: " + timeToWait + " seconds");

            // Wait for the timeToWait duration
            yield return new WaitForSeconds(timeToWait);

            // Timer completed
            Debug.Log("Timer completed after " + timeToWait + " seconds");

            // Do something here after the timer completes
            check = true;
            yield return check;  
            // OnTimerComplete();

            // Optionally, restart the timer or break the loop to stop
            // StartCoroutine(RandomTimerCoroutine()); // Restart the timer
            // break; // Stop the timer
        }
    }

    private void OnTimerComplete()
    {
        // Trigger any actions that should happen after the timer completes
        audioSource2.PlayOneShot(BellSound);
        
    }
    void OnTriggerEnter(Collider objectName)
    {
        Debug.Log("Entered collision with " + objectName.gameObject.name);


        audioSource1.Play();

    }

    void OnTriggerStay(Collider objectName)
    {
        Debug.Log("Colliding with " + objectName.gameObject.name);
        gameObject.GetComponent<Renderer>().material.color = collisionColor.color;
        if (objectName.CompareTag("Pistol"))
        {
            // Assuming GunShoot is a script on the pistol that handles shooting logic
            objectName.GetComponent<GunShoot>().canShoot = false;
        }


    }

    void OnTriggerExit(Collider objectName)
    {
        Debug.Log("Exited collision with " + objectName.gameObject.name);
        gameObject.GetComponent<Renderer>().material.color = ogColor;
        if (objectName.CompareTag("Pistol"))
        {
          if(check){

             OnTimerComplete();
            // Assuming GunShoot is a script on the pistol that handles shooting logic
            objectName.GetComponent<GunShoot>().canShoot = true;
            // Disable the first text when the gun is holstered
        if (FirstText != null)
            FirstText.gameObject.SetActive(false); // Changed to SetActive for disabling the GameObject


            
            // You might want to enable the second text here if needed
            // if (SecondText != null)
            //     SecondText.gameObject.SetActive(true);
          }
        }
    }
}
