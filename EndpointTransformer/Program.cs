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

var originalPath = @"C:\Users\jangirg\source\repos\aws-sdk-net\sdk\src\Core\endpoints.json";

var originalContent = File.ReadAllText(originalPath);

var original = JsonConvert.DeserializeObject<EndpointsRoot>(originalContent);

Console.WriteLine(original);

var transformed = original.DeepClone<EndpointsRoot>();

var psuedoRegion = new HashSet<string>();

var oddFips = new HashSet<string>();
var fipsServices = new HashSet<string>();


foreach (var partition in transformed.Partitions)
{
    foreach (var (serviceName, service) in partition.Services)
    {


        foreach (var (region, endpoint) in service.Endpoints)
        {
            var found = partition.Regions.FirstOrDefault(r => r.Key == region).Key;
            if (found == null)
            {
                Console.WriteLine($"{partition.Partition},{serviceName},{region},{endpoint?.CredentialScope?.Region ?? "<Unknown>"},{endpoint.Hostname ?? "<Unknown>"}");

            }
        }
    }
}