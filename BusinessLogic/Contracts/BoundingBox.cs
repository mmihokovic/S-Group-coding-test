namespace BusinessLogic.Contracts
{
	/// <summary>
	/// The extent (bounding box) of the exported image. 
	/// Unless the bboxSR parameter has been specified, 
	/// the bbox is assumed to be in the spatial reference of the map.
	/// </summary>
	public class BoundingBox
	{
		public decimal XMin { get; set; }
		public decimal YMin { get; set; }
		public decimal XMax { get; set; }
		public decimal YMax { get; set; }

	  public override string ToString()
	  {
	    return XMin + "," + YMin + "," + XMax + "," + YMax;
	  }
	}
}