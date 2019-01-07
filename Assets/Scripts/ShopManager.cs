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
        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString(); //Show the amount of rubies
	}

    //Change Scene if click
    public void OnClickBackMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Show a Rewarded ad that you can´t skip, add 30 rubies, show rubies´ amount
    //and save the game
    public void OnClickAdRubyReward()
    {
        adsManager.GetComponent<AdsManagerShop>().ShowRewardedAd();
        GameManager.gameManagerInstace.AddRuby(30);
        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString();
        GameManager.Save();
    }

    //Subtract 25 rubies and add 1 power up, show rubies´ amount and save the game
    public void OnClickBuyRayPowerUp()
    {
        GameManager.gameManagerInstace.RemoveRuby(25); ;
        GameManager.gameManagerInstace.AddNRayPowerUp(1);
        rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString();
        GameManager.Save();
    }   
}
