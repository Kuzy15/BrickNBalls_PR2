using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManagerGame : MonoBehaviour
{

    public Text endScoreText;
    private LevelManager _levelManager;

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
    }

    public void ShowBanner()
    {
        StartCoroutine(ShowBannerCoroutine());
    }

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

    //If you finish the ad you get some rubies, else nothing
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Debug.Log("The ad was successfully shown.");
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
                endScoreText.text = "Point in this level " + points.ToString();
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
