using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SelectLevelManager : MonoBehaviour {

    //Buttons for the SelectLevel Scene
    public Button[] buttonLevels = new Button[10];
    //Available maps
    public TextAsset[] maps = new TextAsset[10];
    //Ruby text
    public Text rubyText;

    // Start is called before the first frame update
    void Start()
    {
        //resizeManager.GetComponent<ulalaResizeManager>().Resize();

        //Set to each button its LoadLevel method with its map
        buttonLevels[0].onClick.AddListener(delegate { LoadLevel(maps[0], 0); });
        buttonLevels[1].onClick.AddListener(delegate { LoadLevel(maps[1], 1); });
        buttonLevels[2].onClick.AddListener(delegate { LoadLevel(maps[2], 2); });
        buttonLevels[3].onClick.AddListener(delegate { LoadLevel(maps[3], 3); });
        buttonLevels[4].onClick.AddListener(delegate { LoadLevel(maps[4], 4); });
        buttonLevels[5].onClick.AddListener(delegate { LoadLevel(maps[5], 5); });
        buttonLevels[6].onClick.AddListener(delegate { LoadLevel(maps[6], 6); });
        buttonLevels[7].onClick.AddListener(delegate { LoadLevel(maps[7], 7); });
        buttonLevels[8].onClick.AddListener(delegate { LoadLevel(maps[8], 8); });
        buttonLevels[9].onClick.AddListener(delegate { LoadLevel(maps[9], 9); });
        
        //Show each level´s stars and if the level is locked or not
        for(int i = 0; i < GameManager.gameManagerInstace.GetLevels().Length; i++)
        {
            buttonLevels[i].transform.GetChild(1).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[0]);
            buttonLevels[i].transform.GetChild(2).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[1]);
            buttonLevels[i].transform.GetChild(3).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[2]);
            buttonLevels[i].transform.GetChild(4).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._lock);
        }

        //Show rubies´ text
        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString();
    }


    //If the level is unlocked Load the level 
    void LoadLevel(TextAsset map, int level)
    {
        if (!buttonLevels[level].transform.GetChild(4).gameObject.activeSelf) // child 0: text, child 1: star, child 2: star, child 3: star, child 4: locked
        {
            GameManager.gameManagerInstace.SetMapLevel(map);
            SceneManager.LoadScene(1);
        }
    }

    //Go to Shop Scene
    public void OnClickShopScene()
    {
        SceneManager.LoadScene(2);
    }

    //Close app
    public void OnClickPower()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
