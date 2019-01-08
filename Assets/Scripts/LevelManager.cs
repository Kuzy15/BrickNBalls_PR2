using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public BallSpawn ballSpawner;
    public BallStacker ballStacker;
    public DeathZone deathZone;
    public GameField gameField; // BoardManager
    
    // public AimController _aimController;

    public Ball ballPrefab;

    public GameObject resizeManager;

    public Text scoreText;

    //Button fos the Game Scene
    public Button pause;
    public Button home;
    public Button restart;
    public Button play;
    public Button homeEnd;
    public Button restartEnd;
    public Button nextEnd;
    public Button homeLose;
    public Button restartLose;

    public GameObject warnings;


    private uint _nballs; //Number of balls in game
    private bool _spawn; //If you can spawn balls or not
    private int _points; //Points you get
    private int _sameRoundPoints; //Points multiplier used when you destory several bricks in a row
    private bool _paused; //If game is paused or not
    private bool _endRound; //If you end this round
    private bool _firstBall; //If is the first ball that collides with deathZone 

    private GameObject[] _pausedObjects; //To all balls in game

    public static LevelManager levelManagerInstance;

   //Init all variables, gameObjects, buttons and text
    void Awake()
    {
        //Calculate the numbre of balls for each level
        string name = GameManager.gameManagerInstace.GetMapLevel().name;
        string aux = "";
        for (int i = 7; i < name.Length; i++)
        {
            aux += name[i];
        }
        int level;
        Int32.TryParse(aux, out level);
        _nballs = 10 + 10 * (uint)(level - 1);
        _spawn = true;
        _points = 0;
        _sameRoundPoints = 0;
        _firstBall = true;

        deathZone.Init(this);
        ballSpawner.Init(ballPrefab, _nballs);
        ballStacker.Init();
        levelManagerInstance = this;

        resizeManager.GetComponent<ResizeManager>().Resize();

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

    //Set a number of balls
    public void SetNBalls(uint n)
    {
        _nballs = n;
    }

    //Get the number of game balls
    public uint GetNBalls()
    {
        return _nballs;
    }

    //Add one ball
    public void AddBall()
    {
        _nballs++;
    }

    //Set if you can spawn balls
    public void SetSpawn(bool s)
    {
        _spawn = s;
    }

    //Get if you can spawn balls
    public bool GetSpawn()
    {
        return _spawn;
    }

    //Add points in function of the bricks destroyed in the same round
    public void AddPoints()
    {
        _points += (10 * _sameRoundPoints);
        scoreText.GetComponent<Text>().text = "Points: " + _points.ToString();
    }

    //Add one to the multiplier of the bricks destroyed in the same round
    public void AddSameRoundPoints()
    {
        _sameRoundPoints++;
    }

    //Reset bricks´ multiplier
    public void ResetSameRoundPoints()
    {
        _sameRoundPoints = 0;
    }

    //Get if game is paused
    public bool GetPaused()
    {
        return _paused;
    }

    /****************/
    // Buttons Method
    /****************/
    //Pause mehtod
    //Get all balls of the sccene, set its vel to 0, and show pused buttons, you can´t spawn
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

    //Continue method
    //If you are paused, set all spawned balls a vel and hide all paused buttons, you can spawn
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

    //Paused Restart method
    //Restart the game you are playing
    public void OnClickRestartMenu()
    {
        SceneManager.LoadScene(1);
    }

    //Paused Home method
    //Load the select level scene
    public void OnClickHomeMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Win Next Level method
    //Load the next map and give it to the gameManager
    //Load the same secene but with dif map
    public void OnClickNextEndeMenu()
    {
        string name = GameManager.gameManagerInstace.GetMapLevel().name;
        string aux = "";
        for(int i = 7; i < name.Length; i++)
        {
            aux += name[i]; 
        }
        int level;
        Int32.TryParse(aux, out level);
        TextAsset nextLevel = Resources.Load("Maps/mapdata" + (level + 1).ToString()) as TextAsset;
        GameManager.gameManagerInstace.SetMapLevel(nextLevel);
        SceneManager.LoadScene(1);
    }

    //Win Restart method
    //Restart the game you are playing
    public void OnClickRestartEndMenu()
    {
        SceneManager.LoadScene(1);
    }

    //Win Home method
    //Load the select level scene
    public void OnClickHomeEndMenu()
    {
        SceneManager.LoadScene(0);
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
            ballStacker.SetPos(posBallStacker);         
            ballStacker.Show(true); // Make visible the ball stacker
            ProcessPlay(ball);
            Destroy(ball.gameObject);
        }
        else
        {
            ball.GoTo(ballStacker.transform.position);
            ProcessPlay(ball); // HACER: SI SE PONE COMO PARAMETRO (CALLBACK) DEL METODO GOTO VA MAL¿?
        }
    }

    //Call the method MoveBlocks of the gameField //HACERLO DIRECTAMENTE MEJOR NO?¿
    public void MoveBlocks()
    {
        gameField.MoveBlocks();
    }

    //Active the warnings if is necessary
    public void ActiveWarnings()
    {
        warnings.SetActive(gameField.ActiveWarnings());
    }

    //If you destroy all blocks, you can´t spawn, you end this round(map), unlocknext map
    //Set a number of star in function of points and show stars in select level scene
    //Show end buttons and save the game
    public void EndButtonsActive()
    {
        if (gameField.WinGame())
        {
            _spawn = false;
            _endRound = true;
            string name = GameManager.gameManagerInstace.GetMapLevel().name;
            string aux = "";
            for (int i = 7; i < name.Length; i++)
            {
                aux += name[i];
            }
            int level;
            Int32.TryParse(aux, out level);
            if (level < 9) //Only have 10 levels
            {
                GameManager.gameManagerInstace.GetLevels()[level]._lock = false;
            }
            if (_points > gameField.GetTotalBlocks() * 30 / 4)
            {
                GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[0] = true;
            }
            if (_points > gameField.GetTotalBlocks() * 30 / 2)
            {
                GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[1] = true;
            }
            if (_points > gameField.GetTotalBlocks() * 30)
            {
                GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[2] = true;
            }
            GameManager.Save();
            homeEnd.gameObject.SetActive(true);
            restartEnd.gameObject.SetActive(true);
            if (level < 9)
            {
                nextEnd.gameObject.SetActive(true);
            }
        }
    }

    //If you lose this round(map), you can´t spawn and show lose buttons
    public void LoseButtonsActive()
    {
        if (gameField.EndGame())
        {
            _spawn = false;
            homeLose.gameObject.SetActive(true);
            restartLose.gameObject.SetActive(true);
        }
    }

    // Check the state of the game after making a play
    private void ProcessPlay(Ball ball)
    {
        ballStacker.AddBall(); // Add one ball to the balls counter
        if (ballStacker.GetBallStacked() == _nballs) // All balls has been stacked
        {
            ballSpawner.MoveTo(ballStacker.transform.position); // Spawn position is the ball stacker last position
            ballSpawner.Show(true); 
            ballSpawner.SetNBalls(_nballs); // Update the number of balls, it could have changed

            ResetSameRoundPoints();

            ballStacker.ResetNumBalls(); //Restart number of stacked balls    
            
            _firstBall = true;

            if (!_endRound)
            {
              SetSpawn(true);
            }


            MoveBlocks(); //Move gameField blocks
            ActiveWarnings(); //Active warnings if is necessary
            EndButtonsActive(); //Active Win/End buttons if is necessary
            LoseButtonsActive(); //Active Lose buttons if is necessary
        }

    }
}
