using System.Collections.Generic;
using UnityEngine;

namespace Enviroment
{
    public class BirdAIPointProvider : MonoBehaviour
    {
        private List<Transform> targets = new();
        private Transform player;
        
        [SerializeField, Range(0f, 1f)] private float playerPointChance = 0.05f;

        private void Awake()
        {
            foreach (Transform child in transform) targets.Add(child);
            
        }

        public void SetPlayer(Transform playerTarget) => player = playerTarget;

        public Transform GetNextTarget()
        {
            var playerChance = Random.Range(0f, 1f);
            return playerChance > playerPointChance 
                ? targets[Random.Range(0, targets.Count)] 
                : player;
        }
    }
}