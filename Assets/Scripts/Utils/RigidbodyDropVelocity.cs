using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyDropVelocity : MonoBehaviour
    {
        private Rigidbody rb;

        private void Awake() => rb = GetComponent<Rigidbody>();

        public void ResetVelocity()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}