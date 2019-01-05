using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct Pos
{
    public int _x;
    public int _y;
    public Pos(int x, int y)
    {
        _x = x;
        _y = y;
    }
}

public class GameField : MonoBehaviour
{

    public GameObject [] block = new GameObject [8]; //Dif types of blocks
    public GameObject warning;
    public Text rayPowerUpText; 


    private GameObject [,] _field = new GameObject [14 , 11];
    private List<int> _extrafield = new List<int>();
    private List<int> _listBlock = new List<int>(); 
    private TextAsset _map; //To read the map
    private int _numBlocks;
    private int _rayCont;
    private List<Pos> _freePos = new List<Pos>();

    // Use this for initialization
    void Start()
    {
        _map = GameManager.gameManagerInstace.GetMapLevel();
        MapReader mapReader = new MapReader(_map);
        mapReader.Reader(ref _listBlock);
        _numBlocks = 0;
        _rayCont = 0;
        rayPowerUpText.text = GameManager.gameManagerInstace.GetComponent<GameManager>().GetNRayPowerUp().ToString();

        int x = (_listBlock.Count - 1) / 2;
        int indexHits = (_listBlock.Count - 1) / 2 + 1;
        for (int i = 10; i >= 0; i--)
        {
            for (int j = 10; j >= 0; j--)
            {
                ///Creas un bloque de este tipo
                ///y se  lo asignas
                if (_listBlock[x] != 0)
                {
                    Vector3 pos = new Vector3(transform.position.x + j, transform.position.y - i - 1.5f, 0);
                    GameObject aux = Instantiate(block[_listBlock[x] - 1], transform.position, transform.rotation, transform);
                    aux.transform.position = pos;
                    aux.GetComponent<Bricks>().SetTypeBrick(_listBlock[x]);
                    if (_listBlock[x] <= 6)
                    {
                        _numBlocks++;
                        if(_listBlock[x] == 2)
                        {
                            aux.GetComponent<SolidBrick>().SetHits(_listBlock[x] * 2);
                        }
                        else
                        {
                            aux.GetComponent<SolidBrick>().SetHits(_listBlock[x + indexHits]);
                        }
                    }
                    if(_listBlock[x] == 7)
                    {
                        _rayCont++;
                    }
                    _field[i,j] = aux;
                }
                x--;
            }
        }
        if(x > 0)
        {
            for (int i = x; i >= 0; i--)
            {
                _extrafield.Add(_listBlock[i]);
            }
            for (int i = x; i >= 0; i--)
            {
                _extrafield.Add(_listBlock[i + indexHits]);
            }
        }
    }

    public void MoveBlocks()
    {
        int x = (_extrafield.Count - 1) / 2;
        int indexHits = (_extrafield.Count - 1) / 2 + 1;
        for (int i = 13; i >= 0; i--)
        {
            for (int j = 10; j >= 0; j--)
            {
                if (_field[i, j] != null)
                {
                    Vector3 newPos = new Vector3(_field[i, j].gameObject.transform.position.x, (_field[i, j].gameObject.transform.position.y - 1), _field[i, j].gameObject.transform.position.z);
                    _field[i, j].gameObject.transform.position = newPos;
                    GameObject aux = _field[i, j];
                    _field[i, j] = null;
                    _field[i + 1, j] = aux;
                }
            }
        }

        ActiveWarnings();

        if (_extrafield.Count > 0)
        {
            for (int i = 0; i < 11; i++, x--)
            {
                if (_extrafield[x] != 0)
                {
                    Vector3 pos = new Vector3(transform.position.x + i, transform.position.y - 1.5f, 0);
                    GameObject aux = Instantiate(block[_extrafield[x] - 1], transform.position, transform.rotation, transform);
                    aux.transform.position = pos;
                    aux.GetComponent<Bricks>().SetTypeBrick(_listBlock[x]);
                    if (_extrafield[x] <= 6)
                    {
                        _numBlocks++;
                        if (_listBlock[x] == 2)
                        {
                            aux.GetComponent<SolidBrick>().SetHits(_extrafield[x] * 2);
                        }
                        else
                        {
                            aux.GetComponent<SolidBrick>().SetHits(_extrafield[x + indexHits]);
                        }
                    }
                    if (_listBlock[x] == 7)
                    {
                        _rayCont++;
                    }
                    _field[0, i] = aux;
                }
            }
            if(x < 0)
            {
                x = 0;
            }
            _extrafield.RemoveRange(_extrafield.Count - 12, 11);
            _extrafield.RemoveRange(x, 11);
        }
        EndGame();
    }

    public void SetMapsLevel(TextAsset mapLevel)
    {
        _map = mapLevel;
    }

    void ActiveWarnings()
    {
        bool active = false;
        int i = 0;
        while (!active && i < 10)
        {
            if(_field[11, i] != null && _field[11,i].GetComponent<Bricks>().GetTypeBrick() <= 6)
            {
                active = true;
            }

            i++;
        }
        warning.SetActive(active);
    }

    void EndGame()
    {
        bool active = false;
        int i = 0;
        while (!active && i < 10)
        {
            if (_field[12, i] != null && _field[12, i].GetComponent<Bricks>().GetTypeBrick() <= 6)
            {
                active = true;
            }

            i++;
        }
        if (active)
        {
            LevelManager.levelManagerInstance.LoseButtonsActive();
        }
    }

    public void RemoveBlock()
    {
        _numBlocks--;
        if(_extrafield.Count == 0 && _numBlocks == 0)
        {
            LevelManager.levelManagerInstance.EndButtonsActive();
        }
    }


    public void RayPowerUpClick()
    {
        if (GameManager.gameManagerInstace.GetNRayPowerUp() > 0)
        {
            GameManager.gameManagerInstace.RemoveNRayPowerUp(1);
            rayPowerUpText.text = GameManager.gameManagerInstace.GetComponent<GameManager>().GetNRayPowerUp().ToString();

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
                    GameObject aux = Instantiate(block[6], transform.position, transform.rotation, transform);
                    aux.transform.position = pos;
                    aux.GetComponent<Bricks>().SetTypeBrick(7);
                    _field[_freePos[rnd]._x, _freePos[rnd]._y] = aux;
                    _freePos.RemoveAt(rnd);
                }
            }
            _freePos.Clear();
        }
    }
}