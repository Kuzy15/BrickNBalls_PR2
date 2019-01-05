using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStacker : MonoBehaviour {

    public TextMesh _label;
    private uint _ballStacked;

	
	public void Init () {
        ResetNumBalls();
    }

    public void SetPos(Vector2 pos) //Da la posicion al stacker
    {
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void Show(bool s) //Activa y desactiva tanto el script como el render
    {
        GetComponent<SpriteRenderer>().enabled = s;
        _label.GetComponentInChildren<MeshRenderer>().enabled = s;
    }

    public void AddBall() //Añade una bola
    {
        _ballStacked++;
       _label.text = "x" + (_ballStacked).ToString();

       /*if (_ballStacked == LevelManager.levelManagerInstance.GetNBalls()) //Conmpruebas si ya han colisionado todas
        {
            LevelManager.levelManagerInstance._ballSpawner.MoveTo(transform.position); //Le das al spawn su primera posicion
            LevelManager.levelManagerInstance._ballSpawner.Show(true); //Muestras el spawner
            LevelManager.levelManagerInstance._ballSpawner.SetNBalls(_ballStacked); //Le das al spawner el numero de boals stackeadas
            LevelManager.levelManagerInstance.ResetSameRoundPoints();
            Reset(); //Reinicias el numero de bolas stackeadas
            Show(false); //Dejas de mostrar el stacker
            LevelManager.levelManagerInstance._deathZone.SetFisrtBall(true);
            if (!LevelManager.levelManagerInstance.GetEndRound())
            {
                LevelManager.levelManagerInstance.SetSpawn(true);
            }
            LevelManager.levelManagerInstance.MoveBlocks();
        }*/
    }

   /* public void AddBallStacked() // HACER: ESTO TENDRÍA QUE SOBRAR Y EL ADDBALL DE ARRIBA SOLO HACER LAS DOS PRIMERAS LINEAS
    {
        _ballStacked++;
    }*/

    public uint GetBallStacked() //Devuelve las bolas stackeadas
    {
        return _ballStacked;
    }

    public void ResetNumBalls() //Reinicia elcontador
    {
        _ballStacked = 0;
    }
}
