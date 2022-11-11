using System;
using UniRx;
using UnityEngine;

namespace Utils
{
    
    [RequireComponent(typeof(Animator))]
    public class AnimatorVerticalVelocity : MonoBehaviour
    {
        
        [SerializeField] private Rigidbody rb;
        [SerializeField] private string paramName = "VerticalVelocity";
        [SerializeField] private float refreshDelayMs = 200;
        
        private Animator animator;
        
        private IDisposable refreshDisposable;
    
        private void Awake() => animator = GetComponent<Animator>();

        private void OnEnable()
        {
            var repeatSpan = TimeSpan.FromMilliseconds(refreshDelayMs);
            refreshDisposable = Observable
                .Interval(repeatSpan)
                .Subscribe(_ => Refresh());
        }

        private void OnDisable() => refreshDisposable.Dispose();

        private void Refresh()
        {
            var value = rb.velocity.y;
            animator.SetFloat(paramName, value);
        }
    }
}
