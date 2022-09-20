using Gameplay;
using Gameplay.Input;
using Gameplay.Inputs;
using Gameplay.Inputs.Jump;
using SDK.GameState;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private FirstPersonLook look;

    public override void InstallBindings()
    {
        Container.BindInstance(look).AsSingle();
        Container.BindInterfacesAndSelfTo<InputHandler>().FromInstance(new InputHandler()).AsSingle();
        BindLookDeltaProvider();
        BindMovementInputProvider();
        BindJumpInputProvider();
    }

    private void BindJumpInputProvider()
    {
        Container
            .Bind<Jump.IJumpInputProvider>()
            .WithId("DesktopJumpInputProvider")
            .To<JumpInputDesktopProvider>()
            .AsSingle();

        Container
            .Bind<Jump.IJumpInputProvider>()
            .WithId("MobileJumpInputProvider")
            .To<JumpInputMobileProvider>()
            .AsSingle();

        Container.Bind<Jump.IJumpInputProvider>().To<JumpInputProviderRouter>().AsSingle();
    }

    private void BindMovementInputProvider()
    {
        Container
            .Bind<FirstPersonMovement.IMovementInputProvider>()
            .WithId("DesktopMovementProvider")
            .To<MovementDesktopInputProvider>()
            .AsSingle();

        Container
            .Bind<FirstPersonMovement.IMovementInputProvider>()
            .WithId("MobileMovementProvider")
            .To<MovementMobileInputProvider>()
            .AsSingle();

        Container.Bind<FirstPersonMovement.IMovementInputProvider>().To<MovementInputProviderRouter>().AsSingle();
    }

    private void BindLookDeltaProvider()
    {
        Container
            .Bind<FirstPersonLook.ILookDeltaProvider>()
            .WithId("DesktopLookDeltaProvider")
            .To<FirstPersonLookDesktopDeltaProvider>()
            .AsSingle();

        Container
            .Bind<FirstPersonLook.ILookDeltaProvider>()
            .WithId("MobileLookDeltaProvider")
            .To<FirstPersonLookMobileDeltaProvider>()
            .AsSingle();

        Container.Bind<FirstPersonLook.ILookDeltaProvider>().To<FirstPersonLookDeltaProviderRouter>().AsSingle();
    }
}