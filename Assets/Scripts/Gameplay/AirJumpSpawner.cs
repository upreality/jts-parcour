using JetBrains.Annotations;
using SUPERCharacter;
using UnityEngine;

namespace Utils
{
    public class AirJumpSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject instance;
        [CanBeNull] private SUPERCharacterAIO character;

        private void Start()
        {
            character = GetComponentInParent<SUPERCharacterAIO>();
            if (character == null) return;
            character.onJump.AddListener(Spawn);
        }

        private void OnDestroy()
        {
            if (character == null) return;
            character.onJump.RemoveListener(Spawn);
        }

        private void Spawn()
        {
            Instantiate(instance, transform.position, transform.rotation);
        }
    }
}