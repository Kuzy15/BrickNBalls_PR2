using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathZone : MonoBehaviour {

   
    private bool _firstBall;
    private Vector3 _posStacker;


    // Use this for initialization
    void Start () {
        _firstBall = true;
	}

    public void SetFisrtBall(bool b)
    {
        _firstBall = b;
    }

    public bool GetFirstBall()
    {
        return _firstBall;
    }
	
	
    private void OnCollisionEnter2D(Collision2D collision) //Cuando colisionas con la DeathZone
    {
        if(collision.gameObject.tag == "Ball")
        {
            if(_firstBall) //En caso de ser la primera bola
            {
                _firstBall = false;
                _posStacker = collision.gameObject.transform.position;
                LevelManager.levelManagerInstance.SetPosStacker(_posStacker);

                Destroy(collision.gameObject);

                LevelManager.levelManagerInstance.stacker.GetComponent<BallStacker>().Show(true); //Muestras el stacker
                LevelManager.levelManagerInstance.stacker.GetComponent<BallStacker>().AddBall(); //Añades una bola al stacker


            }
            else //En caso de no ser la primera bola
            {
                collision.gameObject.GetComponent<Ball>().GoTo(_posStacker);
                LevelManager.levelManagerInstance.stacker.GetComponent<BallStacker>().AddBall(); //Añades una bola al stacker
            }
        }
    }
}
