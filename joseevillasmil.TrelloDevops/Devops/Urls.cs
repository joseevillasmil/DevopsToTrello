namespace joseevillasmil.TrelloDevops.Devops
{
  public static class Urls
  {
    public const string QueryUrl = "https://dev.azure.com/softlandchile/softland/ventas/_apis/wit/wiql/{queryId}?api-version=5.1";
    public const string WorkItemList = "https://dev.azure.com/softlandchile/softland/_apis/wit/workitems?ids={workitemIds}&api-version=7.0&fields=System.Id,System.Title,System.WorkItemType,System.State,Custom.NumeroIA,Custom.Cliente,System.Description,Custom.FechaCompromisoDev,System.Parent,Microsoft.VSTS.Scheduling.StartDate,Microsoft.VSTS.Scheduling.TargetDate,System.AssignedTo";
  }
}
