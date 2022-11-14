using System;
using System.Collections.Generic;
using Doozy.Engine;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PlayerTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent triggerEvent;
        [SerializeField] private UnityEvent exitTriggerEvent;
        [SerializeField] private string gameEvent;
        [SerializeField] private string exitGameEvent;
        [SerializeField] private string triggerTag = "Player";

        [SerializeField] private bool triggerOnce = false;
        private bool triggeredEnter;
        private bool triggeredExit;

        private bool activeState;

        private readonly Queue<NextEvent> triggerEvents = new();

        private void Update()
        {
            while (triggerEvents.Count > 0)
            {
                var nextEvent = triggerEvents.Dequeue();
                if (nextEvent == NextEvent.Enter)
                {
                    triggerEvent?.Invoke();
                    if(!string.IsNullOrEmpty(gameEvent)) GameEventMessage.SendEvent(gameEvent, gameObject);
                }
                else
                {
                    ExitTrigger();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag(triggerTag) || (triggerOnce && triggeredEnter))
                return;

            triggerEvents.Enqueue(NextEvent.Enter);
            activeState = true;
            triggeredEnter = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.CompareTag(triggerTag) || (triggerOnce && triggeredExit))
                return;

            triggerEvents.Enqueue(NextEvent.Exit);
            activeState = false;
            triggeredExit = true;
        }

        private void ExitTrigger()
        {
            exitTriggerEvent?.Invoke();
            if (!string.IsNullOrEmpty(exitGameEvent)) GameEventMessage.SendEvent(exitGameEvent, gameObject);
        }

        private void OnDestroy()
        {
            if(activeState)
                ExitTrigger();
        }

        private enum NextEvent
        {
            Enter,
            Exit
        }
    }
}