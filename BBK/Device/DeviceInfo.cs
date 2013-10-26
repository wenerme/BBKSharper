using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace BBK.Device
{
    /// <summary>
    /// 设备信息类
    /// </summary>
    public partial class DeviceInfo
    {
        /// <summary>
        /// 设备名
        /// </summary>
        [XmlAttribute]
        public string Name;
        /// <summary>
        /// 设备屏幕宽
        /// </summary>
        [XmlAttribute]
        public int ScreenWidth;
        /// <summary>
        /// 设备屏幕高
        /// </summary>
        [XmlAttribute]
        public int ScreenHeight;
        
        public DeviceInfo()
        {}
        public DeviceInfo(string name, int w, int h)
        {
            Name = name;
            ScreenWidth = w;
            ScreenHeight = h;
        }
    }

    /// <summary>
    /// 静态部分
    /// </summary>
    public partial class DeviceInfo
    {
        /// <summary>
        /// 学习机 9688的配置信息
        /// </summary>
        public static DeviceInfo LM_9688 { get; private set; }
        /// <summary>
        /// 用于快速索引的设备信息列表
        /// </summary>
        private static Dictionary<string, DeviceInfo> DeviceList;

        static DeviceInfo()
        {
            // 初始化静态的配置信息
            LM_9688 = new DeviceInfo("9688", 240, 320);

            // 将配置信息添加到列表
            DeviceList = new Dictionary<string, DeviceInfo>();
            DeviceList.Add(LM_9688.Name, LM_9688);
        }
        /// <summary>
        /// 判断是否有改名字的设备信息
        /// </summary>
        /// <param name="name">设备名</param>
        /// <returns></returns>
        public static bool HaveDeviceInfo(string name)
        {
            return DeviceList.ContainsKey(name);
        }
        /// <summary>
        /// 根据名字获取设备信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DeviceInfo GetDeviceInfoByName(string name)
        {
            if (!HaveDeviceInfo(name))
                throw new ArgumentOutOfRangeException("未找到名为 "+name+" 的设备信息");

            return DeviceList[name];
        }
    }
}
