namespace Footballers.DataProcessor
{
    using AutoMapper;
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlRootAttribute root  = new XmlRootAttribute("Coaches");

            XmlSerializer ser = new XmlSerializer(typeof(ImportCoachWithFootballersDto[]), root);

          using  StringReader reader = new StringReader(xmlString);

            ImportCoachWithFootballersDto[] coachesWithFootballersDtos = (ImportCoachWithFootballersDto[])ser.Deserialize(reader);

            List<Coach> validCoachesModels = new List<Coach>();

            foreach (var coachDto in coachesWithFootballersDtos)
            {
                if (!IsValid(coachDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Coach coach = new Coach
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality,


                };

                foreach (var footbolerDto in coachDto.Footballers )
                {
                    if (!IsValid(footbolerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isValidContractStartDate = DateTime.TryParseExact(footbolerDto.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out DateTime ContractStartDateValue);

                    if (!isValidContractStartDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isValidContractEndDate = DateTime.TryParseExact(footbolerDto.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ContractEndDateValue);

                    if (!isValidContractEndDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if(ContractStartDateValue > ContractEndDateValue)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;

                    }

                    bool isValidBestSkillType = Enum.TryParse(typeof(BestSkillType), footbolerDto.BestSkillType,  out object BestSkillValue);

                    if (!isValidBestSkillType)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    bool isValidPositionType = Enum.TryParse(typeof(PositionType), footbolerDto.PositionType, out object PositionTypeValue);
                    if (!isValidPositionType)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    Footballer footboller = new Footballer()
                    {
                        Name = footbolerDto.Name,
                        ContractStartDate = ContractStartDateValue,
                        ContractEndDate = ContractEndDateValue,
                        BestSkillType = (BestSkillType)BestSkillValue,
                        PositionType = (PositionType)PositionTypeValue
                    };

                    coach.Footballers.Add(footboller);

                }

                validCoachesModels.Add(coach);

                sb.AppendLine($"Successfully imported coach - {coach.Name} with {coach.Footballers.Count} footballers.");

            }

            context.AddRange(validCoachesModels);

            context.SaveChanges();

            return sb.ToString().TrimEnd();


        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportTeamWithFotballersDto[] teamsWithFootballersDtos = JsonConvert.DeserializeObject<ImportTeamWithFotballersDto[]>(jsonString);

            List<Team> validTeamsModels = new List<Team>();
            List<int> fotballersIds = new List<int>(); 

            foreach (var teamDto in teamsWithFootballersDtos)
            {
                if (!IsValid(teamDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (int.Parse(teamDto.Trophies) <= 0 )
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                Team team = new Team
                {
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = int.Parse(teamDto.Trophies),
                };

                foreach (var footballerId in teamDto.Footballers.Distinct())
                {
                    if (!context.Footballers.Any(f => f.Id == footballerId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }




                    TeamFootballer footballer = new TeamFootballer
                    {

                        Team = team,
                        FootballerId = footballerId,

                    };

                    team.TeamsFootballers.Add(footballer);

                }

                validTeamsModels.Add(team);
                sb.AppendLine($"Successfully imported team - {team.Name} with {team.TeamsFootballers.Count} footballers.");

            }

            context.Teams.AddRange(validTeamsModels);

            context.SaveChanges();

            return sb.ToString().TrimEnd();


        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
