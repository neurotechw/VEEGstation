using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VeegStation
{
    /// <summary>
    /// NAT文件中病人信息  --by zt
    /// </summary>
    public class PatInfo
    {
        #region 声明域和属性
        /// <summary>
        /// 姓名
        /// </summary>
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        } 
        /// <summary>
        /// 性别
        /// </summary>
        private string _Gender;

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }	 
        /// <summary>
        /// 检查号
        /// </summary>
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }		 
        /// <summary>
        /// 病人年龄
        /// </summary>
        private string _Age;

        public string Age
        {
            get { return _Age; }
            set { _Age = value; }
        }	         
        /// <summary>
        /// 左右利
        /// </summary>
        private string _Handedness;

        public string Handedness
        {
            get { return _Handedness; }
            set { _Handedness = value; }
        }	
        /// <summary>
        /// 状态
        /// </summary>
        private string _State;

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }	
        /// <summary>
        /// 申请医师
        /// </summary>
        private string _ResidentDoctor;

        public string ResidentDoctor
        {
            get { return _ResidentDoctor; }
            set { _ResidentDoctor = value; }
        }	 
        /// <summary>
        /// 检查类型
        /// </summary>
        private string _Type;

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }	
        /// <summary>
        /// 门诊号
        /// </summary>
        private string _OutPatient;

        public string OutPatient
        {
            get { return _OutPatient; }
            set { _OutPatient = value; }
        }	 
        /// <summary>
        /// 住院号
        /// </summary>
        private string _AdmissionNumber;

        public string AdmissionNumber
        {
            get { return _AdmissionNumber; }
            set { _AdmissionNumber = value; }
        }	 
        /// <summary>
        /// 操作医生
        /// </summary>
        private string _OperateDoctor;

        public string OperateDoctor
        {
            get { return _OperateDoctor; }
            set { _OperateDoctor = value; }
        } 
        /// <summary>
        /// 诊断
        /// </summary>
        private string _Diagnosis;

        public string Diagnosis
        {
            get { return _Diagnosis; }
            set { _Diagnosis = value; }
        } 
        /// <summary>
        /// 既往病史
        /// </summary>
        private string _History;

        public string History
        {
            get { return _History; }
            set { _History = value; }
        }	 
        /// <summary>
        /// 用药
        /// </summary>
        private string _Medicine;

        public string Medicine
        {
            get { return _Medicine; }
            set { _Medicine = value; }
        }	 
        /// <summary>
        /// 归档
        /// </summary>
        private string _Archives;

        public string Archives
        {
            get { return _Archives; }
            set { _Archives = value; }
        }        
        /// <summary>
        /// 备注
        /// </summary>
        private string _Note;

        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }      
        /// <summary>
        /// 病区
        /// </summary>
        private string _BingQu;

        public string BingQu
        {
            get { return _BingQu; }
            set { _BingQu = value; }
        }
        /// <summary>
        /// 床号
        /// </summary>
        private string _ChuangHao;

        public string ChuangHao
        {
            get { return _ChuangHao; }
            set { _ChuangHao = value; }
        }
        /// <summary>
        /// 科室
        /// </summary>
        private string _KeShi;

        public string KeShi
        {
            get { return _KeShi; }
            set { _KeShi = value; }
        }
        //private string FilePath;
        #endregion

        #region 定义每个内容占的字节
        public static int FixedLength_PAT = 5;
        //空6格
        public static int FixedLength_Name = 21;
        public static int FixedLength_Gender = 5;
        public static int FixedLength_ID = 21;
        public static int FixedLength_Age = 13;
        public static int FixedLength_Handedness = 21;
        public static int FixedLength_State = 21;
        public static int FixedLength_ResidentDoctor = 42;
        //public static int FixedLength_Type = 21;
        public static int FixedLength_OutPatient = 21;
        public static int FixedLength_AdmissionNumber = 21;
        public static int FixedLength_OperateDoctor = 21;
        public static int FixedLength_Diagnosis = 93;
        public static int FixedLength_History = 21;
        public static int FixedLength_Medicine = 52;
        //public static int FixedLength_Archives = 9;
        public static int FixedLength_Note = 77;
        public static int FixedLength_BingQu = 31;
        public static int FixedLength_ChuangHao = 21;
        public static int FixedLength_KeShi = 20;
        #endregion

        public PatInfo() 
        {
            InitializePatInfo();
        }

        public void InitializePatInfo() 
        {

        }

        public PatInfo(byte[] patientInfo) 
        {
            ParsePatInfo(patientInfo);
        }

        public void ParsePatInfo(byte[] patientInfo) 
        {
            int i = 0;
            foreach (char c in patientInfo)
            {
                if (patientInfo[i] == (byte)0)
                {
                    patientInfo[i] = (byte)32;
                }
                i++;
            }


            int fileIndex = 0;

            //PAT
            byte[] pat = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_PAT);
            string PAT = Encoding.GetEncoding(936).GetString(pat).Trim();
            fileIndex += FixedLength_PAT;
            fileIndex += 7;
            if (PAT.Equals("[PAT]")) 
            {
                //姓名
                byte[] name = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_Name);
                this._Name = Encoding.GetEncoding(936).GetString(name).Trim();
                fileIndex += FixedLength_Name;

                //性别
                byte[] gender = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_Gender);
                this._Gender = Encoding.GetEncoding(936).GetString(gender).Trim();
                fileIndex += FixedLength_Gender;

                //ID
                byte[] id = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_ID);
                this._ID = Encoding.GetEncoding(936).GetString(id).Trim();
                fileIndex += FixedLength_ID;

                //年龄
                byte[] age = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_Age);
                this._Age = Encoding.GetEncoding(936).GetString(age).Trim();
                fileIndex += FixedLength_Age;

                //左右利
                byte[] handedness = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_Handedness);
                this._Handedness = Encoding.GetEncoding(936).GetString(handedness).Trim();
                fileIndex += FixedLength_Handedness;

                //状态
                byte[] state = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_State);
                this._State = Encoding.GetEncoding(936).GetString(state).Trim();
                fileIndex += FixedLength_State;

                //申请医生
                byte[] residentDoctor = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_ResidentDoctor);
                this._ResidentDoctor = Encoding.GetEncoding(936).GetString(residentDoctor).Trim();
                fileIndex += FixedLength_ResidentDoctor;

                //门诊号
                byte[] outPatient = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_OutPatient);
                this._OutPatient = Encoding.GetEncoding(936).GetString(outPatient).Trim();
                fileIndex += FixedLength_OutPatient;

                //住院号
                byte[] admissionNumber = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_AdmissionNumber);
                this._AdmissionNumber = Encoding.GetEncoding(936).GetString(admissionNumber).Trim();
                fileIndex += FixedLength_AdmissionNumber;

                //操作医生
                byte[] operateDoctor = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_OperateDoctor);
                this._OperateDoctor = Encoding.GetEncoding(936).GetString(operateDoctor).Trim();
                fileIndex += FixedLength_OperateDoctor;

                //诊断
                byte[] diagnosis = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_Diagnosis);
                this._Diagnosis = Encoding.GetEncoding(936).GetString(diagnosis).Trim();
                fileIndex += FixedLength_Diagnosis;

                //病史
                byte[] history = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_History);
                this._History = Encoding.GetEncoding(936).GetString(history).Trim();
                fileIndex += FixedLength_History;
                
                //用药
                byte[] medicine = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_Medicine);
                this._Medicine = Encoding.GetEncoding(936).GetString(medicine).Trim();
                fileIndex += FixedLength_Medicine;

                //备注
                byte[] note = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_Note);
                this._Note = Encoding.GetEncoding(936).GetString(note).Trim();
                fileIndex += FixedLength_Note;

                //病区
                byte[] bingqu = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_BingQu);
                this._BingQu = Encoding.GetEncoding(936).GetString(bingqu).Trim();
                fileIndex += FixedLength_BingQu;

                //床号
                byte[] chuanghao = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_ChuangHao);
                this._ChuangHao = Encoding.GetEncoding(936).GetString(chuanghao).Trim();
                fileIndex += FixedLength_ChuangHao;

                //科室
                byte[] keshi = getFixedLengthByteArrayFromPationInfo(patientInfo, fileIndex, FixedLength_KeShi);
                this._KeShi = Encoding.GetEncoding(936).GetString(keshi).Trim();
                fileIndex += FixedLength_KeShi;
            }
            Debug.WriteLine(string.Format("Patient {0}", this.Name));
        }


        /// <summary>
        /// 消零
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        private string TrimZero(string Source)
        {
            int index = Source.IndexOf('\0');
            if (index < 0)
                return Source;
            return Source.Substring(0, index);
        }

        /// <summary>
        /// 获得对应长度的字符数组
        /// </summary>
        /// <param name="pationInfo"></param>
        /// <param name="startPoint"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] getFixedLengthByteArrayFromPationInfo(byte[] pationInfo, int startPoint, int length)
        {
            byte[] ch = new byte[length];
            Array.Copy(pationInfo, startPoint, ch, 0, length);
            return ch;
        }


    }
}
