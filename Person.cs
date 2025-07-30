using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4._1._1
{
    public class Person
    {
        public static Dictionary<string, Person> personsDict = new();
        public static BindingList<Person> personsBindingList = new();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Address { get; set; }
    }
}
