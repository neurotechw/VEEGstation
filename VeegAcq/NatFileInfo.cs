using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace VeegStation
{
    public class NatFileInfo : NatInfo
    {

        DataFunc datafunc = new DataFunc();

        public NatFileInfo(string NatFileFullName)
        {
            FileInfo fi = new FileInfo(NatFileFullName);
            string natFileName = NatFileFullName.Substring(0, NatFileFullName.Length - fi.Extension.Length) + ".NAT";

            FileStream natFile = new FileStream(natFileName, FileMode.Open);

            byte[] sighdatabuf = datafunc.DataReadFun(natFile, 2);
            string sighdata = new string(Encoding.ASCII.GetChars(sighdatabuf));
            this.Sign = sighdata;

            byte[] versiondatabuf = datafunc.DataReadFun(natFile, 2);
            string strversiondata = (versiondatabuf[1] << 8 | versiondatabuf[0]).ToString();
            this.Version = strversiondata;

            byte[] dateyearbuf = datafunc.DataReadFun(natFile, 4);
            string date = ((dateyearbuf[3] << 8 | dateyearbuf[2]) + "-" + dateyearbuf[1] + "-" + dateyearbuf[0]).ToString();
            this.Date = DateTime.Parse(date);

            byte[] frequcebuf = datafunc.DataReadFun(natFile, 2);
            this.Freq = (frequcebuf[1] << 8 | frequcebuf[0]);

            byte[] brainnumberbuf = datafunc.DataReadFun(natFile, 1);
            this.BrainNumber = brainnumberbuf[0];

            byte[] heartnumber = datafunc.DataReadFun(natFile, 1);
            this.HeartNumber = heartnumber[0];

            byte[] eegtypebuf = datafunc.DataReadFun(natFile, 2);
            this.EegType = (eegtypebuf[1] << 8 | eegtypebuf[0]).ToString();

            natFile.Seek(2, SeekOrigin.Current);

            byte[] patoffbuf = datafunc.DataReadFun(natFile, 4);
            this.PatOff = (patoffbuf[3] << 32 | patoffbuf[2] << 16 | patoffbuf[1] << 8 | patoffbuf[0]);

            byte[] entoffbuf = datafunc.DataReadFun(natFile, 4);
            this.EntOff = (entoffbuf[3] << 32 | entoffbuf[2] << 16 | entoffbuf[1] << 8 | entoffbuf[0]);

            byte[] monoffbuf = datafunc.DataReadFun(natFile, 4);
            this.MonOff = (monoffbuf[3] << 32 | monoffbuf[2] << 16 | monoffbuf[1] << 8 | monoffbuf[0]);

            byte[] cfgoffbuf = datafunc.DataReadFun(natFile, 4);
            this.CfgOff = (cfgoffbuf[3] << 32 | cfgoffbuf[2] << 16 | cfgoffbuf[1] << 8 | cfgoffbuf[0]);

            byte[] dataoffbuf = datafunc.DataReadFun(natFile, 4);
            this.DatOff = (dataoffbuf[3] << 32 | dataoffbuf[2] << 16 | dataoffbuf[1] << 8 | dataoffbuf[0]);

            this.RespNumber = Convert.ToByte(datafunc.DataReadFun(natFile, 1)[0].ToString(), 16);

            this.FlashNumber = Convert.ToByte(datafunc.DataReadFun(natFile, 1)[0].ToString(), 16);

            this.Operate = Convert.ToByte(datafunc.DataReadFun(natFile, 1)[0].ToString(), 16);

            natFile.Seek(1, SeekOrigin.Current);

            byte[] flgbuf = datafunc.DataReadFun(natFile, 4);
            string flgdata = (flgbuf[3] << 32 | flgbuf[2] << 16 | flgbuf[1] << 8 | flgbuf[0]).ToString();
            this.Flag = flgdata;

            byte[] vidioflgbuf = datafunc.DataReadFun(natFile, 4);
            int flg = (vidioflgbuf[3] << 32 | vidioflgbuf[2] << 16 | vidioflgbuf[1] << 8 | vidioflgbuf[0]);
            if (flg == 0)
                this.bIsHaveVideo = false;
            else
                this.bIsHaveVideo = true;

            byte[] nrowsofdatabuf = datafunc.DataReadFun(natFile, 4);
            this.nRowsOfData = (nrowsofdatabuf[3] << 32 | nrowsofdatabuf[2] << 16 | nrowsofdatabuf[1] << 8 | nrowsofdatabuf[0]);

            this.byteConfigType = Convert.ToByte(datafunc.DataReadFun(natFile, 1)[0].ToString(), 16);
            natFile.Seek(3, SeekOrigin.Current);
            this.Remain = datafunc.DataReadFun(natFile, 88);

            EEGFMT mono = new EEGFMT();
            MONTAGE mMontage = new MONTAGE(64);
            natFile.Seek(this.MonOff, SeekOrigin.Begin);
            byte[] mFlag = datafunc.DataReadFun(natFile, 8);
            string monosection = new string(Encoding.ASCII.GetChars(mFlag)).Trim('\0');
            if (monosection == "[MON]")
            {
                mono.Name = new string(Encoding.ASCII.GetChars(datafunc.DataReadFun(natFile, 6))).Trim('\0');
                int i = 0;
                string getstr = (new string(Encoding.ASCII.GetChars(datafunc.DataReadFun(natFile, 5)))).Trim('\0');
                while (!getstr.Equals(""))
                {
                    mMontage.FirstName[i] = getstr;
                    mMontage.SecondName[i] = "REF";
                    getstr = (new string(Encoding.ASCII.GetChars(datafunc.DataReadFun(natFile, 5)))).Trim('\0');
                    i++;
                }
            }

            EVENTHEAD evnthead = new EVENTHEAD();
            natFile.Seek(this.EntOff, SeekOrigin.Begin);
            evnthead.Sign = (new string(Encoding.ASCII.GetChars(datafunc.DataReadFun(natFile, 5))));
            if (evnthead.Sign == "[ENT]")
            {
                natFile.Seek(3, SeekOrigin.Current);
                byte[] evntnum = datafunc.DataReadFun(natFile, 1);
                evnthead.Number = evntnum[0] >> 4;
                EVENT[] evntArr = new EVENT[evnthead.Number];
                natFile.Seek(3, SeekOrigin.Current);
                string evntname = (Encoding.GetEncoding(936).GetString(datafunc.DataReadFun(natFile, 10))).Trim('\0');
                natFile.Seek(2, SeekOrigin.Current);
                byte[] ncolor = datafunc.DataReadFun(natFile, 3);
                COLORREF evntcolor = new COLORREF(ncolor[2], ncolor[1], ncolor[0]);
                evntArr[0] = new EVENT(evntname, evntcolor);
                int j = 1;
                while (j < evnthead.Number)
                {
                    natFile.Seek(1, SeekOrigin.Current);
                    evntname = (Encoding.GetEncoding(936).GetString(datafunc.DataReadFun(natFile, 10))).Trim('\0');
                    natFile.Seek(2, SeekOrigin.Current);
                    ncolor = datafunc.DataReadFun(natFile, 3);
                    evntcolor = new COLORREF(ncolor[2], ncolor[1], ncolor[0]);
                    evntArr[j] = new EVENT(evntname, evntcolor);
                    j++;
                }
            }
            natFile.Close();
            natFile.Dispose();
        }
    }
}
