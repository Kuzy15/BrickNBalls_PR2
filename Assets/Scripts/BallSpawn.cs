using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    private Ball _ball;

    private uint _nBalls;
    private Vector3 _posDest;


   
    public void Init(Ball ballPrefab, uint nBalls) {

        _ball = ballPrefab;
        _nBalls = nBalls;
    }

    public void SpawnBalls() // SpawnBalls(numBalls, dir)
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
                    Instantiate(_ball, transform.position, transform.rotation).GetComponent<Ball>().StartMoving(_posDest); //Creas una bola y le das la velocidad
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


    public void SetNBalls(uint n) //Modifica el numero de bolas
    {
        _nBalls = n;
    }

    public void Show(bool s) //Muestra el spawner
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = s;

    }
}
