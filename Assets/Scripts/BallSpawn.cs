using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    public GameObject ballPrefab;

    private int _nBalls;
    private Vector3 _posDest;


    // Use this for initialization
    void Start () {
        _nBalls = LevelManager.levelManagerInstance.GetNBalls();
    }

    public void SpawnBalls()
    {
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        int wait = 0; 
        while(_nBalls > 0)
        {
                yield return new WaitForFixedUpdate();
            if (!LevelManager.levelManagerInstance.GetPaused())
            {
                wait++;
                if (wait > 3)
                {
                    Instantiate(ballPrefab, transform.position, transform.rotation).GetComponent<Ball>().StartMoving(_posDest); //Creas una bola y le das la velocidad
                    _nBalls--;
                    GetComponentInChildren<TextMesh>().text = "x" + (_nBalls).ToString();
                    wait = 0;
                    if (_nBalls <= 0)
                    {
                        Show(false);
                        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                    }
                }
            }
        }
    }



    public void MoveTo(Vector2 pos) //Mueve elspawner hacia una nueva posicion
    {
        gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void SetPosDest(Vector3 pos) //Coger la posicion clickeada para establecer la dir de la bola
    {
        _posDest = pos;
    }


    public void SetNBalls(int n) //Modifica el numero de bolas
    {
        _nBalls = n;
    }

    public void Show(bool s) //Muestra el spawner
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = s;

    }
}
