
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// 导联结构类  --by zt
    /// </summary>
    public class Montage
    {
        #region 声明域和属性
        /// <summary>
        /// Montage mode 一般为"[MON]"
        /// </summary>
        private byte montageMode;     
        
        /// <summary>
        /// Electrorode number
        /// </summary>
        private byte montageRoutes;  
        
        /// <summary>
        /// 相对导名
        /// </summary>
        private string[] firstName; 

        /// <summary>
        /// 基导名
        /// </summary>
        private string[] secondName; 
        
        /// <summary>
        /// 相对导ID
        /// </summary>
        private string[] firstID;
        
        /// <summary>
        /// 基导名ID
        /// </summary>
        private string[] secondID; 
        
        /// <summary>
        /// 导联名称
        /// </summary>
        private string name;
        
        /// <summary>
        /// 标识
        /// </summary>
        private short flag;
        
        /// <summary>
        /// 导联备注
        /// </summary>
        private string note;
        
        /// <summary>
        /// 脑电盒支持的最高配置如P41,即导联数目,对应关系如下
        /// P10   8导脑电
        /// P11   8导脑电+多参数
        /// P20   16导脑电
        /// P21   16导脑电+多参数
        /// P30   24导脑电
        /// P40   32导脑电
        /// P41   32导脑电+多参数
        /// </summary>
        private string szSetting;   
                            
        /// <summary>
        /// 最高配置的含义 例：8导脑电 
        /// </summary>
        private string szNote; 

        /// <summary>
        /// 导联源
        /// </summary>
        private ArrayList leadSource;
        #endregion
        #region 访问器
        public byte MontageMode
        {
            get { return montageMode; }
            set { montageMode = value; }
        }

        public byte MontageRoutes
        {
            get { return montageRoutes; }
            set { montageRoutes = value; }
        }

        public string[] FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string[] SecondName
        {
            get { return secondName; }
            set { secondName = value; }
        }

        public string[] FirstID
        {
            get { return firstID; }
            set { firstID = value; }
        }

        public string[] SecondID
        {
            get { return secondID; }
            set { secondID = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public short Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public string SzSetting
        {
            get { return szSetting; }
            set { szSetting = value; }
        }

        public string SzNote
        {
            get { return szNote; }
            set { szNote = value; }
        }

        public ArrayList LeadSource
        {
            get { return leadSource; }
            set { leadSource = value; }
        }
        #endregion

        #region 定义每个内容占的字节
        public static int FixedLength_MontageMode = 5;
        public static int FixedLength_SzSetting = 4;
        public static int FixedLength_SzNote = 42;
        #endregion

        public Montage(int num)
        {
            firstName = new string[num];
            secondName = new string[num];
            firstID = new string[num];
            secondID = new string[num];
        }

        public Montage(byte[] montageInfo) 
        {
            ParseMontage(montageInfo);
        }

        /// <summary>
        /// 解析导联信息
        /// </summary>
        /// <param name="montageInfo"></param>
        private void ParseMontage(byte[] montageInfo)
        {

            int i = 0;
            foreach (char c in montageInfo)
            {
                if (montageInfo[i] == (byte)0)
                {
                    montageInfo[i] = (byte)32;
                }
                i++;
            }

            int fileIndex = 0;

            byte[] montageModeBuf = Util.getFixedLengthByteArray(montageInfo, fileIndex, FixedLength_MontageMode);
            string Mode = Encoding.GetEncoding(936).GetString(montageModeBuf).Trim();
            fileIndex += FixedLength_MontageMode;
            
            if (Mode.Equals("[MON]")) 
            {
                fileIndex += 0x797;

                //脑电盒支持的最高配置
                byte[] szSettingBuf = Util.getFixedLengthByteArray(montageInfo, fileIndex, FixedLength_SzSetting);
                this.szSetting = Encoding.GetEncoding(936).GetString(szSettingBuf).Trim();
                fileIndex += FixedLength_SzSetting;

                //最高配置的含义
                byte[] szNoteBuf = Util.getFixedLengthByteArray(montageInfo, fileIndex, FixedLength_SzNote);
                this.szNote = Encoding.GetEncoding(936).GetString(szNoteBuf).Trim();

                //定义导联源
                switch (this.szSetting)
                {
                    case "P10":                 //8导脑电
                        this.leadSource = new ArrayList() { "Fp1", "Fp2", "T3", "T4", "C3", "C4", "O1", "O2", "A1", "A2" };
                        break;
                    case "P11":                 //8导脑电+多参数
                        this.leadSource = new ArrayList() { "Fp1", "Fp2", "T3", "T4", "C3", "C4", "O1", "O2", "A1", "A2" };
                        break;
                    case "P20":                 //16导脑电
                        this.leadSource = new ArrayList() { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "Sp1", "Sp2", "A1", "A2" };
                        break;
                    case "P21":                 //16导脑电+多参数
                        this.leadSource = new ArrayList() { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "Cz", "Pz", "A1", "A2" };
                        break;
                    case "P30":                 //24导脑电
                        this.leadSource = new ArrayList() { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "Fz", "Cz", "Pz", "Oz", 
                                                            "Cp3", "Cp4", "Po3", "Po4", "A1", "A2" };
                        break;
                    case "P40":                 //32导脑电
                        this.leadSource = new ArrayList() { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "AF3", "AF4", "Fc3",
                                                            "Fc4", "Cp3", "Cp4", "Po3", "Po4", "FT7", "FT8", "Cp7", "Cp8", "Fz", "Cz", "Pz", "Oz", "Sp1", "Sp2", "A1", "A2" };                                
                        break;
                    case "P41":                 //32导脑电+多参数
                        this.leadSource = new ArrayList() { "Fp1", "Fp2", "F3", "F4", "C3", "C4", "P3", "P4", "O1", "O2", "F7", "F8", "T3", "T4", "T5", "T6", "AF3", "AF4", "Fc3",
                                                            "Fc4", "Cp3", "Cp4", "Po3", "Po4", "FT7", "FT8", "Cp7", "Cp8", "Fz", "Cz", "Pz", "Oz", "Sp1", "Sp2", "A1", "A2" };                          
                        break;
                    default:
                        break;
                }

                Debug.WriteLine(string.Format("MontageInfo {0},{1}", this.szSetting,this.szNote));
            }
        }
    }
}

