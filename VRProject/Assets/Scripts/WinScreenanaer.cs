using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenanaer : MonoBehaviour
{
    [SerializeField] GameObject winText;
    [SerializeField] GameObject LoseText;
    [SerializeField] GameObject EndScreen;
    public KillRedFella fellaDied;
    public Holster playerDead;
    void YouWon(){
        EndScreen.SetActive(true);
        winText.SetActive(true);
    }
    void YouLose(){
        EndScreen.SetActive(true);
        LoseText.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fellaDied){
            YouWon();
        }else if(playerDead){
            YouLose();
        }
    }
}
