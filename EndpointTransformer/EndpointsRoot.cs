using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EndpointTransformer;

[Serializable]
public class Region
{
    [JsonProperty("description")]
    public string Description { get; set; }
}

[Serializable]
public class Endpoint
{
    [JsonProperty("hostname")]
    public string Hostname { get; set; }

    [JsonProperty("protocols")]
    public IList<string> Protocols { get; set; }

    [JsonProperty("signatureVersions")]
    public IList<string> SignatureVersions { get; set; }

    [JsonProperty("credentialScope")]
    public CredentialScope CredentialScope { get; set; }

    [JsonProperty("sslCommonName")]
    public string SslCommonName { get; set; }

    [JsonProperty("deprecated")]
    public bool? Deprecated { get; set; }

    [JsonProperty("variants")]
    public IList<Variant> Variants { get; set; }
}

[Serializable]
public class CredentialScope
{
    [JsonProperty("region")]
    public string Region { get; set; }

    [JsonProperty("service")]
    public string Service { get; set; }
}

[Serializable]
public class Service
{
    [JsonProperty("defaults")]
    public Endpoint Defaults { get; set; }

    [JsonProperty("endpoints")]
    public IDictionary<string, Endpoint> Endpoints { get; set; }

    [JsonProperty("isRegionalized")]
    public bool? IsRegionalized { get; set; }

    [JsonProperty("partitionEndpoint")]
    public string PartitionEndpoint { get; set; }
}



[Serializable]
public class PartitionRoot
{
    [JsonProperty("defaults")]
    public Endpoint Defaults { get; set; }

    [JsonProperty("dnsSuffix")]
    public string DnsSuffix { get; set; }

    [JsonProperty("partition")]
    public string Partition { get; set; }

    [JsonProperty("partitionName")]
    public string PartitionName { get; set; }

    [JsonProperty("regionRegex")]
    public string RegionRegex { get; set; }

    [JsonProperty("regions")]
    public IDictionary<string, Region> Regions { get; set; }

    [JsonProperty("services")]
    public IDictionary<string, Service> Services { get; set; }
}

[Serializable]
public class EndpointsRoot
{
    [JsonProperty("partitions")]
    public List<PartitionRoot> Partitions { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }
}

[Serializable]
public class Variant
{
    [JsonProperty("tags")]
    public IList<string> Tags { get; set; }

    [JsonProperty("hostname")]
    public string Hostname { get; set; }

    [JsonProperty("dnsSuffix")]
    public string DnsSuffix { get; set; }
}
