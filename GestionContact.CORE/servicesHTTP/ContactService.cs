using GestionContact.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact.CORE.servicesHTTP
{
    public class ContactService : IContactService<Contact>
    {
        static string BaseUri = @"http://localhost:32205/api/";

        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool Create(Contact entity)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJMYXN0TmFtZSI6Ik1vcnJlIiwiRmlyc3ROYW1lIjoiVGhpZXJyeSIsIkVtYWlsIjoidGhpZXJyeS5tb3JyZUBjb2duaXRpYy5iZSIsIm5iZiI6MTYyMTY3OTE2NCwiZXhwIjoxNjIxNzY1NTY0fQ.A2GPAGgGXTsdJiGkelsGKBe8E1ssTvE17s3nrTxyD6tcOkjvDGSlubb2nAtylXkj1CiI5H6L72WRepU6QOwzdA"));

            _httpClient.BaseAddress = new Uri(BaseUri);
            string json = JsonConvert.SerializeObject(entity);

            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage responseMessage = _httpClient.PostAsync("Contact", httpContent).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public bool Delete(int id)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJMYXN0TmFtZSI6Ik1vcnJlIiwiRmlyc3ROYW1lIjoiVGhpZXJyeSIsIkVtYWlsIjoidGhpZXJyeS5tb3JyZUBjb2duaXRpYy5iZSIsIm5iZiI6MTYyMTY3OTE2NCwiZXhwIjoxNjIxNzY1NTY0fQ.A2GPAGgGXTsdJiGkelsGKBe8E1ssTvE17s3nrTxyD6tcOkjvDGSlubb2nAtylXkj1CiI5H6L72WRepU6QOwzdA"));

            _httpClient.BaseAddress = new Uri(BaseUri);

            HttpResponseMessage responseMessage = _httpClient.DeleteAsync("Contact/" + id).Result;
            return responseMessage.IsSuccessStatusCode;

        }

        public IEnumerable<Contact> GetAll(int userId)
        {
            _httpClient.BaseAddress = new Uri(BaseUri);
            _httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJMYXN0TmFtZSI6Ik1vcnJlIiwiRmlyc3ROYW1lIjoiVGhpZXJyeSIsIkVtYWlsIjoidGhpZXJyeS5tb3JyZUBjb2duaXRpYy5iZSIsIm5iZiI6MTYyMTY3OTE2NCwiZXhwIjoxNjIxNzY1NTY0fQ.A2GPAGgGXTsdJiGkelsGKBe8E1ssTvE17s3nrTxyD6tcOkjvDGSlubb2nAtylXkj1CiI5H6L72WRepU6QOwzdA"));
            //la requête
            using (HttpResponseMessage response = _httpClient.GetAsync($"Contact/" + userId).Result)
            {
                IEnumerable<Contact> dto;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (HttpContent content = response.Content)
                    {
                        // la réponse, il ne resterai plus qu'à désérialiser
                        string result = content.ReadAsStringAsync().Result;
                        dto = JsonConvert.DeserializeObject<Contact[]>(result);
                    }
                }
                else
                {
                    dto = null;
                }
                return dto;
            }
        }

        public Contact GetOne(int id, int userId)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJMYXN0TmFtZSI6Ik1vcnJlIiwiRmlyc3ROYW1lIjoiVGhpZXJyeSIsIkVtYWlsIjoidGhpZXJyeS5tb3JyZUBjb2duaXRpYy5iZSIsIm5iZiI6MTYyMTY3OTE2NCwiZXhwIjoxNjIxNzY1NTY0fQ.A2GPAGgGXTsdJiGkelsGKBe8E1ssTvE17s3nrTxyD6tcOkjvDGSlubb2nAtylXkj1CiI5H6L72WRepU6QOwzdA"));
            _httpClient.BaseAddress = new Uri(BaseUri);
            //la requête
            using (HttpResponseMessage response = _httpClient.GetAsync($"Contact/" + id + "/" + userId).Result)
            {
                response.EnsureSuccessStatusCode();
                using (HttpContent content = response.Content)
                {
                    // la réponse, il ne resterai plus qu'à désérialiser
                    string result = content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Contact>(result);
                }
            }

        }

        public bool Update(int id, Contact entity)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJMYXN0TmFtZSI6Ik1vcnJlIiwiRmlyc3ROYW1lIjoiVGhpZXJyeSIsIkVtYWlsIjoidGhpZXJyeS5tb3JyZUBjb2duaXRpYy5iZSIsIm5iZiI6MTYyMTY3OTE2NCwiZXhwIjoxNjIxNzY1NTY0fQ.A2GPAGgGXTsdJiGkelsGKBe8E1ssTvE17s3nrTxyD6tcOkjvDGSlubb2nAtylXkj1CiI5H6L72WRepU6QOwzdA"));

            _httpClient.BaseAddress = new Uri(BaseUri);
            string json = JsonConvert.SerializeObject(entity);

            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage responseMessage = _httpClient.PutAsync("Contact/" + id, httpContent).Result;
            return responseMessage.IsSuccessStatusCode;

        }

        public IEnumerable<Contact> GetAllByName(int userId, string Name)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjEiLCJMYXN0TmFtZSI6Ik1vcnJlIiwiRmlyc3ROYW1lIjoiVGhpZXJyeSIsIkVtYWlsIjoidGhpZXJyeS5tb3JyZUBjb2duaXRpYy5iZSIsIm5iZiI6MTYyMTY3OTE2NCwiZXhwIjoxNjIxNzY1NTY0fQ.A2GPAGgGXTsdJiGkelsGKBe8E1ssTvE17s3nrTxyD6tcOkjvDGSlubb2nAtylXkj1CiI5H6L72WRepU6QOwzdA"));
            _httpClient.BaseAddress = new Uri(BaseUri);
            //la requête
            using (HttpResponseMessage response = _httpClient.GetAsync($"Contact/ByName/" + Name + "/" + userId).Result)
            {
                IEnumerable<Contact> dto;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (HttpContent content = response.Content)
                    {
                        // la réponse, il ne resterai plus qu'à désérialiser
                        string result = content.ReadAsStringAsync().Result;
                        dto = JsonConvert.DeserializeObject<Contact[]>(result);
                    }
                }
                else
                {
                    dto = null;
                }
                return dto;
            }

        }
    }
}
