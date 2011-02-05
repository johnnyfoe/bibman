using System;

namespace BibtexEntryManager.Helpers
{
    public class Compare
    {
        public static bool EqualsIgnoreCase(String a, String b)
        {
            if ((a == null && b == null))
                return true;
            return a.ToLower().Equals(b.ToLower());
        }

    }
}