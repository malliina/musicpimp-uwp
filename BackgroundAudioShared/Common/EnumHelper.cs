﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Common
{
    /// <summary>
    /// Simple helper for converting a string value to
    /// its corresponding Enum literal.
    /// 
    /// e.g. "Running" -> BackgroundTaskState.Running
    /// </summary>
    public static class EnumHelper
    {
        public static T Parse<T>(string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }

}
