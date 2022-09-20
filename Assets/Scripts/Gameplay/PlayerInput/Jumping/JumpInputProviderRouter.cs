using Gameplay.Input;
using Gameplay.Inputs.Jump;
using SDK.Platform.domain;
using Zenject;

namespace Gameplay.Inputs
{
    public class JumpInputProviderRouter: global::Jump.IJumpInputProvider
    {
        [Inject(Id = "DesktopJumpInputProvider")] private global::Jump.IJumpInputProvider desktopProvider;
        [Inject(Id = "MobileJumpInputProvider")] private global::Jump.IJumpInputProvider mobileProvider; 
        [Inject] private IPlatformProvider platformProvider;

        private bool initialized = false;
        private bool isOnDesktop = true;
        
        public bool GetHasJumpInput()
        {
            if (!initialized)
            {
                isOnDesktop = platformProvider.GetCurrentPlatform() == Platform.Desktop;
                initialized = true;
            }
            return isOnDesktop? desktopProvider.GetHasJumpInput() : mobileProvider.GetHasJumpInput();
        }
    }
}