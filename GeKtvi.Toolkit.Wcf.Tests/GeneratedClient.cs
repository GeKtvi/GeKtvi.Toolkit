﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeKtvi.Toolkit.Wcf.Tests
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GeKtvi.Toolkit.Wcf.Tests.IApplicationService", CallbackContract=typeof(GeKtvi.Toolkit.Wcf.Tests.IApplicationServiceCallback))]
    public interface IApplicationService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetWindowPointer", ReplyAction="http://tempuri.org/IApplicationService/GetWindowPointerResponse")]
        System.IntPtr GetWindowPointer();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/GetWindowPointer", ReplyAction="http://tempuri.org/IApplicationService/GetWindowPointerResponse")]
        System.Threading.Tasks.Task<System.IntPtr> GetWindowPointerAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/SubscribeServiceClosing", ReplyAction="http://tempuri.org/IApplicationService/SubscribeServiceClosingResponse")]
        void SubscribeServiceClosing();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApplicationService/SubscribeServiceClosing", ReplyAction="http://tempuri.org/IApplicationService/SubscribeServiceClosingResponse")]
        System.Threading.Tasks.Task SubscribeServiceClosingAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IApplicationServiceCallback
    {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IApplicationService/ServiceClosing")]
        void ServiceClosing();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IApplicationServiceChannel : GeKtvi.Toolkit.Wcf.Tests.IApplicationService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ApplicationServiceClient : System.ServiceModel.DuplexClientBase<GeKtvi.Toolkit.Wcf.Tests.IApplicationService>, GeKtvi.Toolkit.Wcf.Tests.IApplicationService
    {
        
        public ApplicationServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance)
        {
        }
        
        public ApplicationServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName)
        {
        }
        
        public ApplicationServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }
        
        public ApplicationServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }
        
        public ApplicationServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress)
        {
        }
        
        public System.IntPtr GetWindowPointer()
        {
            return base.Channel.GetWindowPointer();
        }
        
        public System.Threading.Tasks.Task<System.IntPtr> GetWindowPointerAsync()
        {
            return base.Channel.GetWindowPointerAsync();
        }
        
        public void SubscribeServiceClosing()
        {
            base.Channel.SubscribeServiceClosing();
        }
        
        public System.Threading.Tasks.Task SubscribeServiceClosingAsync()
        {
            return base.Channel.SubscribeServiceClosingAsync();
        }
    }
}
