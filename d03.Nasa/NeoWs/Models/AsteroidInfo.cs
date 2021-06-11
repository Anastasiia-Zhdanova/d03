using System;
using System.Text.Json.Serialization;

namespace d03.Nasa.NeoWs.Models
{
    public class AsteroidInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("close_approach_data")]
        public CloseApproachData[] ClassCloseApproachData{ get; set; }
        public class CloseApproachData
        {
            [JsonPropertyName("miss_distance")]
            public MissDistance ClassMissDistance{ get; set; }

            public class MissDistance
            {
                [JsonPropertyName("kilometers")]
                public string Kilometers { get; set; }
            }
        }

        public double Distance => Double.Parse(ClassCloseApproachData[0].ClassMissDistance.Kilometers);
    }
}