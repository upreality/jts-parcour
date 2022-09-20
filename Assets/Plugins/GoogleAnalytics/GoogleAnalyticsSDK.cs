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

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void Send(string eventName);
#endif
}