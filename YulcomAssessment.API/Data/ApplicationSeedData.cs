using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecurityLibrary.Cryptography;
using System.Formats.Asn1;
using System.Globalization;
using System;
using YulcomAssesment.API.Constants;
using YulcomAssesment.API.Data;
using CsvHelper;
using YulcomAssesment.API.Models.Yulcom;
using YulcomAssesment.API.Configurations;
using YulcomAssesment.API.Mappers;

namespace DikriptVerify.Data
{
    public static class AppSeedDataInit
    {
        public static void SeedData(this IApplicationBuilder app, YulcomAssesmentConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedKeys(serviceScope.ServiceProvider.GetService<YulcomAssesmentContext>());
                SeedServices(serviceScope.ServiceProvider.GetService<YulcomAssesmentContext>(), configuration);
            }
        }

        private static void SeedKeys(YulcomAssesmentContext context)
        {
            context.Database.SetCommandTimeout(30);
            context.Database.Migrate();

            if (!context.ApiKeys.AnyAsync(x => x.PublicApiKey == EncryptionConstants.PUBLIC_KEY).Result)
            {
                ApiKey api = new()
                {
                    Name  = EncryptionConstants.NAME_VALUE,
                    PublicApiKey = EncryptionConstants.PUBLIC_KEY,
                    SecretApiKey = EncryptionConstants.SECRET_KEY,
                };

                context.ApiKeys.Add(api);
            }

            context.SaveChanges();
        }
        private static void SeedServices(YulcomAssesmentContext context, YulcomAssesmentConfiguration configuration)
        {
            context.Database.SetCommandTimeout(30);
            context.Database.Migrate();

            List<YulcomModel> data = ReadCsv(configuration.CsvPath);

            if (context.YulcomAssesmentData.Count() < 1)
            {
                context.YulcomAssesmentData.AddRange(data.AsDto());
            }

            context.SaveChanges();
        }

        static List<YulcomModel> ReadCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<YulcomModel>().ToList();
            }
        }
    }
}
