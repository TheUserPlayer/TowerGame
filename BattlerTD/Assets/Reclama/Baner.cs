using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class Baner : MonoBehaviour
{
    private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";

    BannerView _bannerView;


    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            CreateBannerView();
            LoadAd();
        });
    }


    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        } 
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom); 
    }

    public void LoadAd()
    { 
        if (_bannerView == null)
        {
            CreateBannerView();
        } 
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
         
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

   

    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    } 
}
