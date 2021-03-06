//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.33440.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name = "BulkSmsSoap", Namespace = "http://tempuri.org/")]
public partial class BulkSms : System.Web.Services.Protocols.SoapHttpClientProtocol
{

    private System.Threading.SendOrPostCallback SendSmsOperationCompleted;

    private System.Threading.SendOrPostCallback SendIVROperationCompleted;

    /// <remarks/>
    public BulkSms(string _url)
    {
        this.Url = _url;
    }

    /// <remarks/>
    public event SendSmsCompletedEventHandler SendSmsCompleted;

    /// <remarks/>
    public event SendIVRCompletedEventHandler SendIVRCompleted;

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendSms", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string SendSms(string requestId, string userId, string receiverId, string msg, string contentType)
    {
        object[] results = this.Invoke("SendSms", new object[] {
                    requestId,
                    userId,
                    receiverId,
                    msg,
                    contentType});
        return ((string)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BeginSendSms(string requestId, string userId, string receiverId, string msg, string contentType, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("SendSms", new object[] {
                    requestId,
                    userId,
                    receiverId,
                    msg,
                    contentType}, callback, asyncState);
    }

    /// <remarks/>
    public string EndSendSms(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }

    /// <remarks/>
    public void SendSmsAsync(string requestId, string userId, string receiverId, string msg, string contentType)
    {
        this.SendSmsAsync(requestId, userId, receiverId, msg, contentType, null);
    }

    /// <remarks/>
    public void SendSmsAsync(string requestId, string userId, string receiverId, string msg, string contentType, object userState)
    {
        if ((this.SendSmsOperationCompleted == null))
        {
            this.SendSmsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendSmsOperationCompleted);
        }
        this.InvokeAsync("SendSms", new object[] {
                    requestId,
                    userId,
                    receiverId,
                    msg,
                    contentType}, this.SendSmsOperationCompleted, userState);
    }

    private void OnSendSmsOperationCompleted(object arg)
    {
        if ((this.SendSmsCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.SendSmsCompleted(this, new SendSmsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendIVR", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public resIVR SendIVR(string receiverId, string templateId, string variableList, string sendDate)
    {
        object[] results = this.Invoke("SendIVR", new object[] {
                    receiverId,
                    templateId,
                    variableList,
                    sendDate});
        return ((resIVR)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BeginSendIVR(string receiverId, string templateId, string variableList, string sendDate, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("SendIVR", new object[] {
                    receiverId,
                    templateId,
                    variableList,
                    sendDate}, callback, asyncState);
    }

    /// <remarks/>
    public resIVR EndSendIVR(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((resIVR)(results[0]));
    }

    /// <remarks/>
    public void SendIVRAsync(string receiverId, string templateId, string variableList, string sendDate)
    {
        this.SendIVRAsync(receiverId, templateId, variableList, sendDate, null);
    }

    /// <remarks/>
    public void SendIVRAsync(string receiverId, string templateId, string variableList, string sendDate, object userState)
    {
        if ((this.SendIVROperationCompleted == null))
        {
            this.SendIVROperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendIVROperationCompleted);
        }
        this.InvokeAsync("SendIVR", new object[] {
                    receiverId,
                    templateId,
                    variableList,
                    sendDate}, this.SendIVROperationCompleted, userState);
    }

    private void OnSendIVROperationCompleted(object arg)
    {
        if ((this.SendIVRCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.SendIVRCompleted(this, new SendIVRCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    public new void CancelAsync(object userState)
    {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
public partial class resIVR
{

    private bool statusField;

    private string errorField;

    /// <remarks/>
    public bool status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }

    /// <remarks/>
    public string error
    {
        get
        {
            return this.errorField;
        }
        set
        {
            this.errorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void SendSmsCompletedEventHandler(object sender, SendSmsCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class SendSmsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{

    private object[] results;

    internal SendSmsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
    {
        this.results = results;
    }

    /// <remarks/>
    public string Result
    {
        get
        {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void SendIVRCompletedEventHandler(object sender, SendIVRCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class SendIVRCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{

    private object[] results;

    internal SendIVRCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
    {
        this.results = results;
    }

    /// <remarks/>
    public resIVR Result
    {
        get
        {
            this.RaiseExceptionIfNecessary();
            return ((resIVR)(this.results[0]));
        }
    }
}
