using SDK.Platform.domain;
using UnityEngine;
using Zenject;

public class FirstPersonLook : MonoBehaviour
{
    [Inject] private ILookDeltaProvider deltaProvider;
    [Inject] private IPlatformProvider platformProvider;
    [SerializeField] Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    [SerializeField] private bool enabledState = true;

    void Reset() => character = GetComponentInParent<FirstPersonMovement>().transform;

    void Start()
    {
        if (platformProvider.GetCurrentPlatform() == Platform.Mobile)
            return;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get smooth velocity.
        var mouseDelta = enabledState ? deltaProvider.GetDelta() : Vector2.zero;
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    public void SetEnabledState(bool enabled)
    {
        enabledState = enabled;
        if (platformProvider.GetCurrentPlatform() == Platform.Mobile)
            return;
        Cursor.lockState = enabledState ? CursorLockMode.Locked : CursorLockMode.Confined;
    }

    public void ResetLook()
    {
        velocity = Vector2.zero;
        frameVelocity = Vector2.zero;
        Input.ResetInputAxes();
    }

    public interface ILookDeltaProvider
    {
        public Vector2 GetDelta();
    }
}