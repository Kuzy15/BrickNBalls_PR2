using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private Vector2 posClicked;
    private Ray ray;
    
    void FixedUpdate () {

        if (LevelManager.levelManagerInstance.GetSpawn()) {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.y < 380 && Input.mousePosition.y > 60)
                {
                    LevelManager.levelManagerInstance.SetSpawn(false);
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    posClicked = ray.origin;

                    LevelManager.levelManagerInstance._ballStacker.Show(false);

                    LevelManager.levelManagerInstance._ballSpawner.SetPosDest(posClicked);
                    LevelManager.levelManagerInstance._ballSpawner.Show(true);
                    LevelManager.levelManagerInstance._ballSpawner.SpawnBalls(); // Ball spawner can launch balls

                }
            }
        }
	}
}
