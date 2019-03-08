/* 
 Date : 05/03/2019
 Author : developpeur-csharp.com
 Project : FlightTracker
*/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightTracker.Models;
using FlightTracker.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace FlightTracker.Tests
{
    [TestClass]
    public class FlightsControllerTest
    {
        #region Private Members
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        [TestMethod]
        public void Create_modify_verify_one_flight()
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
            .UseInMemoryDatabase(databaseName: "create_one_database")
            .Options;
            // Run the test against one instance of the context
            using (var context = new FlyingContext(options))
            {
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

                // Check the calculations
                Assert.AreEqual(flight.FuelAmount, 2897);
                Assert.AreEqual(flight.FlightTime, TimeSpan.FromMinutes(4 * 60 + 36));
                Assert.AreEqual(flight.Distance, 2299);
                Assert.AreEqual("Index", result.ActionName);
            }

            // Run the test against one instance of the context
            using (var context = new FlyingContext(options))
            {
                // Get the view details
                var controller = new FlightsController(context);
                var result = controller.Details(999).Result as ViewResult;
                var flight = (Flight)result.ViewData.Model;

                // Check the calculations
                Assert.AreEqual(flight.FuelAmount, 2897);
                Assert.AreEqual(flight.FlightTime, TimeSpan.FromMinutes(4 * 60 + 36));
                Assert.AreEqual(flight.Distance, 2299);
                Assert.AreEqual("Details", result.ViewName);
            }

            // Run the test against one instance of the context
            using (var context = new FlyingContext(options))
            {
                // Get edit view
                var controller = new FlightsController(context);
                var result = controller.Edit(999).Result as ViewResult;
                var flight = (Flight)result.ViewData.Model;
                Assert.AreEqual("Edit", result.ViewName);

                // Modify input informations
                flight.DepartureName = "Narita International Airport";
                flight.DepartureLatitude = "35.7647018433";
                flight.DepartureLongitude = "140.386001587";
                flight.DestinationName = "Kabul International Airport";
                flight.DestinationLatitude = "34.56589889526367";
                flight.DestinationLongitude = "69.2123031616211";
                flight.FuelConsumption = "2.79";
                flight.TakeoffEffort = "1.004";
                flight.Speed = 833;

                // Save edit view
                result = controller.Edit(999, flight).Result as ViewResult;
            }

            // Run the test against one instance of the context
            using (var context = new FlyingContext(options))
            {
                // Check the calculations
                var controller = new FlightsController(context);
                var result = controller.Details(999).Result as ViewResult;
                var flight = (Flight)result.ViewData.Model;
                Assert.AreEqual(flight.FuelAmount, 17644);
                Assert.AreEqual(flight.FlightTime, TimeSpan.FromMinutes(7 * 60 + 36));
                Assert.AreEqual(flight.Distance, 6323);
            }
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        [DataRow(9)]
        [DataRow(10)]
        [DataRow(11)]
        [DataRow(12)]
        [DataRow(13)]
        [DataRow(14)]
        [DataRow(15)]
        [DataRow(16)]
        [DataRow(17)]
        [DataRow(18)]
        [DataRow(19)]
        [DataRow(20)]
        [DataRow(21)]
        [DataRow(22)]
        [DataRow(23)]
        [DataRow(24)]
        public void Create_many_flights(int id)
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
                .UseInMemoryDatabase(databaseName: "create_many_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new FlyingContext(options))
            {
                // Create a new flight
                var flight = new Flight();
                flight.FlightId = id;
                flight.DepartureName = "Ronald Reagan Washington National Airport" + id;
                flight.DepartureLatitude = "38.8521";
                flight.DepartureLongitude = "-77.037697";
                flight.DestinationName = "Ken Jones Airport" + id;
                flight.DestinationLatitude = "18.1987991333";
                flight.DestinationLongitude = "-76.53450012210001";
                flight.FuelConsumption = "1.26";
                flight.TakeoffEffort = "1.001";
                flight.Speed = 500;

                // Save the flight in the data base
                var controller = new FlightsController(context);
                var result = controller.Create(flight).Result as RedirectToActionResult;
                Assert.IsNotNull(result, $"Flight {id} was not created");
                Assert.AreEqual("Index", result.ActionName);
            }
        }

        [TestMethod]
        public void Verify_all_flights()
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
                .UseInMemoryDatabase(databaseName: "create_many_database")
                .Options;

            using (var context = new FlyingContext(options))
            {
                var controller = new FlightsController(context);
                var result = controller.Index().Result as ViewResult;
            
                var flights = (IEnumerable<Flight>) result.ViewData.Model;
                Assert.AreEqual("Index", result.ViewName);
                Assert.AreEqual(24, flights.Count());
                foreach (Flight flight in flights)
                {
                    Assert.AreEqual(flight.DepartureName, "Ronald Reagan Washington National Airport" + flight.FlightId);
                    Assert.AreEqual(flight.DestinationName, "Ken Jones Airport" + flight.FlightId);
                    Assert.AreEqual(flight.FuelAmount, 2897);
                    Assert.AreEqual(flight.FlightTime, TimeSpan.FromMinutes(4 * 60 + 36));
                    Assert.AreEqual(flight.Distance, 2299);
                }
            }

        }

        [DataTestMethod]
        [DataRow(24)]
        public void Verify_one_flight(int id)
        {
            var options = new DbContextOptionsBuilder<FlyingContext>()
                .UseInMemoryDatabase(databaseName: "create_many_database")
                .Options;

            // Select a flight by its id and check the data it contains
            using (var context = new FlyingContext(options))
            {
                var controller = new FlightsController(context);
                var result = controller.Details(id).Result as ViewResult;
                var flight = (Flight)result.ViewData.Model;

                Assert.AreEqual("Details", result.ViewName);
                Assert.AreNotEqual(null, flight);
                Assert.AreEqual(id, flight.FlightId);
                Assert.AreEqual(flight.DepartureName, "Ronald Reagan Washington National Airport24");
                Assert.AreEqual(flight.DestinationName, "Ken Jones Airport24");
                Assert.AreEqual(flight.FuelAmount, 2897);
                Assert.AreEqual(flight.FlightTime, TimeSpan.FromMinutes(4 * 60 + 36));
                Assert.AreEqual(flight.Distance, 2299);
            }
        }

        #endregion
    }
}
