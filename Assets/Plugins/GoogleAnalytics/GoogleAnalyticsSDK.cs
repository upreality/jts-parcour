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
    
    public static void SendNumEvent(string eventName, string argName, int argValue)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SendNumArg(eventName, argName, argValue.ToString());
#endif
    }
    
    public static void SendStringEvent(string eventName, string argName, string argValue)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SendStringArg(eventName, argName, argValue);
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void Send(string eventName);
    [DllImport("__Internal")]
    private static extern void SendNumArg(string eventName, string argName, string argValue);
    [DllImport("__Internal")]
    private static extern void SendStringArg(string eventName, string argName, string argValue);
#endif
}