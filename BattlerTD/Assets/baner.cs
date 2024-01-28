using UnityEngine;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine.UI;
using System;

public class baner : MonoBehaviour
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
        LoadBannerAd(); 
    }
    private void Start()
    { 
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => {

            print("Ads Initialised !!"); 
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


    #region Banner

    public void LoadBannerAd()
    {
        //create a banner
        CreateBannerView();

        //listen to banner events
        ListenToBannerEvents();

        //load the banner
        if (bannerView == null)
        {
            CreateBannerView();
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        print("Loading banner Ad !!");
        bannerView.LoadAd(adRequest);//show the banner on the screen
    }
    void CreateBannerView()
    {

        if (bannerView != null)
        {
            DestroyBannerAd();
        }
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);
    }
    void ListenToBannerEvents()
    {
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Banner view paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }
    public void DestroyBannerAd()
    {

        if (bannerView != null)
        {
            print("Destroying banner Ad");
            bannerView.Destroy();
            bannerView = null;
        }
    }
    #endregion

  

}