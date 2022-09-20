using System.Collections;
using Doozy.Engine;
using UnityEngine;

namespace Gameplay
{
    public class FallController : MonoBehaviour
    {
        private FirstPersonLook look;
        [SerializeField] private float turnUpDuration = 1f;
        [SerializeField] private string fallenEvent = "Die";

        private void Awake() => look = FindObjectOfType<FirstPersonLook>();

        private void StopFall()
        {
            look.enabled = true;
            GameEventMessage.SendEvent(fallenEvent);
        }

        public void AbortFall()
        {
            StopAllCoroutines();
            StopFall();
        }

        public void StartFall() => StartCoroutine(Fall());

        private IEnumerator Fall()
        {
            look.enabled = false;
            var lookTransform = look.transform;
            var initialRotation = lookTransform.localRotation;
            var lookRotation = Quaternion.LookRotation(Vector3.up);

            var timer = turnUpDuration;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                lookTransform.localRotation = Quaternion.Lerp(initialRotation, lookRotation, 1f - timer / turnUpDuration);
                yield return null;
            }

            StopFall();
        }
    }
}