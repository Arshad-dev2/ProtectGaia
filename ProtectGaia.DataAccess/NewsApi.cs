using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ProtectGaia.Entities.NewsEntity;
using ProtectGaia.Interface;

namespace ProtectGaia.DataAccess
{
    public class NewsApi : INewsApi
    {
        public readonly string BaseURL = @"https://newsapi.org/v2/everything?";
        public NewsResponse UpcomingNews(NewsRequest newsRequest)
        {
            NewsResponse newsResponse = new NewsResponse();
            string requestParameter = string.Format("{0}q={1}&language={2}&sortby={3}&apikey={4}",
                                            BaseURL,
                                            newsRequest.queryString,
                                            newsRequest.language,
                                            newsRequest.sortby,
                                            
                                            newsRequest.APiKey);
            string jsonString = new WebClient().DownloadString(requestParameter);
            newsResponse = JsonConvert.DeserializeObject<NewsResponse>(jsonString);
            var filteredArticles = newsResponse.Articles.Select(x => new Article()
            {
                Author = x.Author,
                Title = x.Title,
                PublishedAt = x.PublishedAt,
                UrlToImage = x.UrlToImage,
                //Description = x.Description,
                Url = x.Url,
                Source = x.Source
            }).Where(
                y=>(y.UrlToImage!=null &&
                    (y.Title.ToLower().Contains("environment")||
                    y.Title.ToLower().Contains("climate") ||
                     y.Title.ToLower().Contains("greenhouse gas")
                     ))).ToList();

            var distinctArticle = filteredArticles.GroupBy(x => x.Source.Name).Select(g => g.First()).ToList().Take(4);

            NewsResponse updatedNewsResponse = new NewsResponse()
            {
                Status = newsResponse.Status,
                Articles = distinctArticle
            };

                //persons.Where(x => !exclusionKeys.Contains(x.compositeKey));
            return updatedNewsResponse;
        }

    }
}
