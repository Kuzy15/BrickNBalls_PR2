using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

    public Text rubyText;
    public GameObject adsManager;

	// Use this for initialization
	void Start () {
        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString();
	}

    public void Update()
    {
        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString();
    }

    public void OnClickBackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickAdRubyReward()
    {
        adsManager.GetComponent<AdsManager>().ShowRewardedAd();
    }

    public void OnClickBuyRayPowerUp()
    {
        GameManager.gameManagerInstace.RemoveRuby(25); ;
        GameManager.gameManagerInstace.AddNRayPowerUp(1);
    }

   
}
