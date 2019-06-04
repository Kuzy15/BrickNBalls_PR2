using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHorizontalBrick : Bricks {

   
    //If it collides with the deathZone destroy it
    //Else if a ball collides with this brick spawn a ray that hits
    //a row and destroy itself at next round
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DeathZone>())
        {
            Destroy(gameObject);
        }
        else
        {
            _nextRoundDestroy = true;
            Vector3 newPos = new Vector3(0, gameObject.transform.position.y, -1);
            gameObject.transform.GetChild(0).gameObject.transform.position = newPos;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
       }
    }

    //Deactivate the ray
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // From base class Bricks, fall one position and check if it has been touched and get destroyed if so.
    public override void Fall()
    {    
        base.Fall();

        if (_nextRoundDestroy)
            Destroy(gameObject);     
    }
}
