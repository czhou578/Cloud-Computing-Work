// See https://aka.ms/new-console-template for more information
using System.Xml;

namespace XmlSearcher2
{
    public class XmlSearcher2
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Data/CustomersOrders.xml");

            XmlElement root = doc.DocumentElement;
            Console.WriteLine(root);

            //string id = "LETSS";
            int count = 0;
            string id = "LAZYK";
            string city = "Eugene";
            //XmlNode result = root.SelectSingleNode("//Customers/Customer[@CustomerID=\"" + id + "\"]");

            //XmlNode result = root.SelectSingleNode("//Customers/Customer[@CustomerID=\"" + id + "\"]");
            //Console.WriteLine("colin: " + result.InnerText);

            //XmlNodeList result = root.SelectNodes("//Customers/Customer");
            //XmlNodeList result = root.SelectNodes("//Orders/Order");

            XmlNodeList result = root.SelectNodes("//Orders/Order/CustomerID[text()=\"" + id + "\"]");
            //XmlNodeList result = root.SelectNodes("//Orders/Order/ShipInfo/ShipCity[text()=\"" + city + "\"]");

            if (result != null)
            {
                //foreach (XmlNode node in result)
                //{
                //    count++;
                //}

                foreach (XmlNode node in result)
                {
                    count++;
                }

                //XmlNode customers = result.SelectSingleNode("FullAddress");
                //foreach (XmlNode customer in customers.ChildNodes)
                //{
                //    Console.WriteLine(customer.InnerText);

                //}

                //foreach (XmlNode customer in customers)
                //{
                //    Console.WriteLine("{0}", customer.InnerText);
                //}

                //XmlNode phone = results.SelectSingleNode("Phone");

                //Console.WriteLine("{0}", phone.InnerText);
                //foreach (XmlNode phoneNode in phone)
                //{
                //    //if (phone != null)
                //    //{
                //    //}

                //}

            }
            else
            {
                Console.WriteLine("colin here");
            }

            Console.WriteLine(count);
        }
    }
}
