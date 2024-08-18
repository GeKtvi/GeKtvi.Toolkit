using System.ServiceModel;

namespace GeKtvi.Toolkit.Wcf.Service
{
    public interface IServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void ServiceClosing();
    }
}
