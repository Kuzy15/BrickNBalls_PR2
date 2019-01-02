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
    

    private int _nballs;
    private bool _spawn;
    private int _points;
    private int _sameRoundPoints;
    private bool _paused;
    private GameObject[] _pausedObjects;

    public static LevelManager levelManagerInstance;

    // Use this for initialization
    void Start()
    {
        resizeManager.GetComponent<ResizeManager>().Resize();
        levelManagerInstance = this;

        _nballs = 25;
        _spawn = true;
        _points = 0;
        _sameRoundPoints = 0;

        scoreText.GetComponent<Text>().text = "Points: " + _points.ToString();

        _paused = false;

    }

private void Update()
    {
    }

    public void SetNBalls(int n)
    {
        _nballs = n;
    }

    public int GetNBalls()
    {
        return _nballs;
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

    public bool GetPaused()
    {
        return _paused;
    }
}
