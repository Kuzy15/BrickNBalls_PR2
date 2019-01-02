using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStacker : MonoBehaviour {

    private int _ballStacked;
	// Use this for initialization
	void Start () {
        _ballStacked = 0;
	}

    public void SetPos(Vector2 pos) //Da la posicion al stacker
    {
        transform.position = new Vector3(pos.x, pos.y, 0);
    }


    public void Show(bool s) //Activa y desactiva tanto el script como el render
    {
        enabled = s;
        GetComponent<SpriteRenderer>().enabled = s;
        GetComponentInChildren<MeshRenderer>().enabled = s;
    }


    public void AddBall() //Añade una bola
    {
        _ballStacked++;
        GetComponentInChildren<TextMesh>().text = "x" + (_ballStacked).ToString();

        if (_ballStacked == LevelManager.levelManagerInstance.GetNBalls()) //Conmpruebas si ya han colisionado todas
        {
            LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().MoveTo(transform.position); //Le das al spawn su primera posicion
            LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().Show(true); //Muestras el spawner
            LevelManager.levelManagerInstance.spawner.GetComponent<BallSpawn>().SetNBalls(_ballStacked); //Le das al spawner el numero de boals stackeadas
            LevelManager.levelManagerInstance.ResetSameRoundPoints();
            Clean(); //Reinicias el numero de bolas stackeadas
            Show(false); //Dejas de mostrar el stacker
            LevelManager.levelManagerInstance.deathZone.GetComponent<DeathZone>().SetFisrtBall(true);
            if (LevelManager.levelManagerInstance.gameField.GetComponent<RayHorizontalBrick>())
            {
                LevelManager.levelManagerInstance.gameField.GetComponent<RayHorizontalBrick>().DestroyBrick();
            }
            if (!LevelManager.levelManagerInstance.GetEndRound())
            {
                LevelManager.levelManagerInstance.SetSpawn(true);
            }
            LevelManager.levelManagerInstance.MoveBlocks();
        }
    }


    public int GetBallStacked() //Devuelve las bolas stackeadas
    {
        return _ballStacked;
    }


    public void Clean() //Reinicia elcontador
    {
        _ballStacked = 0;
    }
}
