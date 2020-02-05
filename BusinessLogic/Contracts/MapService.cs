namespace BusinessLogic.Contracts
{
	public class MapService
	{
	  public string ServiceDescription { get; set; }
	  public string CurrentVersion { get; set; }
	  public string MapName { get; set; }
	  public string Description { get; set; }
	  public string CopyrightText { get; set; }
	  public string Units { get; set; }
	  public string SupportedImageFormatTypes { get; set; }
	  public string Capabilities { get; set; }
	  public bool SingleFusedMapCache { get; set; }
	}
}