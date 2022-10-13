using UnityEngine;
using UnityEngine.Events;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 5f; //How often should the number update

    private float accum = 0.0f;
    private int frames = 0;
    private float timeleft;

    private bool pausedState = false;

    private readonly GUIStyle textStyle = new();

    [SerializeField] private UnityEvent<int> onFPSMeasured = new();

    private void Start()
    {
        timeleft = updateInterval;

        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.white;
    }

    private void Update()
    {
        if(pausedState) return;
        
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (!(timeleft <= 0.0)) return;

        // display two fractional digits (f2 format)
        var fps = accum / frames;
        if (fps > 0.1f) onFPSMeasured?.Invoke((int)fps);
        timeleft = updateInterval;
        accum = 0.0f;
        frames = 0;
    }

    public void SetPaused(bool paused) => pausedState = paused;

    public void Reset()
    {
        timeleft = updateInterval;
        accum = 0.0f;
        frames = 0;
    }
}