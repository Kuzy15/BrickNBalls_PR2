using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


    public GameObject spawner;
    public GameObject stacker;
    public GameObject deathZone;
    public GameObject gameField;

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


    private int _nballs;
    private bool _spawn;
    private int _points;
    private int _sameRoundPoints;
    private bool _paused;
    private bool _endRound;
    private GameObject[] _pausedObjects;

    public static LevelManager levelManagerInstance;

    // Use this for initialization
    void Start()
    {
        resizeManager.GetComponent<ResizeManager>().Resize();
        levelManagerInstance = this;

        _nballs = 10;
        _spawn = true;
        _points = 0;
        _sameRoundPoints = 0;

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

    public void SetNBalls(int n)
    {
        _nballs = n;
    }

    public int GetNBalls()
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

    public void SetPosStacker(Vector3 pos)
    {
        stacker.transform.position = pos;
    }

    public void MoveBlocks()
    {
        gameField.GetComponent<GameField>().MoveBlocks();
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
        _pausedObjects = GameObject.FindGameObjectsWithTag("Ball");
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
}
