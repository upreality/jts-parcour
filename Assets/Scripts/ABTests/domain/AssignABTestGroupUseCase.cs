using System;
using System.Linq;
using ABTests.domain.model;
using ABTests.domain.repositories;
using UnityEngine;
using Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace ABTests.domain
{
    public class AssignABTestGroupUseCase
    {
        [Inject] private IUserABTestRepository userABTestRepository;

        public void Assign(ABTest test)
        {
            var controlGroupPercent = Random.Range(0, 100) + 1;
            if (test.controlGroupPercentage == 100 || controlGroupPercent > test.controlGroupPercentage)
            {
                userABTestRepository.SetABTestControlGroup(test.testName);
                return;
            }

            var groups = test.groups;
            
            // var summaryWeight = groups.Sum(group => group.weight);
            // if (summaryWeight == 0)
            // {
            //     userABTestRepository.SetABTestControlGroup(test.testName);
            //     return;
            // }

            var groupType = GetGroup(test, out var selectedGroup);
            switch (groupType)
            {
                case GroupType.Control:
                    userABTestRepository.SetABTestControlGroup(test.testName);
                    break;
                case GroupType.Custom:
                    userABTestRepository.SetABTestGroup(test.testName, selectedGroup.groupName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static GroupType GetGroup(ABTest test, out ABTestGroup group)
        {
            group = null;
            var groups = test.groups;
            
            var controlGroupPercentage = test.controlGroupPercentage;
            var controlPointer = Random.Range(0, 100) + 1;
            if (controlGroupPercentage == 100 || controlPointer > controlGroupPercentage || groups.Count == 0) 
                return GroupType.Control;

            // var selectedGroupIndex = groups
            //     .Select(testGroup => Mathf.Max(0f, testGroup.weight))
            //     .ToList()
            //     .GetRandomWeightedIndex();
            var selectedGroupIndex = Random.Range(0, groups.Count);
            
            if (selectedGroupIndex < 0 || selectedGroupIndex >= groups.Count) return GroupType.Control;

            group = groups[selectedGroupIndex];
            return GroupType.Custom;
        }

        private enum GroupType
        {
            Control,
            Custom
        }
    }
}