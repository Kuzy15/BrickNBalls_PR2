using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {


    protected int _nHits;
    protected bool _canDestroy;

    public void SetHits(int n)
    {
        _nHits = n;
    }

    public int GetHits()
    {
        return _nHits;
    }

    public void SetDestroy(bool d)
    {
        _canDestroy = d;
    }

    public bool GetDestroy()
    {
        return _canDestroy;
    }
}
