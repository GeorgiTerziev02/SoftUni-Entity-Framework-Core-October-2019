namespace XMLDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Linq;
    using System.Xml.Serialization;
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
            //Console.WriteLine(root.Attribute("count").Value);
            //root.SetAttributeValue("count", "220");
            //Console.WriteLine(root.Attribute("count").Value);

            var books = new List<Book>()
            {
                new Book("Little","ooo",1943),
                new Book("Big","ofo",1950)
            };

            XmlSerializer xmlSerializer = new XmlSerializer(books.GetType());
            using (var writer = new StreamWriter("books_new.xml"))
            {
                xmlSerializer.Serialize(writer, books);
            }

            foreach (var car in cars)
            {
                var carObject = new Car()
                {
                    Make = car.Element("make").Value,
                    Model = car.Element("model").Value,
                    TravelledDistance = long.Parse(car.Element("travelled-distance").Value.ToString())
                };

                //car.Element("make").Value = "BMW";

                //Console.WriteLine(carObject.ToString());
            }

            document.Save("Data/car_modified.xml");

            XmlSerializer carSerializer = new XmlSerializer(typeof(XmlCars));
            using var reader = new StreamReader("Data/cars.xml");
            var carsToPrint = (XmlCars)carSerializer.Deserialize(reader);

            foreach (var car in carsToPrint.Cars)
            {
                Console.WriteLine(car);
            }

            XDocument settings = new XDocument();
            settings.Add(new XElement("settings", new XAttribute("lang", "bg-bg"), new XElement("Xposition", "123"), new XElement("Ypos", "333")));

            settings.Save("setting.xml");

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(File.OpenWrite("books.bin"), books);
        }
    }
}
