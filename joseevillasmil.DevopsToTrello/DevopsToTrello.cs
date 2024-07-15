using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using joseevillasmil.TrelloDevops;
using joseevillasmil.TrelloDevops.Trello;
using joseevillasmil.TrelloDevops.Devops;

namespace joseevillasmil.DevopsToTrello
{
    public class DevopsToTrello
    {
        private readonly ILogger _logger;

        public DevopsToTrello(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DevopsToTrello>();
        }

        [Function("DevopsToTrello")]
        public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            try
            {
                /// Trello Keys
                string TrelloApiKey = Environment.GetEnvironmentVariable("TrelloApiKey");
                string TrelloApiToken = Environment.GetEnvironmentVariable("TrelloApiToken");
                string TrelloBoardId = Environment.GetEnvironmentVariable("TrelloBoardId");
                string TrelloMemberId = Environment.GetEnvironmentVariable("TrelloMemberId");
                /// Devops Keys.
                string DevopsToken = Environment.GetEnvironmentVariable("DevopsToken");
                string DevopsQueryId = Environment.GetEnvironmentVariable("DevopsQueryId");
                string DevopsOrganization = Environment.GetEnvironmentVariable("DevopsOrganization");
                string DevopsProject = Environment.GetEnvironmentVariable("DevopsProject");

                /// Search all avaiable cards.
                var trelloClient = new TrelloClient(TrelloApiKey, TrelloApiToken, TrelloBoardId, TrelloMemberId);
                var cards = trelloClient.GetCards();

                // search all WorkItems in devops.
                var devopsClient = new DevopsClient(DevopsToken, DevopsOrganization, DevopsProject);
                string errorMessage = "";
                var workItems = devopsClient.getWorkItems(DevopsQueryId, ref errorMessage);
                if (!String.IsNullOrEmpty(errorMessage)) _logger.LogError(errorMessage); // log

                // check if a devops item does not exists in trello.
                var newDevopsWorkItems = workItems.Where(w =>
                                                         cards.Where(
                                                             w2 => String.Equals(w2.name.ToLower(), w.fields.Title.ToLower())).Count() == 0
                                                            )
                    .Where(w => !String.IsNullOrEmpty(w.fields.Title))
                    .ToList();
                if (newDevopsWorkItems.Count() > 0)
                {
                    foreach (var newItem in newDevopsWorkItems)
                    {
                        trelloClient.CreateCard(newItem, ref errorMessage);
                        if (!String.IsNullOrEmpty(errorMessage)) _logger.LogError(errorMessage); // log
                    }
                }
                // looks for diferences of states, only states, reload cards
                cards = trelloClient.GetCards();
                // looks for cards that have diferent state than workitem, it's important to use a foreach.
                foreach (var card in cards)
                {
                    var devopsWorkItem = workItems.Where(w => String.Equals(w.fields.Title.ToLower(), card.name.ToLower())).First();
                    if (devopsWorkItem != null)
                    {
                        string stateOfCard = trelloClient.Lists.Where(w => String.Equals(w.id, card.idList)).First().name;
                        // if list name is diferent than workitem state.
                        if (!String.Equals(devopsWorkItem.fields.SystemState.ToLower(), stateOfCard.ToLower()))
                        {
                            // check wich was edited latest.
                            if (devopsWorkItem.fields.SystemChangedDate > card.dateLastActivity)
                            {
                                // if devops was edited, then update card.
                                card.idList = trelloClient.Lists.Where(w => String.Equals(w.name.ToLower(), devopsWorkItem.fields.SystemState.ToLower())).First().id;
                                trelloClient.UpdateCard(card, ref errorMessage);
                                if (!String.IsNullOrEmpty(errorMessage)) _logger.LogError(errorMessage); // log
                            }
                            else if (devopsWorkItem.fields.SystemChangedDate < card.dateLastActivity)
                            {
                                // if card was edited, then update devop.
                                devopsWorkItem.fields.SystemState = trelloClient.Lists.Where(w => String.Equals(w.id, card.idList)).First().name;
                                devopsClient.updateWorkItemState(devopsWorkItem.id.ToString(), stateOfCard, ref errorMessage);
                                if (!String.IsNullOrEmpty(errorMessage)) _logger.LogError(errorMessage); // log
                            }
                        }
                    }
                }
                _logger.LogInformation("Execution Succeeded");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message); // log
            }
        }
    }
}
