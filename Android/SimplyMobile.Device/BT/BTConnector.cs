using System;
using Java.Lang;
using Java.Lang.Reflect;
using Android.Bluetooth;
using Android.Util;

namespace SimplyMobile.Device
{
    public class BTConnector
    {
        public BTConnector ()
        {

        }

        public static Method GetConnectMethod () 
        {
            try 
            {
                return Class.FromType(typeof(BluetoothA2dp)).GetDeclaredMethod(
                    "connect", 
                    Class.FromType(typeof(BluetoothDevice)));
            } 
            catch (NoSuchMethodException ex) 
            {
                Log.Error("BTConnector", "Unable to find connect(BluetoothDevice) method in BluetoothA2dp proxy.");
                return null;
            }
        }
    }
}

