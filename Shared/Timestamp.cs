namespace blazor_project.Shared
{
    public class Timestamp
    {
        public static DateTime FromTimestamp(long t) {
            return DateTimeOffset.FromUnixTimeMilliseconds(t).DateTime;
        }

        public static long ToTimestamp(DateTime time) {
            return new DateTimeOffset(time).ToUnixTimeSeconds() * 1000;
        }
    }
}