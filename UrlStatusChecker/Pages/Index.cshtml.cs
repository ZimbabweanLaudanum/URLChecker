using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

namespace UrlStatusChecker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string StatusCode { get; private set; }
        public string ImageUrl { get; private set; }

        public async Task<IActionResult> OnPostAsync(string url)
        {
            try
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) == true)
                {

                    var response = await _httpClient.GetAsync(url);
                    StatusCode = response.StatusCode.ToString();

                    // ���������, �������� �� ����� ������������
                    if (response.Content.Headers.ContentType.MediaType.StartsWith("image/"))
                    {
                        ImageUrl = url;
                    }
                }
                else
                {
                    StatusCode = "������: ������� �������� URL. ����������, ������� �������������� ������";
                    ImageUrl = "https://i1.sndcdn.com/artworks-000109751744-1hk818-t500x500.jpg";
                }
            }
            
            
            catch (HttpRequestException e)
            {
                StatusCode = $"������: {e.Message}";
                ImageUrl = $"https://http.cat/{StatusCode}.jpg";
            }
            return Page();
        }
    }
}
