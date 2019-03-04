/* 
 Date : 04/03/2019
 Author : developpeur-csharp.com
 Project : FlightTracker
 Description : Class represents a Flight entity 
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace FlightTracker.Models
{
    /// <summary>
    /// Define a Flight entity 
    /// </summary>
    public class Flight
    {
        #region Private Members
        #endregion

        #region Public Properties

        public int FlightId { get; set; }

        [Display(Name = "Departure airport")]
        public string DepartureName { get; set; }
        [Display(Name = "Location latitude")]
        public string DepartureLatitude { get; set; }
        [Display(Name = "Location longitude")]
        public string DepartureLongitude { get; set; }

        [Display(Name = "Destination airport")]
        public string DestinationName { get; set; }
        [Display(Name = "Location latitude")]
        public string DestinationLatitude { get; set; }
        [Display(Name = "Location longitude")]
        public string DestinationLongitude { get; set; }

        [Display(Name = "Amount of fuel")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int FuelAmount { get; set; }
        [Display(Name = "Fuel consumption")]
        public string FuelConsumption { get; set; }
        [Display(Name = "Flight time")]

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public TimeSpan FlightTime { get; set; }
        [Display(Name = "Takeoff effort")]
        public string TakeoffEffort { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int Distance { get; set; }
        public int Speed { get; set; }

        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

    }
}
