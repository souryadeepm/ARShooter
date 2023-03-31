using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : IState
{

    private GameObject hudCanvas;

    private FPSLogic fpsLogic;
    private PlayerShoot playerShoot;

    private TMPro.TMP_Text warningText;
    private WaveManager waveMgr;

    public MainGameState(GameObject hudCanvas,GameObject enemyBase)
    {
        this.hudCanvas = hudCanvas;
        waveMgr = enemyBase.GetComponent<WaveManager>();
    }
   public void EnterState()
    {
        Debug.Log("Entering Main Menu State");
        fpsLogic = GameObject.FindObjectOfType<FPSLogic>();
        playerShoot = GameObject.FindObjectOfType<PlayerShoot>();

        warningText = hudCanvas.GetComponentInChildren<TMPro.TMP_Text>();

        waveMgr.OnWaveChanged += UpdateWarning;
    }

    private void UpdateWarning(EnemyWave wave)
    {
        warningText.text  = wave.waveName;
    }

    public void ExecuteState()
    {
        Debug.Log("Executing Main Menu State");
        fpsLogic.enabled = true;
        playerShoot.enabled = true;
        hudCanvas.SetActive(true);

        GameManager.instance.EnemyBase.GetComponent<WaveManager>().SpawnWave(0);
    }

    public void ExitState()
    {
        Debug.Log("Exiting Main Menu State");
        fpsLogic.enabled = false;
        playerShoot.enabled = false;
        hudCanvas.SetActive(false);
    }
}
