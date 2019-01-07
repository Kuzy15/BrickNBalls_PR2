using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBallBrick : Bricks {

    private void Start() //Set if you can destroy
    {
        _canDestroy = false;
    }

    //If it collides with the deathZone destoy it
    //Else if a ball collides with this brick add one ball
    //and destroy itself
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DeathZone>())
        {
            Destroy(gameObject);
        }
        else
        {
            LevelManager.levelManagerInstance.AddBall();
            LevelManager.levelManagerInstance.ballStacker.AddBall();
            Destroy(gameObject);
        }
    }
}
