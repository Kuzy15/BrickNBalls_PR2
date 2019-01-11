using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManagerGame : MonoBehaviour
{

    public Text endScoreText;
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

    //Show a banner
    public void ShowBanner()
    {
        StartCoroutine(ShowBannerCoroutine());
    }

    //Hide and destroy the banner
    public void HideBanner()
    {
        Advertisement.Banner.Hide(true);
    }

    IEnumerator ShowBannerCoroutine()
    {
        while (!Advertisement.IsReady("banner"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show("banner");
    }

    //Show and ad that you can skip
    public void ShowAd()
    {
        StartCoroutine(ShowAdCoroutine());
    }

    IEnumerator ShowAdCoroutine()
    {
        while (!Advertisement.IsReady("video"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show("video", options);
    }

    //Show and ad that you can´t skip
    public void ShowNoSkipAd()
    {
        StartCoroutine(ShowNoSkipAdCoroutine());
    }

    IEnumerator ShowNoSkipAdCoroutine()
    {
        while (!Advertisement.IsReady("rewardedVideo"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show("rewardedVideo", options);
    }

    //If you finish the ad you get some rubies, else nothing
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Debug.Log("The ad was successfully shown.");
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
                    endScoreText.text = "Point " + points.ToString();
                    SaveAndLoad.Save();
                }
                else
                {
                    _first = false;
                }
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
