using System.Text.Json;
using Microsoft.Extensions.Options;

namespace GoogleReCaptcha.Services;

public class RecaptchaService : IRecaptchaService
{
    private readonly HttpClient _httpClient;

    private readonly RecaptchaOptions _options;

    public RecaptchaService(HttpClient httpClient, IOptions<RecaptchaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<bool> VerifyTokenAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return false;

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("secret", _options.SecretKey),
            new KeyValuePair<string,string>("response", token)
        });

        var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
        if (!response.IsSuccessStatusCode) return false;

        await using var stream = await response.Content.ReadAsStreamAsync();
        var json = await JsonSerializer.DeserializeAsync<JsonElement>(stream);

        if (json.TryGetProperty("success", out var successProp) && successProp.GetBoolean() &&
            json.TryGetProperty("score", out var scoreProp))
        {
            double score = scoreProp.GetDouble();
            return score >= _options.MinimumScore;
        }

        return false;
    }
}