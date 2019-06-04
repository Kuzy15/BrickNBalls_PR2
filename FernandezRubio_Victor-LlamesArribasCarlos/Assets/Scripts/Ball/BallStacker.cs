using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStacker : MonoBehaviour {

    //public TextMesh _label;
    private uint _ballStacked;

	
	public void Init () {
        ResetNumBalls();
    }

    //Give the position to the stacker
    public void SetPos(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    //Active and deactive the script and render
    public void Show(bool s)
    {
        GetComponent<SpriteRenderer>().enabled = s;
        GetComponentInChildren<MeshRenderer>().enabled = s;
       
    }

    //Add one ball
    public void AddBall()
    {
        _ballStacked++;
        GetComponentInChildren<TextMesh>().text = "x" + (_ballStacked).ToString();
    }

    //Get all stacked balls
    public uint GetBallStacked()
    {
        return _ballStacked;
    }

    //Reset stacked balls counter
    public void ResetNumBalls()
    {
        _ballStacked = 0;
    }
}
