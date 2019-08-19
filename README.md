# Xamarin IoTCentral Mobile Gateway

IoTCentral Mobile Gateway is a mobile application written in Xamarin.Forms that allows people connecting devices to Azure IoTCentral. It currently supports provisioning of WiFi (e.g. MXCHIP) and Bluetooth Low Energy devices.
WiFi capable devices can use a “quick pairing” mode to associate with an application without the need of complex procedures that involve web browsers and manual operations.
BLE devices with no internet connection capability can be provisioned and connected to IoTCentral and also managed from the mobile application itself.
User can map telemetry fields to bluetooth characteristics exposed by the physical device.


iOS [![Build status](https://build.appcenter.ms/v0.1/apps/30999060-d23f-4cfb-9c7c-a2d80d71cf10/branches/master/badge)](https://appcenter.ms)
Full BLE experience. WiFi provisioning currently in progress.



Android [![Build status](https://build.appcenter.ms/v0.1/apps/9b2e150e-63cb-4c0d-b163-94ef0e56aa2a/branches/master/badge)](https://appcenter.ms)
Full experience for both BLE and WiFi. WiFI provisioning completely automated and covered e2e.

<img src="https://github.com/lucadruda/iotc-xamarin-ble/raw/master/assets/img1.jpg" height="350"/>
<img src="https://github.com/lucadruda/iotc-xamarin-ble/raw/master/assets/img2.jpg" height="350"/>
<img src="https://github.com/lucadruda/iotc-xamarin-ble/raw/master/assets/img3.jpg" height="350"/>
<img src="https://github.com/lucadruda/iotc-xamarin-ble/raw/master/assets/img4.jpg" height="350"/>
<img src="https://github.com/lucadruda/iotc-xamarin-ble/raw/master/assets/img5.jpg" height="350"/>

## Features
* WiFi provisioning:
Once user selects WiFi mode under preferences, one device in pairing mode can be quickly provisioned to an application following the wizard.
On iOS pairing process is not fully automated so user must follow instructions on the screen.

Pairing mode is currently only available on MXCHIP. You must flash it with the latest 3.0.0 beta firmware that can be downloaded from this page (https://github.com/Azure/iot-central-firmware/releases).
When power-up, press A button for 5 seconds or more until it enter in quick pairing mode. Then go on with pairing procedure on mobile app.
 
 * Bluetooth pairing: 
In Bluetooth mode, once a device is selected within the central application (or created with the “plus” button), the gateway will start scanning for nearby BLE accessories.
After selection a configuration view is presented. Here the user can map BLE features to telemetry fields. Only Contoso and DevKit applications are currently supported.
 
