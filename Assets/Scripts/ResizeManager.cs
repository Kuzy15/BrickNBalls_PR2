using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeManager : MonoBehaviour {

    public Camera mainCamera;
    public Canvas mainCanvas;
    public Canvas topCanvas;
    public Canvas botCanvas;


    float _gameFieldHeight = 14.0f;

    public void Resize()
    {
        Canvas.ForceUpdateCanvases();

        float cameraSizeHeight = mainCamera.orthographicSize * 2;
        float cameraSizeWidth = cameraSizeHeight * mainCamera.aspect;

        float pixelInUnits = cameraSizeHeight / mainCamera.pixelHeight;


        float topCanvasSize = topCanvas.GetComponent<RectTransform>().rect.height * mainCanvas.GetComponent<RectTransform>().localScale.y;
        float botCanvasSize = botCanvas.GetComponent<RectTransform>().rect.height * mainCanvas.GetComponent<RectTransform>().localScale.y;

        /*float gameFieldHeigth = Screen.height - (topCanvasSize + botCanvasSize);

        float totalPixeles = gameFieldHeigth + topCanvasSize + botCanvasSize;
        float totalUnits = totalPixeles / pixelInUnits;*/

        float unidadesTop = topCanvasSize * pixelInUnits;
        float unidadesBot = botCanvasSize * pixelInUnits;

        mainCamera.orthographicSize = ((_gameFieldHeight / 2) + (unidadesTop / 2) + (unidadesBot / 2));

        float cameraWidthUnits = mainCamera.orthographicSize * 2 * mainCamera.aspect;

        /*if (cameraWidthUnits < 12.0f)
        {
            float newCameraHeightSize = 12.0f / mainCamera.aspect;
            mainCamera.orthographicSize = (newCameraHeightSize / 2);

        }
        */
        Destroy(gameObject);
    }
}
