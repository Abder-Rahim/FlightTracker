/* 
 Date : 04/03/2019
 Author : developpeur-csharp.com
 Project : FlightTracker
*/

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightTracker.Models;
using System.Diagnostics;
using System.Device.Location;

namespace FlightTracker.Controllers
{
    /// <summary>
    /// Manages all interactions with the user and sends back the right view
    /// </summary>
    public class FlightsController : Controller
    {

        #region Private Members

        // Store the database context
        private readonly FlyingContext _context;

        #endregion

        #region Public Properties
        #endregion

        #region Constructors

        public FlightsController(FlyingContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// GET: Flights
        /// </summary>
        /// <returns>Return the list of all flights</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flights.ToListAsync());
        }

        /// <summary>
        /// GET: Flights/Details/5
        /// </summary>
        /// <returns>Return a summary of a flight</returns>
        /// <param name=id>Specifies the flight id</param>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        /// <summary>
        /// GET: Flights/Create
        /// </summary>
        /// <returns>Returns a form allowing the user to create a flight</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Flights/Create
        /// Intercept the user's post event and save the sent data
        /// </summary>
        /// <param name=flight>Flight data entered by the user</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,DepartureName,DepartureLatitude,DepartureLongitude,DestinationName,DestinationLatitude,DestinationLongitude,FuelAmount,FuelConsumption,FlightTime,TakeoffEffort,Distance,Speed")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                // Calculate the distance from the departure airport to the arrival airport
                double lat = Convert.ToDouble(flight.DepartureLatitude.Replace(".", ","));
                double lon = Convert.ToDouble(flight.DepartureLongitude.Replace(".", ","));
                var sCoord = new GeoCoordinate(lat, lon);
                lat = Convert.ToDouble(flight.DestinationLatitude.Replace(".", ","));
                lon = Convert.ToDouble(flight.DestinationLongitude.Replace(".", ","));
                var eCoord = new GeoCoordinate(lat, lon);
                double distance = sCoord.GetDistanceTo(eCoord) / 1000;
                flight.Distance = (int)distance;

                double consumption = Convert.ToDouble(flight.FuelConsumption.Replace(".", ","));
                double takeoffEffort = Convert.ToDouble(flight.TakeoffEffort.Replace(".", ","));

                // Calculate flight time and fuel amount
                flight.FuelAmount = (int)(consumption * distance + takeoffEffort);
                flight.FlightTime = TimeSpan.FromMinutes(Math.Round((distance / flight.Speed) * 60));

                // Save flight entity in database
                _context.Add(flight);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        /// <summary>
        /// GET: Flights/Edit/5
        /// </summary>
        /// <returns>Returns a form allowing the user to edit a flight</returns>
        /// <param name=id>Specifies the flight id</param>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        /// <summary>
        /// POST: Flights/Edit/5
        /// Intercept the user's post event and save the sent data
        /// </summary>
        /// <param name=id>Flight id</param>
        /// <param name=flight>Flight data entered by the user</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,DepartureName,DepartureLatitude,DepartureLongitude,DestinationName,DestinationLatitude,DestinationLongitude,FuelAmount,FuelConsumption,FlightTime,TakeoffEffort,Distance,Speed")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    // Calculate the distance from the departure airport to the arrival airport
                    double lat = Convert.ToDouble(flight.DepartureLatitude.Replace(".", ","));
                    double lon = Convert.ToDouble(flight.DepartureLongitude.Replace(".", ","));
                    var sCoord = new GeoCoordinate(lat, lon);
                    lat = Convert.ToDouble(flight.DestinationLatitude.Replace(".", ","));
                    lon = Convert.ToDouble(flight.DestinationLongitude.Replace(".", ","));
                    var eCoord = new GeoCoordinate(lat, lon);
                    double distance = sCoord.GetDistanceTo(eCoord) / 1000;
                    flight.Distance = (int)distance;

                    double consumption = Convert.ToDouble(flight.FuelConsumption.Replace(".", ","));
                    double takeoffEffort = Convert.ToDouble(flight.TakeoffEffort.Replace(".", ","));

                    // Calculate flight time and fuel amount
                    flight.FuelAmount = (int)(consumption * distance + takeoffEffort);
                    flight.FlightTime = TimeSpan.FromMinutes(Math.Round((distance / flight.Speed) * 60));

                    // Save flight entity in database
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        #endregion

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightId == id);
        }

    }
}
