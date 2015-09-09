﻿using System;

namespace ExAs.Utils
{
    public static class Dates
    {
        public static DateTime Yesterday()
        {
            return Today().AddDays(-1);
        }

        public static DateTime Today()
        {
            return DateTime.Today;
        }

        public static DateTime Tomorrow()
        {
            return Today().AddDays(1);
        }
    }
}