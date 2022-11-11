using System;
using Doozy.Engine;
using UniRx;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorRandomize : MonoBehaviour
    {
        [SerializeField] private string paramName = "Random";
        [SerializeField] private int rangeMin = 1;
        [SerializeField] private int rangeMax = 3;
        [SerializeField] private float refreshDelay = 1f;
        
        private Animator animator;

        private IDisposable randomizeDisposable;

        private void Awake() => animator = GetComponent<Animator>();

        private void OnEnable()
        {
            var repeatSpan = TimeSpan.FromSeconds(refreshDelay);
            randomizeDisposable = Observable
                .Interval(repeatSpan)
                .Subscribe(_ => Randomize());
        }

        private void OnDisable() => randomizeDisposable.Dispose();

        private void Randomize()
        {
            if (rangeMax < rangeMin) (rangeMax, rangeMin) = (rangeMin, rangeMax);
            var value = UnityEngine.Random.Range(rangeMin, rangeMax + 1);
            animator.SetInteger(paramName, value);
        }
    }
}