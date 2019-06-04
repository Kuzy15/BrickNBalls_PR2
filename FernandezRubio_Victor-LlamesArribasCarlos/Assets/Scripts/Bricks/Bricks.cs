using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{


    protected int _nHits; //Number of hits
    protected int _type; //Type of brick
    protected bool _nextRoundDestroy; // Used for destroy a brick in the next round (used for raybricks...)
    protected bool _canFall;                                 //More vars for future bricks

    protected LevelManager _levelManager;

    public void Init(LevelManager lm, int t, int nHits)
    {
        _levelManager = lm;
        _type = t;

        _nHits = nHits;
        if (_type == 2)
            _nHits = nHits * 2;

        _nextRoundDestroy = false;
        _canFall = true;
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

    // Fall one position
    public virtual void Fall()
    {
            Vector3 newPos = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 1), gameObject.transform.position.z);
            gameObject.transform.position = newPos;      
    }

    //Return if a brick can fall
    public bool canFall()
    {
        return _canFall;
    }
}
