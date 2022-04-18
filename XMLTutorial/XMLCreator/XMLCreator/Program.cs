// See https://aka.ms/new-console-template for more information

using System;
using System.Xml;

namespace XMLCreator
{
    class Program
    {
        static void main(String[]args)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<book />");
            XmlElement rootElement = xmlDoc.DocumentElement;

            XmlElement child = xmlDoc.CreateElement("title");
            child.InnerText = "Saipiens";
            rootElement.AppendChild(child);

            child = xmlDoc.CreateElement("author");
            child.InnerText = "yuval";
            rootElement.AppendChild (child);

            child = xmlDoc.CreateElement("isbn");
            child.InnerText = "987980-123123";
            rootElement.AppendChild(child);

            child = xmlDoc.CreateElement("publisher");
            child.InnerText = "publisher";
            rootElement.AppendChild(child);

            child = xmlDoc.CreateElement("genre");
            child.SetAttribute("name", "science");
            rootElement.AppendChild(child);

            child = xmlDoc.CreateElement("price");
            child.SetAttribute("currency", "USD");
            child.InnerText = "20.99";
            rootElement.AppendChild(child);

            XmlElement acknowledgeElement = xmlDoc.CreateElement("ack");
            rootElement.AppendChild(acknowledgeElement);

            string[] ackTexts = new string[]
            {
                "ack1",
                "ack2",
                "ack3"
            };

            foreach (string ack in ackTexts)
            {
                string[] data = ack.Split(new char[] { ':' });
                child = xmlDoc.CreateElement("acknowledgement");
                child.SetAttribute("source", data[0]);
                child.InnerText = data[1];
                acknowledgeElement.AppendChild(child);
            }

            xmlDoc.Save("book.xml");
        }
    }
}