using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchManagement.Classes
{
    public enum Sport
    {
        Football,
        Basketball
    }
    public static class EnumExtension
    {   
        public static string GetString(this Sport sport )
        {
            switch(sport)
            {
                case Sport.Football:
                    return "Football";
                case Sport.Basketball:
                    return "Basketball";
                default:
                    return "No Sport";
            }
        }

    }
}
