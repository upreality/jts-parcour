using System;
using System.Collections.Generic;
using ABTests.domain;
using ABTests.domain.model;
using ABTests.domain.repositories;
using UniRx;
using UnityEngine;

namespace ABTests.data
{
    [CreateAssetMenu(menuName = "ABTestsSetup/ABTestsRepository", fileName = "ABTestsRepository")]
    public class ABTestsSoRepository : ScriptableObject, IABTestsRepository
    {
        [SerializeField] private List<ABTest> tests = new();

        public List<ABTest> GetTests() => tests;
    }
}