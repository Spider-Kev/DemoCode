using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using UnityEngine;

public class AdvertisingManager : MonoBehaviour
{
    #region UNITY_METHODS
    private void Awake()
    {
        
    }

    private void Start()
    {
        ShowBanner(true);
    }

    private void OnEnable()
    {
        EventManager.StartListening("GameOver", OnGameOver);
        Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameOver", OnGameOver);
        Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
    }
    #endregion

    #region PUBLIC_METHODS
    public void ShowBanner(bool show)
    {
        if (show)
            Advertising.ShowBannerAd(BannerAdPosition.BottomRight);
        else
            Advertising.HideBannerAd();
    }

    public void ShowInterstitial()
    {
        if (Advertising.IsInterstitialAdReady())
            Advertising.LoadInterstitialAd();
        else
            Debug.Log("Is not ready");
    }

    public void ShowVideo()
    {
        if (Advertising.IsInterstitialAdReady())
            Advertising.ShowRewardedAd();

        // TODO Handle error if ad is not ready
        //else
        //    EventManager.TriggerEvent("RewardedVideoFail",null);
    }
    #endregion

    #region LISTENER_METHODS
    private void OnGameOver(object param)
    {
        ShowBanner(false);
        ShowInterstitial();
    }

    private void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
    {
        // TODO: Give extra life
    }


    #endregion
}
