using System;
using Newtonsoft.Json;

namespace Celerik.NetCore.HttpClient
{
    /// <summary>
    /// Defines the standard response for all services.
    /// </summary>
    /// <typeparam name="TData">Type of the Data property.
    /// </typeparam>
    /// <typeparam name="TStatusCode">Type of the StatusCode poperty.
    /// </typeparam>
    public class ApiResponse<TData, TStatusCode>
        where TData : class
        where TStatusCode : struct, IConvertible
    {
        /// <summary>
        /// Data sent in the response.
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// Indicates whether the service ran successfully.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// An optional localized message describing the execution
        /// of the service.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Describes the type of message, null if there is no message.
        /// </summary>
        public ApiMessageType? MessageType { get; set; }

        /// <summary>
        /// The status code related to service execution (enumeration).
        /// </summary>
        public TStatusCode StatusCode { get; set; }
    }
}
