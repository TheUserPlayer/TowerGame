using UnityEngine;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine.UI;
using System;

public class reclama : MonoBehaviour
{ 

    public string appId = "ca-app-pub-1385093244148841~5602672977";// "ca-app-pub-3940256099942544~3347511713";


    string bannerId;
    string interId;
    string rewardedId;
    string nativeId;

    BannerView bannerView;
    InterstitialAd interstitialAd;
    RewardedAd rewardedAd;
    NativeAd nativeAd;

    private void Awake()
    {  
        LoadInterstitialAd();
    }
    private void Start()
    { 
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => {

            print("Ads Initialised !!");
            ShowInterstitialAd();
        }
        );
#if UNITY_ANDROID
        string bannerId = "ca-app-pub-1385093244148841/2952458907";
        string interId = "ca-app-pub-3940256099942544/1033173712";
        string rewardedId = "ca-app-pub-3940256099942544/5224354917";
        string nativeId = "ca-app-pub-3940256099942544/2247696110";

#elif UNITY_IPHONE
    string bannerId = "ca-app-pub-3940256099942544/2934735716";
    string interId = "ca-app-pub-3940256099942544/4411468910";
    string rewardedId = "ca-app-pub-3940256099942544/1712485313";
    string nativeId = "ca-app-pub-3940256099942544/3986624511";

#endif
    }


     

    #region Interstitial

    public void LoadInterstitialAd()
    {

        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(interId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                print("Interstitial ad failed to load" + error);
                return;
            }

            print("Interstitial ad loaded !!" + ad.GetResponseInfo());

            interstitialAd = ad;
            InterstitialEvent(interstitialAd);
        });

    }
    public void ShowInterstitialAd()
    {

        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            print("Intersititial ad not ready!!");
        }
    }
    public void InterstitialEvent(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Interstitial ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #endregion
       
    #region Native
     

    public void RequestNativeAd()
    {

        AdLoader adLoader = new AdLoader.Builder(nativeId).ForNativeAd().Build();

        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;

        adLoader.LoadAd(new AdRequest.Builder().Build());
    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs e)
    {
        print("Native ad loaded");
        this.nativeAd = e.nativeAd;

        Texture2D iconTexture = this.nativeAd.GetIconTexture();
        Sprite sprite = Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height), Vector2.one * .5f);
         

    }

    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        print("Native ad failed to load" + e.ToString());

    }
    #endregion


    #region extra 

    void GrantCoins(int coins)
    {
        int crrCoins = PlayerPrefs.GetInt("totalCoins");
        crrCoins += coins;
        PlayerPrefs.SetInt("totalCoins", crrCoins);
         
    }
     

    #endregion

}