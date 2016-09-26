using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace InvoiceConverter.Domain.Formats
{
    public class MyXML
    {
        private static Encoding ANSI = Encoding.GetEncoding(1251);
        private XmlDocument doc;
        private string filePath;

        public MyXML(string filePath, string rootElement)
        {
            this.filePath = filePath;

            XmlTextWriter textWritter = new XmlTextWriter(filePath, ANSI);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement(rootElement);
            textWritter.WriteEndElement();
            textWritter.Close();

            doc = new XmlDocument();
            doc.Load(filePath);
        }

        public XmlNode CreateXMLNode(string nodeName, string innerText, XmlNode parrent = null)
        {
            XmlNode node = doc.CreateElement(nodeName);
            node.InnerText = innerText;

            if (parrent == null)
                AppendDoc(node);
            else
                AppendNode(parrent, node);

            return node;
        }

        private void AppendDoc(XmlNode node)
        {
            doc.DocumentElement.AppendChild(node);
        }

        private void AppendNode(XmlNode parrent, XmlNode node)
        {
            parrent.AppendChild(node);
        }

        public void Save()
        {
            doc.Save(filePath);
        }
    }
}
