namespace HealthySystem.Data.Importer
{
    using System;
    using HealthySystem.Data.Importer.Contracts;

    public class DateConverter : IDateConverter
    {
        /// <summary>
        /// Convert UNIX timestamp to System.DateTime
        /// </summary>
        /// <param name="time">UNIX timestamp in seconds from 1970, 1, 1, 0, 0, 0, 0</param>
        /// <returns>DateTime date</returns>
        public DateTime FromUnixToSystemDateTime(string time)
        {
            double timestamp = double.Parse(time);

            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            unixEpoch = unixEpoch.AddSeconds(timestamp);

            var date = DateTime.Parse(unixEpoch.ToShortDateString() + " " + unixEpoch.ToShortTimeString());

            return date;
        }
    }
}