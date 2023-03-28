using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using CarDealer.Utilities;
using Castle.Core.Resource;
using System.IO;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            // IMPORT

            //09.Import Suppliers
            //string xmlInput9 = File.ReadAllText("../../../Datasets/suppliers.xml");
            //Console.WriteLine(ImportSuppliers(context, xmlInput9));

            //// 10. Import Parts

            //string inputXml10 = File.ReadAllText("../../../Datasets/parts.xml");
            //string result10 = ImportParts(context, inputXml10);
            //Console.WriteLine(result10);

            ////11. Import Cars
            //string inputXml11 = File.ReadAllText("../../../Datasets/cars.xml");
            //string result11 = ImportCars(context, inputXml11);
            //Console.WriteLine(result11);

            ////12. Import Customers
            //string inputXml12 = File.ReadAllText("../../../Datasets/customers.xml");
            //string result12 = ImportCustomers(context, inputXml12);
            //Console.WriteLine(result12);

            ////13. Import Sales
            //string inputXml13 = File.ReadAllText("../../../Datasets/sales.xml");
            //string result13 = ImportSales(context, inputXml13);
            //Console.WriteLine(result13);

            // EXPORT

            // 14. Export Cars With Distance

            string result14 = GetCarsWithDistance(context);
            Console.WriteLine(result14);

            File.WriteAllText("../../../Results/car-with-distance.xml", result14);
        }

         
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            IMapper mapper = CreateMapper.InitialMapper();

            ExportCarWithDistanceDto[] carsDtos = context.Cars
                .Where(c => c.TraveledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportCarWithDistanceDto>(mapper.ConfigurationProvider)
                .ToArray();

            return XmlHelper.Serialize<ExportCarWithDistanceDto[]>(carsDtos, "cars");


        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            IMapper mapper = CreateMapper.InitialMapper();

            ImportSaleDto[] salesDtos = XmlHelper.Deserialize<ImportSaleDto[]>(inputXml, "Sales");

            List<Sale> salesModels = new List<Sale>();

            List<int> carIdsInSales = context.Cars.Select(c => c.Id).ToList();

            foreach (var saleDto in salesDtos)
            {
                if (carIdsInSales.Contains(saleDto.CarId))
                {
                    Sale sale = mapper.Map<Sale>(saleDto);

                    salesModels.Add(sale);
                }
               
                 
            }

            context.Sales.AddRange(salesModels);
            context.SaveChanges();

            return $"Successfully imported {salesModels.Count}";


        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = CreateMapper.InitialMapper();

            ImportCustomerDto[] customersDtos = XmlHelper.Deserialize<ImportCustomerDto[]>(inputXml, "Customers");

            Customer[] customersModels = mapper.Map<Customer[]>(customersDtos);

            context.Customers.AddRange(customersModels);
            context.SaveChanges();

            return $"Successfully imported {customersModels.Length}";



        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            ImportCarDto[] carsDtos = XmlHelper.Deserialize<ImportCarDto[]>(inputXml, "Cars");

            List<Car> carsModels = new List<Car>();

            foreach (var dto in carsDtos)
            {
                Car car = new Car()
                {
                    Make = dto.Make,
                    Model = dto.Model,
                    TraveledDistance = dto.TraveledDistance

                };

                foreach (var part in dto.Parts.DistinctBy(p => p.PartId))
                {
                    if (!context.Parts.Any(p => p.Id == part.PartId))
                    {
                        continue;
                    }

                    car.PartsCars.Add(new PartCar
                    {
                        PartId = part.PartId,
                    });  

                }

                carsModels.Add(car);

            }

            context.Cars.AddRange(carsModels);
            context.SaveChanges();
            return $"Successfully imported {carsModels.Count}";

        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var mapper = CreateMapper.InitialMapper();

            ImportPartDto[] partsDtos = XmlHelper.Deserialize<ImportPartDto[]>(inputXml, "Parts");

            List<Part> partsModels = new List<Part>();

            foreach (var dto in partsDtos)
            {
                if (!context.Suppliers.Any(s => s.Id == dto.SupplierId))
                {
                    continue;
                }

                Part part = mapper.Map<Part>(dto);

                partsModels.Add(part);
            }

            context.Parts.AddRange(partsModels);

            context.SaveChanges();

            return $"Successfully imported {partsModels.Count}";

        }
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            //Serialize + Deserialize 

            //XmlRootAttribute root = new XmlRootAttribute("Suppliers");
            //XmlSerializer Ser = new XmlSerializer(typeof(ImportSupplierDto[]),root);

            //StreamReader reader = new StreamReader(inputXml);

            //ImportSupplierDto[] supliersDtos = (ImportSupplierDto[])Ser.Deserialize(reader);

            ImportSupplierDto[] suppliersDtos = XmlHelper.Deserialize<ImportSupplierDto[]>(inputXml, "Suppliers");

            IMapper mapper = CreateMapper.InitialMapper();




            var suppliersModels = mapper.Map<Supplier[]>(suppliersDtos);

            context.Suppliers.AddRange(suppliersModels);

            context.SaveChanges();

            return $"Successfully imported {suppliersModels.Length}";


        }
    }
}