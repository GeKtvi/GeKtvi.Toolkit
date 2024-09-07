using GeKtvi.Toolkit.Wcf.Client;
using GeKtvi.Toolkit.Wcf.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;


namespace GeKtvi.Toolkit.Wcf.Tests
{
    [TestClass]
    public class WcfClientTests
    {
        private readonly WcfServiceTests _serviceTests;
        private readonly ClientConnection<ApplicationServiceClient> _client;
        private bool _isServiceClosed;

        public WcfClientTests()
        {
            _serviceTests = new WcfServiceTests();
            _serviceTests.StartService_EndpointUri_CorrectStartedService();

            var callback = new Tests.ServiceClosingCallback();
            callback.Closing.Subscribe(_ => _isServiceClosed = true);

            _client = new GeKtvi.Toolkit.Wcf.Client.ClientConnection<ApplicationServiceClient>(
                () => callback,
                (ic, binding, address) => new ApplicationServiceClient(ic, binding, address),
                x => x.SubscribeServiceClosing(),
                $"net.pipe://localhost/WcfUnitTest/{Process.GetCurrentProcess().Id}/TestEndpoint"
            );
        }

        [TestMethod]
        public void Connect_ConfiguredClient_CorrectConnectedService() => _client.Connect();

        [TestMethod]
        [Priority(-1)]
        public void OnServiceClose_ServiceAndClient_ClientNotifiedAboutClose()
        {
            Connect_ConfiguredClient_CorrectConnectedService();
            _serviceTests.ShutdownService();
            Thread.Sleep(500);
            Assert.IsTrue(_isServiceClosed, "Service is not closed or not messaged this");
        }

        [TestCleanup]
        public void ShutdownServiceAndClient() => _serviceTests.ShutdownService();
    }

    internal class ServiceClosingCallback : IApplicationServiceCallback, IServiceClosingCallback
    {
        public IObservable<Unit> Closing => _closing.AsObservable();
        private readonly Subject<Unit> _closing = new();

        public void ServiceClosing() => _closing.OnNext(Unit.Default);
    }
}
