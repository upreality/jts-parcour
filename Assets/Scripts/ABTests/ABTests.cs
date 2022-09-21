using System.Runtime.CompilerServices;
using ABTests.domain;
using ABTests.domain.repositories;
using UnityEngine;
using Zenject;
using static ABTests.domain.repositories.IUserABTestRepository;

namespace ABTests
{
    public class ABTests : MonoBehaviour
    {
        [Inject] private IABTestPropertyStateRepository propertyStateRepository;
        [Inject] private AssignRandomABTestUseCase assignRandomABTestUseCase;
        [Inject] private GetAssignableABTestPropertyUseCase getAssignableABTestPropertyUseCase;
        [Inject] private IUserABTestRepository userABTestRepository;
        [Inject] private TryAssignABTestUseCase tryAssignABTestUseCase;

        private void Start()
        {
            tryAssignABTestUseCase.Try();
            if(!getAssignableABTestPropertyUseCase.GetAssignableProperty(out var property)) return;
            propertyStateRepository.SetPropertyEnabled(property);
        }

        private void TryAssignABTestGroup()
        {
            if (userABTestRepository.GetABTest(out var currentTest)) return;
            var currentGroupState = userABTestRepository.GetABTestGroup(currentTest, out _);
            if(currentGroupState != UserABTestGroupState.NotAssigned) return;
            assignRandomABTestUseCase.Assign();
        }
    }
}