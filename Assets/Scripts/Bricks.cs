using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {


    protected int _nHits; //Number of hits
    protected int _type; //Type of brick
    protected bool _nextRoundDestroy; // Used for destroy a brick in the next round (used for raybricks...)
    protected LevelManager _levelManager;

    public void Init(LevelManager lm)
    {
        _levelManager = lm;
    }
    //Set number of hits
    public void SetHits(int n)
    {
        _nHits = n;
    }
    //Get number of hits
    public int GetHits()
    {
        return _nHits;
    }

    //Set type of brick
    public void SetTypeBrick(int t)
    {
        _type = t;
    }

    //Get type of brick
    public int GetTypeBrick()
    {
        return _type;
    }

    //Set can destroy
    public void SetCanDestroyNextRound(bool d)
    {
        _nextRoundDestroy = d;
    }

    //Get can destroy
    public bool GetCanDestroyNextRound()
    {
        return _nextRoundDestroy;
    }
}
