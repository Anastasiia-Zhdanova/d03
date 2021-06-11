using System;
using System.Text.Json.Serialization;

namespace d03.Nasa.NeoWs.Models
{
    public class AsteroidLookup
    {
        [JsonPropertyName("neo_reference_id")]
        public string NeoReferenceId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("nasa_jpl_url")]
        public string NasaJplUrl { get; set; }
        [JsonPropertyName("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardousAsteroid { get; set; }

        [JsonPropertyName("orbital_data")]
        public OrbitalData ClassOrbitalData{ get; set; }      
        public class OrbitalData
        {
            [JsonPropertyName("orbit_class")]
            public OrbitClass ClassOrbitalClass{ get; set; }      
            public class OrbitClass
            {
                [JsonPropertyName("orbit_class_type")]
                public string OrbitClassType { get; set; }
                [JsonPropertyName("orbit_class_description")]
                public string OrbitClassDescription { get; set; }                
            }
        }
        
        public override string ToString()
        {
            if (IsPotentiallyHazardousAsteroid == true)
                return ($"- Asteroid {Name}, SPK-ID: {NeoReferenceId}\n IS POTENTIALLY HAZARDOUS!\nClassification: {ClassOrbitalData.ClassOrbitalClass.OrbitClassType}, {ClassOrbitalData.ClassOrbitalClass.OrbitClassDescription}.\nUrl: {NasaJplUrl}\n");
            return ($"- Asteroid {Name}, SPK-ID: {NeoReferenceId}\nClassification: {ClassOrbitalData.ClassOrbitalClass.OrbitClassType}, {ClassOrbitalData.ClassOrbitalClass.OrbitClassDescription}.\nUrl: {NasaJplUrl}\n");
        }
    }
}