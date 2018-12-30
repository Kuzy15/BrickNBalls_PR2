using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeManager : MonoBehaviour {

    public Camera mainCamera;
    public Canvas mainCanvas;
    public GameObject topCanvas;
    public GameObject botCanvas;


    float _gameFieldWidth = 12.0f;
    float _gameFieldHeight = 14.0f;

    public void Resize()
    {
        Canvas.ForceUpdateCanvases();

        float cameraSizeHeight = mainCamera.orthographicSize * 2;
        float cameraSizeWidth = cameraSizeHeight * mainCamera.aspect;

        float upixelHeight = cameraSizeHeight / mainCamera.pixelHeight;
        float upixelWidth = cameraSizeWidth / mainCamera.pixelWidth;


        float topCanvasSize = topCanvas.GetComponent<RectTransform>().rect.height * mainCanvas.GetComponent<RectTransform>().localScale.y;
       // l.SetTopCanvasSize(topCanvasSize);

        float botCanvasSize = botCanvas.GetComponent<RectTransform>().rect.height * mainCanvas.GetComponent<RectTransform>().localScale.y;
        //l.SetBotCanvasSize(botCanvasSize);

        float gameFieldHeigth = Screen.height - (topCanvasSize + botCanvasSize);

        float totalPixeles = gameFieldHeigth + topCanvasSize + botCanvasSize;
        float totalUnidades = totalPixeles / (upixelHeight);

        float unidadesTop = (topCanvasSize / (upixelHeight));
        float unidadesBot = botCanvasSize / (upixelHeight);

        mainCamera.orthographicSize = ((_gameFieldHeight / 2) + (unidadesTop / 2) + (unidadesBot / 2));
        float yPosition = ((_gameFieldHeight) / 2) + (unidadesTop) - (unidadesBot);



        mainCamera.transform.position = new Vector3(0, yPosition, -10);

        float cameraWidthUnits = mainCamera.orthographicSize * 2 * mainCamera.aspect;

        if (cameraWidthUnits < _gameFieldWidth)
        {
            float newCameraHeightSize = _gameFieldWidth / mainCamera.aspect;
            mainCamera.orthographicSize = (newCameraHeightSize / 2);

        }

        Destroy(gameObject);
    }
}
