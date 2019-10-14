using GooglePlacesApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime.Models
{
    public class AddressInfo
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Longitue { get; set; }
        public string Latitude { get; set; }
    }
}


public class PlacesMatchedSubstring
{
    [Newtonsoft.Json.JsonProperty("length")]
    public int Length { get; set; }

    [Newtonsoft.Json.JsonProperty("offset")]
    public int Offset { get; set; }
}

public class PlacesTerm
{
    [Newtonsoft.Json.JsonProperty("offset")]
    public int Offset { get; set; }

    [Newtonsoft.Json.JsonProperty("value")]
    public string Value { get; set; }
}

public class PlacesLocationPredictions
{
    [Newtonsoft.Json.JsonProperty("predictions")]
    public List<Prediction> Predictions { get; set; }

    [Newtonsoft.Json.JsonProperty("status")]
    public string Status { get; set; }
}





