using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.Common
{
    public static class ValidationsConstants
    {
        // Prisoner

        public const int PrisonerFullNameMaxLength = 20;
        public const int PrisonerFullNameMinLength = 3;
        public const string PrisonerNicknameRegexPattern = @"^(The\s)([A-Z][a-z]*)$";
        public const int AgeMInValue = 18;
        public const int AgeMaxValue = 65;
        public const string bailMinValue = "0";
        public const string bailMaxValue = "79228162514264337593543950335";
        //Officer


        public const int OfficerFullNameMaxLength = 30;
        public const int OfficerFullNameMinLength = 3;

        //Department

        public const int DepartmentNameMaxLength = 25;
        public const int DepartmentNameMinLength = 3;

        // Cell
        public const int CellMinValue = 1;
        public const int CellMaxValue = 1000;

        // Mail

        public const string MailAddressRegexPattern = @"^([A-Za-z0-9\s]+)(\sstr.)$";

        //Salary

        public const string salaryMinValue = "0";
        public const string salaryMaxValue = "79228162514264337593543950335";



    }
}
