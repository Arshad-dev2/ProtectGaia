using System;
using ProtectGaia.Entities.NewsEntity;

namespace ProtectGaia.Interface
{
    public interface INewsApi
    {
        public NewsResponse UpcomingNews(NewsRequest newsRequest);
        
    }
}
