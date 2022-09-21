using System;

namespace ABTests.domain
{
    public interface IABTestPropertyStateRepository
    {
        IObservable<bool> GetPropertyStateFlow(string propName);
        void SetPropertyEnabled(string propName);
    }
}