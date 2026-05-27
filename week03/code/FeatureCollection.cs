// Classes that mirror the relevant parts of the USGS GeoJSON feed. The JSON
// deserializer is configured with PropertyNameCaseInsensitive = true, so
// property names like "Features" match the lower-case JSON keys "features".

public class FeatureCollection
{
    // The "features" array in the JSON, each entry describing one earthquake.
    public List<Feature> Features { get; set; } = new();
}

public class Feature
{
    // The "properties" object holds the place, magnitude, and other data.
    public FeatureProperties Properties { get; set; } = new();
}

public class FeatureProperties
{
    // Human-readable location description, e.g. "1km NE of Pahala, Hawaii".
    public string Place { get; set; } = "";

    // Magnitude of the earthquake. Nullable because the feed sometimes omits it.
    public double? Mag { get; set; }
}
