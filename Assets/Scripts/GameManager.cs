using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GameManager : MonoBehaviour
{
    public Button[] buttomLevels = new Button[10];
    public TextAsset[] maps = new TextAsset[10];

    private GameManager gameManagerInstace;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerInstace = this;
        for(int i = 0; i < maps.Length; i++)
        {
            buttomLevels[i].onClick.AddListener(delegate { LoadLevel(maps[i]); });
        }
    }


    void LoadLevel(TextAsset map)
    {
        //LevelManager.levelManagerInstance.SetMapLevel(map);
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
