using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holster : MonoBehaviour
{
    Color ogColor;
    [SerializeField] Material collisiionColor;
    void Start()
    {
        ogColor = gameObject.GetComponent<Renderer>().material.color;
        Debug.Log(ogColor.ToString());
    }

    void OnTriggerEnter(Collider objectName)
    {
        Debug.Log("Entered collision with " + objectName.gameObject.name);
    }

    void OnTriggerStay(Collider objectName)
    {
        Debug.Log("Colliding with " + objectName.gameObject.name);
        gameObject.GetComponent<Renderer>().material.color = collisiionColor.color;
        if (objectName.CompareTag("Pistol"))
        {
            objectName.GetComponent<GunShoot>().canShoot = false;
        }
    }

    void OnTriggerExit(Collider objectName)
    {
        Debug.Log("Exited collision with " + objectName.gameObject.name);
        gameObject.GetComponent<Renderer>().material.color = ogColor;
        if (objectName.CompareTag("Pistol"))
        {
            objectName.GetComponent<GunShoot>().canShoot = true;
        }
    }
}
