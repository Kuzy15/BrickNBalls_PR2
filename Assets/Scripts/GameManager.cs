using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


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
            Load();
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

    //Save the game (levels info(stars and lock), rubies and rayPowerUp) at a document in a path
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.txt");
        bf.Serialize(file, GameManager.gameManagerInstace.GetLevels());
        bf.Serialize(file, GameManager.gameManagerInstace.GetRuby());
        bf.Serialize(file, GameManager.gameManagerInstace.GetNRayPowerUp());
        file.Close();
    }

    //If exist a saved game, load the info saved before, and give them to the gameManager to update the info
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.txt", FileMode.Open);
            Level[] readLevel = (Level[])bf.Deserialize(file);
            int readRuby = (int)bf.Deserialize(file);
            int readRayPowerUp = (int)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < GameManager.gameManagerInstace._levels.Length; i++)
            {
                GameManager.gameManagerInstace._levels[i] = readLevel[i];
            }

            GameManager.gameManagerInstace._ruby = readRuby;
            GameManager.gameManagerInstace._nRayPowerUp = readRayPowerUp;
        }
    }
}
