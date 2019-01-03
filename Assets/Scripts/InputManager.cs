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

                    LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().SetPosDest(posClicked);
                    LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().SpawnBalls(); // Ball spawner can launch balls
                    LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().GetComponentInChildren<MeshRenderer>().enabled = true;

                }
            }
        }
	}
}
