using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

struct Level
{

}



public class GameManager : MonoBehaviour
{

    public static  GameManager gameManagerInstace;

    private TextAsset _currentMapLevel;

    // Start is called before the first frame update
    private void Start()
    {
        gameManagerInstace = this;
        DontDestroyOnLoad(gameObject);
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
