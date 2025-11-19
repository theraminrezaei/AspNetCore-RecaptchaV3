namespace GoogleReCaptcha.Services;

public record RecaptchaOptions(
    string SiteKey,
    string SecretKey,
    double MinimumScore
    );