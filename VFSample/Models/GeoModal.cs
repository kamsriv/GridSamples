namespace VFSample.Models
{
    using System.Reflection.Metadata.Ecma335;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class GeoModal
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("created_on")]
        public long CreatedOn { get; set; }
        [JsonPropertyName("geolocation_degrees")]
        public string GeoLocationAddress { get; set; }
        public DateTime CreatedDate { get => new DateTime((this.CreatedOn + 62135607600000) * 10000); }
    }
    public class GeoModalData
    {
        [JsonPropertyName ("venues")]
        public IEnumerable<GeoModal> Venues { get; set; }
    }
}
