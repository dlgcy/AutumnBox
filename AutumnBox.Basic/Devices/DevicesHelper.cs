﻿//#define USE_EMU_DEVICE
/*
 关于设备的各种工具类
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.Executer;
using System;
using System.Collections;

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 关于设备的一些静态函数
    /// </summary>
    public static class DevicesHelper
    {
        private static CommandExecuter executer = new CommandExecuter();
        public static DeviceStatus StringStatusToEnumStatus(string statusString) {
            switch (statusString) {
                case "device":
                    return DeviceStatus.RUNNING;
                case "recovery":
                    return DeviceStatus.RECOVERY;
                case "fastboot":
                    return DeviceStatus.FASTBOOT;
                case "sideload":
                    return DeviceStatus.SIDELOAD;
                case "debugging_device":
                    return DeviceStatus.DEBUGGING_DEVICE;
                default:
                    return DeviceStatus.NO_DEVICE;
            }
        }
        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns>设备状态</returns>
        public static DeviceStatus GetDeviceStatus(string id)
        {
            throw new NotImplementedException();
            //return StringStatusToEnumStatus(GetDevices().ToString());
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns>设备列表</returns>
        public static DevicesList GetDevices()
        {
            DevicesList d = new DevicesList();
            executer.GetDevices(ref d);
            return d;
        }
        /// <summary>
        /// 获取一个设备的信息
        /// </summary>
        /// <param name="id">设备的id</param>
        /// <returns>设备信息</returns>
        public static DeviceInfo GetDeviceInfo(string id)
        {
            Hashtable ht = GetBuildInfo(id);
            return new DeviceInfo
            {
                brand = ht["brand"].ToString(),
                code = ht["name"].ToString(),
                androidVersion = ht["androidVersion"].ToString(),
                model = ht["model"].ToString(),
                deviceStatus = GetDeviceStatus(id),
                id = id
            };
        }
        /// <summary>
        /// 获取一个设备的信息
        /// </summary>
        /// <param name="id">设备的id</param>
        /// <returns>设备信息</returns>
        public static DeviceInfo GetDeviceInfo(string id,Hashtable buildInfo,DeviceStatus status)
        {
            return new DeviceInfo
            {
                brand = buildInfo["brand"].ToString(),
                code = buildInfo["name"].ToString(),
                androidVersion = buildInfo["androidVersion"].ToString(),
                model = buildInfo["model"].ToString(),
                deviceStatus = status,
                id = id
            };
        }
        /// <summary>
        /// 获取设备的build信息
        /// </summary>
        /// <param name="id">设备id</param>
        /// <returns>设备build信息</returns>
        public static Hashtable GetBuildInfo(string id)
        {
            Hashtable ht = new Hashtable();
            var executer = new CommandExecuter();
            try { ht.Add("name", executer.Execute(id, "shell \"cat /system/build.prop | grep \"product.name\"\"").LineOut[0].Split('=')[1]); }
            catch { ht.Add("name", ".."); }
    
            try { ht.Add("brand", executer.Execute(id, "shell \"cat /system/build.prop | grep \"product.brand\"\"").LineOut[0].Split('=')[1]); }
            catch { ht.Add("brand", ".."); }

            try { ht.Add("androidVersion", executer.Execute(id, "shell \"cat /system/build.prop | grep \"build.version.release\"\"").LineOut[0].Split('=')[1]); }
            catch { ht.Add("androidVersion", ".."); }
            try { ht.Add("model", executer.Execute(id, "shell \"cat /system/build.prop | grep \"product.model\"\"").LineOut[0].Split('=')[1]); }
            catch { ht.Add("model", ".."); }

            return ht;
        }
    }
}
