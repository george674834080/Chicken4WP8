﻿using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces.DTO;

namespace Tweetinvi.Core.Interfaces.Models
{
    public interface IAccountSettings
    {
        IAccountSettingsDTO AccountSettingsDTO { get; set; }
        string ScreenName { get; }
        PrivacyMode PrivacyMode { get; }
        Language Language { get; }
        
        bool AlwaysUseHttps { get; }
        bool DiscoverableByEmail { get; }
        bool GeoEnabled { get; }
        bool ShowAllInlineMedia { get; }
        bool UseCookiePersonalization { get; }

        ITimeZone TimeZone { get; }
        ITrendLocation TrendLocation { get; }

        bool SleepTimeEnabled { get; }
        int SleepTimeStartHour { get; }
        int SleepTimeEndHour { get; }
    }
}