using System;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using FireBase;
using TMPro;
using UI.HUD;

namespace Ad
{
    public class AdSettings : MonoBehaviour, IRewardedVideoAdListener, IBannerAdListener
    {
        [SerializeField] private SelectLevelMenu _selectLevelMenu;

        private const string AppKey = "5ae8e16f1eec518bbcd69bb3ce865993592a413583eab019";
        private const string AdPlacement = "Light";

        private void Start()
        {
            const int adTypes = Appodeal.REWARDED_VIDEO | Appodeal.BANNER_BOTTOM;
            Appodeal.setRewardedVideoCallbacks(this);
            Appodeal.setBannerCallbacks(this);
            Appodeal.initialize(AppKey, adTypes, true);
        }
        
        public void ShowBanner()
        {
            Appodeal.show(Appodeal.BANNER_BOTTOM);
        }

        public void HideBanner()
        {
            Appodeal.hide(Appodeal.BANNER_BOTTOM);
        }

        public bool CanShowReward()
        {
            return Appodeal.canShow(Appodeal.REWARDED_VIDEO) && !Appodeal.isPrecache(Appodeal.REWARDED_VIDEO);
            return true;
        }

        public void ShowRewardVideo()
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            FireBaseInit.Instance.Reward();
            _selectLevelMenu.UnlockNextLevel();
        }

        public void onRewardedVideoLoaded(bool precache)
        {

        }

        public void onRewardedVideoFailedToLoad()
        {

        }

        public void onRewardedVideoShowFailed()
        {

        }

        public void onRewardedVideoShown()
        {

        }

        public void onRewardedVideoFinished(double amount, string name)
        {

        }

        public void onRewardedVideoClosed(bool finished)
        {
            if (finished)
                _selectLevelMenu.UnlockNextLevel();
        }

        public void onRewardedVideoExpired()
        {

        }

        public void onRewardedVideoClicked()
        {

        }

        public void onBannerLoaded(int height, bool isPrecache)
        {

        }

        public void onBannerFailedToLoad()
        {

        }

        public void onBannerShown()
        {

        }

        public void onBannerClicked()
        {

        }

        public void onBannerExpired()
        {

        }
    }
}
