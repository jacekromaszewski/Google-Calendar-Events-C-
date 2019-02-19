using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Services;


namespace Discovery.ListAPIs
{

    class Program
    {

        static void Main(string[] args)
        {
            StreamReader r = new StreamReader("credentials.json");
            string json = r.ReadToEnd();
            string[] value = json.Split('"');

            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = value[5],
                    ClientSecret = value[25],
                },
                new[] { CalendarService.Scope.Calendar },
                "user",
                CancellationToken.None).Result;


            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });

            Event myEvent = new Event
            {
                Summary = "Short Meeting",
                Start = new EventDateTime()
                {
                    DateTime = new DateTime(2019,1, 16, 21, 15, 0),
                },
                End = new EventDateTime()
                {
                    DateTime = new DateTime(2019, 1, 16, 21, 45, 0),
                }
            };

            Event recurringEvent = service.Events.Insert(myEvent, "primary").Execute();

        }
    }
}
