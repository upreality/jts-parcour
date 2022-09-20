using UnityEngine;
using static UnityEngine.Input;

namespace Gameplay.Inputs
{
    public class MovementDesktopInputProvider : FirstPersonMovement.IMovementInputProvider
    {
        public Vector2 GetInput() => new(GetAxis("Horizontal"), GetAxis("Vertical"));
        public bool GetRunningInput() => GetKey(KeyCode.LeftShift);
    }
}