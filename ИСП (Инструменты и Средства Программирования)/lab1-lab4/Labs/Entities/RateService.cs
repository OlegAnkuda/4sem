using Labs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
//using static Android.Provider.Telephony.Mms;
using NbrbAPI.Models;

namespace Labs.Entities
{
    internal class RateService : IRateService
    {
        private readonly HttpClient httpClient;
        public RateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Rate>> GetRates(DateTime date)
        {
            string apiUrl = $"https://www.nbrb.by/api/exrates/rates?onDate={date:yyyy-MM-dd}&periodicity=0";
            try
            {
                IEnumerable<Rate> response = await httpClient.GetFromJsonAsync<IEnumerable<Rate>>(apiUrl);

                if (response != null && response != null)
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }
    }
}
