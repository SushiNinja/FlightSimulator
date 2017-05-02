using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    class Customer
    {
        //Λίστα στην οποία θα αποθηκεύονται οι πελάτες
        public string customer_Name { get; set; }

        public int passport_Number { get; set; }

        public string ethnicity { get; set; }

        public string address { get; set; }

        public int phone_Number { get; set; }
    }
}
