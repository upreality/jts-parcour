using Zenject;

namespace Gameplay.Inputs.Jump
{
    public class JumpInputMobileProvider: global::Jump.IJumpInputProvider
    {
        [Inject] private InputHandler handler;
        
        public bool GetHasJumpInput()
        {
            return handler.GetInput("Jump") > 0.5f;
        }
    }
}