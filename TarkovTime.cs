using System;

namespace TarkovTime
{
    class TarkovTime
    {
        public string RealTimeToTarkovTime(bool left)
        {
            /** 
             * Tarkov time moves at 7 seconds per second so 1 second real time = 7 seconds tarkov time.
             *
             * Escape from Tarkov runs raids in two "time zones" 12 hours apart.
             * The two timezones is called 'left' and 'right'. Right is + 12 hours.
             * 
             * Tarkov time is locked to Real-time but surprisingly, midnight in Tarkov does not equal
             * unix 0... it equals unix 10800000 which is 3 hours - What's also +3 hours?
             * Yep, St. Petersburg is on Moscov time: UTC+3. therefore, to convert real time to
             * 'left' tarkov time:
             *
             *            tarkov time = (RealLifeUTC + 3 hours) x 7
             * 
             * Time calcualtion needs to be done in units that are Base-10 so miliseconds should suffice.
             */

            /** If 'left' is true then offset must be 3 hours else it must be 15 hours. */
            double offset;
            if (left)
            {
                offset = TimeSpan.FromHours(3).TotalMilliseconds;
            }
            else
            {
                offset = TimeSpan.FromHours(15).TotalMilliseconds;
            }

            // Get the current UTC time
            var utcNow = DateTime.UtcNow.TimeOfDay.TotalMilliseconds;

            // Calculate the Tarkov Time
            var TarkovTime = TimeSpan.FromMilliseconds(offset + (utcNow * 7));

            // Format hours with 0 as first digit if hours less than 10
            string hrs = (TarkovTime.Hours < 10 ? "0" + TarkovTime.Hours.ToString() : TarkovTime.Hours.ToString());
            // Format hours with 0 as first digit if minutes less than 10
            string mins = (TarkovTime.Minutes < 10 ? "0" + TarkovTime.Minutes.ToString() : TarkovTime.Minutes.ToString());

            // Return the result
            return hrs + ":" + mins;

            // Inspiration was found at
            // http://www.blackwasp.co.uk/TimespanMultiplication.aspx
            // and
            // https://github.com/adamburgess/tarkov-time
        }
    }
}