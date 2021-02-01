using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AWSSecretManagerTest
{
    public class AWSSecretHelper
    {
       // public static SecretsManagerCache cache;
        //public static string GetCacheData(string secretName)
        //{
        //    return cache.GetSecretString(secretName).Result;
        //}
        public  string GetSecret(string secretName)
        {
            var config = new AmazonSecretsManagerConfig { RegionEndpoint = RegionEndpoint.USWest2 };
            config.SetWebProxy(HttpClient.DefaultProxy);
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(config);

            GetSecretValueRequest request = new GetSecretValueRequest()
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
            };
            GetSecretValueResponse response = null;
            try
            {
                response = client.GetSecretValueAsync(request).Result;
               // cache = new SecretsManagerCache(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.ToString();
            }

            return response?.SecretString;
        }
    }
}
