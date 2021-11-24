using GestionContact.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using System.Net;

namespace GestionContact.CORE.servicesHTTP
{
    public class UserService : IUserService<User>
    {
        static string BaseUri = @"http://localhost:32205/api/";
     

        public bool Create(User entity)
        {
            using (HttpClient http = new HttpClient())
            {
                http.BaseAddress = new Uri(BaseUri);
                string json = JsonConvert.SerializeObject(entity);

                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                HttpResponseMessage responseMessage = http.PostAsync("User", httpContent).Result;
                return responseMessage.IsSuccessStatusCode;
            }
        }

        public bool Delete(int id)
        {
            using (HttpClient http = new HttpClient())
            {
                http.BaseAddress = new Uri(BaseUri);

                HttpResponseMessage responseMessage = http.DeleteAsync($"{BaseUri}User/" + id).Result;
                return responseMessage.IsSuccessStatusCode;
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                //la requête
                using (HttpResponseMessage response = client.GetAsync($"{BaseUri}User").Result)
                {
                    response.EnsureSuccessStatusCode();
                    using (HttpContent content = response.Content)
                    {
                        // la réponse, il ne resterai plus qu'à désérialiser
                        string result = content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<User[]>(result);
                    }
                }
            }
        }

        public User GetOne(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                //la requête
                using (HttpResponseMessage response = client.GetAsync($"{BaseUri}User/" + id).Result)
                {
                    response.EnsureSuccessStatusCode();
                    using (HttpContent content = response.Content)
                    {
                        // la réponse, il ne resterai plus qu'à désérialiser
                        string result = content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<User>(result);
                    }
                }
            }
        }

        public bool Update(int id, User entity)
        {
            using (HttpClient http = new HttpClient())
            {
                http.BaseAddress = new Uri(BaseUri);
                string json = JsonConvert.SerializeObject(entity);

                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                HttpResponseMessage responseMessage = http.PutAsync("User/" + id, httpContent).Result;
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                    return responseMessage.IsSuccessStatusCode;
                else
                    return false;
            }
        }

        public LoginDto Login(string Email, string password)
        {
            using (HttpClient client = new HttpClient())
            {

                LoginDto dto = new LoginDto()
                {
                    Email = Email,
                    Password = password
                };
                string json = JsonConvert.SerializeObject(dto);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                //la requête
                using (HttpResponseMessage response = client.PostAsync($"{BaseUri}User/Login", httpContent).Result)
                {
                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // la réponse, il ne resterai plus qu'à désérialiser
                            string result = content.ReadAsStringAsync().Result;
                            dto = JsonConvert.DeserializeObject<LoginDto>(result);
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
}
