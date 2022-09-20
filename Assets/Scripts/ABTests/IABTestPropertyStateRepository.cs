using System;

namespace ABTests
{
    public interface IABTestPropertyStateRepository
    {
        IObservable<bool> GetPropertyStateFlow(string propName);
    }
}