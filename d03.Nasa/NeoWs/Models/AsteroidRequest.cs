using System;

namespace d03.Nasa.NeoWs.Models
{
    public record AsteroidRequest(DateTime StartDate, DateTime EndDate, Int32 ResultCount)
    {
        
    }
}