using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EndpointTransformer;

public static class DeepCloneExtension
{
    public static T DeepClone<T>(this T obj)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
    }
}

public static class RegionHelper
{
    public static string GetRegion(string pseudoRegion)
    {
        if (pseudoRegion == "aws-fips")
        {
            return "aws-global"; // waf service
        }

        if (pseudoRegion == "iam-govcloud-fips")
        {
            return "aws-us-gov-global";
        }

        var region = pseudoRegion
            .Replace("fips", "")
            .Replace("dualstack", "")
            .Replace(".", "")
            .Replace("dkr", "")
            .Replace("rekognition", "")
            .Replace("rds", "")
            .Replace("prod", "")
            .Replace("iam", "")
            .Replace("dms", "")
            .Replace("servicediscovery", "")
            .Replace("govcloud", "")
            .Replace("secondary", "")
            .Replace("ProdFips", "")
            .Replace("iso-b", "")
            .Replace("iso", "")
            .Replace("--", "-")
            .Trim('-');

        if (region != "aws-global")
        {
            region = region.Replace("aws", "");
        }

        return region;
    }
}
