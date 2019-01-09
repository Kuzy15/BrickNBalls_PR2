using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManagerShop : MonoBehaviour {

    public Text rubyText;

    //Show and ad that you can´t skip
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }


    //If you finish the ad you get some rubies, else nothing
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Debug.Log("The ad was successfully shown.");
                GameManager.gameManagerInstace.AddRuby(30);
                rubyText.text = GameManager.gameManagerInstace.GetRuby().ToString(); //Show the amount of rubies
                SaveAndLoad.Save();
                break;
            case ShowResult.Skipped:
                //Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                //Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
