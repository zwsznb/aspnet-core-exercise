namespace zws.Core.Extensions
{
    public static class StringExtension
    {
        public static bool IsIn(this string str, params string[] ps)
        {
            if (ps.Contains(str))
            {
                return true;
            }
            return false;
        }
    }
}
