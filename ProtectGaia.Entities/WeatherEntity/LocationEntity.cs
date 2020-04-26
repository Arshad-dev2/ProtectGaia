using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProtectGaia.Entities.WeatherEntity
{
    public partial class LocationEntity
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("components")]
        public Components Components { get; set; }

        [JsonProperty("confidence")]
        public long Confidence { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }

    public partial class Components
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state_code")]
        public string StateCode { get; set; }

        [JsonProperty("suburb")]
        public string Suburb { get; set; }
        [JsonProperty("postcode")]
        public string Postcode { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public partial class Status
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}