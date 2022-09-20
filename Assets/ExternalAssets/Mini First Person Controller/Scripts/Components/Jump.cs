using System;
using Doozy.Engine;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Jump : MonoBehaviour
{
    [Inject] private IJumpInputProvider jumpInputProvider;
    Rigidbody myRigidbody;
    public float jumpStrength = 2;
    public event Action Jumped;
    public string JumpedMessage = "Jump";

    private int extraJumpsLimit = 0;
    private int extraJumps = 0;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        myRigidbody = GetComponent<Rigidbody>();
        extraJumps = extraJumpsLimit;
    }

    void LateUpdate()
    {
        var hasInput = jumpInputProvider.GetHasJumpInput();
        var grounded = !groundCheck || groundCheck.isGrounded;
        if (!hasInput) return;
        if (grounded)
            extraJumps = extraJumpsLimit;
        else if (extraJumps-- <= 0) 
            return;

        myRigidbody.AddForce(Vector3.up * 100 * jumpStrength);
        Jumped?.Invoke();
        GameEventMessage.Send(JumpedMessage);
    }

    public void SetExtraJumpsCount(int count)
    {
        extraJumps = Math.Min(extraJumps, count);
        extraJumpsLimit = count;
    }

    public interface IJumpInputProvider
    {
        public bool GetHasJumpInput();
    }
}