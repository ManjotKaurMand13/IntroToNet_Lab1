using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Room
    {
        // data members
        public string Number;
        public int Capacity;
        public Boolean occupied;
        public List<Reservation> Reservations;
        public string Rating;

        // constructor
        public Room(string number, int capacity, string rating)
        {
            Number = number;
            Capacity = capacity;
            occupied = false;
            Reservations = new List<Reservation>();
            Rating = rating;
        }
    }

    class Reservation
    {
        // data members
        public DateTime Date;
        public int Occupants;
        public bool IsCurrent;
        public Client Client;
        public Room Room;

        // constructor
        public Reservation(DateTime date, int occupants, bool isCurrent, Client client, Room room)
        {
            Date = date;
            Occupants = occupants;
            IsCurrent = isCurrent;
            Client = client;
            Room = room;
        }
    }

    class Client
    {
        // data members
        public string Name;
        public int CreditCard;
        public List<Reservation> Reservations;

        // Constructor
        public Client(string name, int creditCard)
        {
            Name = name;
            CreditCard = creditCard;
            Reservations = new List<Reservation>();
        }
    }
    class Hotel
    {
        // data members and getters/setters
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Client> Clients { get; set; }

        // Client GetClient(int clientID)
        public Client GetClient(int clientID)
        {
            // loop through clients
            foreach (Client c in Clients)
            {
                // if CreditCard Id matches clientID
                if (c.CreditCard == clientID)
                {
                    // return client
                    return c;
                }
            }
            return null;
        }

        //Reservation GetReservation(int ID)
        public Reservation GetReservation(int ID)
        {
            // loop through reservations
            foreach (Reservation r in Reservations)
            {
                // if ID matches reservation ID
                if (r.Occupants == ID)
                {
                    // return reservation
                    return r;
                }
            }
            return null;
        }

        public Room GetRoom(string roomNumber)
        {
            // loop through rooms
            foreach (Room r in Rooms)
            {
                // if roomNumber matches room number
                if (r.Number == roomNumber)
                {
                    // return room
                    return r;
                }
            }
            return null;
        }

        public List<Room> GetVacantRooms()
        {
            // create list of vacant rooms
            List<Room> vacantRooms = new List<Room>();
            foreach (Room r in Rooms)
            {
                // if room is not occupied
                if (!r.occupied)
                {
                    vacantRooms.Add(r);
                }
            }
            // return list of vacant rooms
            return vacantRooms;
        }

        public List<Client> TopThreeClients()
        {
            // create list of top three clients
            List<Client> topThreeClients = new List<Client>();
            List<int> topThreeClientIDs = new List<int>();
            List<int> topThreeClientCount = new List<int>();

            // loop through clients
            foreach (Client c in Clients)
            {
                // if client has no reservations
                int count = 0;

                // loop through reservations
                foreach (Reservation r in Reservations)
                {
                    // if client ID matches reservation client ID
                    if (r.Client.CreditCard == c.CreditCard)
                    {
                        // increment count
                        count++;
                    }
                }

                // if count is greater than 0
                topThreeClientIDs.Add(c.CreditCard);
                // add count to list of counts
                topThreeClientCount.Add(count);
            }
            for (int i = 0; i < 3; i++)
            {
                // loop through counts
                int max = 0;
                int maxIndex = 0;
                for (int j = 0; j < topThreeClientCount.Count; j++)
                {
                    // if count is greater than max
                    if (topThreeClientCount[j] > max)
                    {
                        // set max to count
                        max = topThreeClientCount[j];
                        // set max index to j
                        maxIndex = j;
                    }
                }
                // add client to top three clients
                topThreeClients.Add(GetClient(topThreeClientIDs[maxIndex]));
                topThreeClientCount.RemoveAt(maxIndex);
                topThreeClientIDs.RemoveAt(maxIndex);
            }
            // return list of top three clients
            return topThreeClients;
        }

        public Reservation AutomaticReservation(int clientID, int occupants)
        {
            // create reservation
            foreach (Room r in Rooms)
            {
                // if room is not occupied
                if (!r.occupied && r.Capacity >= occupants)
                {
                    // create reservation
                    Reservation res = new Reservation(DateTime.Now, occupants, true, GetClient(clientID), r);
                    r.Reservations.Add(res);
                    r.occupied = true;
                    return res;
                }
            }
            return null;
        }

        // write main method here
        public static void Main(string[] args)
        {
            // create hotel
            Hotel hotel = new Hotel();
            hotel.Name = "Hotel California";
            hotel.Address = "123 Main St";
            hotel.Rooms = new List<Room>();
            hotel.Reservations = new List<Reservation>();
            hotel.Clients = new List<Client>();

            // create rooms
            Room room1 = new Room("101", 2, "A");
            Room room2 = new Room("102", 2, "A");
            Room room3 = new Room("103", 2, "A");
            Room room4 = new Room("104", 2, "A");
            Room room5 = new Room("105", 2, "A");

            // add rooms to hotel
            hotel.Rooms.Add(room1);
            hotel.Rooms.Add(room2);
            hotel.Rooms.Add(room3);
            hotel.Rooms.Add(room4);
            hotel.Rooms.Add(room5);


            // create clients
            Client client1 = new Client("John Smith", 123456789);
            Client client2 = new Client("Jane Doe", 987654321);
            Client client3 = new Client("Jack Black", 98765432);

            // add clients to hotel
            hotel.Clients.Add(client1);
            hotel.Clients.Add(client2);
            hotel.Clients.Add(client3);

            // create reservations
            Reservation reservation1 = new Reservation(DateTime.Now, 2, true, client1, room1);
            Reservation reservation2 = new Reservation(DateTime.Now, 2, true, client2, room2);
            Reservation reservation3 = new Reservation(DateTime.Now, 2, true, client3, room3);

            // add reservations to hotel
            hotel.Reservations.Add(reservation1);
            hotel.Reservations.Add(reservation2);
            hotel.Reservations.Add(reservation3);

            // print hotel info
            Console.WriteLine("Hotel Name: " + hotel.Name);
            Console.WriteLine("Hotel Address: " + hotel.Address);
            foreach (Room r in hotel.Rooms)
            {
                Console.WriteLine("Room Number: " + r.Number + " Capacity: " + r.Capacity);
            }

            // print client info
            Console.WriteLine("Client 1: " + client1.Name + " Credit Card: " + client1.CreditCard);

            // print reservation info
            Console.WriteLine("Reservation 1: " + reservation1.Occupants + " " + reservation1.Room.Number);

            // print top three clients
            Console.WriteLine("Top Three Clients: ");
            foreach (Client c in hotel.TopThreeClients())
            {
                Console.WriteLine("Client: " + c.Name + " Credit Card: " + c.CreditCard);
            }

            // call automatic reservation here
            Reservation res = hotel.AutomaticReservation(123456789, 2);
            Console.WriteLine("Automatic Reservation: " + res.Room.Number);

        }

    }
}



