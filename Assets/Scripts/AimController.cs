using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AimController : MonoBehaviour {

    private Vector2 posClicked;
    private Ray ray;
    
    void FixedUpdate () {

        if (LevelManager.levelManagerInstance.GetSpawn())
        {
#if UNITY_STANDALONE_WIN
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.y < 405 && Input.mousePosition.y > 60)
                {
                    LevelManager.levelManagerInstance.SetSpawn(false);
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    posClicked = ray.origin;

                    LevelManager.levelManagerInstance.ballStacker.Show(false);

                    LevelManager.levelManagerInstance.ballSpawner.SetPosDest(posClicked);
                    LevelManager.levelManagerInstance.ballSpawner.Show(true);
                    LevelManager.levelManagerInstance.ballSpawner.SpawnBalls(); // Ball spawner can launch balls

                }
            }
#endif
#if UNITY_ANDROID
            if (Input.GetTouch(0).position.y > 250 && Input.GetTouch(0).position.y < 1700)
            {
                LevelManager.levelManagerInstance.SetSpawn(false);
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                posClicked = ray.origin;

                LevelManager.levelManagerInstance.ballStacker.Show(false);

                LevelManager.levelManagerInstance.ballSpawner.SetPosDest(posClicked);
                LevelManager.levelManagerInstance.ballSpawner.Show(true);
                LevelManager.levelManagerInstance.ballSpawner.SpawnBalls(); // Ball spawner can launch balls

            }
#endif
        }
	}
}
