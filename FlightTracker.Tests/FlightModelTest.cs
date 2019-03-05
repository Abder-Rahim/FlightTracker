/* 
 Date : 05/03/2019
 Author : developpeur-csharp.com
 Project : FlightTracker.Tests
*/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightTracker.Tests
{
    [TestClass]
    public class FlightModelTest
    {

        [TestMethod]
        public void Add_writes_modify_in_database()
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_modify_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new FlyingContext (options))
            {
                // Create a new flight
                var flight = new Flight();
                flight.FlightId = 999;
                flight.DepartureName = "DepartureName";
                flight.DepartureLatitude = "DepartureLatitude";
                flight.DepartureLongitude = "DepartureLongitude";
                flight.DestinationName = "DestinationName";
                flight.DestinationLatitude = "DestinationLatitude";
                flight.DestinationLongitude = "DestinationLongitude";
                flight.FuelAmount = 999;
                flight.FuelConsumption = "FuelConsumption";
                flight.FlightTime = TimeSpan.FromMinutes(999);
                flight.TakeoffEffort = "TakeoffEffort";
                flight.Distance = 999;
                flight.Speed = 999;

                // Save the flight in the data base
                context.Add(flight);
                context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved
            using (var context = new FlyingContext(options))
            {
                Assert.AreEqual(1, context.Flights.Count());
                Assert.AreEqual(999, context.Flights.Single().FlightId);
                Assert.AreEqual("DepartureName", context.Flights.Single().DepartureName);
                Assert.AreEqual("DepartureLatitude", context.Flights.Single().DepartureLatitude);
                Assert.AreEqual("DepartureLongitude", context.Flights.Single().DepartureLongitude);
                Assert.AreEqual("DestinationName", context.Flights.Single().DestinationName);
                Assert.AreEqual("DestinationLatitude", context.Flights.Single().DestinationLatitude);
                Assert.AreEqual("DestinationLongitude", context.Flights.Single().DestinationLongitude);
                Assert.AreEqual(999, context.Flights.Single().FuelAmount);
                Assert.AreEqual("FuelConsumption", context.Flights.Single().FuelConsumption);
                Assert.AreEqual(TimeSpan.FromMinutes(999), context.Flights.Single().FlightTime);
                Assert.AreEqual("TakeoffEffort", context.Flights.Single().TakeoffEffort);
                Assert.AreEqual(999, context.Flights.Single().Distance);
                Assert.AreEqual(999, context.Flights.Single().Speed);
            }

            // Modify some fields, unmodified fields must keep their old values
            using (var context = new FlyingContext(options))
            {
                var flight = context.Flights.Find(999);
                Assert.AreNotEqual(null, flight);
                flight.DepartureName = "DepartureName+";
                flight.DestinationName = "DestinationName+";
                flight.FuelAmount = 888;

                // Save flight entity in database
                context.Update(flight);
                context.SaveChangesAsync();

            }

            // Use a separate instance of the context to verify correct data was saved
            using (var context = new FlyingContext(options))
            {
                Assert.AreEqual(1, context.Flights.Count());
                Assert.AreEqual(999, context.Flights.Single().FlightId);
                Assert.AreEqual("DepartureName+", context.Flights.Single().DepartureName);
                Assert.AreEqual("DepartureLatitude", context.Flights.Single().DepartureLatitude);
                Assert.AreEqual("DepartureLongitude", context.Flights.Single().DepartureLongitude);
                Assert.AreEqual("DestinationName+", context.Flights.Single().DestinationName);
                Assert.AreEqual("DestinationLatitude", context.Flights.Single().DestinationLatitude);
                Assert.AreEqual("DestinationLongitude", context.Flights.Single().DestinationLongitude);
                Assert.AreEqual(888, context.Flights.Single().FuelAmount);
                Assert.AreEqual("FuelConsumption", context.Flights.Single().FuelConsumption);
                Assert.AreEqual(TimeSpan.FromMinutes(999), context.Flights.Single().FlightTime);
                Assert.AreEqual("TakeoffEffort", context.Flights.Single().TakeoffEffort);
                Assert.AreEqual(999, context.Flights.Single().Distance);
                Assert.AreEqual(999, context.Flights.Single().Speed);
            }
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void Add_several_flights_to_database(int id)
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
                .UseInMemoryDatabase(databaseName: "Add_select_several_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new FlyingContext(options))
            {
                // Create a new flight
                var flight = new Flight();
                flight.FlightId = id;
                flight.DepartureName = "DepartureName" + id;
                flight.DepartureLatitude = "DepartureLatitude" + id;
                flight.DepartureLongitude = "DepartureLongitude" + id;
                flight.DestinationName = "DestinationName" + id;
                flight.DestinationLatitude = "DestinationLatitude" + id;
                flight.DestinationLongitude = "DestinationLongitude" + id;
                flight.FuelAmount = 999 + id;
                flight.FuelConsumption = "FuelConsumption" + id;
                flight.FlightTime = TimeSpan.FromMinutes(999 + id);
                flight.TakeoffEffort = "TakeoffEffort" + id;
                flight.Distance = 999 + id;
                flight.Speed = 999 + id;

                // Save the flight in the data base
                context.Add(flight);
                context.SaveChangesAsync();
            }

        }

        [TestMethod]
        public void Select_all_flights_from_database()
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
                .UseInMemoryDatabase(databaseName: "Add_select_several_database")
                .Options;

            using (var context = new FlyingContext(options))
            {
                Assert.AreEqual(5, context.Flights.Count());
                foreach (Flight flight in context.Flights)
                {
                    Assert.AreEqual("DepartureName" + flight.FlightId, flight.DepartureName);
                    Assert.AreEqual("DestinationName" + flight.FlightId, flight.DestinationName);
                    Assert.AreEqual(999 + flight.FlightId, flight.FuelAmount);
                }
            }

        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void Select_one_flight_from_database(int id)
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
                .UseInMemoryDatabase(databaseName: "Add_select_several_database")
                .Options;

            // Select a flight by its id and check the data it contains
            using (var context = new FlyingContext(options))
            {
                var flight = context.Flights.Find(id);
                Assert.AreNotEqual(null, flight);
                Assert.AreEqual(id, flight.FlightId);
                Assert.AreEqual("DepartureName" + id, flight.DepartureName);
                Assert.AreEqual("DestinationName" + id, flight.DestinationName);
                Assert.AreEqual(999 + id, flight.FuelAmount);
            }
        }

    }
}
