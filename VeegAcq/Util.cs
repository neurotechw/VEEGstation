using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// xu
    /// </summary>
    public class Util
    {
        public static TimeSpan DurationToTimeSpan(double Duration)
        {
            return new TimeSpan((long)(Duration * 10000000));
        }

        public static double RawToSignal(short Raw)
        {
            return Raw / -7.78125;  //数值有点不对
        }

        /// <summary>
        /// 从string类型转换到hex  --by zt
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        /// <summary>
        /// FileStream读取字节流  --by zt
        /// </summary>
        /// <param name="natFile"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] DataReadFun(FileStream natFile, int length)
        {
            byte[] dateDatabuf = new byte[length];
            natFile.Read(dateDatabuf, 0, length);
            return dateDatabuf;
        }

        /// <summary>
        /// StreamReader读取字节流  --by zt
        /// </summary>
        /// <param name="streamReader"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] DataReadStreamFun(StreamReader streamReader, int length)
        {
            byte[] dateDatabuf = new byte[length];
            streamReader.BaseStream.Read(dateDatabuf, 0, length);
            return dateDatabuf;
        }

        /// <summary>
        /// 获得相应长度的字节数组  --by zt
        /// </summary>
        /// <param name="header"></param>
        /// <param name="startPoint"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] getFixedLengthByteArray(byte[] nationInfo, int startPoint, int length)
        {
            byte[] ch = new byte[length];
            Array.Copy(nationInfo, startPoint, ch, 0, length);
            return ch;
        }
    }

    public class NationFileInfoComparer : IComparer<NationFileInfo>
    {
        public int Compare(NationFileInfo x, NationFileInfo y)
        {
            return -Math.Sign((x.StartTime - y.StartTime).TotalSeconds);
        }
    }

    public class NationFileComparer : IComparer<NationFile>
    {
        public int Compare(NationFile x, NationFile y)
        {
            return -Math.Sign((x.StartDateTime - y.StartDateTime).TotalSeconds);
        }
    }

    public class VideoFileInfoComparer : IComparer<VideoFileInfo>
    {
        public int Compare(VideoFileInfo x, VideoFileInfo y)
        {
            return -Math.Sign((x.StartTime - y.StartTime).TotalSeconds);
        }
    }
}
