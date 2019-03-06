using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightTracker.Models;
using FlightTracker.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace FlightTracker.Tests
{
    [TestClass]
    public class FlightsControllerTest
    {
        [TestMethod]
        public void Flights_controller_create()
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
            .UseInMemoryDatabase(databaseName: "database")
            .Options;
            var context = new FlyingContext(options);
            var controller = new FlightsController(context);

            // Create a new flight
            var flight = new Flight();
            flight.FlightId = 999;
            flight.DepartureName = "Ronald Reagan Washington National Airport";
            flight.DepartureLatitude = "38.8521";
            flight.DepartureLongitude = "-77.037697";
            flight.DestinationName = "Ken Jones Airport";
            flight.DestinationLatitude = "18.1987991333";
            flight.DestinationLongitude = "-76.53450012210001";
            flight.FuelConsumption = "1.26";
            flight.TakeoffEffort = "1.001";
            flight.Speed = 500;
            var result = controller.Create(flight).Result as RedirectToActionResult;
            
            Assert.AreEqual(flight.FuelAmount, 2897);
            Assert.AreEqual(flight.FlightTime, TimeSpan.FromMinutes(4*60+36));
            Assert.AreEqual(flight.Distance, 2299);
            Assert.AreEqual("Index", result.ActionName);

        }


        [TestMethod]
        public void Flights_controller_details()
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
            .UseInMemoryDatabase(databaseName: "database")
            .Options;
            var context = new FlyingContext(options);
            var controller = new FlightsController(context);
            var result = controller.Details(999).Result as RedirectToActionResult;

        }

    }

}
