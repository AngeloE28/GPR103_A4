using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarTimer : MonoBehaviour
{
    public GameManager myGameManager; //A reference to the GameManager in the scene.

    public Image barTimer;  //Reference ot barTimer

    // Start is called before the first frame update
    void Start()
    {
        barTimer = GetComponent<Image>();   // Gets the image
    }

    // Update is called once per frame
    void Update()
    {
        // Image fill amount will reduce with each second
        if (myGameManager.gameTimeRemaining > 0)
        {
            barTimer.fillAmount = myGameManager.gameTimeRemaining / myGameManager.totalGameTime;
        }
    }
}
