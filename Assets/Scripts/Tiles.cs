using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {

    
    int _id;
    int _numHits;
    bool _hitVertical;
    bool _hitHorizontal;
    bool _goDown;
    bool _addOne;
    bool _addTwo;
    bool _addThree;

    public class Block : Tiles
    {
        public int g()
        {
           return _id ;
        }    
    }

    public class EX
    {
        Block block = new Tiles.Block();
        void Main()
        {
            if(block._addOne == true) { }
        }
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
/*
    int Getid(){
        return _id;
    }

    bool GetHitVertical()
    {
        return _hitVertical;
    }

    bool GetHitHorizontal()
    {
        return _hitHorizontal;
    }

    bool GetGoDown()
    {
        return _goDown;
    }

    bool GetAddOne()
    {
        return _addOne;
    }

    bool GetAddTwo()
    {
        return _addTwo;
    }

    bool GetAddThree()
    {
        return _addThree;
    }

  */
}
