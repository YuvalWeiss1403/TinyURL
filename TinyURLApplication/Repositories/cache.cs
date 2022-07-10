using System.Collections.Generic;
using TinyURLApplication.Entities;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Runtime.Caching;
using System.Linq;
using System.Collections;
using System;

namespace TinyURLApplication.Repositories
{
    public class cache
    {

        private const string CacheKey = "URLs";
        public List<URLObject> cachelist;
        public int CacheRows;
        public IEnumerable MyCache;

        public cache()
        {
            this.cachelist = new List<URLObject>();
            this.CacheRows = 0;
        }


        public void AddToCache(URLObject newurl)
        {
            this.cachelist.Add(newurl);
            CacheRows++;
            AddToJsonFile(newurl);
            MyCache = GetSetURL();
        }

        public void AddToJsonFile(URLObject NewUrl)
        {
            List<URLObject> _data = new List<URLObject>();
            _data.Add(NewUrl);
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(@"C:\Users\Yuval\source\repos\TinyURLApp\TinyURLApplication\cache.json", json);
        }

        //returns the cache list of URLs
        public IEnumerable GetUrls()
        {
            return this.cachelist;
        }

        //setting the cache table , and returning the updated cache tabel
        public IEnumerable GetSetURL()
        {
            ObjectCache cacheTable = MemoryCache.Default;
            if (cacheTable.Contains(CacheKey))
                return (IEnumerable)cacheTable.Get(CacheKey);
            else
            {
                IEnumerable URLs = this.GetUrls();
                // Store data in the cache    
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cacheTable.Add(CacheKey, URLs, cacheItemPolicy);

                return URLs;
            }
        }


    }
}




