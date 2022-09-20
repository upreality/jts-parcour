using UnityEngine;

public class GameAnalyticsInitializer : MonoBehaviour
{
    void Awake() => GameAnalyticsSDK.GameAnalytics.Initialize();
}