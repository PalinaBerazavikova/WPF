using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Serialization
{
    [Serializable]
    public class Catalog
    {
        public List<Book> ListBook = new List<Book>();
        public string Xmlns;
        public DateTime Date;
        public const string DateFormat = "yyyy-MM-dd";
        public const string Isbn = "isbn";
        public const string Author = "author";
        public const string EmptyString = "";
        public const string Title = "title";
        public const string Publisher = "publisher";
        public const string Description = "description";

        internal void Fill(string pathToXmlFile)
        {
            XDocument doc = XDocument.Load(pathToXmlFile, LoadOptions.SetBaseUri);
            Xmlns = doc.Root.Attribute("xmlns").Value;
            XNamespace xmlns = Xmlns;
            Date = DateTime.ParseExact(doc.Root.Attribute("date").Value, DateFormat, System.Globalization.CultureInfo.CurrentCulture);
            var books = doc.Element(xmlns + "catalog").Elements(xmlns + "book")
            .Select(b => new Book
            {
                Id = b.Attribute("id").Value,
                Isbn = this.GetData(xmlns, b, Isbn),
                Author = this.GetData(xmlns, b, Author),
                Title = this.GetData(xmlns, b, Title),
                Genre = ParseGenre(b.Element(xmlns + "genre").Value),
                Publisher = this.GetData(xmlns, b, Publisher),
                PublishDate = this.ParseDateTime(xmlns, b, "publish_date"),
                Description = this.GetData(xmlns, b, Description),
                RegistrationDate = this.ParseDateTime(xmlns, b, "registration_date"),
            })
            .ToList();
            ListBook.AddRange(books);
        }

        public DateTime ParseDateTime(XNamespace xmlns, XElement b, string str)
        {
            return DateTime.ParseExact(b.Element(xmlns + str).Value, DateFormat, System.Globalization.CultureInfo.CurrentCulture);
        }
        public string GetData(XNamespace xmlns, XElement x, string str)
        {
            return x.Element(xmlns + str) == null ? EmptyString : x.Element(xmlns + str).Value;
        }

        private Genre ParseGenre(string genre)
        {
            Genre genreEnumValue = Genre.Undefined;
            switch (genre)
            {
                case "Computer":
                    genreEnumValue = Genre.Computer;
                    break;
                case "Fantasy":
                    genreEnumValue = Genre.Fantasy;
                    break;
                case "Romance":
                    genreEnumValue = Genre.Romance;
                    break;
                case "Horror":
                    genreEnumValue = Genre.Horror;
                    break;
                case "ScienceFiction":
                    genreEnumValue = Genre.ScienceFiction;
                    break;
                default:
                    genreEnumValue = Genre.ScienceFiction;
                    break;
            }
            return genreEnumValue;
        }

        public enum Genre
        {
            Undefined = 0,
            Computer,
            Fantasy,
            Romance,
            Horror,
            ScienceFiction
        }
    }
}
