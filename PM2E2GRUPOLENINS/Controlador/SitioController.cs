using Android.Views;
using Newtonsoft.Json;
using PM2E2GRUPOLENINS.Config;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PM2E2GRUPOLENINS.Controlador
{
    public class SitioController
    {
        public async static Task<Modelo.Msg> CreateSitio(Modelo.Sitio sitio)
        {
            Modelo.Msg msg = new Modelo.Msg();

            String jsonObject = JsonConvert.SerializeObject(sitio);
            System.Net.Http.StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;
                response = await client.PostAsync(ConfigProcess.ApiPOST, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    msg = JsonConvert.DeserializeObject<Modelo.Msg>(result);
                }
            }

            return msg;
        }
        public async static Task<List<Modelo.Sitio>> GetSitio()
        {
            List<Modelo.Sitio> alums = new List<Modelo.Sitio>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(ConfigProcess.ApiGET);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    alums = JsonConvert.DeserializeObject<List<Modelo.Sitio>>(result);
                }

                return alums;
            }
        }
    }
}
