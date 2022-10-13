#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

public static class GoogleAnalyticsSDK
{
    public static void SendEvent(string eventName)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Send(eventName);
#endif
    }
    
    public static void ActivateOptimizeEvent()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ActivateOptimize();
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void Send(string eventName);
    [DllImport("__Internal")]
    private static extern void ActivateOptimize();
#endif
}