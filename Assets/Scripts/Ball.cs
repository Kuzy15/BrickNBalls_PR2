using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {


    private Vector3 _dir;

    public void StartMoving(Vector3 pos)
    {
        float mod = Mathf.Sqrt(Mathf.Pow(pos.x - transform.position.x, 2) + Mathf.Pow(pos.y - transform.position.y, 2));
        _dir.x = (pos.x - transform.position.x) / mod;
        _dir.y = (pos.y - transform.position.y) / mod;
        GetComponent<Rigidbody2D>().velocity = new Vector2(_dir.x * 10, _dir.y * 10);

    }

    public void GoTo(Vector3 pos, System.Action<Ball> callback = null) {
        float mod = Mathf.Sqrt(Mathf.Pow(pos.x - transform.position.x, 2) + Mathf.Pow(pos.y - transform.position.y, 2));
        _dir.x = (pos.x - transform.position.x) / mod;
        StartCoroutine(GoToCoroutine(pos, callback));

    }


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


}
