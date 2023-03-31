using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : IState
{
    private GameObject mainMenuCanvas;

    public MainMenuState(GameObject mainMenuCanvas)
    {
        this.mainMenuCanvas = mainMenuCanvas;
    }

    public GameObject MainMenuCanvas { get => mainMenuCanvas; set => mainMenuCanvas = value; }
    void IState.EnterState()
    {
        Debug.Log("--Entered Main Menu State--");
        mainMenuCanvas.SetActive(true);
    }

    void IState.ExecuteState()
    {
        Debug.Log("--Executing Main Menu State--");
    }

    void IState.ExitState()
    {
        Debug.Log("--Exiting Main Menu State--");
        mainMenuCanvas.SetActive(false); 
    }

    
}
