using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.DependencyInjection;

namespace fitness_web_api.Extensions.Services
{
    public static class DataProtectionService
    {
        /// <summary>
        ///     Adds Data Protection Service
        /// </summary>
        /// <param name="services"></param>
        public static void AddDataProtectionService(this IServiceCollection services)
        {
            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });
        }
    }
}