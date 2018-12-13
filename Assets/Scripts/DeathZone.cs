using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathZone : MonoBehaviour {

   
    public bool _firstBall;
    private Vector2 _posStacker;


    // Use this for initialization
    void Start () {
        _firstBall = true;
	}
	
	
    private void OnCollisionEnter2D(Collision2D collision) //Cuando colisionas con la DeathZone
    {
        if(collision.gameObject.tag == "Ball")
        {
            if(_firstBall) //En caso de ser la primera bola
            {
                _firstBall = false;

                LevelManager.levelManagerInstance.SetPosStacker(collision.gameObject.transform.position);

                Destroy(collision.gameObject);

                LevelManager.levelManagerInstance.stacker.GetComponent<BallStacker>().Show(true); //Muestras el stacker
                LevelManager.levelManagerInstance.stacker.GetComponent<BallStacker>().AddBall(); //Añades una bola al stacker


            }
            else //En caso de no ser la primera bola
            {
                //collision.gameObject.GetComponent<Ball>().GoTo(_posStacker, 0);
                Destroy(collision.gameObject);
                LevelManager.levelManagerInstance.stacker.GetComponent<BallStacker>().AddBall(); //Añades una bola al stacker

               
            }
        }
    }
}
