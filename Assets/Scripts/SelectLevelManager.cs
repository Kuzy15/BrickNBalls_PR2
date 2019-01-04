using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SelectLevelManager : MonoBehaviour {


    public Button[] buttomLevels = new Button[10];
    public TextAsset[] maps = new TextAsset[10];

    public Text rubyText;

    // Start is called before the first frame update
    void Start()
    {
        //resizeManager.GetComponent<ulalaResizeManager>().Resize();

        buttomLevels[0].onClick.AddListener(delegate { LoadLevel(maps[0], 0); });
        buttomLevels[1].onClick.AddListener(delegate { LoadLevel(maps[1], 1); });
        buttomLevels[2].onClick.AddListener(delegate { LoadLevel(maps[2], 2); });
        buttomLevels[3].onClick.AddListener(delegate { LoadLevel(maps[3], 3); });
        buttomLevels[4].onClick.AddListener(delegate { LoadLevel(maps[4], 4); });
        buttomLevels[5].onClick.AddListener(delegate { LoadLevel(maps[5], 5); });
        buttomLevels[6].onClick.AddListener(delegate { LoadLevel(maps[6], 6); });
        buttomLevels[7].onClick.AddListener(delegate { LoadLevel(maps[7], 7); });
        buttomLevels[8].onClick.AddListener(delegate { LoadLevel(maps[8], 8); });
        buttomLevels[9].onClick.AddListener(delegate { LoadLevel(maps[9], 9); });
        
        for(int i = 0; i < GameManager.gameManagerInstace.GetLevels().Length; i++)
        {
            buttomLevels[i].transform.GetChild(1).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[0]);
            buttomLevels[i].transform.GetChild(2).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[1]);
            buttomLevels[i].transform.GetChild(3).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[2]);
            buttomLevels[i].transform.GetChild(4).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._lock);
        }

        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString();
    }



    void LoadLevel(TextAsset map, int level)
    {
        if (!buttomLevels[level].transform.GetChild(4).gameObject.activeSelf)
        {
            GameManager.gameManagerInstace.SetMapLevel(map);
            SceneManager.LoadScene(1);
        }
    }

    public void OnClickShopScene()
    {
        SceneManager.LoadScene(2);
    }
}
