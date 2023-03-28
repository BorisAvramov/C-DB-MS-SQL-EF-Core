using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Common
{
    public static class ValidationConstants
    {
        // Team
        public const int TeamNameMaxLength = 50;
        public const int TeamLogoUrlMAxLength = 2048;
        public const int TeamInitialMxLength = 4;

        // Color

        public const int ColorNameMaxLength = 10;

        // Townm

        public const int TownNameMaxLength = 58;

        //Country

        public const int CountryNameMaxLength = 56;

        //Player

        public const int PlayerMaxLengthName = 100;

        // Position

        public const int PositionNameMaxLength = 50;

        // Game

        public const int GameResultMaxLength = 7;

        // user

        public const int UserNameMaxLength = 36;

        // password

        public const int PasswordMaxLength = 255;

        // email

        public const int EmailMaxLength = 255;


    }
}
