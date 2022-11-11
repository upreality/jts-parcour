using System;
using Analytics.adapter;
using Analytics.playeractions;
using Doozy.Engine;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Jump : MonoBehaviour
{
    [Inject] private IJumpInputProvider jumpInputProvider;
    [Inject] private AnalyticsAdapter analytics;
    Rigidbody myRigidbody;
    public float jumpStrength = 2;
    public event Action Jumped;
    public string JumpedMessage = "Jump";

    private int extraJumpsLimit = 0;
    private int extraJumps = 0;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;

    [SerializeField] private float jumpWindowTimerDefault = 0.01f;
    [SerializeField] private float jumpWindowTimer;


    private bool grounded = false;

    public void SetDefaultJumpWindow(float window)
    {
        jumpWindowTimerDefault = window;
    }

    void Reset()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        extraJumps = extraJumpsLimit;
        jumpWindowTimer = jumpWindowTimerDefault;
    }

    private void Update()
    {
        grounded = !groundCheck || groundCheck.isGrounded;
        if (grounded)
            jumpWindowTimer = jumpWindowTimerDefault;
        else if (jumpWindowTimer > 0.01f)
            jumpWindowTimer = Mathf.Max(0f, jumpWindowTimer - Time.deltaTime);
    }

    void LateUpdate()
    {
        var hasInput = jumpInputProvider.GetHasJumpInput();
        if (!hasInput) return;

        if (grounded || jumpWindowTimer > 0)
        {
            extraJumps = extraJumpsLimit;
            jumpWindowTimer = 0f;
        }
        else if (extraJumps-- <= 0 || jumpWindowTimer <= 0.01f)
            return;

        myRigidbody.AddForce(Vector3.up * 100 * jumpStrength);
        Jumped?.Invoke();
        GameEventMessage.Send(JumpedMessage);
        analytics.SendActionEvent(PlayerAction.Jump);
    }

    public void SetExtraJumpsCount(int count)
    {
        extraJumps = Math.Min(extraJumps, count);
        extraJumpsLimit = count;
    }

    public void SetJumpStrength(float strength)
    {
        jumpStrength = strength;
    }

    public interface IJumpInputProvider
    {
        public bool GetHasJumpInput();
    }
}