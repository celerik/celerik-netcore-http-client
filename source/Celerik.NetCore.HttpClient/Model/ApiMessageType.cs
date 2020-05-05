namespace Celerik.NetCore.HttpClient
{
    /// <summary>
    /// Defines the possible types of messages that the API can
    /// send in the response.
    /// </summary>
    public enum ApiMessageType
    {
        /// <summary>
        /// The process ran successfully and some contextual
        /// information is sent.
        /// </summary>
        Info = 1,

        /// <summary>
        /// The process ran successfully.
        /// </summary>
        Success = 2,

        /// <summary>
        /// The process ran successfully but there is a warning
        /// message.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// There was an error executing the process.
        /// </summary>
        Error = 4
    }
}
