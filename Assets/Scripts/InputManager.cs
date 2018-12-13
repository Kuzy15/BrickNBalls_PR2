using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Vector2 posClicked;
    private Ray ray;
    // Use this for initialization
  
    
    void FixedUpdate () {
        if (LevelManager.levelManagerInstance._spawn) {
            if (Input.GetMouseButtonDown(0))
            {
                LevelManager.levelManagerInstance._spawn = false;
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                posClicked = ray.origin;

                LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().SetPosDest(posClicked);
                LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().enabled = true; // Ball spawner can launch balls
                LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().GetComponentInChildren<MeshRenderer>().enabled = true;

            }
        }
	}
}
