using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace VeegStation
{
    /// <summary>
    /// xcg
    /// </summary>
//    public class NatFileInfo : NatInfo
//    {

//        DataFunc datafunc = new DataFunc();

//        public NatFileInfo(string NatFileFullName)
//        {
//            //打开nat文件,nat文件里存有除数据以外的信息
//            FileInfo fi = new FileInfo(NatFileFullName);
//            string natFileName = NatFileFullName.Substring(0, NatFileFullName.Length - fi.Extension.Length) + ".NAT";
//            FileStream natFile = new FileStream(natFileName, FileMode.Open);

//            //获得文件标识，当该字段为“VE”，标识为NAT文件
//            byte[] sighdatabuf = datafunc.DataReadFun(natFile, 2);
//            string sighdata = new string(Encoding.ASCII.GetChars(sighdatabuf));
//            this.Sign = sighdata;

//            //获得诺诚软件的版本号
//            byte[] versiondatabuf = datafunc.DataReadFun(natFile, 2);
//            string strversiondata = (versiondatabuf[1] << 8 | versiondatabuf[0]).ToString();
//            this.Version = strversiondata;

//            //获得检测的日期
//            byte[] dateyearbuf = datafunc.DataReadFun(natFile, 4);
//            string date = ((dateyearbuf[3] << 8 | dateyearbuf[2]) + "-" + dateyearbuf[1] + "-" + dateyearbuf[0]).ToString();
//            this.Date = DateTime.Parse(date);

//            //获得每秒采集的点数
//            byte[] frequcebuf = datafunc.DataReadFun(natFile, 2);
//            this.Freq = (frequcebuf[1] << 8 | frequcebuf[0]);

//            //脑电导数
//            byte[] brainnumberbuf = datafunc.DataReadFun(natFile, 1);
//            this.BrainNumber = brainnumberbuf[0];

//            //心电导数
//            byte[] heartnumber = datafunc.DataReadFun(natFile, 1);
//            this.HeartNumber = heartnumber[0];

//            //不知含义
//            byte[] eegtypebuf = datafunc.DataReadFun(natFile, 2);
//            this.EegType = (eegtypebuf[1] << 8 | eegtypebuf[0]).ToString();

//            natFile.Seek(2, SeekOrigin.Current);

//            //获得nat文件中存储病人信息的位置  将32改为24  --by zt
//            byte[] patoffbuf = datafunc.DataReadFun(natFile, 4);
//            this.PatOff = (patoffbuf[3] << 24 | patoffbuf[2] << 16 | patoffbuf[1] << 8 | patoffbuf[0]);

//            //获得nat文件中存储事件的配置信息的位置  将32改为24  --by zt
//            byte[] entoffbuf = datafunc.DataReadFun(natFile, 4);
//            this.EntOff = (entoffbuf[3] << 24 | entoffbuf[2] << 16 | entoffbuf[1] << 8 | entoffbuf[0]);

//            //获得nat文件中导联配置中每个通道的导名  将32改为24  --by zt
//            byte[] monoffbuf = datafunc.DataReadFun(natFile, 4);
//            this.MonOff = (monoffbuf[3] << 24 | monoffbuf[2] << 16 | monoffbuf[1] << 8 | monoffbuf[0]);

//            //获得nat文件中采集时的时间信息的位置  将32改为24  --by zt
//            byte[] cfgoffbuf = datafunc.DataReadFun(natFile, 4);
//            this.CfgOff = (cfgoffbuf[3] << 24 | cfgoffbuf[2] << 16 | cfgoffbuf[1] << 8 | cfgoffbuf[0]);

//            //获得数据区??不知道解析出来的数据的作用  将32改为24  --by zt
//            byte[] dataoffbuf = datafunc.DataReadFun(natFile, 4);
//            this.DatOff = (dataoffbuf[3] << 24 | dataoffbuf[2] << 16 | dataoffbuf[1] << 8 | dataoffbuf[0]);

//            //呼吸选择??不知道含义
//            this.RespNumber = Convert.ToByte(datafunc.DataReadFun(natFile, 1)[0].ToString(), 16);

//            //闪光选择??不知道含义
//            this.FlashNumber = Convert.ToByte(datafunc.DataReadFun(natFile, 1)[0].ToString(), 16);

//            //??不知道含义
//            this.Operate = Convert.ToByte(datafunc.DataReadFun(natFile, 1)[0].ToString(), 16);

//            natFile.Seek(1, SeekOrigin.Current);

//            //??不知道含义，将32改为24  --by zt
//            byte[] flgbuf = datafunc.DataReadFun(natFile, 4);
//            string flgdata = (flgbuf[3] << 24 | flgbuf[2] << 16 | flgbuf[1] << 8 | flgbuf[0]).ToString();
//            this.Flag = flgdata;

//            //是否有视频的标志，但不知道0表示有视频还是没视频 将32改为24  --by zt
//            byte[] vidioflgbuf = datafunc.DataReadFun(natFile, 4);
//            int flg = (vidioflgbuf[3] << 24 | vidioflgbuf[2] << 16 | vidioflgbuf[1] << 8 | vidioflgbuf[0]);
//            if (flg == 0)
//                this.bIsHaveVideo = false;
//            else
//                this.bIsHaveVideo = true;

//            //一帧的数据大小,不知道帧的含义 将32改为24  --by zt
//            byte[] nrowsofdatabuf = datafunc.DataReadFun(natFile, 4);
//            this.nRowsOfData = (nrowsofdatabuf[3] << 24 | nrowsofdatabuf[2] << 16 | nrowsofdatabuf[1] << 8 | nrowsofdatabuf[0]);

//            // 配置信息??不知道含义
//            this.byteConfigType = Convert.ToByte(datafunc.DataReadFun(natFile, 1)[0].ToString(), 16);

//            natFile.Seek(3, SeekOrigin.Current);

//            //保留88个字节
//            this.Remain = datafunc.DataReadFun(natFile, 88);

//            //获得软件默认的导联配置
//            EEGFmt mono = new EEGFmt();
//            Montage mMontage = new Montage(64);// 为什么是64,谢晨光自定义的  --by zt

//            //将数据读取的指针定位到nat文件中存储系统默认导联配置的信息位置
//            natFile.Seek(this.MonOff, SeekOrigin.Begin);

//            //nat文件中存储系统默认导联配置的信息块的开始标志，为"[MON]"
//            byte[] mFlag = datafunc.DataReadFun(natFile, 8);
//            string monosection = new string(Encoding.ASCII.GetChars(mFlag)).Trim('\0');
//            if (monosection == "[MON]")
//            {
//                //获得导联配置的类型，基本上都为"XM"
//                mono.Name = new string(Encoding.ASCII.GetChars(datafunc.DataReadFun(natFile, 6))).Trim('\0');
//                int i = 0;
                
//                //读取连续的5个字节，获得第一个通道的名称
//                string getstr = (new string(Encoding.ASCII.GetChars(datafunc.DataReadFun(natFile, 5)))).Trim('\0');
//                while (!getstr.Equals("")) //nat文件中导联数据信息的结尾存储的是大量的0，因此可以作为获取通道名称的结束条件
//                {
//                    mMontage.FirstName[i] = getstr;
//                    mMontage.SecondName[i] = "REF";
//                    //读取连续的5个字节，获得下一个通道的名称
//                    getstr = (new string(Encoding.ASCII.GetChars(datafunc.DataReadFun(natFile, 5)))).Trim('\0');
//                    i++;
//                }
//            }

//            //获得采集数据的软件的事件配置信息
//            EventHead evnthead = new EventHead();
//            //nat文件中存储系统默认事件配置的信息块的开始标志，为"[ENT]"
//            natFile.Seek(this.EntOff, SeekOrigin.Begin);
//            //nat文件中事件区的标识，为"[ENT]"
//            evnthead.Sign = (new string(Encoding.ASCII.GetChars(datafunc.DataReadFun(natFile, 5))));
//            if (evnthead.Sign == "[ENT]")
//            {
//                natFile.Seek(3, SeekOrigin.Current);
//                //获得软件定义的事件的个数
//                byte[] evntnum = datafunc.DataReadFun(natFile, 1);
//                evnthead.Number = evntnum[0] >> 4;

//                //存储软件自定义的事件
//                Event[] evntArr = new Event[evnthead.Number];

//                natFile.Seek(3, SeekOrigin.Current);
//                //获得事件的名称
//                string evntname = (Encoding.GetEncoding(936).GetString(datafunc.DataReadFun(natFile, 10))).Trim('\0');

//                natFile.Seek(2, SeekOrigin.Current);
//                //为什么是10个byte，还要跳过两个，12个岂不更好
//                //获得显示器上显示事件的标志的颜色
//                byte[] ncolor = datafunc.DataReadFun(natFile, 3);
//                EventColor evntcolor = new EventColor(ncolor[2], ncolor[1], ncolor[0]);

//                evntArr[0] = new Event(evntname, evntcolor);

//                int j = 1;
//                while (j < evnthead.Number)//获得软件定义的事件
//                {
//                    natFile.Seek(1, SeekOrigin.Current);
//                    evntname = (Encoding.GetEncoding(936).GetString(datafunc.DataReadFun(natFile, 10))).Trim('\0');
//                    natFile.Seek(2, SeekOrigin.Current);
//                    ncolor = datafunc.DataReadFun(natFile, 3);
//                    evntcolor = new EventColor(ncolor[2], ncolor[1], ncolor[0]);
//                    evntArr[j] = new Event(evntname, evntcolor);
//                    j++;
//                }
//            }
//            natFile.Close();
//            natFile.Dispose();
//        }
//    }
}
