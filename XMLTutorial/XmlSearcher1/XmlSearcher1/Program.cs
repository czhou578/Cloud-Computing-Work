// See https://aka.ms/new-console-template for more information
using System;
using System.Xml;

namespace XmlSearcher1
{
    public class XmlSearcher1
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Data/PurchaseOrders.xml");

            XmlElement root = doc.DocumentElement;
            string orderNum = "99505";

            XmlNode purchase = root.SelectSingleNode("PurchaseOrder[@PurchaseOrderNumber=\"" + orderNum + "\"]");
            //XmlNodeList results = root.SelectNodes("PurchaseOrder/Address");
            //Console.WriteLine(results);

            if (purchase != null)
            {
                XmlNodeList items = purchase.SelectNodes("Items/Item");
                if (items != null)
                {
                    Console.WriteLine("There are {0} items in order number {1}", items.Count, orderNum);
                }
                //Console.WriteLine("Found {0} orders\n", results.Count);
                //foreach (XmlNode address in purchase)
                //{
                //    //XmlNode deliveryNote = order.SelectSingleNode("DeliveryNotes");
                //    XmlNode zipNode = address.SelectSingleNode("Zip");
                //    if (zipNode != null)
                //    {
                //        Console.WriteLine("{0}", zipNode.InnerText);
                //    }
                //    //Console.WriteLine("{0}", order.InnerText);
                //    //if (deliveryNote != null)
                //    //{

                //    //}
                //}
            }
        }

    }

}

