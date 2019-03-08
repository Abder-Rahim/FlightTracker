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

        [Required]
        [Display(Name = "Departure airport")]
        public string DepartureName { get; set; }

        [Required]
        [Display(Name = "Location latitude")]
        [Range(-90, 90)]
        public double DepartureLatitude { get; set; }

        [Required]
        [Display(Name = "Location longitude")]
        [Range(-180, 180)]
        public double DepartureLongitude { get; set; }

        [Required]
        [Display(Name = "Destination airport")]
        public string DestinationName { get; set; }

        [Required]
        [Display(Name = "Location latitude")]
        [Range(-90, 90)]
        public double DestinationLatitude { get; set; }

        [Required]
        [Display(Name = "Location longitude")]
        [Range(-180, 180)]
        public double DestinationLongitude { get; set; }

        [Display(Name = "Amount of fuel")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int FuelAmount { get; set; }

        [Required]
        [Display(Name = "Fuel consumption")]
        public double FuelConsumption { get; set; }

        [Display(Name = "Flight time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public TimeSpan FlightTime { get; set; }

        [Required]
        [Display(Name = "Takeoff effort")]
        public double TakeoffEffort { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int Distance { get; set; }

        [Required]
        public int Speed { get; set; }

        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

    }
}
