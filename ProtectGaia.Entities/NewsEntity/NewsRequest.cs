using System;
using System.Collections.Generic;

namespace ProtectGaia.Entities.NewsEntity
{
    /// <summary>
    /// Request for News API
    /// </summary>
    public class NewsRequest
    {

        //Environment climate change carbon emissions
        public string queryString { get; set; }

        public string language { get; set; }

        public string sortby { get; set; }

        public List<string> domainList { get; set; }

        public string APiKey { get; set; }

        public NewsRequest()
        {
            domainList = new List<string>();
            APiKey = "52b88b19d42f4856ab5609ef65740197";
        }
    }
}
