namespace SoftJail.DataProcessor
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        //Problem 3 EXPORT

        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Where(p => ids.Contains(p.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(po => new
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name,

                    })
                    .OrderBy(o => o.OfficerName)
                    .ToArray(),
                    TotalOfficerSalary =  Math.Round( (p.PrisonerOfficers.Select( po => po.Officer.Salary)).Sum(), 2 ) //!!!
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToArray();

            string json = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return json;

        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {

            StringBuilder sb = new StringBuilder();

            string[] prisonersNamesTokens = prisonersNames.Split(",").ToArray();

            ExportPrisonerWithMails[] validPrisoners = context.Prisoners
                .Where(p => prisonersNamesTokens.Contains(p.FullName))
                .ProjectTo<ExportPrisonerWithMails>(Mapper.Configuration)
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .ToArray();
              


            XmlRootAttribute root = new XmlRootAttribute("Prisoners");

            XmlSerializer ser = new XmlSerializer(typeof(ExportPrisonerWithMails[]), root);

            StringWriter writer = new StringWriter(sb);

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

            namespaces.Add(String.Empty, String.Empty);

            ser.Serialize(writer, validPrisoners, namespaces);

            return sb.ToString().TrimEnd();

        }
    }
}