using System;

  public static class DateTimeExtensions
    {
        public static DateTime UnixSecondsToDateTime(this DateTime dateTime, long unixTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
        }

        public static DateTime UnixMillisecondsToDateTime(this DateTime dateTime, long unixTime)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTime).DateTime;
        }
    }

