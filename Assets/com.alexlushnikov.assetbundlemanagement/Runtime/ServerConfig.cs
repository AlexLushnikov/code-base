﻿using UnityEngine;

namespace AssetBundleManagement
{
    public class ServerConfig
    {
        public string CurrentDomain = "http://localhost/files/";

        public static ServerConfig Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ServerConfig();
                }

                return _Instance;
            }
            set { _Instance = value; }
        }

        private static ServerConfig _Instance;

        public static string GetWithDomain(string url)
        {
            return url.Contains("http") || url.Contains("https") ? url : URLCombine(Instance.CurrentDomain, url);
        }

        public static string URLCombine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return $"{uri1}/{uri2}";
        }

        public static string GetServerPath(string assetName)
        {
            var platformFolder = GetPlatformFolder();
            var url = GetWithDomain($"/AssetBundles/{platformFolder}/{assetName}");
            return url;
        }

        public static string GetPlatformFolder()
        {
            if (Application.isEditor)
            {
                return "standalone";
            }

            if (Application.platform == RuntimePlatform.Android)
            {
                return "android";
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return "ios";
            }

            return "standalone";
        }
    }
}