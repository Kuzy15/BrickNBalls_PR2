using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHorizontalBrick : Bricks {

    private bool _touch = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _touch = true;
        Vector3 newPos = new Vector3(0, gameObject.transform.position.y, -1);
        gameObject.transform.GetChild(0).gameObject.transform.position = newPos;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void DestroyBrick()
    {
        if(_touch)
            Destroy(gameObject);
    }
}
