using UnityEngine;

namespace Gameplay
{
    public class Restart : MonoBehaviour
    {
        [SerializeField] private Transform spawn;
        [SerializeField] private Rigidbody[] playerRigidbodies;
        [SerializeField] private Transform[] cameras;

        public void Respawn()
        {
            foreach (var playerRigidbody in playerRigidbodies)
            {
                playerRigidbody.velocity = Vector3.zero;
                var playerObject = playerRigidbody.transform;
                playerObject.position = spawn.position;
                playerObject.rotation = spawn.rotation;
            }
        
            foreach (var cam in cameras)
            {
                cam.localRotation = Quaternion.identity;
                if (!cam.TryGetComponent<FirstPersonLook>(out var look)) continue;
                look.ResetLook();
            }
        }
    }
}
