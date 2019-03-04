/* 
 Date : 04/03/2019
 Author : developpeur-csharp.com
 Project : FlightTracker
 Description : Database context class 
*/

using Microsoft.EntityFrameworkCore;

namespace FlightTracker.Models
{
    /// <summary>
    /// Get the database context
    /// </summary>
    public class FlyingContext : DbContext
    {
        #region Private Members
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        public FlyingContext(DbContextOptions<FlyingContext> options)
            : base(options)
        { }

        public DbSet<Flight> Flights { get; set; }
    }
}