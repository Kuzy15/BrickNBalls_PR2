using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public Text text; //info text

    private LevelManager _levelManager;
    private bool _first;

    private void Awake()
    {
        if (!Advertisement.isInitialized)
        {
            Advertisement.Initialize("2988623", true);
        }
    }

    public void Init(LevelManager lm)
    {
        _levelManager = lm;
        _first = true;
    }

    //Show and ad that you can skip
    public void GameShowAd()
    {
        StartCoroutine(GameShowAdCoroutine());       
    }

    IEnumerator GameShowAdCoroutine()
    {
        while (!Advertisement.IsReady("video"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        ShowOptions options = new ShowOptions { resultCallback = GameHandleShowResult };
        Advertisement.Show("video", options);
        
    }

    //Show and ad that you can´t skip
    public void ShowNoSkipAd()
    {
        StartCoroutine(GameShowNoSkipAdCoroutine());
    }

    IEnumerator GameShowNoSkipAdCoroutine()
    {
        while (!Advertisement.IsReady("rewardedVideo"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        ShowOptions options = new ShowOptions { resultCallback = GameHandleShowResult };
        Advertisement.Show("rewardedVideo", options);      
    }

  

    //If you finish the ad you get some points, else nothing
    private void GameHandleShowResult(ShowResult result)
    {
        
        switch (result)
        {
            case ShowResult.Finished:
#if UNITY_EDITOR
                Debug.Log("The ad was successfully shown.");

#endif
               
                if (!_first)
                {
                    int points = _levelManager.GetPoints() + 250;
                    _levelManager.SetPoints(points);
                    int level = GameManager.gameManagerInstace.GetCurrentLevel();
                    if (points > _levelManager.gameField.GetTotalBlocks() * 30 / 4)
                    {
                        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[0] = true;
                    }
                    if (points > _levelManager.gameField.GetTotalBlocks() * 30 / 2)
                    {
                        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[1] = true;
                    }
                    if (points > _levelManager.gameField.GetTotalBlocks() * 30)
                    {
                        GameManager.gameManagerInstace.GetLevels()[level - 1]._stars[2] = true;
                    }
                    text.text = "Points " + points.ToString();
                    SaveAndLoad.Save();
                }
                else
                {
                    _first = false;
                }
                break;
            case ShowResult.Skipped:
                _first = false;
#if UNITY_EDITOR
                Debug.Log("The ad was skipped before reaching the end.");
#endif
                break;
            case ShowResult.Failed:
#if UNITY_EDITOR
                Debug.LogError("The ad failed to be shown.");
#endif                       
                
                break;
                
        }
    }


    //Show and ad that you can´t skip
    public void ShopShowRewardedAd()
    {
        StartCoroutine(ShopShowRewardedAdCoroutine());
    }

    IEnumerator ShopShowRewardedAdCoroutine()
    {
        while (!Advertisement.IsReady("rewardedVideo"))
        {
            yield return new WaitForSeconds(0.5f);

        }
        ShowOptions options = new ShowOptions { resultCallback = ShopHandleShowResult };
        Advertisement.Show("rewardedVideo", options);
    }


    //If you finish the ad you get some rubies, else nothing
    private void ShopHandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Debug.Log("The ad was successfully shown.");
                GameManager.gameManagerInstace.AddRuby(30);
                text.text = GameManager.gameManagerInstace.GetRuby().ToString(); //Show the amount of rubies
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
