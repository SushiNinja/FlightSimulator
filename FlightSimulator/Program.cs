using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{

    class Program
    {      
        //Αρχική οθόνη στην οποία η χρήστης θα επιλέγει τι θέλει να κάνει.
        private static int startingScreen()
        {
            int value = 0;
            string choice;
            bool condition = true;

            while (condition)
            {
                Console.Clear();
                Console.WriteLine("What would you want to do? \n" +
                "1.View all flights \n" +
                "2.Arrange a flight \n" +
                "3.Exit \n");
                choice = Console.ReadLine();

                //If can convert string(choice) to int(value) and 1<=value<=3 then
                if ((Int32.TryParse(choice, out value)) && value >= 1 && value <= 3)
                {
                    condition = false;
                }
            };

            return value;
        }


        //Αρχικοποίηση κάποιων πτήσεων
        private static void initializeFlights(List<Flight> obj)
        {
            for (int i=0;i<10;i++) {
                obj.Add(new Flight());
            }
        }

        //Μέθοδος για να εκτυπώσουμε τις πτήσεις
        private static void showFlights(List<Flight> obj,string msg)
        {

            foreach (Flight fl in obj)
            {
                Console.WriteLine(fl);
            }
            
            Console.Write(msg);
        }
        //Μέθοδος για να εκτυπώσουμε τους κωδικούς των πτήσεων.
        private static void showFlightsCodes(string msg) {

            Console.WriteLine("{0}",msg);
            Console.WriteLine("Code 0001: Athina->Thes/niki\n" +
                              "Code 0010: Thes/niki-> Kalamata\n" +
                              "Code 0011: Santorini->Trikala\n" +
                              "Code 0100: Kalamata->Athina\n" +
                              "Code 0101: Kalamata->Trikala\n");
             
        }

        private static int validateFlightCode(string choice) {

            string[] codes = {"0001","0010","0011","0100","0101"};

            if (codes.Contains(choice))
            {
                return 1;
            }
            else return 0;

            return 0;

        }

        //Μέθοδος στην οποία θα ψάξουμε με flight code ID(choice variable)θα ψάξουμε
        //την λίστα με τις πτήσεις και αν υπάρχει θα επιστρέψουμε το ID της πτήσης 
        //αλλιώς αν δεν υπάρχει θα επιστρέψουμε 0.

        private static int validateFlight(List<Flight> obj, string choice) {

            Console.Clear();
            List<Flight> list = null;

            switch (choice) {
                case "0001":
                    list = obj.FindAll(x => (x.start == "Athina") && (x.destination == "Thessaloniki"));
                    Console.WriteLine("{0} flights have been found with start:Athina and destination:Thessaloniki",
                    list.Count);
                    break;
                case "0010":
                    list = obj.FindAll(x => (x.start == "Thessaloniki") && (x.destination == "Kalamata"));
                    Console.WriteLine("{0} flights have been found with start:Thessaloniki and destination:Kalamata",
                     list.Count);
                    break;
                case "0011":
                    list = obj.FindAll(x => (x.start == "Santorini") && (x.destination == "Trikala"));
                    Console.WriteLine("{0} flights have been found with start:Santorini and destination:Trikala",
                    list.Count);
                    break;
                case "0100":
                    list = obj.FindAll(x => (x.start == "Kalamata") && (x.destination == "Athina"));
                    Console.WriteLine("{0} flights have been found with start:Kalamata and destination:Athina",
                    list.Count);
                    break;
                case "0101":
                    list = obj.FindAll(x => (x.start == "Kalamata") && (x.destination == "Trikala"));
                    Console.WriteLine("{0} flights have been found with start:Kalamata and destination:Trikala",
                    list.Count);
                    break;
            }

            showFlights(list,"");

            //Ο χρήστης μας δίνει την πτήση που ενδιαφέρεται απο την λίστα.
            if (list.Count!=0) {
                Console.WriteLine("Choose the flight you prefer based on the flight id number");
                string flight = Console.ReadLine();
                int number;

                //Αν το result είναι null τότε σημαίνει οτι το TryParse απέτυχε και ο χρήστης
                //δεν έχει δώσει σωστό format.Άν το isInside είναι 0 τότε σημαίνει οτι ο 
                //κωδικός δεν είναι μεσα στην λίστα.
                bool result = Int32.TryParse(flight, out number);
                bool isInside = list.Exists(x => x.flight_id == number);

                //Άν ο χρήστης δεν έχει δώσει σωστή πτήση τότε κανε Loop μέχρι να δώσει.
                while (!result || !isInside) {
                    Console.Clear();
                    Console.WriteLine("You have chosen an incorrect flight id number. \n"+
                        "Choose the flight you prefer based on the flight id number. \n");

                    showFlights(list, "");
                    flight = Console.ReadLine();
                    result = Int32.TryParse(flight, out number);
                    isInside = list.Exists(x => x.flight_id == number);

                }
                Console.WriteLine("The flight id you chose is"+number);
                return number;
            }
            return 0;
        }

        //Θα προσπαθήσουμε να κλείσουμε την πτήση άν έχει χώρο αλλιώς θα μπεί σε λίστα.
        private static void registerUser(List<Flight> obj,List<Customer> cust,int fl_id) {
            Console.Clear();
            
            //Μεταβλητές που θα μας βοηθήσουν στην καταχώρηση του πελάτη.
            string name, ethn, address,fake;
            int phone,pass;
            
            //Βρίσκουμε την πτήση την οποία ενδιαφερόμαστε να καταχωρήσουμε τον χρήστη.
            Flight flight = obj.Find(x=>x.flight_id==fl_id);

            //Έλεγχος των στοιχείων πριν την καταχώρηση.
            #region
            Console.WriteLine("Give your full name");
            name = Console.ReadLine();
            while (name.Any(char.IsDigit) || name.Length == 0)
            {
                Console.WriteLine("You gave an invalid name. Please give your full name");
                name = Console.ReadLine();
            }

            Console.WriteLine("Give your address");
            address = Console.ReadLine();

            Console.WriteLine("State your ethnicity");
            ethn = Console.ReadLine();
            while (ethn.Any(char.IsDigit) || ethn.Length == 0)
            {
                Console.WriteLine("You gave an invalid input. Please state your ethnicity");
                ethn = Console.ReadLine();
            }

            Console.WriteLine("Give your passport number");
            fake = Console.ReadLine();
            while (Int32.TryParse(fake, out pass) || fake.Length == 0)
            {
                Console.WriteLine("You gave an invalid input. Please give your passport number");
                fake = Console.ReadLine();
            }

            Console.WriteLine("Give your phone number");
            fake = Console.ReadLine();
            while (Int32.TryParse(fake, out phone) || fake.Length == 0)
            {
                Console.WriteLine("You gave an invalid input. Please give your passport number");
                fake = Console.ReadLine();
            }

            #endregion

            //Η πτήση είναι γεμάτη οπότε θα βάλουμε τον πελάτη σε μια σειρά αναμονής
            if (flight.book_number == 5)
            {
                flight.AddAtLast(name);
            }
           //Η πτήση δεν είναι γεμάτη οπότε αυξάνουμε τον αριθμό τον επιβατών κατα 1.
            else              
                flight.book_number++;
            // καταχώρηση χρήστη στην βάση δεδομένων
            cust.Add(new Customer()
            {
                customer_Name = name,
                address = address,
                ethnicity = ethn,
                passport_Number = pass,
                phone_Number = phone,
            });

        }

        private static void bookFlight(List<Flight> obj,List<Customer> cust) {

            //Value είναι η επιλογή της πτήσης 
            int chosenFlight;
            string choice;
            int loop = 0;

            Console.Clear();

            showFlightsCodes("Choose your flight code number");

            choice = Console.ReadLine();

            loop = validateFlightCode(choice);

            // Loop για να δωθεί σωστό flight ID. 
            while (loop==0) {

                Console.Clear();
                showFlightsCodes("The flight code you entered doesn't exist. Please give valid flight code");
                choice = Console.ReadLine();
                loop = validateFlightCode(choice);

            }

            //Ο χρήστης έχει δώσει σωστό flight code id και κάνουμε έλεγχο για το άν υπάρχει 
            //πτήση στην βάση δεδομένων με αυτόν τον κωδικό και άν υπάρχει επιστρέφουμε 
            //το ID της πτήσης.
            chosenFlight = validateFlight(obj,choice);

            //Άν βρέθηκε η πτήση τότε προσπάθησε και κλείσε θέση.
            if (chosenFlight!=0) {

                registerUser(obj,cust,chosenFlight);

            }

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Console.Title = "Flight Simulator 2017";
            List<Flight> flightSchedule = new List<Flight>();
            List<Customer> customerList = new List<Customer>();
            initializeFlights(flightSchedule);


            int i = -1;

            do
            {
                i = startingScreen();

                switch (i)
                {

                    case 1:
                        Console.Clear();
                        showFlights(flightSchedule, "Press any key to continue");
                        Console.ReadKey();
                        break;
                    case 2:
                        bookFlight(flightSchedule, customerList);
                        break;
                    case 3:
                        System.Environment.Exit(-1);
                        break;

                }


            } while (i != 3);

            Console.ReadKey();
        }

        
    }
}
