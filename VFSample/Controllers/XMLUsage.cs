// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
namespace VFSample.Controllers
{
    [XmlRoot("users")]
    public class UserList
    {
        [XmlElement("user")]
        public List<User> Users { get; set; }
    }

    public class User
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("birthdate")]
        public DateTime BirthDate { get; set; }
        [XmlElement("isactive")]
        public bool IsActive { get; set; }
    }

    public class UserData
    {
        public void GetObject()
        {
            string xmlConfig = @"<users>
                          <user>
                            <id>1</id>
                            <name>Rama</name>
                            <birthdate>2000-01-30</birthdate>
                            <isactive>true</isactive>
                          </user>
                        </users>";

            StringReader rdr = new StringReader(xmlConfig);
            XmlSerializer ser = new XmlSerializer(typeof(UserList));
            var obj = ser.Deserialize(rdr) as UserList;
            //Console.WriteLine(obj.GetType());
        }
    }
}