using System;
using UnityEngine.Networking;

namespace Unity.Ai.Assistant.Protocol.Client
{
    internal class ConnectionException : Exception
    {
        public UnityWebRequest.Result Result { get; private set; }
        public string Error { get; private set; }

        // NOTE: Cannot keep reference to the request since it will be disposed.
        public ConnectionException(UnityWebRequest request)
            : base($"result={request.result} error={request.error}")
        {
            Result = request.result;
            Error = request.error ?? "";
        }
    }
}
