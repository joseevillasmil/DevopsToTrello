using System;
using System.Collections.Generic;

#nullable enable
namespace joseevillasmil.TrelloDevops.Devops
{
  public class QueryResult
  {
    public string queryType { get; set; }

    public string queryResultType { get; set; }

    public DateTime asOf { get; set; }

    public List<Column> columns { get; set; }

    public List<WorkItem> workItems { get; set; }
  }
}
