using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {


    public Canvas _pause;
    public Canvas _play;
    public Canvas _winLevel;
    public Canvas _loseLevel;
 

    
    public void Init () {

        _play.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
        _winLevel.gameObject.SetActive(false);
        _loseLevel.gameObject.SetActive(false);
    }

    public void Play()
    {
        _pause.gameObject.SetActive(false);
        _play.gameObject.SetActive(true);

    }

    public void Pause()
    {
        _play.gameObject.SetActive(false);
        _pause.gameObject.SetActive(true);
    }

    public void WinLevel()
    {
        _play.gameObject.SetActive(false);
        _winLevel.gameObject.SetActive(true);
    }

    public void LoseLevel()
    {
        _play.gameObject.SetActive(false);
        _loseLevel.gameObject.SetActive(true);
    }

}
