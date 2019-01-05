using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBallBrick : Bricks {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DeathZone>())
        {
            Destroy(gameObject);
        }
        else
        {
            LevelManager.levelManagerInstance.AddBall();
            LevelManager.levelManagerInstance.stacker.GetComponent<BallStacker>().AddBallStacked();
            Destroy(gameObject);
        }
    }
}
