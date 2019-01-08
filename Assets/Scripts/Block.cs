using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public int _nHits;
    
	
	void Start () {
        GetComponentInChildren<TextMesh>().text = _nHits.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject)
        {
            _nHits--;
        }
        GetComponentInChildren<TextMesh>().text = _nHits.ToString();
        if (_nHits <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetHits(int n)
    {
        _nHits = n;
    }
}
