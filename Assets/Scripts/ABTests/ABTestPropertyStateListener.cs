using ABTests.domain;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace ABTests
{
    public class ABTestPropertyStateListener : MonoBehaviour
    {
        [SerializeField] private string propName;
        [SerializeField] private UnityEvent onEnableProperty;
        [Inject] private IABTestPropertyStateRepository propertyStateRepository;

        private void Start()
        {
            propertyStateRepository
                .GetPropertyStateFlow(propName)
                .DistinctUntilChanged()
                .Subscribe(ApplyPropertyState)
                .AddTo(this);
        }

        private void ApplyPropertyState(bool propertyEnabled)
        {
            if (!propertyEnabled || onEnableProperty == null) return;
            onEnableProperty.Invoke();
        }
    }
}