using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {


    private Vector2 _dir;
    // Use this for initialization
    public void Start() {

    }


    public void StartMoving(Vector3 pos)
    {
        StartCoroutine(StartMovingCoroutine(pos));
    }

    private IEnumerator StartMovingCoroutine(Vector3 pos) //Se encarga de dar velocidad a las bolas
    {

        float mod = Mathf.Sqrt(Mathf.Pow(pos.x - transform.position.x, 2) + Mathf.Pow(pos.y - transform.position.y, 2));
        _dir.x = (pos.x - transform.position.x) / mod;
        _dir.y = (pos.y - transform.position.y) / mod;
        GetComponent<Rigidbody2D>().velocity = new Vector2(_dir.x * 10, _dir.y * 10);


        yield return null;
    }


    /*void Stop() //Para la bola
    {
        GetComponent<Rigidbody2D>().velocity.Set(0, 0);
    }*/


    public void GoTo(Vector3 pos, float time, System.Action<Ball> callback = null) { 

        StartCoroutine(GoToCoroutine(pos, time, callback));

    }


    private IEnumerator GoToCoroutine(Vector3 pos, float time, System.Action<Ball> callback = null) 
    {
        /*if(callback != null)
        {
            callback(this);
        }*/

      
        yield return null;
    }


}
