﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimController : MonoBehaviour {


    private Vector2 posClicked;
    private Ray ray;
    private LevelManager _levelManager;

    //Init variables
    public void Init(LevelManager lm)
    {
        _levelManager = lm;
    }

    //See if you click/touch and if the position is inside the game field, spawn balls to that position
    void FixedUpdate () {
        if (_levelManager.GetSpawn())
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.y < 405 && Input.mousePosition.y > 60)
                {
                    _levelManager.SetSpawn(false);
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    posClicked = ray.origin;

                    _levelManager.ballStacker.Show(false);

                    _levelManager.ballSpawner.SetPosDest(posClicked);
                    _levelManager.ballSpawner.Show(true);
                    _levelManager.ballSpawner.SpawnBalls(); // Ball spawner can launch balls

                }
            }
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).position.y > 250 && Input.GetTouch(0).position.y < 1700)
                {
                    _levelManager.SetSpawn(false);
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    posClicked = ray.origin;

                    _levelManager.ballStacker.Show(false);

                    _levelManager.ballSpawner.SetPosDest(posClicked);
                    _levelManager.ballSpawner.Show(true);
                    _levelManager.ballSpawner.SpawnBalls(); // Ball spawner can launch balls

                }
            }
#endif
        }
    }
}
