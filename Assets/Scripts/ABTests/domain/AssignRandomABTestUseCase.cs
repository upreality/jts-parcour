using System.Linq;
using ABTests.domain.repositories;
using ModestTree;
using UnityEngine;
using Utils;
using Zenject;

namespace ABTests.domain
{
    public class AssignRandomABTestUseCase
    {
        [Inject] private AssignABTestGroupUseCase assignABTestGroupUseCase;
        [Inject] private IABTestsRepository aBTestsRepository;

        public void Assign()
        {
            var tests = aBTestsRepository.GetTests();
            var selectedTestIndex = tests
                .Select(test => Mathf.Max(0f, test.weight))
                .ToList()
                .GetRandomWeightedIndex();

            if (selectedTestIndex < 0 || selectedTestIndex >= tests.Count)
                return;

            var selectedTest = tests[selectedTestIndex];
            assignABTestGroupUseCase.Assign(selectedTest);
        }
    }
}