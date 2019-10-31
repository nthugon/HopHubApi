using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HopHubApi.Models
{
    /// <summary>
    /// Interface for interacting with the hop hub database.
    /// </summary>
    public interface IHopHubDatabase : IDisposable
    {
        /// <summary>
        /// Gets or sets Beers DbSet.
        /// </summary>
        DbSet<Beer> Beers { get; set; }

        /// <summary>
        /// Gets or sets Reviews DbSet.
        /// </summary>
        DbSet<Review> Reviews { get; set; }

        /// <summary>
        /// Checks database connection.
        /// </summary>
        /// <returns>
        /// Returns <see langword="true"/> if the database connection is okay; otherwise
        /// returns <see langword="false"/>.
        /// </returns>
        Task<bool> CheckDatabaseConnectionAsync();

        /// <summary>
        /// Executes the database migration scripts.
        /// </summary>
        void ExecuteDatabaseMigration();

        /// <summary>
        /// Method for getting all Beer records in Beers DbSet.
        /// </summary>
        /// <returns>List of all Beer records in Beers DbSet.</returns>
        Task<List<Beer>> GetAllBeersAsync();

        /// <summary>
        /// Method for getting all Beer records and their accompanying Review records from the Beers DbSet and Reviews DbSet.
        /// </summary>
        /// <returns>List of all Beer records and their accompanying Review records from the Beers DbSet and Reviews DbSet.</returns>
        Task<List<Beer>> GetAllBeersWithReviewsAsync();

        /// <summary>
        /// Method for getting a Beer by its unique identifier from the Beers DbSet.
        /// </summary>
        /// <param name="id">Unique identifier of the Beer to get.</param>
        /// <returns>Beer read from Beers DbSet.</returns>
        Task<Beer> GetBeerByIdAsync(int id);

        /// <summary>
        /// Method for creating a Beer in the Beers DbSet. 
        /// </summary>
        /// <param name="beer">Beer DTO.</param>
        /// <returns>Beer created in and read from the Beers DbSet.</returns>
        Task<Beer> CreateBeerAsync(Beer beer);

        /// <summary>
        /// Method for updating a Beer in the Beers DbSet.
        /// </summary>
        /// <param name="beer">Beer DTO containing changes.</param>
        /// <returns>Task</returns>
        Task UpdateBeerAsync(Beer beer);

        /// <summary>
        /// Method for deleting a Beer in the Beers DbSet.
        /// </summary>
        /// <param name="beer">Beer DTO.</param>
        /// <returns>Task</returns>
        Task DeleteBeerAsync(Beer beer);

        /// <summary>
        /// Method for getting all Review records in Reviews DbSet.
        /// </summary>
        /// <returns>List of all Review records in Reviews DbSet.</returns>
        Task<List<Review>> GetAllReviewsAsync();

        /// <summary>
        /// Method for getting a Review by its unique identifier from the Reviews DbSet.
        /// </summary>
        /// <param name="id">Unique identifier of the Review to get.</param>
        /// <returns>Review read from Reviews DbSet.</returns>
        Task<Review> GetReviewByIdAsync(int id);

        /// <summary>
        /// Method for getting Review records by the BeerId the record is associated with.
        /// </summary>
        /// <param name="id">Unique identifier of the Beer the Review record is associated with.</param>
        /// <returns>List of Review records in the Reviews DbSet that contain the BeerId.</returns>
        Task<List<Review>> GetReviewsByBeerIdAsync(int id);

        /// <summary>
        /// Method for creating a Review in the Reviews DbSet. 
        /// </summary>
        /// <param name="review">Review DTO.</param>
        /// <returns>Review created in and read from the Reviews DbSet.</returns>
        Task<Review> CreateReviewAsync(Review review);

        /// <summary>
        /// Method for updating a Review in the Reviews DbSet.
        /// </summary>
        /// <param name="review">Review DTO containing changes.</param>
        /// <returns>Task</returns>
        Task UpdateReviewAsync(Review review);

        /// <summary>
        /// Method for deleting a Review in the Reviews DbSet.
        /// </summary>
        /// <param name="review">Review DTO.</param>
        /// <returns>Task</returns>
        Task DeleteReviewAsync(Review review);
    }
}
