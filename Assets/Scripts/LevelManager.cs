using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public GameObject spawner;
    public GameObject stacker;
    public GameObject deathZone;
    public GameObject gameField;

   public GameObject resizeManager;


    private int _nballs;
    private bool _spawn;
    private int _points;
    private int _sameRoundPoints;

    public static LevelManager levelManagerInstance;

    private void Awake()
    {
        resizeManager.GetComponent<ResizeManager>().Resize();
    }

    // Use this for initialization
    void Start () {
        levelManagerInstance = this; 
        _nballs = 12;
        _spawn = true;
        _points = 0;
        _sameRoundPoints = 0;
    }

    private void Update()
    {
        Debug.Log(_points);
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
    }

    public void AddSameRoundPoints()
    {
        _sameRoundPoints++;
    }

    public void ResetSameRoundPoints()
    {
        _sameRoundPoints = 0;
    }
}
