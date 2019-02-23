/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Yaml.Serialization;
using System.Yaml;
using System.IO;
using Bam.Net;
using System.Reflection;
using System.Collections;
using YamlDotNet.Serialization;

namespace Bam.Net
{
    // TOOD: replace all uses of YamlSerializer (System.Yaml) with YamlDotNet.  
    // Convert yaml to json (yaml.YamlToJson) for all significant operations
    public static partial class YamlExtensions
    {
        public static YamlSequence ToYamlSequence(this IEnumerable enumerable)
        {
            YamlSequence seq = new YamlSequence();
            foreach(object obj in enumerable)
            {
                seq.Add(obj.ToYamlNode());
            }
            return seq;
        }

        public static YamlNode ToYamlNode(this object val)
        {
            Type type = val.GetType();
            YamlMapping node = new YamlMapping();
            List<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());
            properties.Sort((l, r) => l.MetadataToken.CompareTo(r.MetadataToken));
            foreach (PropertyInfo property in properties)
            {
                object propVal = property.GetValue(val);
                if(propVal != null)
                {
                    if (property.IsEnumerable())
                    {
                        YamlSequence yamlSequence = new YamlSequence();
                        node.Add(property.Name, yamlSequence);
                        foreach (object v in ((IEnumerable)propVal))
                        {
                            yamlSequence.Add(v.ToYamlNode());
                        }
                    }
                    else
                    {
                        if (property.PropertyType == typeof(bool) ||
                            property.PropertyType == typeof(bool?) ||
                            property.PropertyType == typeof(int) ||
                            property.PropertyType == typeof(int?) ||
                            property.PropertyType == typeof(long) ||
                            property.PropertyType == typeof(long?) ||
                            property.PropertyType == typeof(decimal) ||
                            property.PropertyType == typeof(decimal?) ||
                            property.PropertyType == typeof(double) ||
                            property.PropertyType == typeof(double?) ||
                            property.PropertyType == typeof(string) ||
                            property.PropertyType == typeof(byte[]) ||
                            property.PropertyType == typeof(DateTime) ||
                            property.PropertyType == typeof(DateTime?))
                        {
                            node.Add(property.Name, new YamlScalar(propVal.ToString()));
                        }
                        else
                        {
                            node.Add(property.Name, propVal.ToYamlNode());
                        }
                    }
                }
            }
            return node;
        }

    }
}
