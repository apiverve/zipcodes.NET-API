using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace APIVerve
{
public class foundCities
{
    [JsonProperty("zipcode")]
    public string zipcode { get; set; }

    [JsonProperty("state_abbr")]
    public string stateabbr { get; set; }

    [JsonProperty("latitude")]
    public string latitude { get; set; }

    [JsonProperty("longitude")]
    public string longitude { get; set; }

    [JsonProperty("city")]
    public string city { get; set; }

    [JsonProperty("state")]
    public string state { get; set; }

}

public class data
{
    [JsonProperty("search")]
    public string search { get; set; }

    [JsonProperty("foundCities")]
    public foundCities[] foundCities { get; set; }

}

public class ResponseObj
{
    [JsonProperty("status")]
    public string status { get; set; }

    [JsonProperty("error")]
    public object error { get; set; }

    [JsonProperty("data")]
    public data data { get; set; }

}

}
