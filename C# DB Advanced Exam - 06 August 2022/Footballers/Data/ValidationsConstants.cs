using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.Data
{
    public static class ValidationsConstants
    {
        // Footballer
        public const int FootballerNameMinLength = 2;
        public const int FootballerNameMaxLength = 40;

        //Team

        public const  int TeamNameMaxLength = 40;
        public const  int TeamNameMinLength = 3;
        public const  int NationalityMaxLength = 40;
        public const  int NationalityMinLength = 2;
        public const  string TeamNameRegexPattern = @"^[A-Za-z0-9\s\.\-]+$";

        //Coach

        public const int CoachNameMaxLength = 40;
        public const int CoachNameMInLength = 2;

    }
}
