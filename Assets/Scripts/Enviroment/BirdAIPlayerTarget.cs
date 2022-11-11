using UnityEngine;

namespace Enviroment
{
    public class BirdAIPlayerTarget: MonoBehaviour
    {
        [SerializeField] private BirdAIPointProvider pointProvider;

        private void OnEnable() => pointProvider.SetPlayer(transform);
    }
}