namespace ContosoSuitesWebAPI.Entities
{
    public class VectorSearchResult
    {
        public required int SalonId { get; set; }
        public required string Salon { get; set; }
        public required string Details { get; set; }
        public required string Source { get; set; }
        public required float SimilarityScore { get; set; }
    }
}
