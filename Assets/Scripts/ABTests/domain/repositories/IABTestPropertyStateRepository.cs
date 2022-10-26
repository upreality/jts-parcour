using System;

namespace ABTests.domain.repositories
{
    public interface IABTestPropertyStateRepository
    {
        IObservable<bool> GetPropertyStateFlow(string propName);
        void SetPropertyEnabled(string propName);
    }
}