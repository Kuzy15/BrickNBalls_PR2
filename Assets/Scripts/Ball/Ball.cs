using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D _rb;
    private Vector3 _dir;
    private Vector3 _vel;
    public LevelManager levelManager;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    //Set the vel of a ball
    public void StartMoving(Vector3 pos) // StartMoving(pos, velocity)
    {
        float mod = Mathf.Sqrt(Mathf.Pow(pos.x - transform.position.x, 2) + Mathf.Pow(pos.y - transform.position.y, 2));
        _dir.x = (pos.x - transform.position.x) / mod;
        _dir.y = (pos.y - transform.position.y) / mod;
        _rb.velocity = new Vector3(_dir.x * 15, _dir.y * 15);

    }

    //Go to a position, in this case sink/stacker position
    public void GoTo(Vector3 pos, System.Action<Ball> callback = null) { // GoTo(pos, float time, ...callback)

        float mod = Mathf.Sqrt(Mathf.Pow(pos.x - transform.position.x, 2) + Mathf.Pow(pos.y - transform.position.y, 2));
        _dir.x = (pos.x - transform.position.x) / mod;

        StartCoroutine(GoToCoroutine(pos, callback));
    }

    //Coroutine to go to sink/stacker position, when the ball is in sink/stacker position, it is destroyed
    private IEnumerator GoToCoroutine(Vector3 pos, System.Action<Ball> callback = null) 
    {
        while (pos.x <= transform.position.x - 0.3f || pos.x >= transform.position.x + 0.3f)
        {
            yield return new WaitForFixedUpdate();

            transform.position = new Vector3(transform.position.x + (_dir.x * 0.3f), pos.y, 0);

            if (callback != null)
            {
                callback(this);
            }
        }
        Destroy(gameObject);
    }

    // Stop the ball
    public void Stop()
    {
        _rb.velocity = new Vector2(0, 0);
    }

    // Save current ball's velocity and stop the ball
    public void Pause()
    {
        _vel = _rb.velocity;
        _rb.velocity = new Vector2(0, 0);
    }

    // Change balls's velocity to it's last one
    public void Continue()
    {
       _rb.velocity = new Vector3(_vel.x,_vel.y);
    }
}
