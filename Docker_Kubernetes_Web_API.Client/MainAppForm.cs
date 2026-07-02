using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Identity.Client;
using Azure.Core;
using Azure.Identity;



namespace OAuth2Client
{
    public partial class MainAppForm : Form
    {
        public MainAppForm()
        {
            InitializeComponent();
        }

        private async void button_Invoke_Click(object sender, EventArgs e)
        {

            
            string tenantID = "02ab6bc4-9edc-4e50-b34f-f804fb5ff6fa";

            
            // 1. Initialize the confidential client application
            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                .Create("feb6fe33-8229-4760-b244-a428a9f437ab")                 // Client ( Calling Application ) Application(client)ID
                .WithClientSecret("5aB8Q~guVMdZOkCRhHRjJdlclD39gKnAsS7d9bjd")       // Client ( Calling Application ) Secret Value
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantID}"))   // https://login.microsoftonline.com/{tenantId}
                .Build();

            // 2. Request the OAuth token for your App Service
            // Note: The scope typically matches your App Service's Application ID URI or is the /.default for custom app registrations
            
            string[] scopes = new string[] { "api://1a983a44-a729-4269-a083-254a2e0b6745/.default" };   // api://YOUR_APP_SERVICE_CLIENT_ID/.default
           
            //string[] scopes = new string[] { "api://1a983a44-a729-4269-a083-254a2e0b6745/Web.API.Access/.default" };   // api://YOUR_APP_SERVICE_CLIENT_ID/.default

            // api://1a983a44-a729-4269-a083-254a2e0b6745/Web.API.Access   Actual Scope

            AuthenticationResult result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            

            string accessToken = result.AccessToken;



            /*
            // 1. Define your Azure AD / Entra ID tenant and client application credentials
            string tenantId = "02ab6bc4-9edc-4e50-b34f-f804fb5ff6fa";
            string clientId = "feb6fe33-8229-4760-b244-a428a9f437ab";
            string clientSecret = "5aB8Q~guVMdZOkCRhHRjJdlclD39gKnAsS7d9bjd";
            string accessToken = "";


            // 2. Define the exact scope for the target API using the required '/.default' format
            // This tells Azure to bake all pre-configured application permissions into the token
            string targetApiAppIdUri = "api://1a983a44-a729-4269-a083-254a2e0b6745";
            string[] scopes = new[] { $"{targetApiAppIdUri}/.default" };

            // 3. Initialize the ClientSecretCredential object
            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

            try
            {
                // 4. Request the direct application token 
                var tokenRequestContext = new TokenRequestContext(scopes);
                AccessToken tokenResult = await credential.GetTokenAsync(tokenRequestContext);

                // 5. Output your bearer token string containing the designated App Roles
                accessToken = tokenResult.Token;

                Console.WriteLine($"Access Token: {accessToken}");
            }
            catch (AuthenticationFailedException ex)
            {
                Console.WriteLine($"Authentication failed: {ex.Message}");
            }
            */

            /*
            string tenantId = "02ab6bc4-9edc-4e50-b34f-f804fb5ff6fa";
            string clientId = "feb6fe33-8229-4760-b244-a428a9f437ab";
            string clientSecret = "5aB8Q~guVMdZOkCRhHRjJdlclD39gKnAsS7d9bjd";
            string accessToken = "";


            // 2. Define the exact scope for the target API using the required '/.default' format
            // This tells Azure to bake all pre-configured application permissions into the token
            string targetApiAppIdUri = "api://1a983a44-a729-4269-a083-254a2e0b6745";
            string[] scopes = new[] { $"{targetApiAppIdUri}/.default" };

            // 3. Initialize the ClientSecretCredential object
            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);


            // 2. Request a token for your backend App Service scope 
            // The scope pattern for app-to-app auth is "api://<backend-client-id>/.default"
            var tokenRequestContext = new TokenRequestContext(new[] { "api://1a983a44-a729-4269-a083-254a2e0b6745/.default" });
            var tokenResult = await credential.GetTokenAsync(tokenRequestContext);

            accessToken = tokenResult.Token;
            */


            // 3. Make the API call to your App Service

            
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://oauth2webapi-e2fnfjetahdyc2b0.canadacentral-01.azurewebsites.net/");

                // Attach the OAuth token as a Bearer token
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");


                try
                {

                    // Call your secured endpoint
                    HttpResponseMessage response = await client.GetAsync("api/Secured/SecuredZipCodeLookup/14052");

                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        // Handle your response data here
                    }

                }
                catch (AuthenticationFailedException ex)
                {
                    Console.WriteLine("Error: " + ex.InnerException.Message);

                }
            }
            

            /*
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://oauth2webapi-e2fnfjetahdyc2b0.canadacentral-01.azurewebsites.net/");

                // Attach the OAuth token as a Bearer token
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");


                try
                {

                    // Call your secured endpoint
                    HttpResponseMessage response = await client.GetAsync("api/Secured/GetTokenClaims");

                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        // Handle your response data here
                    }

                }
                catch (AuthenticationFailedException ex)
                {
                    Console.WriteLine("Error: " + ex.InnerException.Message);

                }
            }
            */


        }
    }
}
