using joseevillasmil.TrelloDevops.Trello;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace joseevillasmil.TrelloDevops
{
    public class TrelloClient
    {
        /// <summary>
        /// Author: Jose Villasmil - https://www.josevillasmil.cl
        ///  Docs Links: https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-group-boards
        /// </summary>
        private string _apiKey = "";
        private string _token = "";
        private string _boardId = "";
        private string _memberId = "";
        private List<Trello.List> _lists = new List<Trello.List>();
        private Dictionary<string, string> _urlApi = new Dictionary<string, string>()
        {
            {"GetCards", "https://api.trello.com/1/boards/{{boardId}}/cards?key={{apiKey}}&token={{apiToken}}"},
            {"CreateCard", "https://api.trello.com/1/cards?key={{apiKey}}&token={{apiToken}}"},
            {"UpdateCard", "https://api.trello.com/1/cards/{{id}}?key={{apiKey}}&token={{apiToken}}"},
            {"AddMember", "https://api.trello.com/1/cards/{{id}}/idMembers?key={{apiKey}}&token={{apiToken}}" },
            {"GetLists", "https://api.trello.com/1/boards/{{boardId}}/lists?key={{apiKey}}&token={{apiToken}}"},
        };
        public List<Trello.List> Lists { get { return _lists; } }
        /// <summary>
        /// Create client with parameters.
        /// </summary>
        /// <param name="apiKey"> ApiKey generated in trello</param>
        /// <param name="token"> Token generated in trello from the apiKey </param>
        /// <param name="boardId">The id or the shortcut of the board in trello.</param>
        public TrelloClient(string apiKey, string token, string boardId, string memberId)
        {
            this._apiKey = apiKey;
            this._token = token;
            this._boardId = boardId;
            this._memberId = memberId;

            foreach(KeyValuePair<string, string> item in _urlApi)
            {
                _urlApi[item.Key] = item.Value.Replace("{{boardId}}", boardId)
                                              .Replace("{{apiKey}}", apiKey)
                                              .Replace("{{apiToken}}", token) ;
            }
            LoadLists();
        }
        /// <summary>
        /// First, load all lists, internally. (new, working, closed)
        /// </summary>
        private void LoadLists()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    this._lists = httpClient.GetFromJsonAsync<List<Trello.List>>(_urlApi["GetLists"])
                                            .GetAwaiter()
                                            .GetResult();
                    if (this._lists.Count > 0)
                    {
                        this._lists = this._lists.OrderBy(o => o.pos).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Return all cards of the board.
        /// </summary>
        /// <returns>List of cards of the boards.</returns>
        public List<Trello.Card> GetCards()
        {
            List<Trello.Card> cards = new List<Trello.Card>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    cards = httpClient.GetFromJsonAsync<List<Trello.Card>>(_urlApi["GetCards"])
                                            .GetAwaiter()
                                            .GetResult();

                }
            }
            catch(Exception e)
            {

            }
            return cards;
        }
        /// <summary>
        /// Create a new card, always in the first list of the board.
        /// </summary>
        /// <param name="_card"></param>
        /// <returns>true / false and an errorMessage string</returns>
        public bool CreateCard(Devops.WorkItem workItem, ref string errorMenssage)
        {
            var trelloCard = new Card()
            {
                idBoard = _boardId,
                name = workItem.fields.Title,
                desc = workItem.fields.SystemDescription,
                idMembers = (new List<string>(){
                    (this._memberId)
                })
            };
            string idList = this._lists.Where(w => String.Equals(w.name.ToLower(), workItem.fields.SystemState.ToLower())).FirstOrDefault()?.id;
            if (String.IsNullOrEmpty(idList)) idList = this._lists.OrderBy( o=> o.pos).ToList().First().id;
            trelloCard.idList = idList;
            trelloCard.idMembers.Add(idList);
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(trelloCard);
                    var result =  httpClient.PostAsJsonAsync(_urlApi["CreateCard"], trelloCard).GetAwaiter().GetResult();
                }
            }
            catch(Exception e) {
                errorMenssage = e.Message;
                return false;
            }
            
            return true;
        }
        /// <summary>
        /// Update Card in trello board
        /// </summary>
        /// <param name="card">The object of the card to update</param>
        /// <returns>true / false and the error message string if exists.</returns>
        public bool UpdateCard(Card card, ref string errorMenssage)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(card);
                    var result = httpClient.PutAsJsonAsync(_urlApi["UpdateCard"].Replace("{{id}}", card.id), card).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                errorMenssage = e.Message;
                return false;
            }

            return true;
        }
        /// <summary>
        /// Add member to a card, method in progress....
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="memberId"></param>
        /// <param name="errorMenssage"></param>
        /// <returns>true / false and the error message string if exists.</returns>
        //public bool AddMember(string cardId, string memberId, ref string errorMenssage)
        //{
        //    try
        //    {
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //         }
        //    }
        //    catch (Exception e)
        //    {
        //        errorMenssage = e.Message;
        //        return false;
        //    }

        //    return true;
        //}
    }
}
