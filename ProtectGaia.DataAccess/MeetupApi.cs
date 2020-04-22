using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using ProtectGaia.Entities.MeetupEntity;
using ProtectGaia.Interface;

namespace ProtectGaia.DataAccess
{
    public class MeetupApi : IMeetupApi
    {
        string BaseUrl = "https://api.meetup.com/";
        string APIKey = "v3q5mlblp7vhbs2eqhma4gg7vs";
        string endpointAPIKey = "2/events?key=";
        string Sign = "&sign=true";
        string Path;
        string GroupPath = "&group_urlname=";
        string GroupURL = "environment";


        public EventRequest UpcomingEvents()
        {

            string url = BaseUrl + endpointAPIKey + APIKey + GroupPath + GroupURL + Sign;

            //this is the automatic generated json class
            //of course you have to rename the classes into decent names
            ResponseEntity Meetup;

            WebClient wc = new WebClient();

            try
            {
                var rawdata = wc.DownloadString(url);


                //simply write the result to the console
                JsonTextReader reader = new JsonTextReader(new StringReader(rawdata));
                while (reader.Read())
                {
                    if (reader.Value != null)
                    {
                        Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                    }
                    else
                    {
                        Console.WriteLine("Token: {0}", reader.TokenType);
                    }
                }

                //the first time
                //after reading the meetup in rawdata in the debugging mode
                //show rawdata in html format and copy result to http://json2csharp.com/
                //now declare the 'generated' classes above

                //now you can use the classes to store the result of the jsonconvert()

                Meetup = JsonConvert.DeserializeObject<ResponseEntity>(rawdata);
                //the result is stored in object Meetup and is ready for use

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            Console.ReadLine();
            return null;
        }
    }
}
