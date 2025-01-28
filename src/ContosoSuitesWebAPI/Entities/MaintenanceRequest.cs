using System.Text.Json.Serialization;

namespace ContosoSuitesWebAPI.Entities
{
    /// <summary>
    /// Represents a maintenance request.
    /// </summary>
    public class MaintenanceRequest
    {
        [JsonPropertyName("id")]
        public string id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("Salon_id")]
        public int Salon_id { get; set; }

        [JsonPropertyName("Salon")]
        public string Salon { get; set; }

        [JsonPropertyName("source")]
        public string source { get; set; }

        [JsonPropertyName("date")]
        public DateTime date { get; set; }

        [JsonPropertyName("details")]
        public string details { get; set; }

        [JsonPropertyName("room_number")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? room_number { get; set; }

        [JsonPropertyName("location")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? location { get; set; }
    }
}
