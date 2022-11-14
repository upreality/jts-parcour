using System;
using Doozy.Engine;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class SimpleTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent triggerEvent;
        [SerializeField] private UnityEvent exitTriggerEvent;

        private NextEvent next = NextEvent.None;

        private void Update()
        {
            if(next == NextEvent.None) return;
            
            if(next == NextEvent.Enter) triggerEvent?.Invoke();
            if(next == NextEvent.Exit) triggerEvent?.Invoke();

            next = NextEvent.None;
        }


        private void OnTriggerEnter(Collider other) => next = NextEvent.Enter;

        private void OnTriggerExit(Collider other) => next = NextEvent.Exit;

        private enum NextEvent
        {
            Enter,
            Exit,
            None
        }
    }
}