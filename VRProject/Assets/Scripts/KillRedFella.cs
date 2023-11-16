using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillRedFella : MonoBehaviour
{
    bool fellaDied = false;
    public void Die(){
        fellaDied = true;
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
