using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ads.presentation.InterstitialAdNavigator.decorators
{
    public class InterstitialAdNavigatorLockLookDecorator : MonoBehaviour, IInterstitalAdNavigator
    {
        [Inject] private FirstPersonLook look;
        [Inject] private IInterstitalAdNavigator Target;

        private bool uiShownState = false;

        public void SetUIShownState(bool shown) => uiShownState = shown;

        public IObservable<ShowInterstitialResult> ShowAd()
        {
            SetLockedState(true);
            return Target
                .ShowAd()
                .Do(_ => SetLockedState(false))
                .DoOnError(_ => SetLockedState(false));
        }

        private void SetLockedState(bool locked)
        {
            Debug.Log("SetLockedState: " + locked);
            if (locked)
                look.SetEnabledState(false);
            
            if (!locked && !uiShownState) 
                look.SetEnabledState(true);

            Time.timeScale = locked ? 0f : 1f;
        }
    }
}