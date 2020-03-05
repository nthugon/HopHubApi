namespace HopHubApi.Models
{
    /// <summary>
    /// Request for creating or updating a beer.
    /// </summary>
    public class BeerRequest
    {
        /// <summary>
        /// Gets or sets the name of the beer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the style of the beer.
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Gets or sets the brewery of the beer.
        /// </summary>
        public string Brewery { get; set; }

        /// <summary>
        /// Gets or sets the abv of the beer.
        /// </summary>
        public decimal Abv { get; set; }
    }
}
