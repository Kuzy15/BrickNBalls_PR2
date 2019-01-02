using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public static  GameManager gameManagerInstace;
    public Level[] levels = new Level[10];

    private TextAsset _currentMapLevel;

    // Start is called before the first frame update
    private void Awake()
    {
        if (gameManagerInstace == null)
        {
            gameManagerInstace = this;

            DontDestroyOnLoad(gameObject);

            levels[0] = new Level(false, false, false, false);
            for (int i = 1; i < levels.Length; i++)
            {
                levels[i] = new Level(false, false, false, true);
            }
        }
    }


    public void SetMapLevel(TextAsset map)
    {
        _currentMapLevel = map;

    }

    public TextAsset GetMapLevel()
    {
        return _currentMapLevel; 
    }


}
