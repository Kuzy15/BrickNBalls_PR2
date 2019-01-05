using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidBrick : Bricks {


    void Start()
    {
        GetComponentInChildren<TextMesh>().text = _nHits.ToString();
    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            _nHits--;
        }
        GetComponentInChildren<TextMesh>().text = _nHits.ToString();
        if (_nHits <= 0)
        {
            LevelManager.levelManagerInstance.AddSameRoundPoints();
            LevelManager.levelManagerInstance.AddPoints();
            LevelManager.levelManagerInstance._gameField.RemoveBlock();
            Destroy(gameObject);
        }
    }
}

