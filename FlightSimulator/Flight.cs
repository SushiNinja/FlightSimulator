using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{

    public class Flight 
    {
        
        public static int flight_uid=1;
        public int  seat_number, flight_id, book_number, ticket_price;
        public DateTime depart_time, arrival_time;
        public string start, destination, aero_type;

        //Κάθε πτήση θα έχει μια ουρά πελατών στην οποία θα καταγράφονται όλοι όσοι 
        //περιμένουν να υπάρξει ακυρωση πτήσης.
        private Node head;
        private Node current;
        public int Count;

        static Random rnd = new Random();
        int rndom;

        string[] countries = new string[] { "Athina", "Kalamata", "Thessaloniki", "Trikala",
                                              "Santorini"};

        

        // Default Constructor για την δημιουργία πτήσεων
        public Flight() {

            flight_id = flight_uid++;
            seat_number = 5;
            book_number = rnd.Next(0, 6);
            ticket_price = 200;

            //Duration είναι μία τυχαία μεταβλητή που θα την προσθέσουμε στο depart_time για να μας δώσει τυχαία ώρα
            TimeSpan duration = new TimeSpan(rndom = rnd.Next(0, 4), rndom = rnd.Next(0, 60), rndom = rnd.Next(0, 60));
            depart_time = DateTime.Now;
            depart_time = depart_time.Add(duration);
            
            duration = new TimeSpan(rndom = rnd.Next(0,4), rndom = rnd.Next(0, 60), rndom = rnd.Next(0, 60));
            arrival_time = depart_time.Add(duration);

            start = countries[rndom=rnd.Next(0,5)];
            do
            {
                destination = countries[rndom = rnd.Next(0,5)];
            } while (start == destination);

            aero_type = "Boeing 747";

            head = new Node();
            current = head;

        }

        public void AddAtLast(string name)
        {
            Node newNode = new Node();
            newNode.Name = name;
            current.Next = newNode;
            current = newNode;
            Count++;
        }

        //Μέθοδος για την εκτύπωση της ουράς της πτήσης
        public void PrintAllNodes()
        {
            Console.Write("Priority in the Queue->");
            Node curr = head;
            while (curr.Next != null)
            {
                curr = curr.Next;
                Console.Write(curr.Name);
                Console.Write("-> ");
            }
            Console.Write(" End of the line");
        }

        //Μέθοδος στην οποία ο 1ος πελάτης στην ουρα της πτήσης διαγράφεται.
        public void Remove()
        {
            if (Count > 0)
            {
                head.Next = head.Next.Next;
                Count--;
            }
            else
            {
                Console.WriteLine("There is no queue on this flight");
            }
        }


        public override string ToString(){
            return("Flight ID number " +flight_id+
                              "\nSeat Number " + seat_number +
                              "\nBook Number " + book_number +
                              "\nTicket Price " + ticket_price +
                              "\nDate Time " + depart_time +
                              "\nArrival Time " + arrival_time+
                              "\nStart " + start +
                              "\nDestination " + destination +
                              "\nAeroplane Type " + aero_type+
                              "\n");
        }
    }
}
