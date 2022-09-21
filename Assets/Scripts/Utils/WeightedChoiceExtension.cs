using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class WeightedChoiceExtension
    {
        public static int GetRandomWeightedIndex(this List<float> weights)
        {
            if(weights == null || weights.Count == 0) return -1;
 
            var w = 0f;
            var total = 0f;
            int i;
            for(i = 0; i < weights.Count; i++)
            {
                w = weights[i];
                if (float.IsPositiveInfinity(w)) return i;
                else if(w >= 0f && !float.IsNaN(w)) total += weights[i];
            }
 
            var r = Random.value;
            var s = 0f;
 
            for(i = 0; i < weights.Count; i++)
            {
                w = weights[i];
                if (float.IsNaN(w) || w <= 0f) continue;
     
                s += w / total;
                if (s >= r) return i;
            }
 
            return -1;
        }
    }
}