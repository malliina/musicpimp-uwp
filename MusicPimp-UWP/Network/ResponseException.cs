using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Network
{
    public class ResponseException : Exception
    {
        public HttpResponseMessage Response { get; private set; }
        public HttpStatusCode StatusCode { get { return Response.StatusCode; } }
        public Uri RequestUri { get { return Response.RequestMessage.RequestUri; } }
        public ResponseException(HttpResponseMessage response) : base($"Invalid response code: {response.StatusCode}")
        {
            Response = response;
        }
    }
}
