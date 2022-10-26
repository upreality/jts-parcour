using System;
using System.Collections.Generic;
using System.Linq;
using ABTests.domain.repositories;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace ABTests
{
    public class ABTestProperties : MonoBehaviour, IABTestPropertyStateRepository
    {
        [SerializeField] private List<ABTestProperty> properties = new();

        public void SetPropertyEnabled(string propName)
        {
            if (FindProperty(propName, out var property))
            {
                ApplyProperty(property);
                return;
            }

            Debug.LogError($"AB test property \'{property.propName}\'not found");
        }

        public IObservable<bool> GetPropertyStateFlow(string propName) => !FindProperty(propName, out var property)
            ? Observable.Return(false)
            : property.enabledState;

        private bool FindProperty(string propName, out ABTestProperty property)
        {
            property = null;
            var hasProp = properties.Any(definedProp => definedProp.propName == propName);
            if (!hasProp) return false;
            property = properties.First(definedProp => definedProp.propName == propName);
            return true;
        }

        private static void ApplyProperty(ABTestProperty property)
        {
            Debug.Log("ApplyProperty: " + property.propName);
            property.enabledState.Value = true;
            property.onApply?.Invoke();
            GoogleAnalyticsSDK.SendStringEvent("apply_ab_test_property", "ab_test_property_setup", property.propName);
        }

        [Serializable]
        private class ABTestProperty
        {
            public string propName;
            [NonSerialized] public ReactiveProperty<bool> enabledState = new(false);
            public UnityEvent onApply;
        }
    }
}