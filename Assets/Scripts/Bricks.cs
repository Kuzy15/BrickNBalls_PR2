using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {


    protected int _nHits;

    public void SetHits(int n)
    {
        _nHits = n;
    }

    public int GetHits()
    {
        return _nHits;
    }
}
