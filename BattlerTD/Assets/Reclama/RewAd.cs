using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewAd : MonoBehaviour
{
    private string RewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd rewardedAd;
    public GameObject rewardedAdObject; 

    private void OnEnable()
    {
        rewardedAd = new RewardedAd(RewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        int coins = PlayerPrefs.GetInt("coins");
        coins += 150;
        PlayerPrefs.SetInt("coins", coins);
    }

    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            MoveAdObjectToLastSibling();
        }
    }

    private void MoveAdObjectToLastSibling()
    {
        if (rewardedAdObject != null)
        {
            rewardedAdObject.transform.SetAsLastSibling();
        }
    }
}