using CarDealer.DTOs.Import;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.Utilities
{
    public static class XmlHelper
    {
        public static  T Deserialize<T>(string inputXml, string rootName)
        {
            XmlRootAttribute root = new XmlRootAttribute(rootName);
            XmlSerializer Ser = new XmlSerializer(typeof(T), root);

           using StringReader reader = new StringReader(inputXml);

            T supliersDtos = (T)Ser.Deserialize(reader);

            return supliersDtos;
        } 

        public static string Serialize<T>(T obj, string rootName)
        {
            StringBuilder sb = new StringBuilder();


            XmlRootAttribute root = new XmlRootAttribute(rootName);

            XmlSerializer Ser = new XmlSerializer(typeof(T), root);


            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(sb);

            Ser.Serialize(writer, obj, namespaces);

            return sb.ToString().TrimEnd();

             
        }
    }
}
