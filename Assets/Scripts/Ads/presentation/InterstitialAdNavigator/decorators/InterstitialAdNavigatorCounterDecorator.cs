using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ads.presentation.InterstitialAdNavigator.decorators
{
    public class InterstitialAdNavigatorCounterDecorator : MonoBehaviour, IInterstitalAdNavigator
    {
        [Inject] private IInterstitalAdNavigator adNavigator;

        [SerializeField]
        private int showInterval = 1;
        private int invokeTimes;

        public IObservable<ShowInterstitialResult> ShowAd()
        {
            if (showInterval < 1)
                return Observable.Return(new ShowInterstitialResult(false, "zero or less show interval"));

            invokeTimes++;
            if (invokeTimes < showInterval)
                return Observable.Return(new ShowInterstitialResult(false, "period not reached"));

            invokeTimes = 0;
            
            Debug.LogWarning($"Shown ad");
            return adNavigator.ShowAd();
        }

        public void SetShowInterval(int interval) => showInterval = interval;

        public interface IInterstitialShowIntervalProvider
        {
            public IObservable<int> GetShowInterval();
        }
    }
    
    public class NoIntervalInterstitialShowIntervalProvider: InterstitialAdNavigatorCounterDecorator.IInterstitialShowIntervalProvider
    {
        public IObservable<int> GetShowInterval() => Observable.Return(1);
    }
}