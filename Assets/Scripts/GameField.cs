using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{

    GameObject[,] _field = new GameObject[14, 11];

    int[] _typeBlock = new int[121];
    int[] _hitsBlock = new int[121];

    public TextAsset map;
    public GameObject [] block = new GameObject [6];

    // Use this for initialization
    void Start()
    {

        MapReader mapReader = new MapReader(map);
        mapReader.Reader(ref _typeBlock, ref _hitsBlock);

        int x = 0;
        for (int i = 0; i < 11; i++)//14
        {
            for (int j = 0; j < 11; j++)
            {
                ///Creas un bloque de este tipo
                ///y se  lo asignas
                if (_typeBlock[x] != 0)
                {
                    Vector3 pos = new Vector3(transform.position.x + j, transform.position.y - i, 0);
                    GameObject aux = Instantiate(block[_typeBlock[x] - 1], transform.position, transform.rotation, transform);
                    aux.transform.position = pos;
                    if (_typeBlock[x] <= 6)
                    {
                        aux.GetComponent<SolidBrick>().SetHits(_hitsBlock[x]);
                    }
                    _field[i, j] = aux;
                }
                x++;
            }
        }
    }


    public void MoveBlocks()
    {
        for (int i = 10; i >= 0; i--)
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
    }
}