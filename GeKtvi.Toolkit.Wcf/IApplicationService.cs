using System;
using System.ServiceModel;

namespace GeKtvi.Toolkit.Wcf.Service
{
    [ServiceContract(CallbackContract = typeof(IServiceEvents))]
    public interface IApplicationService : IDisposable
    {
        [OperationContract]
        IntPtr GetWindowPointer();

        void CloseService();

        [OperationContract]
        void SubscribeServiceClosing();
    }
}
