namespace HealthySystem.Data.Importer.Contracts
{
    using System;

    public interface IDateConverter
    {
        DateTime FromUnixToSystemDateTime(string time);
    }
}