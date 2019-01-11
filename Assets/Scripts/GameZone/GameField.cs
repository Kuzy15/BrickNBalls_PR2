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

    public GameObject [] _block = new GameObject [8]; //Dif types of blocks
    public Text _rayPowerUpText; 


    private GameObject [,] _field = new GameObject [14 , 11]; //Board for gameObjects(bricks)
    private List<int> _extrafield = new List<int>(); //List for bricks that doesn´t fit in the board 
    private List<int> _listBlock = new List<int>(); //List for the types and hits of blocks
    private TextAsset _map; //To read the map
    private int _numBlocks; //bricks counter
    private int _rayCont; //rayBricks counter
    private int _totalBlocks; //Total Blocks to calculate the stars with points
    private List<Pos> _freePos = new List<Pos>(); //Free positons of the board
    private LevelManager _levelManager;

    // Use this for initialization
    //Init all variables, maps, gameObjects, counters ...
    public void Init(LevelManager lm)
    {
        _levelManager = lm;
        _map = GameManager.gameManagerInstace.GetMapLevel();
        MapReader mapReader = new MapReader(_map);
        mapReader.Reader(ref _listBlock);
        //_numBlocks = 0;
        _rayCont = 0;
        _totalBlocks = 0;
        _rayPowerUpText.text = GameManager.gameManagerInstace.GetComponent<GameManager>().GetNRayPowerUp().ToString();

        int x = (_listBlock.Count - 1) / 2;
        int indexHits = (_listBlock.Count - 1) / 2 + 1;

        //Fill the field and create the gameObjects
        for (int i = 10; i >= 0; i--)
        {
            for (int j = 10; j >= 0; j--)
            {
                if (_listBlock[x] != 0)
                {
                    Vector3 pos = new Vector3(transform.position.x + j, transform.position.y - i - 1.5f, 0);
                    GameObject aux = Instantiate(_block[_listBlock[x] - 1], transform.position, transform.rotation, transform);

                    aux.GetComponent<Bricks>().Init(_levelManager, _listBlock[x], _listBlock[x + indexHits]);
                    aux.transform.position = pos;

                    if (_listBlock[x] <= 6)
                    {
                        _totalBlocks++;
 
                    }

                    else if(_listBlock[x] == 7)
                    {
                        _rayCont++;
                    }

                    _field[i,j] = aux;
                }
                x--;
            }
        }

        //The rest of bricks are saved separately 
        if (x > 0)
        {
            for (int i = x; i >= 0; i--)
            {
                if(_listBlock[i] < 7)
                {
                    _totalBlocks++;
                }
                else if (_listBlock[x] == 7)
                {
                    _rayCont++;
                }
                _extrafield.Add(_listBlock[i]);
            }
            for (int i = x; i >= 0; i--)
            {
                _extrafield.Add(_listBlock[i + indexHits]);
            }
        }

       
    }

    //Amount of bricks in scene
    private void Update()
    {
        _numBlocks  = GameObject.FindGameObjectsWithTag("Bricks").Length - 6;
    }

    //Get the total bricks/blocks
    public int GetTotalBlocks()
    {
        return _totalBlocks;
    }

    //Move gameObject in 'logic' matrix (field) and in the secene
    //Check if there are bricks that must destroy like rayBricks(destroy at next round), other example is if the bricks
    //can go down, should check here and do something
    //If you have elements in extraField you create gameObject at matrix and scene at first and top positions respectively
    public void MoveBlocks()
    {
        int x = (_extrafield.Count - 1) / 2;
        int indexHits = (_extrafield.Count - 1) / 2 + 1;
        for (int j = 10; j >= 0; j--)
        {
            for (int i = 13; i >= 0; i--)
            {
                if (_field[i, j] != null)
                {

                    if (!_field[i, j].gameObject.GetComponent<Bricks>().canFall())
                    {

                        j--;
                        i = 13;

                    }

                    else
                    {
                        _field[i, j].gameObject.GetComponent<Bricks>().Fall();
                        GameObject aux = _field[i, j];
                        _field[i, j] = null;
                        _field[i + 1, j] = aux;
                    }

                }
            }
        }

        //Case of have extraField
        if (_extrafield.Count > 0)
        {
            for (int i = 0; i < 11; i++, x--)
            {
                if (_extrafield[x] != 0)
                {
                    Vector3 pos = new Vector3(transform.position.x + i, transform.position.y - 1.5f, 0);
                    GameObject aux = Instantiate(_block[_extrafield[x] - 1], transform.position, transform.rotation, transform);
                    aux.GetComponent<Bricks>().Init(_levelManager, _listBlock[x], _extrafield[x + indexHits]);
                    aux.transform.position = pos;
 
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
        
    }

    //Check if is necessary active the warnings
    public bool ActiveWarnings()
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
        return active;
    }

    //Check if you lose the game
    public bool EndGame()
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
        return active;
    }

    //Check if you destroy all bricks and there aren´t anymore on extrafield
    public bool WinGame() { 
        if(_extrafield.Count == 0 && _numBlocks == 0)
        {
            return true;
        }
        return false;
    }


  

    //If you have got ray powerup, see the free positions of the board and it creates two
    //rayBricks in a random free position
    //Then save the game
    private void CalculateFreePos()
    {
        if (GameManager.gameManagerInstace.GetNRayPowerUp() > 0)
        {
            GameManager.gameManagerInstace.RemoveNRayPowerUp(1);
            _rayPowerUpText.text = GameManager.gameManagerInstace.GetComponent<GameManager>().GetNRayPowerUp().ToString();

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

        }
    }

    public void CreateRayPowerUp()
    {

        CalculateFreePos();

        for (int i = 0; i < 2; i++)
        {
            if (_freePos.Count > 0)
            {
                int rnd = Random.Range(0, _freePos.Count - 1);

                Vector3 pos = new Vector3(transform.position.x + _freePos[rnd]._y, transform.position.y - _freePos[rnd]._x - 1.5f, 0);
                GameObject aux = Instantiate(_block[6], transform.position, transform.rotation, transform);

                aux.GetComponent<Bricks>().Init(_levelManager, 7, 0);

                aux.transform.position = pos;
                _field[_freePos[rnd]._x, _freePos[rnd]._y] = aux;
                _freePos.RemoveAt(rnd);
            }
        }
        _freePos.Clear();

        SaveAndLoad.Save();
    }
}