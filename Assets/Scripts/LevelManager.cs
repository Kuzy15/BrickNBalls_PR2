using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public GameObject spawner;
    public GameObject stacker;
    public GameObject deathZone;
    public GameObject gameField;


    public int _nballs;
    public bool _spawn;

    public static LevelManager levelManagerInstance;

    // Use this for initialization
    void Start () {
        levelManagerInstance = this; 
        _nballs = 12;
        _spawn = true; 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPosStacker(Vector3 pos)
    {
        stacker.transform.position = pos;
    }

    public void MoveBlocks()
    {
        gameField.GetComponent<GameField>().MoveBlocks();
    }
}
