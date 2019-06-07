using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script used for controlling the differents canvas to show

public class UIManager : MonoBehaviour {


    public Canvas _pause;
    public Canvas _play;
    public Canvas _winLevel;
    public Canvas _loseLevel;
 

    // Show play canvas(buttons, images, text,....), hide others canvas
    public void Init () {

        _play.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
        _winLevel.gameObject.SetActive(false);
        _loseLevel.gameObject.SetActive(false);
    }

    // Show the canvas for playing
    public void Play()
    {
        _pause.gameObject.SetActive(false);
        _play.gameObject.SetActive(true);
    }

    // Show the canvas for pausing the game
    public void Pause()
    {
        _play.gameObject.SetActive(false);
        _pause.gameObject.SetActive(true);
    }

    // Show the canvas for winning a level
    public void WinLevel()
    {
        _play.gameObject.SetActive(false);
        _winLevel.gameObject.SetActive(true);
    }

    // Show the canvas for losing a level
    public void LoseLevel()
    {
        _play.gameObject.SetActive(false);
        _loseLevel.gameObject.SetActive(true);
    }

    // Hide the canvas that appears when a level is passed
    public void HideWinLevel()
    {
        _winLevel.gameObject.SetActive(false);
    }
    // Show the canvas that appears when a level is passed
    public void ShowWinLevel()
    {
        _winLevel.gameObject.SetActive(true);
    }
}
