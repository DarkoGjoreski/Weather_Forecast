using Weather_Forecast.Application.Abstracitions;

namespace Weather_Forecast.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        public async Task<string> SearchByCity(string city)
        {
            //We can do validations on the backend also, not only if the data is there but if it is valid also.
            //We can do this in the domain also by writeing methods that check the validity.
            //We can use Fluent validator library also.
            if (string.IsNullOrEmpty(city))
                throw new Exception("No city was provided");

            string apiKey = "7dd1d3039c12960e4f9921dafa2238ad";

            // OpenWeatherMap API endpoint, all the parameters can be dunamic depending on the front end input
            string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&cnt=4&units=metric";
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return String.Empty;
        }
    }
}
