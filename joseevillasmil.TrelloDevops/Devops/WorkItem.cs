namespace joseevillasmil.TrelloDevops.Devops
{
  public class WorkItem
  {
    public int id { get; set; }

    public int rev { get; set; }

    public WorkItemFields fields { get; set; }

    public string url { get; set; }
  }
}
