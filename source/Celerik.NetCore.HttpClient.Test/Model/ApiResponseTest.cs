using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.HttpClient.Test
{
    public enum StatusCode
    {
        Ok = 200,
        Error = 500
    };

    [TestClass]
    public class ApiResponseTest
    {
        [TestMethod]
        public void Defaults()
        {
            var response = new ApiResponse<object, StatusCode>();

            Assert.AreEqual(null, response.Data);
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(null, response.MessageType);
            Assert.AreEqual(false, response.Success);
        }
    }
}
