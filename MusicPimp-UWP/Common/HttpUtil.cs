﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Network
{
    public class HttpUtil
    {
        // HTTP constants
        public static readonly string
            Authorization = "Authorization",
            Accept = "Accept",
            Basic = "Basic",
            ContentDisposition = "Content-Disposition",
            ContentType = "Content-Type",
            Http = "http",
            Https = "https",
            Json = "application/json",
            OctetStream = "application/octet-stream";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>a value for the Authorization HTTP header: "Basic encoded_credentials_here"</returns>
        public static string BasicAuthHeader(string username, string password)
        {
            return Basic + " " + BasicAuthEncoded(username, password);
        }
        public static string BasicAuthEncoded(string username, string password)
        {
            return ToUtf8Base64(String.Format("{0}:{1}", username, password));
        }
        public static string Base64ColonSeparated(string first, string second, string third)
        {
            return ToUtf8Base64(String.Format("{0}:{1}:{2}", first, second, third));
        }
        private static string ToUtf8Base64(string content)
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>true if the URI has the http or https scheme, false otherwise</returns>
        public static bool IsHttpOrHttps(Uri uri)
        {
            // .Scheme throws invalidopex for relative URIs
            return uri.IsAbsoluteUri && (uri.Scheme == Http || uri.Scheme == Https);
        }
        public static AuthenticationHeaderValue BasicAuthHeaderValue(string username, string password)
        {
            var encoded = BasicAuthEncoded(username, password);
            return new AuthenticationHeaderValue(Basic, encoded);
        }
        public static HttpClient NewJsonHttpClient(bool tryUseCompression = true)
        {
            var handler = new HttpClientHandler();
            if (tryUseCompression && handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            else
            {
                handler.AutomaticDecompression = DecompressionMethods.None;
            }
            var client = new HttpClient(handler);
            var headers = client.DefaultRequestHeaders;
            headers.Accept.ParseAdd(Json);
            return client;
        }
        public static HttpClient NewHttpClient(string username, string password)
        {
            var client = NewJsonHttpClient();
            var headers = client.DefaultRequestHeaders;
            headers.Authorization = HttpUtil.BasicAuthHeaderValue(username, password);
            return client;
        }
    }
}
