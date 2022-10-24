using Plugins.FileIO;
using UnityEngine;
using UnityEngine.Events;

public class FPSOnceProxy : MonoBehaviour
{
    private const string fpsSentKey = "FPS_Sent";

    public UnityEvent<int> sendOnce = new();

    public void SendFPS(int fps)
    {
        if (LocalStorageIO.HasKey(fpsSentKey))
            return;

        sendOnce?.Invoke(fps);
        LocalStorageIO.SetInt(fpsSentKey, 1);
        LocalStorageIO.Save();
    }
}