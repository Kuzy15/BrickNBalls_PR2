using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    private Ball _ball;
    private uint _nBalls;
    private Vector3 _posDest;
    private LevelManager _levelManger;

    //Init variables
    public void Init(Ball ballPrefab, uint nBalls, LevelManager lm) {

        _ball = ballPrefab;
        _nBalls = nBalls;
        _levelManger = lm;
    }

    //Start spawn coroutine
    public void SpawnBalls()
    {
        StartCoroutine(SpawnBall());
    }

    //Stop spawn coroutine
    public void StopSpawnBalls()
    {
        StopCoroutine(SpawnBall());
    }

    //Spawn a number of balls with a vel in a dir
    private IEnumerator SpawnBall()
    {
        int wait = 0; 
        while(_nBalls > 0)
        {
                yield return new WaitForFixedUpdate();
            if (!_levelManger.GetPaused())
            {
                wait++;
                if (wait > 3)
                {
                    Instantiate(_ball, transform.position, transform.rotation).GetComponent<Ball>().StartMoving(_posDest); //Create a ball and set a velocity
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

    //Move the spawner to a new position
    public void MoveTo(Vector2 pos) 
    {
        gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
    }

    //Get clicked position to set the ball´s dir
    public void SetPosDest(Vector3 pos) 
    {
        _posDest = pos;
    }

    //Set a numebr of balls
    public void SetNBalls(uint n)
    {
        _nBalls = n;
    }

    //Show the spawner
    public void Show(bool s)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = s;

    }
}
