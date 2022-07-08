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
            var utcNow = DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
            var TarkovTime = TimeSpan.FromMilliseconds(offset + (utcNow * 7));

            // This is ugly but it trims the string as needed - feel free to suggest another way to do it!
            return TarkovTime.ToString().Remove(TarkovTime.ToString().Length - 11).Remove(0, 2);

            // Inspiration was found at
            // http://www.blackwasp.co.uk/TimespanMultiplication.aspx
            // and
            // https://github.com/adamburgess/tarkov-time
        }
    }
}