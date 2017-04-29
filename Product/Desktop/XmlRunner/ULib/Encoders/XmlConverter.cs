using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ULib.Encoders
{
    /// <summary>
    /// The XmlConvertor class provides static methods
    /// to convert an XML string to / from an object.
    /// </summary>
    public class XmlConvertor
    {
        private const string CONVERT_EXCEPTION_MSG = "Cannot convert XML to Object. Possible reason could be the XML is not well formed or invalid.";
        private const string UNEXPECTED_EXCEPTION_MSG = "Cannot convert XML to Object even when the correctness of te XML has been checked. This exception indicates a software bug.";

        /// <summary>
        /// Constructor.
        /// </summary>
        private XmlConvertor()
        {
            // XmlConvertor only provides static methods, so do not try to instantiate it.
        }

        /// <summary>
        /// Converts a string of xml into an object.
        /// </summary>
        /// <param name="xml">A string of xml</param>
        /// <param name="type">object's type</param>
        /// <returns>An object</returns>
        public static object XmlToObject(string xml, Type type)
        {
            // Check the input arguments.
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            object o = null;
            bool success = false;

            while (!success)
            {
                // Create a serializer for the type.
                XmlSerializer serializer = new XmlSerializer(type);

                // Deserialize the XML string to object.                      
                try
                {
                    using (StringReader strReader = new StringReader(xml))
                    {
                        using (XmlReader reader = XmlReader.Create(strReader))
                        {
                            o = serializer.Deserialize(reader);
                        }
                    }
                    success = true;
                }
                catch (Exception error)
                {
                    error.Handle();
                    throw error;
                }
            }

            return o;
        }

        /// <summary>
        /// Converts an object into a string of xml. Currently only support UTF8 output.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <returns>A string of xml</returns>
        public static string ObjectToXml(object obj)
        {
            return ObjectToXml(obj, false);
        }

        /// <summary>
        /// Converts an object into a string of xml. Currently only support UTF8 output.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <param name="toBeIndented">If <c>True</c>, the result will be formatted.</param>
        /// <returns>A string of xml</returns>
        public static string ObjectToXml(object obj, bool toBeIndented)
        {
            UTF8Encoding encoding = new UTF8Encoding(false);
            return ObjectToXml(obj, toBeIndented, encoding);
        }

        public static void SerializeToXML(object target, string filename)
        {
            if (target != null)
            {
                XmlSerializer serializer = new XmlSerializer(target.GetType());
                TextWriter textWriter = new StreamWriter(filename);
                serializer.Serialize(textWriter, target);
                textWriter.Close();
            }
        }

        /// <summary>
        /// Converts an object into a string of xml. Currently only support UTF8 output.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <param name="toBeIndented">If <c>True</c>, the result will be formatted.</param>
        /// <param name="encoding">The <c>Encoding</c> object.</param>
        /// <returns>A string of xml</returns>
        public static string ObjectToXml(object obj, bool toBeIndented, Encoding encoding)
        {
            // Check the input arguments.
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            string xml = string.Empty;
            /* Add retry machanism here because in case the auto compiled dll for
             * serialization cannot be found then recreate the XmlSerializer can
             * regenerate the dll.
             * */
            int retry = 1;
            bool success = false;

            while (!success)
            {
                try
                {
                    // Serialize the object to XML string.
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (XmlTextWriter writer = new XmlTextWriter(stream, encoding))
                        {
                            writer.Formatting = (toBeIndented ? Formatting.Indented : Formatting.None);
                            serializer.Serialize(writer, obj);
                            xml = encoding.GetString(stream.ToArray());
                        }
                    }
                    success = true;
                }
                catch (FileNotFoundException error)
                {
                    if (retry > 0)
                        retry--;
                    else
                        error.Handle();
                }
                catch (Exception error)
                {
                    error.Handle();
                }
            }

            return xml;
        }
    }
}
