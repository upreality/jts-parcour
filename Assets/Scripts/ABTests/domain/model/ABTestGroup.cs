using System;

namespace ABTests.domain.model
{
    [Serializable]
    public class ABTestGroup
    {
        public string groupName;
        public string property;
        // [Range(0f, 1f)] public float weight = 1f;
    }
}