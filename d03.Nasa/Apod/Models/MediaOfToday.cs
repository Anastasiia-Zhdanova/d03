using System;
using System.Text.Json.Serialization;

namespace d03.Nasa.Apod.Models
{
    public class MediaOfToday
    {
        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("explanation")]
        public string Explanation { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }

        public override string ToString()
        {
            return ($"{Date:d}\nā{Title}ā by {Copyright}\n{Explanation}\n{Url}\n");
        }
    }
}