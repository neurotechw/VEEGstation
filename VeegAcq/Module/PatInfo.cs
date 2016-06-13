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
        private string name;

        /// <summary>
        /// 性别
        /// </summary>
        private string gender;

        /// <summary>
        /// 检查号
        /// </summary>
        private string id;

        /// <summary>
        /// 病人年龄
        /// </summary>
        private string age;

        /// <summary>
        /// 左右利
        /// </summary>
        private string handedness;

        /// <summary>
        /// 状态
        /// </summary>
        private string state;

        /// <summary>
        /// 申请医师
        /// </summary>
        private string residentDoctor;

        /// <summary>
        /// 检查类型
        /// </summary>
        private string type;

        /// <summary>
        /// 门诊号
        /// </summary>
        private string outPatient;

        /// <summary>
        /// 住院号
        /// </summary>
        private string admissionNumber;

        /// <summary>
        /// 操作医生
        /// </summary>
        private string operateDoctor;

        /// <summary>
        /// 诊断
        /// </summary>
        private string diagnosis;

        /// <summary>
        /// 既往病史
        /// </summary>
        private string history;

        /// <summary>
        /// 用药
        /// </summary>
        private string medicine;

        /// <summary>
        /// 归档
        /// </summary>
        private string archives;

        /// <summary>
        /// 备注
        /// </summary>
        private string note;

        /// <summary>
        /// 病区
        /// </summary>
        private string bingQu;

        /// <summary>
        /// 床号
        /// </summary>
        private string chuangHao;

        /// <summary>
        /// 科室
        /// </summary>
        private string keShi;
        //private string FilePath;
        #endregion

        #region 访问器
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Handedness
        {
            get { return handedness; }
            set { handedness = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string ResidentDoctor
        {
            get { return residentDoctor; }
            set { residentDoctor = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string OutPatient
        {
            get { return outPatient; }
            set { outPatient = value; }
        }

        public string AdmissionNumber
        {
            get { return admissionNumber; }
            set { admissionNumber = value; }
        }

        public string OperateDoctor
        {
            get { return operateDoctor; }
            set { operateDoctor = value; }
        }

        public string Diagnosis
        {
            get { return diagnosis; }
            set { diagnosis = value; }
        }

        public string History
        {
            get { return history; }
            set { history = value; }
        }

        public string Medicine
        {
            get { return medicine; }
            set { medicine = value; }
        }

        public string Archives
        {
            get { return archives; }
            set { archives = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public string BingQu
        {
            get { return bingQu; }
            set { bingQu = value; }
        }

        public string ChuangHao
        {
            get { return chuangHao; }
            set { chuangHao = value; }
        }

        public string KeShi
        {
            get { return keShi; }
            set { keShi = value; }
        }
        #endregion

        #region 定义每个内容占的字节
        public static int FixedLength_PAT = 5;
        //空7格
        public static int FixedLength_Name = 21;
        public static int FixedLength_Gender = 3; //5
        public static int FixedLength_ID = 23; //21
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
            //将null转化为""
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
            byte[] pat = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_PAT);
            string PAT = Encoding.GetEncoding(936).GetString(pat).Trim();
            fileIndex += FixedLength_PAT;
            fileIndex += 7;
            if (PAT.Equals("[PAT]")) 
            {
                //姓名
                byte[] name = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_Name);
                this.name = Encoding.GetEncoding(936).GetString(name).Trim();
                fileIndex += FixedLength_Name;

                //性别
                byte[] gender = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_Gender);
                this.gender = Encoding.GetEncoding(936).GetString(gender).Trim();
                fileIndex += FixedLength_Gender;

                //ID
                byte[] id = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_ID);
                this.id = Encoding.GetEncoding(936).GetString(id).Trim();
                fileIndex += FixedLength_ID;

                //年龄
                byte[] age = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_Age);
                this.age = Encoding.GetEncoding(936).GetString(age).Trim();
                fileIndex += FixedLength_Age;

                //左右利
                byte[] handedness = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_Handedness);
                this.handedness = Encoding.GetEncoding(936).GetString(handedness).Trim();
                fileIndex += FixedLength_Handedness;

                //状态
                byte[] state = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_State);
                this.state = Encoding.GetEncoding(936).GetString(state).Trim();
                fileIndex += FixedLength_State;

                //申请医生
                byte[] residentDoctor = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_ResidentDoctor);
                this.residentDoctor = Encoding.GetEncoding(936).GetString(residentDoctor).Trim();
                fileIndex += FixedLength_ResidentDoctor;

                //门诊号
                byte[] outPatient = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_OutPatient);
                this.outPatient = Encoding.GetEncoding(936).GetString(outPatient).Trim();
                fileIndex += FixedLength_OutPatient;

                //住院号
                byte[] admissionNumber = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_AdmissionNumber);
                this.admissionNumber = Encoding.GetEncoding(936).GetString(admissionNumber).Trim();
                fileIndex += FixedLength_AdmissionNumber;

                //操作医生
                byte[] operateDoctor = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_OperateDoctor);
                this.operateDoctor = Encoding.GetEncoding(936).GetString(operateDoctor).Trim();
                fileIndex += FixedLength_OperateDoctor;

                //诊断
                byte[] diagnosis = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_Diagnosis);
                this.diagnosis = Encoding.GetEncoding(936).GetString(diagnosis).Trim();
                fileIndex += FixedLength_Diagnosis;

                //病史
                byte[] history = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_History);
                this.history = Encoding.GetEncoding(936).GetString(history).Trim();
                fileIndex += FixedLength_History;
                
                //用药
                byte[] medicine = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_Medicine);
                this.medicine = Encoding.GetEncoding(936).GetString(medicine).Trim();
                fileIndex += FixedLength_Medicine;

                //备注
                byte[] note = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_Note);
                this.note = Encoding.GetEncoding(936).GetString(note).Trim();
                fileIndex += FixedLength_Note;

                //病区
                byte[] bingqu = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_BingQu);
                this.bingQu = Encoding.GetEncoding(936).GetString(bingqu).Trim();
                fileIndex += FixedLength_BingQu;

                //床号
                byte[] chuanghao = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_ChuangHao);
                this.chuangHao = Encoding.GetEncoding(936).GetString(chuanghao).Trim();
                fileIndex += FixedLength_ChuangHao;

                //科室
                byte[] keshi = Util.getFixedLengthByteArray(patientInfo, fileIndex, FixedLength_KeShi);
                this.keShi = Encoding.GetEncoding(936).GetString(keshi).Trim();
                fileIndex += FixedLength_KeShi;
            }
            Debug.WriteLine(string.Format("Patient {0}", this.Name));
        }


        /// <summary>
        /// 消零,暂未用到 --by xjg
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

    }
}
