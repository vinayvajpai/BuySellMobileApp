﻿using System;
namespace BuySell.Utility
{
    public static class ZoomExtension
    {
        public static double Clamp(this double self, double min, double max)
        {
            return Math.Min(max, Math.Max(self, min));
        }
    }
}
