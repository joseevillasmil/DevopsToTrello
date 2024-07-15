using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using joseevillasmil.TrelloDevops.Devops;
using Newtonsoft.Json;

namespace joseevillasmil.TrelloDevops
{
    public class DevopsClient
    {
        /// <summary>s
        ///  Author: Jose Villasmil - https://www.josevillasmil.cl
        ///  Docs Links https://learn.microsoft.com/en-us/rest/api/azure/devops/wit/work-items/update?view=azure-devops-rest-7.1&tabs=HTTP
        /// </summary>
        private string token = "";
        private string organization = "";
        private string project = "";
        public DevopsClient(string userAccesToken, string organization, string project)
        {
            token = userAccesToken;
            this.organization = organization;
            this.project = project;
        }
        /// <summary>
        /// We execute a defined query in devops (it makes it way easier)
        /// </summary>
        /// <param name="queryId">Id of the query string in azure devops.</param>
        /// <param name="errorMessage">a reference string to return error message, return empty if all its ok.</param>
        /// <returns>List of WorkItems and errorMessage</returns>
        public List<WorkItem> getWorkItems(string queryId, ref string errorMessage)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            QueryResult queryResult = new QueryResult();
            WorkListResult workListResult = new WorkListResult();
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    string base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(":" + token));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
                    string _urlQuery = $"https://dev.azure.com/{this.organization}/{this.project}/ventas/_apis/wit/wiql/{queryId}?api-version=5.1";
                    QueryResult _queryResult = httpClient.GetFromJsonAsync<QueryResult>(_urlQuery).GetAwaiter().GetResult();
                    // Now, search all items found in query
                    string workitemIds = string.Join(",", _queryResult.workItems.Select(s => s.id.ToString()).ToList());
                    string _urlItems = $"https://dev.azure.com/{this.organization}/{this.project}/_apis/wit/workitems?ids={workitemIds}&api-version=7.0&fields=System.Id,System.Title,System.WorkItemType,System.State,Custom.NumeroIA,Custom.Cliente,System.Description,Custom.FechaCompromisoDev,System.Parent,Microsoft.VSTS.Scheduling.StartDate,Microsoft.VSTS.Scheduling.TargetDate,System.AssignedTo,System.ChangedDate";
                    string WorkItems = httpClient.GetStringAsync(_urlItems).GetAwaiter().GetResult();
                    workItems = JsonConvert.DeserializeObject<WorkListResult>(WorkItems).value;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            return workItems;
        }
        /// <summary>
        /// Update state of a Devops workitem.
        /// Documentation link https://learn.microsoft.com/en-us/rest/api/azure/devops/wit/work-items/update?view=azure-devops-rest-7.1&tabs=HTTP
        /// </summary>
        /// <param name="workItemId">Id of the workitem</param>
        /// <param name="newState">New state string.</param>
        /// <param name="errorMessage">a reference string to return error message, return empty if all its ok.</param>
        /// <returns>true / false and errorMessage</returns>
        public bool updateWorkItemState(string workItemId, string newState, ref string errorMessage)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(":" + token));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
                    string _urlUpdate = $"https://dev.azure.com/{this.organization}/_apis/wit/workitems/{workItemId}?api-version=7.1-preview.3";
                    // it's important to say that this method requires a PATCH request with Content-Type = application/json-patch+json
                    // so its not possible to use PatchJsonAsync, we need to use PatchAsync.
                    string jsonString = JsonConvert.SerializeObject(new List<UpdateField>() {
                        new UpdateField()
                        {
                            op = "add",
                            path = "/fields/System.State",
                            value = newState
                        }
                    });
                    var requestMessage = new HttpRequestMessage();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
                    requestMessage.Method = HttpMethod.Patch;
                    requestMessage.Content = new StringContent(jsonString, Encoding.UTF8, "application/json-patch+json");
                    requestMessage.RequestUri = new Uri(_urlUpdate);
                    var result = httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
                }
            }
            catch(Exception e)
            {
                errorMessage = e.Message;
                return false;
            }
            return true;
        }
    }
}
