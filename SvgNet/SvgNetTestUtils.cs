using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using NUnit.Framework;

namespace SvgNet.SvgGdi
{
    ///<summary>
    /// A utility class for shared test methods.
    ///</summary>
    public class SvgNetTestUtils
    {
        ///<summary>
        /// Checks the given <c>XmlElement</c> to see if it is valid SVG.
        /// This is not really a very good test, but it is an improvement
        /// on what was being used before.
        ///</summary>
        ///<param name="root">The XmlElement to test.</param>
        public static void AssertValidSvg(XmlElement root)
        {
            Assert.NotNull(root);

            Assert.That(root.HasChildNodes);
            Assert.That(root.LocalName, Is.EqualTo("svg"));

            List<XmlNode> nodes = root.ChildNodes.Cast<XmlNode>().ToList();
            ICollection nodeNames = List.Map(nodes).Property("LocalName");

            Assert.That(nodeNames, Has.Some.EqualTo("g"));
            Assert.That(nodeNames, Has.Some.EqualTo("defs"));

            List<XmlNode> gNodeChildren = nodes.Find(n => n.Name.Equals("g")).ChildNodes.Cast<XmlNode>().ToList();
            ICollection gNodeChildrenNames = List.Map(gNodeChildren).Property("LocalName");

            Assert.That(gNodeChildrenNames, Has.Some.EqualTo("line"));
            Assert.That(gNodeChildrenNames, Has.Some.EqualTo("text"));
            Assert.That(gNodeChildrenNames, Has.Some.EqualTo("rect"));

            List<XmlNode> defsNodeChildren = nodes.Find(n => n.Name.Equals("defs")).ChildNodes.Cast<XmlNode>().ToList();
            ICollection defsNodeChildrenNames = List.Map(defsNodeChildren).Property("LocalName");

            Assert.That(defsNodeChildrenNames, Has.Some.EqualTo("clipPath"));

            // TODO? Would like to validate the SVG against a Schema or DTD, but two
            //       separate serious attempts have ended in dismal failure.
        }
    }
}