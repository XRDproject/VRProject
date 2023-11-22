using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenanaer : MonoBehaviour
{
    [SerializeField] GameObject winText;
    [SerializeField] GameObject LoseText;
    [SerializeField] GameObject EndScreen;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject[] MenuButtons;

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
        MenuPanel.GetComponent<Image>().color = new Color(255,0,0,100);
        foreach (var button in MenuButtons)
        {
            var tmpButton = button.GetComponent<Button>();
            var colors = tmpButton.colors;
            colors.normalColor = Color.red;
            tmpButton.colors = colors;
        }

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
