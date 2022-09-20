using UnityEngine;
using static UnityEngine.Input;

namespace Gameplay.Input
{
    public class FirstPersonLookDesktopDeltaProvider: FirstPersonLook.ILookDeltaProvider
    {
        public Vector2 GetDelta() => new(GetAxisRaw("Mouse X"), GetAxisRaw("Mouse Y"));
    }
}