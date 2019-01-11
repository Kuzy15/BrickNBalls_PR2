using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeManager : MonoBehaviour {

    public Camera mainCamera;
    public Canvas mainCanvas;
    public Canvas topCanvas;
    public Canvas botCanvas;


    private float _gameFieldHeight = 14.0f * 1.5f;
    private float _gameFieldWidth = 11.5f * 1.5f;
    private LevelManager _levelManager;
    private float _topStop;
    private float _botStop;

    public void Init(LevelManager lm)
    {
        _levelManager = lm;
    }

    public void Resize()
    {

        float cameraSizeHeight = mainCamera.orthographicSize * 2;
        float pixelInUnits = cameraSizeHeight / mainCamera.pixelHeight;


        float topCanvasSize = topCanvas.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        _topStop = topCanvasSize;// * pixelInUnits;

        float botCanvasSize = botCanvas.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        _botStop = botCanvasSize;// *pixelInUnits;

        float gameFieldHole = Screen.height - (topCanvasSize + botCanvasSize) * pixelInUnits;

        float newCameraSize = _gameFieldHeight * (gameFieldHole * pixelInUnits) / cameraSizeHeight;
        mainCamera.orthographicSize = newCameraSize / 2;

        
       /* if (Screen.width * pixelInUnits < 11.5f)
        {
            Screen.w = 11.5f * pixelInUnits;

        }*/

        Canvas.ForceUpdateCanvases();
        Destroy(gameObject);
    }

    public float GetTopStop()
    {
        return _topStop;
    }

    public float GetBotStop()
    {
        return _botStop;
    }
}
