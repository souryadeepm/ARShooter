using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject enemyBase;

    private GameObject homeBase;
    public GameObject EnemyBase { get => EnemyBase; set { enemyBase = value;/*StartImageScanning();*/ } }

    public GameObject HomeBase { get => homeBase; set { homeBase = value; /*StartGame();*/ } }
    [Header("Main Game references")]
    [SerializeField] private GameObject mainMenuCanvas;

    [Header("Plane Scanning references")]

    [SerializeField] private GameObject planeScanningCanvas;
    [SerializeField] private GameObject imageScanningCanvas;
    [SerializeField] private GameObject enemyBasePrefab;
    [SerializeField] private GameObject homeBasePrefab;
    [SerializeField] private GameObject mainGameCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    

     

    StateMachine stateMachine;
    private void Awake()
    {
        if (instance != null & instance != this)
            Destroy(gameObject);
        else
            instance = this;    

    }

    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new MainMenuState(mainMenuCanvas));
        stateMachine.ExecuteStateUpdate();
    }

    public void StartPlaneScanning()
    {
        stateMachine.ChangeState(new PlaneScanningState(planeScanningCanvas, enemyBasePrefab,homeBasePrefab));
        stateMachine.ExecuteStateUpdate();
    }

    public void StartImageScanning()
    {
        stateMachine.ChangeState(new ImageScanningState(imageScanningCanvas));
        stateMachine.ExecuteStateUpdate();
    }

    private void StartMaingame()
    {
        stateMachine.ChangeState(new MainGameState(mainGameCanvas, enemyBase));
        stateMachine.ExecuteStateUpdate();
    }

    public void StartGameOver()
    {
        stateMachine.ChangeState(new GameOverState(gameOverCanvas));
        stateMachine.ExecuteStateUpdate();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        StartMaingame();
    }
}
 