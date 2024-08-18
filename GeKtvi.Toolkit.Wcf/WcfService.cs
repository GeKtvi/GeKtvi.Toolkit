using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;

namespace GeKtvi.Toolkit.Wcf.Service
{
    public class WcfService<T>(T serviceInstance,
                               string serviceName = "NxOpenService",
                               string baseAddress = "net.pipe://localhost/") : IDisposable where T : IDisposable
    {
        public string BaseAddress { get; } = baseAddress;
        public string ServiceName { get; set; } = serviceName;
        public IObservable<Unit> Errors => _errors.AsObservable();
        public bool UseUniqueProcessAddress { get; set; } = false;
        public async Task StartAsync() => await Task.Run(Start);

        private ServiceHost? _serviceHost;
        private readonly Subject<Unit> _errors = new();

        public void Start()
        {
            string baseAddress = BaseAddress;
            if (UseUniqueProcessAddress)
                baseAddress += $"{Process.GetCurrentProcess().Id}/";
            Uri baseAddressUri = new(baseAddress);

            _serviceHost = new ServiceHost(serviceInstance, baseAddressUri);

            _serviceHost.Faulted += (s, e) => _errors.OnNext(Unit.Default);
            try
            {
                _serviceHost.AddServiceEndpoint(
                    typeof(T),
                    new NetNamedPipeBinding()
                    {
                        MaxReceivedMessageSize = int.MaxValue,
                        ReceiveTimeout = TimeSpan.MaxValue
                    },
                    ServiceName);

                ServiceMetadataBehavior smb = new();
                if (_serviceHost.Description.Behaviors.Contains(typeof(ServiceMetadataBehavior)) == false)
                    _serviceHost.Description.Behaviors.Add(smb);

                _serviceHost.AddServiceEndpoint(
                    typeof(IMetadataExchange),
                    MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                    "mex");

                _serviceHost.Open();
                _serviceHost.UnknownMessageReceived += (s, e) => _errors.OnNext(Unit.Default);
            }
            catch (CommunicationException)
            {
                _serviceHost.Abort();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            serviceInstance.Dispose();

            if (_serviceHost is not null && _serviceHost.State != CommunicationState.Faulted)
                _serviceHost.Close();
        }

        ~WcfService() => Dispose();
    }
}
