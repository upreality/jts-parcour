using ABTests.domain;
using ABTests.domain.repositories;
using UnityEngine;
using Zenject;

namespace ABTests
{
    public class ABTestsSetup : MonoBehaviour
    {
        [Inject] private IABTestPropertyStateRepository propertyStateRepository;
        [Inject] private GetAssignableABTestPropertyUseCase getAssignableABTestPropertyUseCase;
        [Inject] private TryAssignABTestUseCase tryAssignABTestUseCase;

        private void Start()
        {
            tryAssignABTestUseCase.Try();
            if (!getAssignableABTestPropertyUseCase.GetAssignableProperty(out var property)) return;
            propertyStateRepository.SetPropertyEnabled(property);
        }
    }
}