using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace REGAL.WMS.WebApi.Core
{
    public sealed class CacheManager
    {
        ObjectCache objCache = null;

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return objCache.Count(); }
        }

        public CacheManager()
        {
            objCache = MemoryCache.Default;
        }

        /// <summary>
        /// Add an item into cache. Never expire.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            Add(key, value, ObjectCache.InfiniteAbsoluteExpiration);
        }

        /// <summary>
        /// Add an item into cache with specific expiration time.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        public void Add(string key, object value, DateTimeOffset expireTime)
        {
            objCache.Add(key, value, expireTime);
        }

        /// <summary>
        /// Check cache if it contains specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            return objCache.Contains(key);
        }

        /// <summary>
        /// Retrive object from cache by specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return objCache[key];
        }

        /// <summary>
        /// Add or replace an item on cache. Never expire.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, object value)
        {
            Set(key, value, ObjectCache.InfiniteAbsoluteExpiration);
        }

        /// <summary>
        /// Add or replace an item on cache with specific expiration time.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        public void Set(string key, object value, DateTimeOffset expireTime)
        {
            objCache.Set(key, value, expireTime);
        }

        /// <summary>
        /// Remove object from cache by specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Remove(string key)
        {
            return objCache.Remove(key);
        }
    }
}
