using Microsoft.AspNetCore.Mvc;

namespace Index_Libri.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleAuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public GoogleAuthController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        // Endpoint to initiate the Google OAuth flow
        [HttpGet("/auth/login")]
        public IActionResult Login()
        {
            var clientId = _configuration["OAuth:ClientID"];
            var redirectUri = "https://localhost:7169/auth/callback";
            var scope = "https://www.googleapis.com/auth/books";
            var url = $"https://accounts.google.com/oauth2/v2/auth?client_id={clientId}&redirect_uri={redirectUri}&response_type=code&scope={scope}";
            return Redirect(url);
        }

        // Endpoint to handle the callback
        [HttpGet("/auth/callback")]
        public async Task<IActionResult> GoogleCallback(string code)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token");
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["client_id"] = _configuration["OAuth:ClientID"],
                    ["client_secret"] = _configuration["OAuth:ClientSecret"],
                    ["code"] = code,
                    ["grant_type"] = "authorization_code",
                    ["redirect_uri"] = "https://localhost:7169/auth/google/callback",
                    ["scope"] = "https://www.googleapis.com/auth/books"
                });

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Process the response content (extract access token, etc.)
                    // For simplicity, assuming authentication is successful
                    return Ok(new { authenticated = true });
                }
                else
                {
                    // Handle non-successful HTTP response (e.g., error from Google API)
                    return StatusCode((int)response.StatusCode, "Error during authentication");
                }
            }
            catch (Exception ex)
            {
                // Handle general exception (e.g., network error)
                return StatusCode(500, "Internal server error");
            }
        }

        // Endpoint to provide ClientID to the frontend
        // TO DO: Implement CryptoJS to encrypt the ClientID
        [HttpGet("/auth/clientid")]
        public IActionResult GetClientID()
        {
            return Ok(new { clientID = _configuration["OAuth:ClientID"] });
        }
    }
}
