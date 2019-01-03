using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {


    protected int _nHits;
    protected int _type;

    public void SetHits(int n)
    {
        _nHits = n;
    }

    public int GetHits()
    {
        return _nHits;
    }

    public void SetTypeBrick(int t)
    {
        _type = t;
    }

    public int GetTypeBrick()
    {
        return _type;
    }
}
