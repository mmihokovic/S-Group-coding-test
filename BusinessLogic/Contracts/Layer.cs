namespace BusinessLogic.Contracts
{
	public class Layer
	{
		// TODO: implement
	  public int Id { get; set; }

	  public string Name { get; set; }
	  public int? ParentLayerId { get; set; }
	  public bool DefaultVisibility { get; set; }
	  public int[] SubLayerIds { get; set; }
	  public decimal MinScale { get; set; }
	  public decimal MaxScale { get; set; }
	  public string[] Capabilities { get; set; }
	  public string Description { get; set; }
	  public string Type { get; set; }
	}
}