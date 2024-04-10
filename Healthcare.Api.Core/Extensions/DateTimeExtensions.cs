namespace Healthcare.Api.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToArgentinaTime(this DateTime dateTime)
        {
            TimeZoneInfo argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, argentinaTimeZone);
        }
    }
}
