using System;
using CrazyGames;
using UniRx;
using UnityEngine;
using Zenject;

namespace SDK.GameState
{
    public class CrazyGameStateHandler : MonoBehaviour
    {
        [Inject] private IGameStateNavigator gameStateNavigator;

#if CRAZY_SDK
        private void Start() => gameStateNavigator.GetGameState().Subscribe(HandleGameState).AddTo(this);

        private void HandleGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Active:
                    CrazySDK.Instance.GameplayStart();
                    break;
                case GameState.Disabled:
                    CrazySDK.Instance.GameplayStop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        #endif
    }
}
