using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// 定义读取byte[]数据的类 --by xcg
    /// </summary>
      public class DataFunc
    {
          /// <summary>
        /// 读取byte[]数据的方法
          /// </summary>
          /// <param name="natFile">打开的文件流</param>
          /// <param name="length">读取的长度</param>
          /// <returns></returns>
        public byte[] DataReadFun(FileStream natFile, int length)
        {
            //声明长度为length的byte类型的数组
            byte[] dateDatabuf = new byte[length];

            //从文件流中读取length长度的数据，并存储到byte[]数组中
            natFile.Read(dateDatabuf, 0, length);

            //返回读取的结果
            return dateDatabuf;
        }

         /// <summary>
         /// 将小端存储转换成大端存储
         /// </summary>
         /// <param name="bytesToInvert"></param>
        public void HighToLow(ref byte[] bytesToInvert)
        {
            //声明byte类型变量
            byte temp;

            //以数组的中间字节为轴对称调换
            for (int i = 0; i < bytesToInvert.Length / 2; i++)
            {
                //记录数组中第i个字节的数据
                temp = bytesToInvert[i];

                //将数组的第（bytesToInvert.Length - i - 1）个的数据赋值给第i个
                bytesToInvert[i] = bytesToInvert[bytesToInvert.Length - i - 1];

                //将数组的第 i 个的数据赋值给第（bytesToInvert.Length - i - 1）个
                bytesToInvert[bytesToInvert.Length - i - 1] = temp;
            }
        }

          /// <summary>
          /// 将Byte类型的数组转成十六进制的字符串
          /// </summary>
          /// <param name="bytes"></param>
          /// <returns>一个十六进制的字符串变量</returns>
        public string ByteArrToHexString(byte[] bytes)
        {
            //声明一个字符串变量，初始值为空
            string hexString = string.Empty;

            //如果传入的byte类型的数组不为空
            if (bytes != null)
            {
                //声明一个StringBuilder类型的变量
                StringBuilder stringBuilder = new StringBuilder();

                //遍历传入的byte数组
                for (int i = 0; i < bytes.Length; i++)
                {
                    //将byte类型的数据转换成十六进制的字符，附加到stringBuilder的尾部
                    stringBuilder.Append(bytes[i].ToString("X2"));
                }

                //stringBuilder转换成字符串
                hexString = stringBuilder.ToString();
            }

            //返回十六进制的字符串
            return hexString;
        }

    }
}
