using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Messages;
using Microsoft.Identity.Client;
using UIKit;
using Xamarin.Forms;

namespace iotc_xamarin_ble.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            MessagingCenter.Subscribe<RequestMessage>(this, Constants.SERVICE_START, async message => {
               // start service
            });

            MessagingCenter.Subscribe<RequestMessage>(this, Constants.SERVICE_STOP, async message => {
                // stop service
            });
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
            return true;
        }
    }
}
