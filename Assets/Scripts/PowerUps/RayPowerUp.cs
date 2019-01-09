using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RayPowerUp : PowerUp {

    private List<Pos> _freePos = new List<Pos>(); //Free positons of the board
    private GameObject[,] _field;
    protected GameObject _block;


   /* private void Start()
    {
        _powerUpText = gameObject.transform.GetChild(1).GetComponent<Text>();
        _powerUpText.text = GameManager.gameManagerInstace.GetComponent<GameManager>().GetNRayPowerUp().ToString();
        _block = _levelManager.gameField.GetBlocks()[6];
        _amount = GameManager.gameManagerInstace.GetNRayPowerUp();
    }
    //If you have got ray powerup, see the free positions of the board and it creates two
    //rayBricks in a random free position
    //Then save the game
    public void PowerUpClick()
    {
        _field = _levelManager.gameField.GetGameField();

        if (_amount > 0)
        {
            _amount--;
            GameManager.gameManagerInstace.RemoveNRayPowerUp(1);
            _powerUpText.text = _amount.ToString();

            Pos freepos;
            for (int i = 0; i < 11; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    if (_field[i, j] == null)
                    {
                        freepos = new Pos(i, j);
                        _freePos.Add(freepos);
                    }
                }
            }
            for (int a = 0; a < 2; a++)
            {
                if (_freePos.Count > 0)
                {
                    int rnd = Random.Range(0, _freePos.Count - 1);

                    Vector3 pos = new Vector3(transform.position.x + _freePos[rnd]._y, transform.position.y - _freePos[rnd]._x - 1.5f, 0);
                    GameObject aux = Instantiate(_block, transform.position, transform.rotation, transform);
                    aux.GetComponent<Bricks>().Init(_levelManager);
                    aux.transform.position = pos;
                    aux.GetComponent<Bricks>().SetTypeBrick(7);
                    _field[_freePos[rnd]._x, _freePos[rnd]._y] = aux;
                    _freePos.RemoveAt(rnd);
                }
            }
            _freePos.Clear();
        }
        _levelManager.gameField.SetGameField(_field);
        SaveAndLoad.Save();
    }*/
}
