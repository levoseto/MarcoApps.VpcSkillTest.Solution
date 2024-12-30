namespace MarcoApps.VpcSkillTest.Services.Mobile.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;

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