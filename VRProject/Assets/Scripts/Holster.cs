using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure you are using the TextMeshPro namespace

public class Holster : MonoBehaviour
{
    Color ogColor;
    [SerializeField] Material collisionColor;
    [SerializeField] TextMeshPro FirstText; // Changed to TextMeshPro for 3D text
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
    }

    void OnTriggerEnter(Collider objectName)
    {
        Debug.Log("Entered collision with " + objectName.gameObject.name);
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
