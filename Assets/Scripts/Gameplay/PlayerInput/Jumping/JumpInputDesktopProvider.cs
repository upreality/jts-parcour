namespace Gameplay.Inputs.Jump
{
    public class JumpInputDesktopProvider: global::Jump.IJumpInputProvider
    {
        public bool GetHasJumpInput() => UnityEngine.Input.GetButtonDown("Jump");
    }
}