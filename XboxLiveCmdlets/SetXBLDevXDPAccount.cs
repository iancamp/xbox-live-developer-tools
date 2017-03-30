﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

namespace XboxLiveCmdlet
{
    using System;
    using System.Management.Automation;
    using System.Security;

    [Cmdlet(VerbsCommon.Set, "XblDevXDPAccount")]
    public class SetXblDevXDPCredential : XboxliveCmdlet
    {
        [Parameter (Mandatory = true, Position = 0)]
        public string UserName { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public SecureString Password { set; get; }

        protected override void ProcessRecord()
        {
            try
            {
                var task = Microsoft.Xbox.Services.Tool.Auth.GetXDPEToken(this.UserName, this.Password);
                task.Wait();
            }
            catch(AggregateException e)
            {
                var innerEx = e.InnerException;
                WriteError(new ErrorRecord(innerEx, "Set-XBLDevXDPAccount failed", ErrorCategory.SecurityError, null));
            }
        }
        
    }
}
