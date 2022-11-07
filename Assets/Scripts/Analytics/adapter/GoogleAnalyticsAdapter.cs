#if GOOGLE
using System;
using Analytics.ads;
using Analytics.levels;
using Analytics.playeractions;
using Analytics.screens;
using Analytics.session.domain;
using Analytics.settings;
using UnityEngine;

namespace Analytics.adapter
{
    public class GoogleAnalyticsAdapter: AnalyticsAdapter
    {
        public override void SendAdEvent(AdAction action, AdType type, AdProvider provider, IAdPlacement placement)
        {
            var eventName = action switch
            {
                AdAction.Request => "ad_request",
                AdAction.Show => "ad_show",
                AdAction.Failure => "ad_failure",
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };
            GoogleAnalyticsSDK.SendEvent(eventName);
        }

        public override void SendActionEvent(PlayerAction action)
        {
            var eventName = action switch
            {
                PlayerAction.Jump => "jump",
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };
            GoogleAnalyticsSDK.SendEvent(eventName);
        }

        public override void SendSettingsEvent(SettingType type, string val)
        {
            var eventName = type switch
            {
                SettingType.SoundToggle => $"sound_toggle_{val}",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            GoogleAnalyticsSDK.SendEvent(eventName);
        }

        public override void SendScreenEvent(string screenName, ScreenAction action)
        {
            var eventName = action switch
            {
                ScreenAction.Open => $"screen_open_{screenName}",
                ScreenAction.Close => $"screen_close_{screenName}",
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };
            GoogleAnalyticsSDK.SendEvent(eventName);
        }

        public override void SendLevelEvent(LevelPointer levelPointer, LevelEvent levelEvent)
        {
            var eventName = levelEvent switch
            {
                LevelEvent.Load => $"level_load_{levelPointer.LevelId}",
                LevelEvent.Start => $"level_start_{levelPointer.LevelId}",
                LevelEvent.Fail => $"level_fail_{levelPointer.LevelId}",
                LevelEvent.Complete => $"level_complete_{levelPointer.LevelId}",
                _ => throw new ArgumentOutOfRangeException(nameof(levelEvent), levelEvent, null)
            };
            GoogleAnalyticsSDK.SendEvent(eventName); 
        }

        public override void SendSessionEvent(SessionEvent sessionEvent, LevelPointer currentLevelPointer)
        {
            var eventName = sessionEvent switch
            {
                SessionEvent.Start => $"session_start_event",
                SessionEvent.Quit => $"session_quit_event",
                _ => throw new ArgumentOutOfRangeException(nameof(sessionEvent), sessionEvent, null)
            };
            GoogleAnalyticsSDK.SendEvent(eventName);
        }

        public override void SendErrorEvent(string error)
        {
            GoogleAnalyticsSDK.SendEvent(error);
        }

        public override void SetPlayerId(string id)
        {
            //do nothing
        }

        public override void InitializeWithoutPlayerId()
        {
            //do nothing
        }

        public override void SendFirstOpenEvent()
        {
            //do nothing
        }

        public override void SendPurchasedEvent(long purchaseId)
        {
            GoogleAnalyticsSDK.SendEvent($"purchase_{purchaseId}");
        }

        public override void SendBalanceAddedEvent(int amount)
        {
            GoogleAnalyticsSDK.SendEvent($"balance_added");
        }
    }
}
#endif