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
        #region Private Members
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

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
                flight.DepartureName = "Ronald Reagan Washington National Airport";
                flight.DepartureLatitude = 38.8521d;
                flight.DepartureLongitude = -77.037697d;
                flight.DestinationName = "Ken Jones Airport";
                flight.DestinationLatitude = 18.1987991333d;
                flight.DestinationLongitude = -76.53450012210001d;
                flight.FuelConsumption = 1.26d;
                flight.TakeoffEffort = 1.001d;
                flight.Speed = 500;
                flight.FuelAmount = 999;
                flight.FlightTime = TimeSpan.FromMinutes(999);
                flight.Distance = 999;

                // Save the flight in the data base
                context.Add(flight);
                context.SaveChangesAsync();
            }

            // Use a separate instance of the context to verify correct data was saved
            using (var context = new FlyingContext(options))
            {
                Assert.AreEqual(1, context.Flights.Count());
                Assert.AreEqual(999, context.Flights.Single().FlightId);
                Assert.AreEqual("Ronald Reagan Washington National Airport", context.Flights.Single().DepartureName);
                Assert.AreEqual(38.8521d, context.Flights.Single().DepartureLatitude);
                Assert.AreEqual(-77.037697d, context.Flights.Single().DepartureLongitude);
                Assert.AreEqual("Ken Jones Airport", context.Flights.Single().DestinationName);
                Assert.AreEqual(18.1987991333d, context.Flights.Single().DestinationLatitude);
                Assert.AreEqual(-76.53450012210001d, context.Flights.Single().DestinationLongitude);
                Assert.AreEqual(1.26d, context.Flights.Single().FuelConsumption);
                Assert.AreEqual(1.001d, context.Flights.Single().TakeoffEffort);
                Assert.AreEqual(500, context.Flights.Single().Speed);
                Assert.AreEqual(999, context.Flights.Single().FuelAmount);
                Assert.AreEqual(TimeSpan.FromMinutes(999), context.Flights.Single().FlightTime);
                Assert.AreEqual(999, context.Flights.Single().Distance);
            }

            // Modify some fields, unmodified fields must keep their old values
            using (var context = new FlyingContext(options))
            {
                var flight = context.Flights.Find(999);
                Assert.AreNotEqual(null, flight);
                flight.DepartureName = "+Ronald Reagan Washington National Airport+";
                flight.DestinationName = "+Ken Jones Airport+";
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
                Assert.AreEqual("+Ronald Reagan Washington National Airport+", context.Flights.Single().DepartureName);
                Assert.AreEqual(38.8521d, context.Flights.Single().DepartureLatitude);
                Assert.AreEqual(-77.037697d, context.Flights.Single().DepartureLongitude);
                Assert.AreEqual("+Ken Jones Airport+", context.Flights.Single().DestinationName);
                Assert.AreEqual(18.1987991333d, context.Flights.Single().DestinationLatitude);
                Assert.AreEqual(-76.53450012210001d, context.Flights.Single().DestinationLongitude);
                Assert.AreEqual(1.26d, context.Flights.Single().FuelConsumption);
                Assert.AreEqual(1.001d, context.Flights.Single().TakeoffEffort);
                Assert.AreEqual(500, context.Flights.Single().Speed);
                Assert.AreEqual(888, context.Flights.Single().FuelAmount);
                Assert.AreEqual(TimeSpan.FromMinutes(999), context.Flights.Single().FlightTime);
                Assert.AreEqual(999, context.Flights.Single().Distance);
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
                flight.DepartureName = "Ronald Reagan Washington National Airport" + id;
                flight.DepartureLatitude = 38.8521d + id;
                flight.DepartureLongitude = -77.037697d + id;
                flight.DestinationName = "Ken Jones Airport" + id;
                flight.DestinationLatitude = 18.1987991333d + id;
                flight.DestinationLongitude = -76.53450012210001d + id;
                flight.FuelConsumption = 1.26d + id;
                flight.TakeoffEffort = 1.001d + id;
                flight.Speed = 500 + id;
                flight.FuelAmount = 999 + id;
                flight.FlightTime = TimeSpan.FromMinutes(999 + id);
                flight.Distance = 999 + id;

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
                    Assert.AreEqual("Ronald Reagan Washington National Airport" + flight.FlightId, flight.DepartureName);
                    Assert.AreEqual("Ken Jones Airport" + flight.FlightId, flight.DestinationName);
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
                Assert.AreEqual("Ronald Reagan Washington National Airport" + id, flight.DepartureName);
                Assert.AreEqual("Ken Jones Airport" + id, flight.DestinationName);
                Assert.AreEqual(999 + id, flight.FuelAmount);
            }
        }
        #endregion
    }
}
