using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SelectLevelManager : MonoBehaviour {


    public Button[] buttomLevels = new Button[10];
    public TextAsset[] maps = new TextAsset[10];


    // Start is called before the first frame update
    void Start()
    {
        buttomLevels[0].onClick.AddListener(delegate { LoadLevel(maps[0]); });
        buttomLevels[1].onClick.AddListener(delegate { LoadLevel(maps[1]); });
        buttomLevels[2].onClick.AddListener(delegate { LoadLevel(maps[2]); });
        buttomLevels[3].onClick.AddListener(delegate { LoadLevel(maps[3]); });
        buttomLevels[4].onClick.AddListener(delegate { LoadLevel(maps[4]); });
        buttomLevels[5].onClick.AddListener(delegate { LoadLevel(maps[5]); });
        buttomLevels[6].onClick.AddListener(delegate { LoadLevel(maps[6]); });
        buttomLevels[7].onClick.AddListener(delegate { LoadLevel(maps[7]); });
        buttomLevels[8].onClick.AddListener(delegate { LoadLevel(maps[8]); });
        buttomLevels[9].onClick.AddListener(delegate { LoadLevel(maps[9]); });
    }


    void LoadLevel(TextAsset map)
    {
        GameManager.gameManagerInstace.SetMapLevel(map);
        SceneManager.LoadScene(1);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
