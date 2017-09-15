﻿namespace AutumnBox.Basic.Devices
{
    public struct DeviceInfo
    {
        public string model { internal set; get; }//型号
        public string brand { internal set; get; }//厂商
        public string code { internal set; get; }//代号
        public string id { internal set; get; }//id
        public string m { get { return brand + " " + model; } }
        public string androidVersion { internal set; get; }//安卓版本
        public DeviceStatus deviceStatus { internal set; get; }//设备状态
    }
    public struct DeviceSimpleInfo {
        public string Id { get; internal set; }
        public DeviceStatus Status { get; internal set; }
        public override string ToString()
        {
            return Id;
        }
        public static bool operator ==(DeviceSimpleInfo left, DeviceSimpleInfo right) {
            return (left.Id == right.Id);
        }
        public static bool operator !=(DeviceSimpleInfo left, DeviceSimpleInfo right)
        {
            return !(left.Id == right.Id);
        }
    }
}
