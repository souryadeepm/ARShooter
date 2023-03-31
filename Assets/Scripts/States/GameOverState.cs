using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : IState
{
    private GameObject gameOverCanvas;
    private GameObject homeBase;
    private Health homeHealth;

    private WaveManager waveMgr;
    public GameOverState(GameObject gameOvercanvas)
    {
        this.gameOverCanvas = gameOvercanvas;
    }
    public void EnterState()
    {
        gameOverCanvas.SetActive(true);
        homeBase = GameManager.instance.HomeBase;
        homeHealth = homeBase.GetComponent<Health>();
        waveMgr = GameObject.FindObjectOfType<WaveManager>();
    }

    public void ExecuteState()
    {
        homeHealth.ChangeHealth(homeHealth.MaxHealth);
        waveMgr.CleanUpEnemies();
    }

    public void ExitState()
    {
        gameOverCanvas.SetActive(false);
    }
}
