namespace MauiPets.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-diff).Date;
        }

        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime AddWeeks(this DateTime date, int weeks)
        {
            return date.AddDays(weeks * 7);
        }

        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            return StartOfWeek(date, startOfWeek).AddDays(6);
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            return StartOfMonth(date).AddMonths(1).AddDays(-1);
        }
    }
}
