namespace GoogleReCaptcha.Services;

public interface IRecaptchaService
{
    Task<bool> VerifyTokenAsync(string token);
}