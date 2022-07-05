namespace TvMaze.Ingestor;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);

public class DvdCountry
{
    public string name { get; set; }
    public string code { get; set; }
    public string timezone { get; set; }
}

public class Externals
{
    public int tvrage { get; set; }
    public int? thetvdb { get; set; }
    public string imdb { get; set; }
}

public class Image2
{
    public string medium { get; set; }
    public string original { get; set; }
}

public class Links2
{
    public Self self { get; set; }
    public Previousepisode previousepisode { get; set; }
    public Nextepisode nextepisode { get; set; }
}

public class Network
{
    public int id { get; set; }
    public string name { get; set; }
    public Country country { get; set; }
    public string officialSite { get; set; }
}

public class Nextepisode
{
    public string href { get; set; }
}

public class Previousepisode
{
    public string href { get; set; }
}

public class Rating
{
    public double? average { get; set; }
}

public class ShowDto
{
    public int id { get; set; }
    public string url { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string language { get; set; }
    public List<string> genres { get; set; }
    public string status { get; set; }
    public int? runtime { get; set; }
    public int averageRuntime { get; set; }
    public string premiered { get; set; }
    public string ended { get; set; }
    public string officialSite { get; set; }
    public Schedule schedule { get; set; }
    public Rating rating { get; set; }
    public int weight { get; set; }
    public Network network { get; set; }
    public WebChannel webChannel { get; set; }
    public DvdCountry dvdCountry { get; set; }
    public Externals externals { get; set; }
    public Image2 image { get; set; }
    public string summary { get; set; }
    public int updated { get; set; }
    public Links2 _links { get; set; }
}

public class Schedule
{
    public string time { get; set; }
    public List<string> days { get; set; }
}

public class WebChannel
{
    public int id { get; set; }
    public string name { get; set; }
    public Country country { get; set; }
    public string officialSite { get; set; }
}

