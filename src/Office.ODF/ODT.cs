// Copyright (c) 2016 Lukasz Jaskiewicz. All Rights Reserved
// Licenced under the European Union Public Licence 1.1 (EUPL v.1.1)
// See licence.txt in the project root for licence information
// Written by Lukasz Jaskiewicz (lukasz@jaskiewicz.de)

namespace Net.DevDone.Office.ODF
{
    using System;
    using System.Xml;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Common;

    using NodeAttrs = System.Collections.Generic.Dictionary<string, string>;
    using NodeChilds = System.Collections.Generic.List<Net.DevDone.Web.Common.HTMLNode>;
    using NameTransformer = System.Collections.Generic.Dictionary<string, string>;

    public class ODT
    {
        NameTransformer TagNameTransformer = new NameTransformer()
        {
            { ODFTag.office.text, HTMLTag.article }
        };

        NameTransformer AttrNameTransformer = new NameTransformer()
        {
            { ODFAttr.text.style_name, HTMLAttr.class_ }
        };

        public string ToHTML(Stream contentXmlStream)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(contentXmlStream);
            var bodyNode = xmldoc.GetElementsByTagName(ODFTag.office.body).Item(0);

            HTMLNode he = new HTMLNode(HTMLTag.html);
            var head = he.NewChild(HTMLTag.head);
            var body = he.NewChild(HTMLTag.body);

            NodeWalker(bodyNode.ChildNodes, body);

            StringWriter strWriter = new StringWriter();
            he.GetHTML(strWriter);
            return strWriter.GetStringBuilder().ToString();
        }

        public void NodeWalker(XmlNodeList childs, HTMLNode htmlNode)
        {
            foreach (XmlNode child in childs)
            {
                if (child.NodeType == XmlNodeType.Element)
                {
                    var htmlChild = htmlNode.NewChild(
                        TagNameTransformer.ContainsKey(child.Name) ? TagNameTransformer[child.Name] : child.LocalName,
                        child.Value,
                        AddAttributes(child));

                    if (child.HasChildNodes)
                    {
                        NodeWalker(child.ChildNodes, htmlChild);
                    }

                    PostProcessHTMLNode(child, htmlChild);
                }
                else if (child.NodeType == XmlNodeType.Text)
                {
                    htmlNode.SetVal(child.Value);
                }
            }
        }

        private NodeAttrs AddAttributes(XmlNode xmlNode)
        {
            NodeAttrs attrs = new NodeAttrs();

            var attrNode = xmlNode.Attributes.GetNamedItem(ODFAttr.text.style_name);

            if (attrNode != default(XmlNode))
            {
                var name = AttrNameTransformer.ContainsKey(attrNode.Name) ? AttrNameTransformer[attrNode.Name] : attrNode.LocalName;
                attrs.Add(name, "WebODT" + attrNode.Value);
            }

            return attrs;
        }

        private void PostProcessHTMLNode(XmlNode xmlNode, HTMLNode htmlEle)
        {
            if (xmlNode.Name.Equals(ODFTag.text.h, StringComparison.OrdinalIgnoreCase))
            {
                var attrNode = xmlNode.Attributes.GetNamedItem(ODFAttr.text.outline_level);

                if (attrNode != default(XmlNode))
                {
                    htmlEle.SetTag(xmlNode.LocalName + attrNode.Value);
                }
            }
        }
    }
}
