using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBallBrick : Bricks {

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
            _levelManager.AddBall();
            _levelManager.ballStacker.AddBall();
            Destroy(gameObject);
        }
    }
}
