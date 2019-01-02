using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{

    public GameObject [] block = new GameObject [6]; //Dif types of blocks
    public GameObject warning;


    private GameObject [,] _field = new GameObject [14 , 11];
    private List<int> _extrafield = new List<int>();
    private List<int> _listBlock = new List<int>(); 
    private TextAsset _map; //To read the map


    // Use this for initialization
    void Start()
    {
        _map = GameManager.gameManagerInstace.GetMapLevel();
        MapReader mapReader = new MapReader(_map);
        mapReader.Reader(ref _listBlock);

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
                    if (_listBlock[x] <= 6)
                    {
                        if(_listBlock[x] == 2)
                        {
                            aux.GetComponent<SolidBrick>().SetHits(_listBlock[x] * 2);
                        }
                        else
                        {
                            aux.GetComponent<SolidBrick>().SetHits(_listBlock[x + indexHits]);
                        }
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
                    if (_extrafield[x] <= 6)
                    {
                        if (_listBlock[x] == 2)
                        {
                            aux.GetComponent<SolidBrick>().SetHits(_extrafield[x] * 2);
                        }
                        else
                        {
                            aux.GetComponent<SolidBrick>().SetHits(_extrafield[x + indexHits]);
                        }
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
            if(_field[11,i] != null)
            {
                active = true;
            }

            i++;
        }
        warning.SetActive(active);
    }
}