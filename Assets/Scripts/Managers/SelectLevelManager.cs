using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class SelectLevelManager : MonoBehaviour {

    //Ruby text
    public Text rubyText;
    //Available maps
    private TextAsset[] maps = new TextAsset[10];
    //Buttons
    private Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0;i < 10; i++)
        {
            maps[i] = Resources.Load("Maps/mapdata" + (i + 1).ToString()) as TextAsset;
        }

        //Take only level buttons and sort them
        Button[] allButtons = FindObjectsOfType<Button>();
        buttons = new Button[allButtons.Length - 2];
        int index;
        for(int i = 0; i < allButtons.Length; i++)
        {
            if (allButtons[i].name[0] == 'L')
            {
                string level = "";
                for(int j = 5; j < allButtons[i].name.Length; j++)
                {
                    level += allButtons[i].name[j];
                }
                Int32.TryParse(level, out index);
                index--;
                buttons[index] = allButtons[i];
            }
        }
        //Set to each button its LoadLevel method with its map
#region Listener Buttons
        buttons[0].onClick.AddListener(delegate { LoadLevel(maps[0], 0); });
        buttons[1].onClick.AddListener(delegate { LoadLevel(maps[1], 1); });
        buttons[2].onClick.AddListener(delegate { LoadLevel(maps[2], 2); });
        buttons[3].onClick.AddListener(delegate { LoadLevel(maps[3], 3); });
        buttons[4].onClick.AddListener(delegate { LoadLevel(maps[4], 4); });
        buttons[5].onClick.AddListener(delegate { LoadLevel(maps[5], 5); });
        buttons[6].onClick.AddListener(delegate { LoadLevel(maps[6], 6); });
        buttons[7].onClick.AddListener(delegate { LoadLevel(maps[7], 7); });
        buttons[8].onClick.AddListener(delegate { LoadLevel(maps[8], 8); });
        buttons[9].onClick.AddListener(delegate { LoadLevel(maps[9], 9); });
#endregion   
        //Show each level´s stars and if the level is locked or not
        for (int i = 0; i < GameManager.gameManagerInstace.GetLevels().Length; i++)
        {
            buttons[i].transform.GetChild(1).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[0]);
            buttons[i].transform.GetChild(2).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[1]);
            buttons[i].transform.GetChild(3).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[2]);
            buttons[i].transform.GetChild(4).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._lock);
        }

        //Show rubies´ text
        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString();
    }


    //If the level is unlocked Load the level 
    void LoadLevel(TextAsset map, int level)
    {
        if (!buttons[level].transform.GetChild(4).gameObject.activeSelf) // child 0: text, child 1: star, child 2: star, child 3: star, child 4: locked
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
