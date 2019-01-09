using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHorizontalBrick : Bricks {

    private void Start() //Set dif vars., in this case if can be destory at next round
    {
        _nextRoundDestroy = false;
    }

    //If it collides with the deathZone destoy it
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
}
