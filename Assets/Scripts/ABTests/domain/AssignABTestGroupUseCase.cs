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
            if (GetGroup(test, out var selectedGroup) == GroupType.Custom)
            {
                SetGroup(test.testName, selectedGroup.groupName);
                return;
            }

            SetControlGroup(test.testName);
        }

        private static GroupType GetGroup(ABTest test, out ABTestGroup group)
        {
            group = null;
            var groups = test.groups;

            var controlGroupPercentage = test.controlGroupPercentage;
            var controlPointer = Random.Range(0, 100) + 1;
            if (controlGroupPercentage == 100 || controlPointer < controlGroupPercentage || groups.Count == 0)
                return GroupType.Control;

            var selectedGroupIndex = groups
                .Select(testGroup => Mathf.Max(0f, testGroup.weight))
                .ToList()
                .GetRandomWeightedIndex();
            
            if (selectedGroupIndex < 0 || selectedGroupIndex >= groups.Count) 
                return GroupType.Control;

            group = groups[selectedGroupIndex];
            return GroupType.Custom;
        }

        private void SetControlGroup(string testName)
        {
            userABTestRepository.SetABTestControlGroup(testName);
            Debug.Log($"Assigned test {testName} with Control group");
        }

        private void SetGroup(string testName, string groupName)
        {
            userABTestRepository.SetABTestGroup(testName, groupName);
            Debug.Log($"Assigned test {testName} with group {groupName}");
        }

        private enum GroupType
        {
            Control,
            Custom
        }
    }
}