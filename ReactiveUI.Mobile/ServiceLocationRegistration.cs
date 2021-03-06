﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Mobile
{
    public class ServiceLocationRegistration : IWantsToRegisterStuff
    {
        public void Register()
        {
#if WP8
            RxApp.Register(typeof(WP8SuspensionHost), typeof (ISuspensionHost));
#elif WINRT
            RxApp.Register(typeof(WinRTSuspensionHost), typeof (ISuspensionHost));
#endif
        }
    }
}
