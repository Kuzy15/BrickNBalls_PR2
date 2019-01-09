using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerUp : MonoBehaviour {

    protected Text _powerUpText;
    protected LevelManager _levelManager;
    protected int _amount;


    public void Init(LevelManager lm)
    {
        _levelManager = lm;
    }
}
