using UnityEngine;
using Zenject;

namespace ABTests
{
    public class ABTestsInstaller: MonoInstaller
    {
        [SerializeField] private ABTestProperties abTestProperties;
        public override void InstallBindings()
        {
            Container.Bind<IABTestPropertyStateRepository>().FromInstance(abTestProperties).AsSingle();
        }
    }
}