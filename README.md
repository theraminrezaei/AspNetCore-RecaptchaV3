# Google reCAPTCHA v3 in ASP.NET Core

This is a simple example project demonstrating **how to integrate Google reCAPTCHA v3** in an ASP.NET Core MVC application. The goal is to provide a clean, minimal, and easy-to-understand implementation for bot protection.

---

## 🚀 What This Project Does

* Integrates **Google reCAPTCHA v3** without a visible checkbox
* Sends a token to the server for verification
* Validates the token using Google's API and checks the score
* Accepts the form only if the score meets the minimum threshold

Perfect for small applications or learning purposes.

---

## 📌 How to Use

### 1. Get Your reCAPTCHA v3 Keys

Create a site here:
[Google reCAPTCHA Admin](https://www.google.com/recaptcha/admin)

Choose:

* **reCAPTCHA v3**

Copy:

* **Site Key**
* **Secret Key**

### 2. Add Keys to `appsettings.json`

```json
"RecaptchaV3": {
  "SiteKey": "YOUR_SITE_KEY",
  "SecretKey": "YOUR_SECRET_KEY",
  "MinimumScore": 0.5
}
```

### 3. Run the Project

```bash
dotnet restore
dotnet run
```

Open in browser:

```
https://localhost:5001
```

---

## 🧩 How It Works

1. User submits the form
2. reCAPTCHA v3 generates a token asynchronously
3. Token is sent to the server via a hidden field
4. Server verifies token with Google API
5. Google returns a score; if score >= MinimumScore → form is processed

---

## 🔐 Notes

* Do not expose your **Secret Key** in public repositories
* Always verify the token **on the server**
* Adjust `MinimumScore` according to your risk tolerance (default 0.5)

---

This project is intentionally minimal, focused, and ready to use as a demonstration or quick integration into an ASP.NET Core application.
