using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


   /* public GameObject spawner;
    public GameObject stacker;
    public GameObject deathZone;
    public GameObject gameField;*/


    public BallSpawn _ballSpawner;
    public BallStacker _ballStacker;
    public DeathZone _deathZone;
    public GameField _gameField; // BoardManager
    // public AimController _aimController;

    public Ball _ballPrefab;

    public GameObject resizeManager;

    public Text scoreText;

    public Button pause;
    public Button home;
    public Button restart;
    public Button play;
    public Button homeEnd;
    public Button restartEnd;
    public Button nextEnd;
    public Button homeLose;
    public Button restartLose;


    private uint _nballs;
    private bool _spawn;
    private int _points;
    private int _sameRoundPoints;
    private bool _paused;
    private bool _endRound;
    private bool _firstBall;

    private GameObject[] _pausedObjects;

    public static LevelManager levelManagerInstance;

   
    void Awake()
    {
        _nballs = 12;
        _spawn = true;
        _points = 0;
        _sameRoundPoints = 0;
        _firstBall = true;

        _deathZone.Init(this);
        _ballSpawner.Init(_ballPrefab, _nballs);
        _ballStacker.Init();
        resizeManager.GetComponent<ResizeManager>().Resize();
        levelManagerInstance = this;


        scoreText.GetComponent<Text>().text = "Points: " + _points.ToString();

        pause.gameObject.SetActive(true);
        home.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        play.gameObject.SetActive(false);
        homeEnd.gameObject.SetActive(false);
        restartEnd.gameObject.SetActive(false);
        nextEnd.gameObject.SetActive(false);
        restartLose.gameObject.SetActive(false);
        homeLose.gameObject.SetActive(false);

        _paused = false;
        _endRound = false;


    }

    private void Update()
    {
        Debug.Log(_nballs);
    }

    public void SetNBalls(uint n)
    {
        _nballs = n;
    }

    public uint GetNBalls()
    {
        return _nballs;
    }

    public void AddBall()
    {
        _nballs++;
    }

    public void SetSpawn(bool s)
    {
        _spawn = s;
    }

    public bool GetSpawn()
    {
        return _spawn;
    }

    public void MoveBlocks()
    {
        _gameField.MoveBlocks();
    }

    public void AddPoints()
    {
        _points += (10 * _sameRoundPoints);
        scoreText.GetComponent<Text>().text = "Points: " + _points.ToString();

    }

    public void AddSameRoundPoints()
    {
        _sameRoundPoints++;
    }

    public void ResetSameRoundPoints()
    {
        _sameRoundPoints = 0;
    }

    public void OnClickPauseMenu()
    {
        _pausedObjects = GameObject.FindGameObjectsWithTag("Ball"); // HACER: CAMBIAR ESTO, NO ES BUENO BUSCAR CON TAG
        for(int i =0; i < _pausedObjects.Length; i++)
        {
            _pausedObjects[i].GetComponent<Ball>().Pause();
        }
        pause.gameObject.SetActive(false);
        home.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        play.gameObject.SetActive(true);
        _spawn = false;
        _paused = true;
    }

    public void OnClickContinueMenu()
    {
        for (int i = 0; i < _pausedObjects.Length; i++)
        {
            _pausedObjects[i].GetComponent<Ball>().Continue();
        }
        pause.gameObject.SetActive(true);
        home.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        play.gameObject.SetActive(false);
        _spawn = true;
        _paused = false;

    }

    public void OnClickRestartMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickHomeMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickNextEndeMenu()
    {
        string name = GameManager.gameManagerInstace.GetMapLevel().name;
        int level = name[name.Length - 1] - 48;
        GameManager.gameManagerInstace.GetLevels()[level]._lock = false;
        ///En funcion de los ptnos conseguidos
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[0] = true;
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[1] = true;
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[2] = true;
        ///

        GameManager.Save();
        TextAsset nextLevel = Resources.Load("Maps/mapdata" + (level + 1).ToString()) as TextAsset;
        GameManager.gameManagerInstace.SetMapLevel(nextLevel);
        SceneManager.LoadScene(1);
    }

    public void OnClickRestartEndMenu()
    {
        string name = GameManager.gameManagerInstace.GetMapLevel().name;
        int level = name[name.Length - 1] - 48;
        GameManager.gameManagerInstace.GetLevels()[level]._lock = false;
        ///En funcion de los ptnos conseguidos
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[0] = true;
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[1] = true;
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[2] = true;
        ///
        SceneManager.LoadScene(1);
    }

    public void OnClickHomeEndMenu()
    {
        string name = GameManager.gameManagerInstace.GetMapLevel().name;
        int level = name[name.Length - 1] - 48;
        GameManager.gameManagerInstace.GetLevels()[level]._lock = false;
        ///En funcion de los ptnos conseguidos
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[0] = true;
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[1] = true;
        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[2] = true;
        ///
        SceneManager.LoadScene(0);
    }

    public bool GetPaused()
    {
        return _paused;
    }

    public bool GetEndRound()
    {
        return _endRound;
    }

    public void EndButtonsActive()
    {
        _spawn = false;
        _endRound = true;
        homeEnd.gameObject.SetActive(true);
        restartEnd.gameObject.SetActive(true);
        nextEnd.gameObject.SetActive(true);
    }

    public void LoseButtonsActive()
    {
        _spawn = false;
        homeLose.gameObject.SetActive(true);
        restartLose.gameObject.SetActive(true);
    }

    /*
     * A ball has came into the death zone, check if it's the first and 
     * change the ball stacker position to this first ball position.
     * If it's not the first ball move the ball to the ball stacker and destroy the ball.
     */
    public void BallIntoDeathZone(Ball ball)
    {

        
        if (_firstBall) // The ball arrived is the first one
        {
            _firstBall = false;
            Vector2 posBallStacker = new Vector2(ball.transform.position.x, ball.transform.position.y + 0.3f);
            _ballStacker.SetPos(posBallStacker);         
            _ballStacker.Show(true); // Make visible the ball stacker
            ProcessPlay(ball);
            Destroy(ball.gameObject);

        }
        else
        {
            ball.GoTo(_ballStacker.transform.position);
            ProcessPlay(ball); // HACER: SI SE PONE COMO PARAMETRO (CALLBACK) DEL METODO GOTO VA MAL¿?
        }

    }

    // Check the state of the game after making a play
    private void ProcessPlay(Ball ball)
    {

        _ballStacker.AddBall(); // Add one ball to the balls counter
      

        if (_ballStacker.GetBallStacked() == _nballs) // All balls has been stacked
        {
            // HACER: COMPROBAR SI EL NIVEL SE HA COMPLETADO, SI SE HA PERDIDO Y SI HAY QUE ACTIVAR LOS AVISOS. 
            // HACERLO SEGUN LOS METODOS QUE TENEMOS EN EL SCRIPT DE GAMEFIELD, PARA LLAMARLOS CON _gameField.CheckEndLevel() POR EJEMPLO



            _ballSpawner.MoveTo(_ballStacker.transform.position); // Spawn position is the ball stacker last position
            _ballSpawner.Show(true); 
            _ballSpawner.SetNBalls(_nballs); // Update the number of balls, it could have changed

            ResetSameRoundPoints();

            _ballStacker.ResetNumBalls(); //Reinicias el numero de bolas stackeadas     
            
            _firstBall = true;

            if (!GetEndRound())
            {
              SetSpawn(true);
            }


            MoveBlocks();
        }

    }


}
