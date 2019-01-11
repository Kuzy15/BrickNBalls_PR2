using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeManager : MonoBehaviour {

    public Camera mainCamera;
    public Canvas mainCanvas;
    public Canvas topCanvas;
    public Canvas botCanvas;


    private float _gameFieldHeight = 14.0f * 1.5f; //gameField height * the scale of all gameObjects of the scene
    private float _gameFieldWidth = 11.5f * 1.5f; //gameField width * the scale of all gameObjects of the scene
    private LevelManager _levelManager;
    private float _topStop; //To return canvas size and Know the limits for input
    private float _botStop; //To return canvas size and Know the limits for input

    public void Init(LevelManager lm)
    {
        _levelManager = lm;
    }

    //Resize the camera distance to adapt the gap we have to the gameField
    public void Resize()
    {

        float cameraSizeHeight = mainCamera.orthographicSize * 2;
        float pixelInUnits = cameraSizeHeight / mainCamera.pixelHeight;


        float topCanvasSize = topCanvas.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        _topStop = topCanvasSize;

        float botCanvasSize = botCanvas.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        _botStop = botCanvasSize;

        float gameFieldHole = Screen.height - (topCanvasSize + botCanvasSize) * pixelInUnits;

        float newCameraSize = _gameFieldHeight * (gameFieldHole * pixelInUnits) / cameraSizeHeight;
        mainCamera.orthographicSize = newCameraSize / 2;

        Canvas.ForceUpdateCanvases();
        Destroy(gameObject);
    }

    //Get top limit
    public float GetTopStop()
    {
        return _topStop;
    }

    //Get bot limit
    public float GetBotStop()
    {
        return _botStop;
    }
}
