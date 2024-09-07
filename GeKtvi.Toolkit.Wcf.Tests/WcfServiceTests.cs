using GeKtvi.Toolkit.Wcf.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeKtvi.Toolkit.Wcf.Tests
{
    [TestClass]
    public class WcfServiceTests
    {
        private readonly WcfService<GeKtvi.Toolkit.Wcf.Service.IApplicationService> _service;

        public WcfServiceTests()
        {
            _service = new WcfService<GeKtvi.Toolkit.Wcf.Service.IApplicationService>(
                new ServiceBase(() => IntPtr.Zero),
                "TestEndpoint",
                "net.pipe://localhost/WcfUnitTest/",
                true
            );
        }

        [TestMethod]
        public void StartService_EndpointUri_CorrectStartedService() => _service.Start();

        [TestCleanup]
        public void ShutdownService() => _service.Dispose();
    }
}