using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public int _nHits;
    
	
	// Update is called once per frame
	void Update () {
        GetComponentInChildren<TextMesh>().text = _nHits.ToString();
		if(_nHits <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject)
        {
            _nHits--;
        }
    }

    public void SetHits(int n)
    {
        _nHits = n;
    }
}
