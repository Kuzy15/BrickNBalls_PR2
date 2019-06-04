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
    public UIManager uiManager;
    public AimController aimController;
    public AdsManagerGame adsManagerGame;
    public ResizeManager resizeManager;
    
    public Ball ballPrefab;

    public Text scoreText;
    public Text endScoreText;

    public GameObject warnings;

    public Button rayPowerUpButton;
    public Button fallBallsButton;

    public Button adsButton;

    private uint _nballs; //Number of balls in game
    private bool _spawn; //If you can spawn balls or not
    private int _points; //Points you get
    private int _sameRoundPoints; //Points multiplier used when you destory several bricks in a row
    private bool _paused; //If game is paused or not
    private bool _endRound; //If you end this round
    private bool _firstBall; //If is the first ball that collides with deathZone

    private GameObject[] _pausedObjects; //To all balls in game

   //Init all variables, gameObjects, buttons and text
    void Awake()
    {
        //Calculate the numbre of balls for each level
        _nballs = 10 + 10 * (uint)(GameManager.gameManagerInstace.GetCurrentLevel() - 1);

        _spawn = true;
        _points = 0;
        _sameRoundPoints = 0;
        _firstBall = true;

        gameField.Init(this);
        deathZone.Init(this);
        ballSpawner.Init(ballPrefab, _nballs, this);
        ballStacker.Init();
        uiManager.Init();
        adsManagerGame.Init(this);
        resizeManager.Init(this);
        resizeManager.Resize();

        aimController.Init(this, resizeManager.GetTopStop(), resizeManager.GetBotStop());



        scoreText.GetComponent<Text>().text = "Points: " + _points.ToString();

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

    //Get the points
    public int GetPoints()
    {
        return _points;
    }

    //Set the points
    public void SetPoints(int  p)
    {
        _points = p; ;
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
        _pausedObjects = GameObject.FindGameObjectsWithTag("Ball");
        for(int i =0; i < _pausedObjects.Length; i++)
        {
            if(_pausedObjects[i] != null) // If doesn't get destroyed in the moment the game is paused
                 _pausedObjects[i].GetComponent<Ball>().Pause();
        }
       
        _spawn = false;
        _paused = true;

        uiManager.Pause();
        adsManagerGame.ShowBanner();
    }

    //Continue method
    //If you are paused, set all spawned balls a vel and hide all paused buttons, you can spawn
    public void OnClickContinueMenu()
    {
        for (int i = 0; i < _pausedObjects.Length; i++)
        {
            if (_pausedObjects[i] != null) // If doesn't get destroyed in the moment the game is paused
                _pausedObjects[i].GetComponent<Ball>().Continue();
        }
       
        _spawn = true;
        _paused = false;

        uiManager.Play();
        adsManagerGame.HideBanner();
    }

    //Paused Restart method
    //Restart the game you are playing
    public void OnClickRestartMenu()
    {
        SceneManager.LoadScene(1);
        adsManagerGame.HideBanner();
    }

    //Paused Home method
    //Load the select level scene
    public void OnClickHomeMenu()
    {
        SceneManager.LoadScene(0);
        adsManagerGame.HideBanner();
    }

    //Win Next Level method
    //Load the next map and give it to the gameManager
    //Load the same secene but with dif map
    public void OnClickNextEndeMenu()
    {
        int level = GameManager.gameManagerInstace.GetCurrentLevel();

        if (level < 9)
        {
            TextAsset nextLevel = Resources.Load("Maps/mapdata" + (level + 1).ToString()) as TextAsset;
            GameManager.gameManagerInstace.SetMapLevel(nextLevel);
            SceneManager.LoadScene(1);
        }
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

    //Show a skipeable ad and deactivate bonus button
    public void OnClickAdsPoints()
    {
        adsButton.gameObject.SetActive(false);
        adsManagerGame.ShowAd();
    }

    //Create ray bricks
    public void OnClickRayPowerUp()
    {
        gameField.CreateRayPowerUp();
    }

    //Accumulate all balls at spawn/stacker position
    //field move 1 position and check if you lose or not
    public void OnClickFallBalls()
    {
        ballSpawner.StopSpawnBalls();
        GameObject[] _inGameBalls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < _inGameBalls.Length; i++)
        {
            Destroy(_inGameBalls[i]);
        }
        if (ballSpawner.transform.position != ballStacker.transform.position)
        {
            ballSpawner.MoveTo(ballStacker.transform.position); // Spawn position is the ball stacker last position
        }
        SetSpawn(true);
        rayPowerUpButton.gameObject.SetActive(true);
        fallBallsButton.gameObject.SetActive(false);
        ballSpawner.Show(true);
        ballSpawner.SetNBalls(_nballs); // Update the number of balls, it could have changed

        ResetSameRoundPoints();

        ballStacker.ResetNumBalls(); //Restart number of stacked balls    
        ballStacker.Show(false);

        _firstBall = true;

        MoveBlocks(); //Move gameField blocks
        ActiveWarnings(); //Active warnings if is necessary

        if (gameField.WinGame())
        {
            LevelCompleted();
        }
        else if (gameField.EndGame())
        {
            LevelLost();
        }
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
            ball.GoTo(ballStacker.transform.position, ProcessPlay);
            //ProcessPlay(ball);
        }
    }

    //Call the method MoveBlocks of the gameField
    public void MoveBlocks()
    {
        gameField.MoveBlocks();
    }

    //Active the warnings if is necessary
    public void ActiveWarnings()
    {
        warnings.SetActive(gameField.ActiveWarnings());
    }

    public void DeactivateWarnings()
    {
        warnings.SetActive(false);
    }

    //If you destroy all blocks, you can´t spawn, you end this round(map), unlock next map
    //Set a number of star in function of points and show stars in select level scene
    //Show end buttons and save the game
    public void LevelCompleted()
    {
        _spawn = false;
        adsManagerGame.ShowNoSkipAd();

        int level = GameManager.gameManagerInstace.GetCurrentLevel();

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
        endScoreText.text = "Points " + _points.ToString();

        SaveAndLoad.Save();
        uiManager.WinLevel();

    }

    //If you lose this round(map), you can´t spawn and show lose buttons
    public void LevelLost()
    {     
            _spawn = false;
            uiManager.LoseLevel();      
    }

    // Check the state of the game after making a play
    private void ProcessPlay(Ball ball)
    {
        ballStacker.AddBall(); // Add one ball to the balls counter
        if (ballStacker.GetBallStacked() == _nballs) // All balls has been stacked
        {
            rayPowerUpButton.gameObject.SetActive(true);
            fallBallsButton.gameObject.SetActive(false);
            ballSpawner.MoveTo(ballStacker.transform.position); // Spawn position is the ball stacker last position
            ballSpawner.Show(true); 
            ballSpawner.SetNBalls(_nballs); // Update the number of balls, it could have changed

            ResetSameRoundPoints();

            ballStacker.ResetNumBalls(); //Restart number of stacked balls    
            ballStacker.Show(false);
            
            _firstBall = true;

            if (!_endRound)
            {
              SetSpawn(true);
            }

            MoveBlocks(); //Move gameField blocks
            ActiveWarnings(); //Active warnings if is necessary

            if (gameField.WinGame())
            {
                LevelCompleted(); 
            }
            else if (gameField.EndGame())
            {
                LevelLost();
            }      
        }
    }
}
