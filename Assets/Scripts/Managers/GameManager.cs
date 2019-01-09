using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Struct Levels which contains the stars and the lock of each level
[System.Serializable]
public struct Level
{
    public bool[] _stars;
    public bool _lock;

    public Level (bool star1, bool star2, bool star3 , bool locked)
    {
        _stars = new bool[3] { star1,star2,star3};
        _lock = locked;  
    }
}



public class GameManager : MonoBehaviour
{
    private TextAsset _currentMapLevel;
    private Level[] _levels = new Level[10];

    //[SerializeField]
    private int _ruby;
    //[SerializeField]
    private int _nRayPowerUp;

    public static  GameManager gameManagerInstace;

    // Start is called before the first frame update
    private void Awake()
    {
        //Only if gameManager is null, we init all if it is the first game and then load if there is
        //some data saved before
        if (gameManagerInstace == null)
        {
            gameManagerInstace = this;

            DontDestroyOnLoad(gameObject);

            _levels[0] = new Level(false, false, false, false);
            for (int i = 1; i < _levels.Length; i++)
            {
                _levels[i] = new Level(false, false, false, true);
            }

            _ruby = 100;
            _nRayPowerUp = 2;
            SaveAndLoad.Load();
        }
    }

    //Set map level
    public void SetMapLevel(TextAsset map)
    {
        _currentMapLevel = map;
    }

    //Get current map
    public TextAsset GetMapLevel()
    {
        return _currentMapLevel; 
    }

    //Return the current level(int)
    public int GetCurrentLevel()
    {
        string name = _currentMapLevel.name;
        string aux = "";
        for (int i = 7; i < name.Length; i++)
        {
            aux += name[i];
        }
        int level;
        Int32.TryParse(aux, out level);
        return level;
    }

    //Get info of levels (stars and lock)
    public Level[] GetLevels()
    {
        return _levels;
    }

    //Get number of rubies
    public int GetRuby()
    {
        return _ruby;
    }

    //Set a number of rubies
    public void SetRuby(int r)
    {
        _ruby = r;
    }

    //Add a number of rubies
    public void AddRuby(int r)
    {
        _ruby += r;
    }

    //Subtract a number of rubies
    public void RemoveRuby(int r)
    {
        _ruby -= r;
    }

    //Get the amount of ray powerup
    public int GetNRayPowerUp()
    {
        return _nRayPowerUp;
    }

    //Set the amount of ray powerup
    public void SetNRayPowerUp(int rpu)
    {
        _nRayPowerUp = rpu;
    }

    //Add a number of ray powerup
    public void AddNRayPowerUp(int rpu)
    {
        _nRayPowerUp += rpu;
    }

    //Subtract a number of ray powerup
    public void RemoveNRayPowerUp(int rpu)
    {
        _nRayPowerUp -= rpu;
    }
}
