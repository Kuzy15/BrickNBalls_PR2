using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {

    public GameObject ballPrefab;

    private int _nBalls;
    private Vector3 _posDest;
    private float _InstantiationTimer = 0.0f;


    // Use this for initialization
    void Start () {
        _nBalls = LevelManager.levelManagerInstance._nballs;
        enabled = false;

    }


    private void FixedUpdate()
    {

        _InstantiationTimer -= Time.deltaTime; //Cada este tiempo
        if (_InstantiationTimer <= 0)
        {

            Instantiate(ballPrefab, transform.position, transform.rotation).GetComponent<Ball>().StartMoving(_posDest); //Creas una bola y le das la velocidad
            _nBalls--;
            _InstantiationTimer = 0.1f;

            GetComponentInChildren<TextMesh>().text = "x" + (_nBalls).ToString();

            if (_nBalls <= 0)
            {
                Show(false);
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                enabled = false;

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
