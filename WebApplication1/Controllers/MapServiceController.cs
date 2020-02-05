using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic.Contracts;
using BusinessLogic.Parsers;

namespace WebApplication1.Controllers
{
	// TODO: solve as many of the unimplemented tasks below as you can
	// All tasks should be implemented using the same service specified below
	// service url: https://sampleserver1.arcgisonline.com/ArcGIS/rest/services/Demographics/ESRI_Census_USA/MapServer/
	// service api help page: http://sampleserver1.arcgisonline.com/ArcGIS/SDK/REST/index.html?mapserver.html
	/// <summary>
	/// Controller for getting information about map services
	/// </summary>
	[RoutePrefix("api/mapservice")]
	public class MapServiceController : ApiController
	{
	  private const string ArcGisUrl = @"https://sampleserver1.arcgisonline.com/ArcGIS/rest/services/Demographics/ESRI_Census_USA/MapServer/";

    /// <summary>
    /// Get a map service
    /// </summary>
    /// <returns>MapService</returns>
    [HttpGet]
		[Route("GetService")]
		public MapService GetService()
		{
      try
      {
        using (var client = new HttpClient())
        {
          var response = client.GetAsync(ArcGisUrl + "?f=json").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseContent = response.Content;
            var responseString = responseContent.ReadAsStringAsync().Result;

            var parser = new ArcGisOnlineParser();
            var mapService = parser.ParseSpecification(responseString);

            return mapService;
          }
        }
      }
      catch (Exception)
      {
        //handle error;
        throw;
      }
		  return null;
		}

		// api help page: http://sampleserver1.arcgisonline.com/ArcGIS/SDK/REST/index.html?layer.html
		/// <summary>
		/// Get all layers from a map service
		/// </summary>
		/// <returns>A list of layers</returns>
		[HttpGet]
		[Route("GetLayers")]
		public IEnumerable<Layer> GetLayers()
		{
		  try
		  {
		    using (var client = new HttpClient())
		    {

          var response = client.GetAsync(ArcGisUrl + "layers?f=json&pretty=true").Result;

		      if (response.IsSuccessStatusCode)
		      {
		        var responseContent = response.Content;
		        var responseString = responseContent.ReadAsStringAsync().Result;

		        var parser = new ArcGisOnlineParser();
		        var layers = parser.ParseLayers(responseString);

		        return layers;
		      }
		    }
		  }
		  catch (Exception)
		  {
		    //handle error;
		    throw;
		  }
		  return null;
    }

		// api help page: http://sampleserver1.arcgisonline.com/ArcGIS/SDK/REST/index.html?layer.html
		/// <summary>
		/// Gets all layers that support the "query" operation
		/// </summary>
		/// <returns>A list of layers</returns>
		[HttpGet]
		[Route("GetQueriableLayers")]
		public IEnumerable<Layer> GetQueriableLayers()
		{
		  try
		  {
		    using (var client = new HttpClient())
		    {

		      var response = client.GetAsync(ArcGisUrl + "layers?f=json&pretty=true").Result;

		      if (response.IsSuccessStatusCode)
		      {
		        var responseContent = response.Content;
		        var responseString = responseContent.ReadAsStringAsync().Result;

		        var parser = new ArcGisOnlineParser();
		        var layers = parser.ParseQueriableLayers(responseString);

		        return layers;
		      }
		    }
		  }
		  catch (Exception)
		  {
		    //handle error;
		    throw;
		  }
		  return null;
    }

		// api help page: http://sampleserver1.arcgisonline.com/ArcGIS/SDK/REST/index.html?export.html
		// test values: -207.682974279982,-40.6075371681153,-37.1804225764967,129.89501453537
		/// <summary>
		/// Gets the url of a generated image given a specific bounding box
		/// </summary>
		/// <param name="bbox"></param>
		/// <returns>A image url</returns>
		[HttpGet]
		[Route("GetMapImage")]
		public string GetMapImage([FromUri]BoundingBox bbox)
		{
		  try
		  {
		    using (var client = new HttpClient())
		    {

		      var response = client.GetAsync(ArcGisUrl + "export?f=json&bbox=" + bbox).Result;

		      if (response.IsSuccessStatusCode)
		      {
		        var responseContent = response.Content;
		        var responseString = responseContent.ReadAsStringAsync().Result;

		        var parser = new ArcGisOnlineParser();
		        var url = parser.ParseMapImageUrl(responseString);

		        return url;
		      }
		    }
		  }
		  catch (Exception)
		  {
		    //handle error;
		    throw;
		  }
		  return null;
    }
	}
}
