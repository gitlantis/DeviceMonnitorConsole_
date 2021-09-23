using Newtonsoft.Json;
using softHardwareAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace softHardwareAdmin
{
    class TranCiever
    {
        public async Task<T> PostAsync<T>(string suburl, T data)
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = await GetToken();

                StringContent strContent = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseString = await client.PostAsync(Constants.BasURL + suburl, strContent);
                var respResult = JsonConvert.DeserializeObject<T>(await responseString.Content.ReadAsStringAsync());
                return respResult;

            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }
        }

        public async Task<Guid> PostGuiderAsync<T>(string suburl, T data)
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = await GetToken();

                StringContent strContent = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseString = await client.PostAsync(Constants.BasURL + suburl, strContent);
                var respResult = JsonConvert.DeserializeObject<Guid>(await responseString.Content.ReadAsStringAsync());
                return respResult;

            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }
        }

        public async Task<T> PostAsync<T>(string suburl, Guid data)
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = await GetToken();

                StringContent strContent = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseString = await client.PostAsync(Constants.BasURL + suburl, strContent);
                var respResult = JsonConvert.DeserializeObject<T>(await responseString.Content.ReadAsStringAsync());
                return respResult;

            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }
        }


        private async Task<string> GetToken()
        {
            HttpClient client = new HttpClient();

            var token = await ReadToken();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //var tokenString = await client.PostAsync(Constants.BasURL + "User/ValidateTokin", tokenContent);
            StringContent tokenContent = new StringContent(JsonConvert.SerializeObject(token), System.Text.Encoding.UTF8, "application/json");
            var result = await client.PostAsync(Constants.BasURL + "User/ValidateToken", tokenContent);

            if (!result.IsSuccessStatusCode) token = await Authorize();
            return token;
        }
        private async Task<string> Authorize()
        {

            var model = new User();
            model.username = Constants.ApiUser;
            model.password = Constants.ApiPass;

            HttpClient client = new HttpClient();
            StringContent strContent = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");

            try
            {
                var responseString = await client.PostAsync(Constants.BasURL + "User/Login", strContent);

                var content = await responseString.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(content);

                using (var writer = new StreamWriter("auth.chip"))
                {
                    writer.WriteLine(user.token);
                }

                return user.token;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private async Task<string> ReadToken()
        {
            try
            {
                using (var reader = new StreamReader("auth.chip"))
                {
                    string line = "";
                    string str = "";
                    while ((line = reader.ReadLine()) != null) str += line;
                    return str;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
