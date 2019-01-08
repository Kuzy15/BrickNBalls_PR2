using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidBrick : Bricks {


    void Start() //Show the block´s hits number and set if you can destroy
    {
        GetComponentInChildren<TextMesh>().text = _nHits.ToString();
        _canDestroy = false;
    }

  //if you colision with the deathZone destory the gameObject, if you colision with
  //other thing, subtract 1 to hits
  //In case of having 0 hits, add points and destroy the brick

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DeathZone>())
        {
            Destroy(gameObject);
        }
        else
        {
            _nHits--;
        }
        GetComponentInChildren<TextMesh>().text = _nHits.ToString();
        if (_nHits <= 0)
        {
            LevelManager.levelManagerInstance.AddSameRoundPoints();
            LevelManager.levelManagerInstance.AddPoints();
            //LevelManager.levelManagerInstance.gameField.RemoveBlock();
            Destroy(gameObject);
        }
    }
}

