using ABTests.data;
using ABTests.domain;
using ABTests.domain.repositories;
using UnityEngine;
using Zenject;

namespace ABTests._di
{
    public class ABTestsInstaller: MonoInstaller
    {
        [SerializeField] private ABTestProperties abTestProperties;
        [SerializeField] private ABTestsSoRepository abTestsSoRepository;
        public override void InstallBindings()
        {
            Container.Bind<IABTestPropertyStateRepository>().FromInstance(abTestProperties).AsSingle();
            Container.Bind<IABTestsRepository>().FromInstance(abTestsSoRepository).AsSingle();
            Container.Bind<IUserABTestRepository>().To<UserABTestLocalStorageRepository>().AsSingle();
        }
    }
}