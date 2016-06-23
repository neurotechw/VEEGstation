using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VeegStation
{
    /// <summary>
    /// Controller类  --by zt
    /// </summary>
    public partial class VeegControl
    {
        /// <summary>
        /// 回放Form
        /// </summary>
        public PlaybackForm myPlaybackForm;

        /// <summary>
        /// 初始化Controller
        /// </summary>
        /// <param name="playbackForm">回放Form</param>
        public VeegControl(PlaybackForm playbackForm) 
        {
            this.myPlaybackForm = playbackForm;
            ModuleInitial(); 
        }

        /// <summary>
        /// 初始化各自模块
        /// </summary>
        private void ModuleInitial()
        {
            //公共数据初始化
            CommonDataInit();

            //配置模块初始化
            ConfigInit();
        }

        /// <summary>
        /// 退出时，保存配置文件
        /// </summary>
        public void PlaybackQuit() 
        {
            SaveXmlConfig();
        }

    //    /// <summary>
    //    /// 将预定义事件列表存到文件中
    //    /// -- by lxl
    //    /// </summary>
    //    /// <param name="filename">文件名路径</param>
    //    private int SavePreDefineEventsToFile(string filename)
    //    {
    //        try
    //        {
    //            //打开文件流
    //            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    //            BinaryWriter bw = new BinaryWriter(fs);

    //            //读取每个事件，并把每个事件写入其在文件中的位置
    //            foreach (PreDefineEvent p in myPlaybackForm.GetPreDefineEventList())
    //            {
    //                //建立一个字节BUFFER，将要数据按格式转换成BYTE放入BUFFER中
    //                byte[] byteBuf = PreDefineEventsToHex(p);
    //                if (byteBuf == null)
    //                {
    //                    System.Windows.Forms.MessageBox.Show("解析失败:数据转换成BYTE出错");
    //                    return 0;
    //                }

    //                bw.Seek(p.PosInFile, SeekOrigin.Begin);
    //                bw.Write(byteBuf);
    //            }

    //            foreach (int p in positionInFileOfDeletedFileList)
    //            {
    //                bw.Seek(p, SeekOrigin.Begin);
    //                bw.Write(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
    //            }
    //            bw.Close();
    //            fs.Close();

    //            //返回1表示成功
    //            return 1;
    //        }
    //        catch (Exception e)
    //        {
    //            MessageBox.Show("预定义事件写入文件错误" + e.Message);
    //            return 0;
    //        }
    //    }

    //    /// <summary>
    //    /// 将预定义事件转换成BYTE数组
    //    /// -- by lxl
    //    /// </summary>
    //    /// <param name="pdEvent"></param>
    //    /// <returns></returns>
    //    private byte[] PreDefineEventsToHex(PreDefineEvent pdEvent)
    //    {
    //        //建立一个BYTE数组用于存储事件信息
    //        byte[] returnBytes;
    //        returnBytes = new byte[8];

    //        //按照格式构造BYTE数组
    //        returnBytes[0] = 0x45;
    //        returnBytes[1] = Convert.ToByte(pdEvent.EventNameIndex);
    //        returnBytes[2] = returnBytes[3] = 0x00;
    //        returnBytes[4] = BitConverter.GetBytes(pdEvent.EventPosition)[0];
    //        returnBytes[5] = BitConverter.GetBytes(pdEvent.EventPosition)[1];
    //        returnBytes[6] = returnBytes[7] = 0x00;

    //        return returnBytes;
    //    }

    //    /// <summary>
    //    /// 将自定义事件列表保存到文件中
    //    /// -- by lxl
    //    /// </summary>
    //    /// <param name="filename"></param>
    //    private int SaveCustomeEventsToFile(string filename)
    //    {
    //        try
    //        {
    //            if (File.Exists(filename))
    //            {
    //                File.Delete(filename);
    //            }

    //            //list没有内容则只删除文件
    //            if (customEventList.Count <= 0)
    //                return 0;

    //            //打开文件流
    //            FileStream fs = new FileStream(filename, FileMode.CreateNew, FileAccess.ReadWrite);
    //            BinaryWriter bw = new BinaryWriter(fs);

    //            //建立一个字节BUFFER，将要数据按格式转换成BYTE放入BUFFER中
    //            byte[] eventByteBuf = CustomEventsToHex(customEventList);

    //            //建立一个文件头的BYTEBUF，将自定义事件文件头写进去
    //            byte[] headByteBuf = generateCustomEventHeadByte();
    //            if (eventByteBuf == null)
    //            {
    //                MessageBox.Show("解析失败:数据转换成BYTE出错");
    //                return 0;
    //            }
    //            bw.Seek(0, SeekOrigin.Begin);
    //            bw.Write(headByteBuf);
    //            bw.Write(eventByteBuf);
    //            bw.Close();
    //            fs.Close();

    //            return 1;
    //        }
    //        catch (Exception e)
    //        {
    //            MessageBox.Show("自定义事件写入文件错误" + e.Message);
    //            return 0;
    //        }
    //    }

    //    /// <summary>
    //    /// 生成自定义事件.ent文件的头部二进制
    //    /// -- by lxl
    //    /// </summary>
    //    /// <returns></returns>
    //    private byte[] generateCustomEventHeadByte()
    //    {
    //        byte[] returnBytes = new byte[9];

    //        //将ENTCM按照ASCII转码存入前5个BYTE
    //        Encoding.ASCII.GetBytes("ENTCM", 0, "ENTCM".Count(), returnBytes, 0);

    //        //将事件个数转码存入第六个BYTE
    //        returnBytes[5] = BitConverter.GetBytes(customEventList.Count())[0];

    //        return returnBytes;
    //    }

    //    /// <summary>
    //    /// 将自定义事件按格式转成BYTE数组
    //    /// -- by lxl
    //    /// </summary>
    //    /// <param name="cEvent"></param>
    //    /// <returns></returns>
    //    private byte[] CustomEventsToHex(List<CustomEvent> cEvent)
    //    {
    //        //建立一个BYTE数组用于存储事件信息
    //        byte[] returnBytes;
    //        returnBytes = new byte[cEvent.Count() * 128];

    //        for (int i = 0; i < cEvent.Count(); i++)
    //        {
    //            Encoding.GetEncoding(936).GetBytes(cEvent[i].EventName, 0, cEvent[i].EventName.Count(), returnBytes, i * 128);
    //            returnBytes[i * 128 + 104] = BitConverter.GetBytes(cEvent[i].EventPosition)[0];
    //            returnBytes[i * 128 + 105] = BitConverter.GetBytes(cEvent[i].EventPosition)[1];
    //            returnBytes[i * 128 + 108] = BitConverter.GetBytes(cEvent[i].EventColorIndex)[0];           //int32转换成BYTE数组为4个BYTE，由于只有第一个故取数组下标[0]

    //            //将时间写入事件文件中
    //            DateTime time = nfi.StartDateTime.AddSeconds(cEvent[i].EventPosition / sampleRate);
    //            returnBytes[i * 128 + 120] = BitConverter.GetBytes((UInt16)time.Hour)[0];
    //            returnBytes[i * 128 + 121] = BitConverter.GetBytes((UInt16)time.Hour)[1];
    //            returnBytes[i * 128 + 122] = BitConverter.GetBytes((UInt16)time.Minute)[0];
    //            returnBytes[i * 128 + 123] = BitConverter.GetBytes((UInt16)time.Minute)[1];
    //            returnBytes[i * 128 + 124] = BitConverter.GetBytes((UInt16)time.Second)[0];
    //            returnBytes[i * 128 + 125] = BitConverter.GetBytes((UInt16)time.Second)[1];
    //        }

    //        return returnBytes;
    //    }

    }
}
