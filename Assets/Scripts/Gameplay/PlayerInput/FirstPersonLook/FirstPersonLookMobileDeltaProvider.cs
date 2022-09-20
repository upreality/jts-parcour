using System.Linq;
using UnityEngine;
using static UnityEngine.Input;

namespace Gameplay.Inputs
{
    public class FirstPersonLookMobileDeltaProvider : FirstPersonLook.ILookDeltaProvider
    {
        private float multiplier = 0.2f;
        private int minX = Screen.width / 2;

        public Vector2 GetDelta()
        {
            var movedTouches = touches.Where(touch => touch.phase == TouchPhase.Moved).ToList();
            if (!movedTouches.Any(TouchInLookControlArea))
                return Vector2.zero;
            return movedTouches.First(TouchInLookControlArea).deltaPosition * multiplier;
        }

        private bool TouchInLookControlArea(Touch touch) => touch.position.x > minX;
    }
}