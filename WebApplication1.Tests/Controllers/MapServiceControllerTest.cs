using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;

namespace WebApplication1.Tests.Controllers
{
  [TestClass]
  public class MapServiceControllerTest
  {
    [TestMethod]
    public void GetServiceTest()
    {
      var controller = new MapServiceController();

      var result = controller.GetService();
      Assert.IsNotNull(result);
      Assert.IsNotNull(result.MapName);
      Assert.IsNotNull(result.Capabilities);
      Assert.IsNotNull(result.CopyrightText);
      Assert.IsNotNull(result.CurrentVersion);
      Assert.IsNotNull(result.Description);
      Assert.IsNotNull(result.ServiceDescription);
      Assert.IsNotNull(result.SingleFusedMapCache);
      Assert.IsNotNull(result.SupportedImageFormatTypes);
      Assert.IsNotNull(result.Units);
    }

    [TestMethod]
    public void GetLayersTest()
    {
      var controller = new MapServiceController();

      var result = controller.GetLayers();
      Assert.AreEqual(6, result.Count());
    }

    [TestMethod]
    public void GetQueriableLayersTest()
    {
      var controller = new MapServiceController();

      var result = controller.GetQueriableLayers();
      Assert.AreEqual(6, result.Count());
      Assert.IsTrue(result.All(q => q.Capabilities.Contains("Query")));
    }

    [TestMethod]
    public void GetMapImageTest()
    {
      var bbox = new BoundingBox
      {
        XMin = -207.682974279982M,
        YMin = -40.6075371681153M,
        XMax = -37.1804225764967M,
        YMax = 129.89501453537M
      };

      var controller = new MapServiceController();

      var result1 = controller.GetMapImage(bbox);
      Assert.IsNotNull(result1);
      Assert.IsFalse(String.IsNullOrWhiteSpace(result1));
      bbox.XMax -= 10;
      var result2 = controller.GetMapImage(bbox);
      Assert.IsNotNull(result2);
      Assert.IsFalse(String.IsNullOrWhiteSpace(result2));
      Assert.AreNotEqual(result1, result2);
    }
  }
}
