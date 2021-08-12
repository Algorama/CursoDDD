using Kernel.Domain.Model.Dtos;
using Kernel.Domain.Model.Helpers;
using Kernel.Domain.Model.Settings;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Newtonsoft.Json;
using System;

namespace Kernel.Infra.Jwt
{
    public class JwtTokenHelper : ITokenHelper
    {
        private readonly AppSettings _appSettings;

        public JwtTokenHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string GenerateNewSecret() => Guid.NewGuid().ToString("N").Substring(0, 20);

        public string GetTokenString(Token token)
        {
            var secret = GetSecretFromSettings();

            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var tokenString = encoder.Encode(token, secret);

            return tokenString;
        }

        public Token GetToken(string tokenString)
        {
            if (string.IsNullOrWhiteSpace(tokenString))
                return null;

            var secret = GetSecretFromSettings();

            try
            {
                var algorithm = new HMACSHA256Algorithm();
                var serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                var validator = new JwtValidator(serializer, provider);
                var urlEncoder = new JwtBase64UrlEncoder();
                var decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(tokenString, secret, verify: true);
                return JsonConvert.DeserializeObject<Token>(json);
            }
            catch (SignatureVerificationException)
            {
                Console.Out.WriteLine("Token String is an invalid JWT!");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex);
            }
            return null;
        }

        private string GetSecretFromSettings()
        {
            var secret = _appSettings?.TokenSettings?.TokenSecretKey;
            if (string.IsNullOrWhiteSpace(secret))
                throw new Exception("The Secrect Key was not found in appsettings!");

            return secret;
        }
    }
}
