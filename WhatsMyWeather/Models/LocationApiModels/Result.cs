namespace WhatsMyWeather.Models.LocationApiModels;


public class LocationApiRootobject
{
    public string documentation { get; set; }
    public License[] licenses { get; set; }
    public Result[] results { get; set; }
    public Status status { get; set; }
    public Stay_Informed stay_informed { get; set; }
    public string thanks { get; set; }
    public Timestamp timestamp { get; set; }
    public int total_results { get; set; }
}

public class Status
{
    public int code { get; set; }
    public string message { get; set; }
}

public class Stay_Informed
{
    public string blog { get; set; }
    public string mastodon { get; set; }
}

public class Timestamp
{
    public string created_http { get; set; }
    public int created_unix { get; set; }
}

public class License
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Result
{
    public Bounds bounds { get; set; }
    public Components components { get; set; }
    public int confidence { get; set; }
    public string formatted { get; set; }
    public Geometry geometry { get; set; }
}

public class Bounds
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Northeast
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Southwest
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Components
{
    public string ISO_31661_alpha2 { get; set; }
    public string ISO_31661_alpha3 { get; set; }
    public string[] ISO_31662 { get; set; }
    public string _category { get; set; }
    public string _type { get; set; }
    public string continent { get; set; }
    public string country { get; set; }
    public string country_code { get; set; }
    public string municipality { get; set; }
    public string political_union { get; set; }
    public string state { get; set; }
    public string state_code { get; set; }
    public string village { get; set; }
    public string postcode { get; set; }
    public string railway { get; set; }
    public string road { get; set; }
    public string hamlet { get; set; }
    public string region { get; set; }
    public string residential { get; set; }
    public string road_type { get; set; }
    public string town { get; set; }
}

public class Geometry
{
    public float lat { get; set; }
    public float lng { get; set; }
}
