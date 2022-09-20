using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Inputs
{
    public class ResetInput: MonoBehaviour
    {
        [Inject] private InputHandler handler;

        public void ResetAxis()
        {
            handler.Reset();
        }
    }
}