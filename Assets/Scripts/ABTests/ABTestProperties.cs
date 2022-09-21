using System;
using System.Collections.Generic;
using System.Linq;
using ABTests.domain;
using Plugins.FileIO;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace ABTests
{
    public class ABTestProperties : MonoBehaviour, IABTestPropertyStateRepository
    {
        [SerializeField] private List<ABTestProperty> properties = new();

        private void Start()
        {
            Debug.Log("Checking session storage ab test props");
            properties.Where(IsSupported).ToList().ForEach(ApplyProperty);
        }

        public void SetPropertyEnabled(string propName)
        {
            if (!FindProperty(propName, out var property)) return;
            ApplyProperty(property);
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

        private static bool IsSupported(ABTestProperty property) => LocalStorageIO.HasSessionKey(property.propName);

        private static void ApplyProperty(ABTestProperty property)
        {
            Debug.Log("ApplyProperty: " + property.propName);
            property.enabledState.Value = true;
            property.onApply?.Invoke();
        }

        [Serializable]
        private class ABTestProperty
        {
            public string propName;
            public ReactiveProperty<bool> enabledState = new(false);
            public UnityEvent onApply;
        }
    }
}