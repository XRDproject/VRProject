using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenanaer : MonoBehaviour
{
    [SerializeField] GameObject winText;
    [SerializeField] GameObject LoseText;
    [SerializeField] GameObject EndScreen;
    public HitBox fellaDied;
    public Holster playerDead;
    private BoxCollider fellaBoxCollider;
    private Animator fellaAnimator;
     private static readonly int InitialState = Animator.StringToHash("onInitialState");
    void YouWon(){
        EndScreen.SetActive(true);
        winText.SetActive(true);
    }
    void YouLose(){
        fellaAnimator.SetTrigger(InitialState);
        fellaBoxCollider.enabled = false;
        EndScreen.SetActive(true);
        LoseText.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject holster = GameObject.FindGameObjectWithTag("Holster");
        playerDead =  holster.GetComponent<Holster>();
        fellaBoxCollider =  fellaDied.GetComponent<BoxCollider>();
        fellaAnimator = fellaDied.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fellaDied.fellaDied){
            YouWon();
        }else if(playerDead.playerDead){
            YouLose();
        }
    }
}
