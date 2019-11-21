namespace XMLDemo
{
    using System;
    using System.IO;
    using System.Xml.Linq;
    using XMLDemo.Models;

    class Program
    {
        static void Main(string[] args)
        {
            XDocument document = XDocument.Load("Data/cars.xml");
            //document.Declaration.Version = "3.0";
            //document.Root.Add(new XElement("car", new XElement("make", "Audi")));
            //document.Save("Data/cars_updated.xml");

            XElement root = document.Root;
            var cars = root.Elements();

            foreach (var car in cars)
            {
                var carObject = new Car()
                {
                    Make = car.Element("make").Value,
                    Model = car.Element("model").Value,
                    TravelledDistance = long.Parse(car.Element("travelled-distance").ToString())
                };

                Console.WriteLine($"{car.Element("make")} {car.Element("model").Value}");
            }
        }
    }
}
