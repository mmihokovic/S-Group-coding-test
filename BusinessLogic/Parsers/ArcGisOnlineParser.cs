using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using Newtonsoft.Json;

namespace BusinessLogic.Parsers
{
  public class ArcGisOnlineParser
  {
    public MapService ParseSpecification(string jsonString)
    {
      var json = JsonConvert.DeserializeObject<dynamic>(jsonString);
      return new MapService
      {
        ServiceDescription = json.serviceDescription,
        CurrentVersion = json.currentVersion,
        MapName = json.mapName,
        Description = json.description,
        CopyrightText = json.copyrightText,
        Units = json.units,
        SupportedImageFormatTypes = json.supportedImageFormatTypes,
        Capabilities = json.capabilities,
        SingleFusedMapCache = json.singleFusedMapCache,
      };
    }

    public IEnumerable<Layer> ParseLayers(string jsonString)
    {
      var layers = new List<Layer>();
      var json = JsonConvert.DeserializeObject<dynamic>(jsonString);
      foreach (var l in json.layers)
      {
        var layer = new Layer
        {
          Id = l.id,
          Name = l.name,
          ParentLayerId = l.parentLayerId,
          DefaultVisibility = l.defaultVisibility,
          SubLayerIds = l.subLayerIds?.ToObject<int[]>(),
          MinScale = l.minScale,
          MaxScale = l.maxScale,
          Type = l.type,
          Description = l.description,
          Capabilities = l.capabilities?.ToObject<string>().Split(',')
        };
        layers.Add(layer);
      }
      return layers;
    }

    public IEnumerable<Layer> ParseQueriableLayers(string responseString)
    {
      var layers = ParseLayers(responseString);
      var queriableLayers = layers.Where(l => l.Capabilities.Any(c => c.Equals("Query"))).ToList();
      return queriableLayers;
    }

    public string ParseMapImageUrl(string jsonString)
    {
      var json = JsonConvert.DeserializeObject<dynamic>(jsonString);
      return json.href;
    }
  }
}
