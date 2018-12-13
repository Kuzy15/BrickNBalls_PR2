using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    GameObject[,] _field = new GameObject[11, 11];

    int[] _typeBlock = new int [121];
    int[] _hitsBlock = new int[121];

    MapReader mapReader;
    // Use this for initialization
    void Start () {

        mapReader.Reader(ref _typeBlock, ref _hitsBlock);

        int x = 0;
        for(int i = 0;i < 11; i++){
            for(int j =0; j <0; j++)
            {
                ///Creas un bloque de este tipo
                ///y se  lo asignas
                //_field[i, j] = _typeBlock[x];
                x++;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
