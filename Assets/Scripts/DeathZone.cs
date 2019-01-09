using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathZone : MonoBehaviour {


    private LevelManager _levelManager;

    //Initialize LevelManager
    public void Init(LevelManager levelManager) //MEJOR HACERLO DIRECTAMENTE?¿
    {
        _levelManager = levelManager;
    }

    //If ball collides with deathZone stop and calls levelManager
    private void OnTriggerEnter2D(Collider2D col)
    {
        Ball b = col.GetComponent<Ball>();
        if(b != null)
        {
            b.Stop();
            //LevelManager.levelManagerInstance.BallIntoDeathZone(b);
            _levelManager.BallIntoDeathZone(b);
        }
    }
}
