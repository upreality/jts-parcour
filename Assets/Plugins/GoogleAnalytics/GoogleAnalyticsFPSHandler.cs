using UnityEngine;

public class GoogleAnalyticsFPSHandler : MonoBehaviour
{
    public void HandleFPS(int fps)
    {
        Debug.Log("Send FPS: " + fps);
        GoogleAnalyticsSDK.SendNumEvent("measure_fps", "fps", fps);
    }
}