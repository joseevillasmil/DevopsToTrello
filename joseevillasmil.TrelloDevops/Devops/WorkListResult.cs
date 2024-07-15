using System.Collections.Generic;

namespace joseevillasmil.TrelloDevops.Devops
{
  public class WorkListResult
  {
    public int count { get; set; }

    public List<WorkItem> value { get; set; }
  }
}
