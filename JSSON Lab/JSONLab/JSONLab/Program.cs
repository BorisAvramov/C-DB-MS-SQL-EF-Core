using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Formats.Asn1;
using System.Globalization;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace JSONLab
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Car car = new Car
            {
                Model = "Audi",
                Year = new DateTime(2017, 12, 05),
                Engine = new Engine
                {
                    HorsePower = 200,
                    Volume = 3
                }


            };


            Console.WriteLine(car.Model);

            // install NuGet pack =>  Newtonsoft.jsaon 

            // SERIALIZEOBJECT WITH Newtonsoft.jsaon
            string jsonCarNewton = JsonConvert.SerializeObject(car, Formatting.Indented);

            Console.WriteLine(jsonCarNewton);

            // install NuGet pack =>  System.Text.Jsaon 

            // SERIALIZEOBJECT WITH System.Text.Jsaon 


            //string jsonCar = JsonSerializer.Serialize(car);

            //Console.WriteLine(jsonCar);

            // create json file in folder bin -> debug
            //File.WriteAllText("CarJson.json", jsonCar);


            // deserialize
            Car carObject = JsonConvert.DeserializeObject<Car>(jsonCarNewton);
            Console.WriteLine(carObject.Model);

            var jsonfROMnETeXEMPLE = "{\"employees\":[\r\n    {\"name\":\"Shyam\", \"email\":\"shyamjaiswal@gmail.com\"},  \r\n    { \"name\":\"Bob\", \"email\":\"bob32@gmail.com\"},  \r\n    { \"name\":\"Jai\", \"email\":\"jai87@gmail.com\"}  \r\n    ]}  ";

            Rootobject obj = JsonConvert.DeserializeObject<Rootobject>(jsonfROMnETeXEMPLE);

            Console.WriteLine(String.Join(", ", obj.employees.Select(e => e.name)));

            // create seetings of serialize
            // camelCase in json object is the naming strategy


            var settings = new JsonSerializerSettings
            {

                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                     NamingStrategy = new CamelCaseNamingStrategy()
                },
                DateFormatString = "MM/dd/yyyy"

            };

            Console.WriteLine(JsonConvert.SerializeObject(car, settings));


            // anonymous -> serialize

            Console.WriteLine(JsonConvert.SerializeObject(new {Car = car, Info = "MyCar"}, Formatting.Indented));

            // anonymous -> deserialize

            string json = File.ReadAllText("CarJson.json");

            var a = new
            {
                Model = "",
                Year = "",
            };


            Console.WriteLine(JsonConvert.DeserializeAnonymousType(json, a, settings));

            // LINQ to JSON

            var jObject = JObject.Parse(json);

            foreach (var  item in jObject.Children())
            {
                Console.WriteLine(item);
            }

            // CSV - Select from sql and save to folder of project as .CSV file
            // it's like excel table 
            // then read file  with stream.reader
            // but it has to install NuGet pack. CsvHelper
        //    {
        //        using (var reader = new StreamReader("path\\to\\file.csv"))
        //        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //        {
        //            var records = csv.GetRecords<Foo>();
        //        }
        //    }
        
        //    public class Foo
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //}



    }

        class Car
        {
            public string Model { get; set; }

            public DateTime Year { get; set; }

            // when serialize object to json string then ignore this prop Engine

            [JsonIgnore]
            public Engine Engine { get; set; }

        }
        class Engine
        {
            public int HorsePower { get; set; }

            public double Volume { get; set; }


        }
    }
}