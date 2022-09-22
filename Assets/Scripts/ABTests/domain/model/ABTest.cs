using System;
using System.Collections.Generic;
using UnityEngine;

namespace ABTests.domain.model
{
    [Serializable]
    public class ABTest
    {
        public string testName;
        [Range(0f, 1f)] public float weight = 1f;
        [Range(0, 100)] public int controlGroupPercentage = 50;
        public List<ABTestGroup> groups = new(1);
    }
}