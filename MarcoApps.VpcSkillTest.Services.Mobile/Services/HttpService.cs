namespace MarcoApps.VpcSkillTest.Services.Mobile.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;

    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>(endpoint);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener datos de {endpoint}: {ex.Message}");
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                return await _httpClient.PostAsJsonAsync(endpoint, data);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar datos a {endpoint}: {ex.Message}");
            }
        }

        public async Task<T> PostAsync<T, K>(string endpoint, K requestBody)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}, {response.ReasonPhrase}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar datos a {endpoint}: {ex.Message}");
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
        {
            try
            {
                return await _httpClient.PutAsJsonAsync(endpoint, data);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar datos en {endpoint}: {ex.Message}");
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            try
            {
                return await _httpClient.DeleteAsync(endpoint);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar datos de {endpoint}: {ex.Message}");
            }
        }
    }
}