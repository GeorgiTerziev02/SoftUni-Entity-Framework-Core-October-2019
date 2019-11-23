namespace XMLDemo.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType("book")]
    public class Book
    {
        public Book()
        {

        }

        public Book(string title, string author, int year)
        {
            this.Title = title;
            this.Author = author;
            this.Year = year;
        }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlAttribute]
        public int Year { get; set; }
    }
}
