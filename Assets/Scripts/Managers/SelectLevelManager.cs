using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;

public class SelectLevelManager : MonoBehaviour {

    //Ruby text
    public Text rubyText;

    public Button buttonPrefab;

    private Button _buttonAux;

    private int _nLevels;
    //Available maps
    private TextAsset[] maps;
    //Buttons
    private Button[] _buttons;

    public Canvas canvasButtonsLevels;

    public Image scrollView;

    private const int MAX_SCROLL = 550;

    private void Update()
    {
        Debug.Log(canvasButtonsLevels.transform.position);

       if (canvasButtonsLevels.transform.position.y < MAX_SCROLL)
           canvasButtonsLevels.transform.position = new Vector3(canvasButtonsLevels.transform.position.x, MAX_SCROLL, canvasButtonsLevels.transform.position.z);
    }


    // Start is called before the first frame update
    void Start()
    {
       
        string myPath = "Assets/Resources/Maps";
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.txt");

        _nLevels = info.Length;

        maps = new TextAsset[_nLevels];
        _buttons = new Button[_nLevels];

        for (int i = 0; i < _nLevels; i++)
        {
            maps[i] = Resources.Load("Maps/mapdata" + (i + 1).ToString()) as TextAsset;
        }


        int posX = 0;
        int posY = 0;
        for (int i = 0; i < _nLevels; i++)
        {
            if (posX == 0)
            {
                _buttonAux = Instantiate(buttonPrefab, new Vector3(Screen.width / 2 - 100 + 50 * posX, Screen.height / 2  + 150 - 85 * posY, 0), new Quaternion(0, 0, 0, 0), canvasButtonsLevels.transform)/*.GetComponentInChildren<TextMesh>().text = i.ToString()*/;              
                posX++;
            }
            else if (posX == 1)
            {
                _buttonAux = Instantiate(buttonPrefab, new Vector3((Screen.width / 2) - 50 + 50 * posX, Screen.height / 2 + 150 - 85 * posY, 0), new Quaternion(0, 0, 0, 0), canvasButtonsLevels.transform)/*.GetComponentInChildren<TextMesh>().text = i.ToString()*/;
              
                posX++;
            }
            else if (posX == 2)
            {
                _buttonAux = Instantiate(buttonPrefab, new Vector3(Screen.width / 2 + 50 * posX, Screen.height / 2 + 150 - 85 * posY, 0), new Quaternion(0, 0, 0, 0), canvasButtonsLevels.transform)/*.GetComponentInChildren<TextMesh>().text = i.ToString()*/;              
                posX = 0;
                posY++;
            }
            _buttonAux.GetComponentInChildren<Text>().text = (i + 1).ToString();
            _buttonAux.name = "Level" + (i + 1).ToString();

            _buttons[i] = _buttonAux;

           
        }

      


        //Set to each button its LoadLevel method with its map
        #region Listener Buttons

        for(int i = 0; i < _nLevels; i++)
        {
            int copy = i;
            _buttons[i].onClick.AddListener(delegate { LoadLevel(maps[copy], copy); });
        }


        /*_buttons[0].onClick.AddListener(delegate { LoadLevel(maps[0], 0); });
        _buttons[1].onClick.AddListener(delegate { LoadLevel(maps[1], 1); });
        _buttons[2].onClick.AddListener(delegate { LoadLevel(maps[2], 2); });
        _buttons[3].onClick.AddListener(delegate { LoadLevel(maps[3], 3); });
        _buttons[4].onClick.AddListener(delegate { LoadLevel(maps[4], 4); });
        _buttons[5].onClick.AddListener(delegate { LoadLevel(maps[5], 5); });
        _buttons[6].onClick.AddListener(delegate { LoadLevel(maps[6], 6); });
        _buttons[7].onClick.AddListener(delegate { LoadLevel(maps[7], 7); });
        _buttons[8].onClick.AddListener(delegate { LoadLevel(maps[8], 8); });
        _buttons[9].onClick.AddListener(delegate { LoadLevel(maps[9], 9); });*/
#endregion   

        //Show each level´s stars and if the level is locked or not
        for (int i = 0; i < GameManager.gameManagerInstace.GetLevels().Length; i++)
        {
            _buttons[i].transform.GetChild(1).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[0]);
            _buttons[i].transform.GetChild(2).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[1]);
            _buttons[i].transform.GetChild(3).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._stars[2]);
            _buttons[i].transform.GetChild(4).gameObject.SetActive(GameManager.gameManagerInstace.GetLevels()[i]._lock);
        }

        //Show rubies´ text
        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString();
    }


    //If the level is unlocked Load the level 
    void LoadLevel(TextAsset map, int level)
    {
        if (!_buttons[level].transform.GetChild(4).gameObject.activeSelf) // child 0: text, child 1: star, child 2: star, child 3: star, child 4: locked
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
