using Newtonsoft.Json;
using System;
namespace joseevillasmil.TrelloDevops.Devops
{
  public class WorkItemFields
  {
    [JsonProperty("System.AreaPath")]
    public string SystemAreaPath { get; set; }

    [JsonProperty("System.TeamProject")]
    public string SystemTeamProject { get; set; }

    [JsonProperty("System.IterationPath")]
    public string SystemIterationPath { get; set; }

    [JsonProperty("System.WorkItemType")]
    public string SystemWorkItemType { get; set; }

    [JsonProperty("System.State")]
    public string SystemState { get; set; }

    [JsonProperty("System.Reason")]
    public string SystemReason { get; set; }

    [JsonProperty("System.CreatedDate")]
    public DateTime? SystemCreatedDate { get; set; }

    [JsonProperty("System.ChangedDate")]
    public DateTime? SystemChangedDate { get; set; }

    [JsonProperty("System.CommentCount")]
    public int? SystemCommentCount { get; set; }

    [JsonProperty("System.Parent")]
    public int? Parent { get; set; }

    [JsonProperty("System.AssignedTo")]
    public WorkItemOwner AssignedTo { get; set; }

    [JsonProperty("System.Title")]
    public string Title { get; set; }

    [JsonProperty("System.BoardColumn")]
    public string SystemBoardColumn { get; set; }

    [JsonProperty("System.BoardColumnDone")]
    public bool SystemBoardColumnDone { get; set; }

    [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
    public DateTime? MicrosoftVSTSCommonStateChangeDate { get; set; }

    [JsonProperty("Microsoft.VSTS.Common.Priority")]
    public int? MicrosoftVSTSCommonPriority { get; set; }

    [JsonProperty("Microsoft.VSTS.Common.Severity")]
    public string MicrosoftVSTSCommonSeverity { get; set; }

    [JsonProperty("Microsoft.VSTS.Common.StackRank")]
    public double MicrosoftVSTSCommonStackRank { get; set; }

    [JsonProperty("Microsoft.VSTS.Scheduling.TargetDate")]
    public DateTime? TargetDate { get; set; }

    [JsonProperty("Microsoft.VSTS.CMMI.TargetResolveDate")]
    public DateTime? ResolveDte { get; set; }

    [JsonProperty("Custom.Sistema")]
    public string CustomSistema { get; set; }

    [JsonProperty("Custom.NumeroIA")]
    public int? CustomNumeroIA { get; set; }

    [JsonProperty("Custom.Cliente")]
    public string CustomCliente { get; set; }

    [JsonProperty("Custom.Proceso")]
    public string CustomProceso { get; set; }

    [JsonProperty("Custom.NivelCriticidad")]
    public string CustomNivelCriticidad { get; set; }

    [JsonProperty("Custom.SubClasificacion")]
    public string CustomSubClasificacion { get; set; }

    [JsonProperty("Custom.SolucionClasificacion")]
    public string CustomSolucionClasificacion { get; set; }

    [JsonProperty("Custom.FechaCreacion")]
    public DateTime? CustomFechaCreacion { get; set; }

    [JsonProperty("Custom.FechaCompromisoDev")]
    public DateTime? CustomFechaCompromisoDev { get; set; }

    [JsonProperty("System.Description")]
    public string SystemDescription { get; set; }

    [JsonProperty("Custom.Solucion")]
    public string CustomSolucion { get; set; }

    [JsonProperty("System.Tags")]
    public string SystemTags { get; set; }

    [JsonProperty("Microsoft.VSTS.Scheduling.StoryPoints")]
    public double MicrosoftVSTSSchedulingStoryPoints { get; set; }

    [JsonProperty("Microsoft.VSTS.Scheduling.RemainingWork")]
    public double MicrosoftVSTSSchedulingRemainingWork { get; set; }

    [JsonProperty("Microsoft.VSTS.Scheduling.OriginalEstimate")]
    public double MicrosoftVSTSSchedulingOriginalEstimate { get; set; }

    [JsonProperty("Microsoft.VSTS.Scheduling.CompletedWork")]
    public double MicrosoftVSTSSchedulingCompletedWork { get; set; }

    [JsonProperty("Microsoft.VSTS.Common.Activity")]
    public string MicrosoftVSTSCommonActivity { get; set; }

    [JsonProperty("Microsoft.VSTS.Scheduling.StartDate")]
    public DateTime? StartDate { get; set; }

    public string href { get; set; }
  }
}
