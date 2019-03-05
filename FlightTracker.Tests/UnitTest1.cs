using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightTracker.Tests
{
    [TestClass]
    public class FlightTracker_ModelFlight
    {

        [TestMethod]
        public void Add_writes_to_database()
        {

            var options = new DbContextOptionsBuilder<FlyingContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new FlyingContext (options))
            {
                // Create a new flight
                var flight = new Flight();
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
        }
    }
}
