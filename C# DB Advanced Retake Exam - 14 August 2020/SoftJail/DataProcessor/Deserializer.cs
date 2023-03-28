namespace SoftJail.DataProcessor
{
    using AutoMapper;
    using Data;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public class Deserializer
    {
        // Problem 2 IMPORT

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportDepartmentWithCellsDto[] departmentsWithCellsDtos = JsonConvert.DeserializeObject<ImportDepartmentWithCellsDto[]>(jsonString);
            ICollection<Department> validDepartmentsModels = new List<Department>();
            // •	If a department is invalid, do not import its cells. =>  ICollection<Cell> cellsModels = new List<Cell>();
            foreach (var departmentDto in departmentsWithCellsDtos)
            {
                // validates •	If any validation errors occur (such as if a department name is too long/short or a cell number is out of range) proceed as described above

                if (!IsValid(departmentDto))
                {
                    sb.AppendLine($"Invalid Data");
                    continue;
                }
                // validates •	If a Department doesn’t have any Cells, he is invalid.
                if (!departmentDto.Cells.Any())
                {
                    sb.AppendLine($"Invalid Data");
                    continue;
                }
                // validates •	If one Cell has invalid CellNumber, don’t import the Department.

                if (departmentDto.Cells.Any(c => !IsValid(c)))
                {
                    sb.AppendLine($"Invalid Data");
                    continue;
                }

                Department department = new Department()
                {
                    Name = departmentDto.Name,

                };

                foreach (var cellDto in departmentDto.Cells)
                {
                    Cell cell = Mapper.Map<Cell>(cellDto);

                    department.Cells.Add(cell);  
                }

                validDepartmentsModels.Add(department);
                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");

            }

            context.Departments.AddRange(validDepartmentsModels);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();


            ImportPrisonerWithMailsDto[] prisonersDtos = JsonConvert.DeserializeObject<ImportPrisonerWithMailsDto[]>(jsonString);


            List<Prisoner> validPrisonersModels = new List<Prisoner>();

            foreach (var prisonerDto in prisonersDtos)
            {

                if (!IsValid(prisonerDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                if (prisonerDto.Mails.Any(m => !IsValid(m)))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                bool isValidIncercanationDate = DateTime.TryParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime IncarcerationDateValue);

                if (!isValidIncercanationDate)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                DateTime? ReleaseDateValue = null;

                if (!String.IsNullOrEmpty(prisonerDto.ReleaseDate))
                {
                    bool isValidReleaseDate = DateTime.TryParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);
                    if (!isValidReleaseDate)
                    {
                        sb.AppendLine("Invalid Data");
                        continue;
                    }

                    ReleaseDateValue = releaseDate;

                }

                Prisoner prisoner = new Prisoner
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = IncarcerationDateValue,
                    ReleaseDate = ReleaseDateValue,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId

                };

                foreach (var mailDto in prisonerDto.Mails)
                {
                    Mail mail = Mapper.Map<Mail>(mailDto);
                    prisoner.Mails.Add(mail);

                }

               validPrisonersModels.Add(prisoner);

                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
               

            }

            context.Prisoners.AddRange(validPrisonersModels);

            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
               
            XmlRootAttribute root = new XmlRootAttribute("Officers");


            XmlSerializer ser = new XmlSerializer(typeof(ImportOfficerDto[]), root);

           using StringReader reader = new StringReader(xmlString);

            ImportOfficerDto[] officersWithPrisonersDtos = (ImportOfficerDto[])ser.Deserialize(reader);

            List<Officer> validOfficersMOdels = new List<Officer>();

            foreach (var officerDto in officersWithPrisonersDtos)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                bool isPositionEnumValid = Enum.TryParse(typeof(Position), officerDto.Position, out object positionValue);

                if (!isPositionEnumValid)
                {
                    sb.AppendLine("Invalid Data");
                    continue;   

                }

                bool isWeapoEnumValid = Enum.TryParse(typeof(Weapon), officerDto.Weapon, out object weaponValue);

                if (!isWeapoEnumValid)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                Officer officer = new Officer
                {
                    //<Name>Minerva Holl</Name>
                    //  <Money>2582.55</Money>
                    //  <Position>Overseer</Position>
                    //  <Weapon>ChainRifle</Weapon>
                    //  <DepartmentId>2</DepartmentId>

                    FullName = officerDto.FullName,
                    Salary = officerDto.Salary,
                    Position = (Position)positionValue,
                    Weapon = (Weapon)weaponValue,
                    DepartmentId= officerDto.DepartmentId,


                };

                foreach (var prisonerDto in officerDto.Prisoners)
                {
                    OfficerPrisoner prisoner = Mapper.Map<OfficerPrisoner>(prisonerDto);
                    //OfficerPrisoner prisoner = new OfficerPrisoner
                    //{
                    //    Officer = officer,
                    //    PrisonerId = prisonerDto.Id
                    //};

                    officer.OfficerPrisoners.Add(prisoner);
                }
                validOfficersMOdels.Add(officer);

                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(validOfficersMOdels);

            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }
          
        // Helper Method for attribute validations

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}