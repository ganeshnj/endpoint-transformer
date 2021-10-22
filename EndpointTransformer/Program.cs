// See https://aka.ms/new-console-template for more information

using EndpointTransformer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

//var originalPath = @"C:\Users\jangirg\Desktop\endpoints.json";

var originalPath = @"C:\Users\jangirg\Downloads\endpoints (3).json";

var originalContent = File.ReadAllText(originalPath);

var original = JsonConvert.DeserializeObject<EndpointsRoot>(originalContent);


foreach (var partition in original.Partitions)
{
    foreach (var (serviceName, service) in partition.Services)
    {
        foreach (var (region, endpoint) in service.Endpoints)
        {
            if (region.Contains("fips-") || region.Contains("-fips") || region.Contains("-fips-"))
            {
                var newRegion = region.Replace("-fips", "").Replace("fips-", "").Replace("-fips-", "");

                var newEndpoint = service.Endpoints.FirstOrDefault(e => e.Key == newRegion).Value;
                if (newEndpoint == null)
                {
                    Console.WriteLine($"Transformed region missing: {partition.Partition},{serviceName},old region: {region}, new region: {newRegion}");
                }
                else
                {
                    var fipsVariant = newEndpoint?.Variants?.FirstOrDefault(v => v.Tags.Count == 1 && v.Tags.Contains("fips"));
                    if (fipsVariant == null)
                    {
                        Console.WriteLine($"Fips variant missing: {partition.Partition},{serviceName},old region: {region}, new region: {newRegion}");
                    }

                    if (fipsVariant.Hostname != endpoint.Hostname)
                    {
                        Console.WriteLine($"Fips variant hostname not same as pseduo region: {partition.Partition},{serviceName},old region: {region}, new region: {newRegion}");
                    }
                }

                if (endpoint.Hostname != $"{serviceName}-fips.{newRegion}.{partition.DnsSuffix}")
                {
                    var fipsDefaultVariant = service.Defaults?.Variants?.FirstOrDefault(v => v.Tags.Count == 1 && v.Tags.Contains("fips"));
                    if (fipsDefaultVariant == null)
                    {
                        Console.WriteLine($"Service must have fips variant default: {partition.Partition},{serviceName},{region},{endpoint.Hostname}");
                    }
                }
            }
        }
    }
}