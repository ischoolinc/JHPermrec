using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JHPermrec.UpdateRecord.DAL
{
    /// <summary>
    /// 異動 Content 內容
    /// </summary>
    public class StudBatchUpdateRecContentEntity
    {


        private XmlElement _XmlData;

        public StudBatchUpdateRecContentEntity(XmlElement xml)
        {
            _XmlData = xml;

            if (_XmlData == null)
                _XmlData = new XmlDocument().CreateElement("異動紀錄");
        }

        /// <summary>
        /// 取得異動紀錄全部資料
        /// </summary>
        /// <returns></returns>
        public XmlElement GetXmlData()
        {
            return _XmlData;        
        }


        /// <summary>
        /// 取得編號
        /// </summary>
        /// <returns></returns>
        public string GetID()
        {
            return _XmlData.GetAttribute("編號");
        }

        /// <summary>
        /// 設定編號
        /// </summary>
        /// <param name="str"></param>
        public void SetID(string str)
        {
            _XmlData.SetAttribute("編號", str);
        }

        /// <summary>
        /// 取得座號
        /// </summary>
        /// <returns></returns>
        public string GetSeatNo()
        {
            return _XmlData.GetAttribute("座號");
        }

        /// <summary>
        /// 設定座號
        /// </summary>
        /// <param name="str"></param>
        public void SetSeatNo(string str)
        {
            _XmlData.SetAttribute("座號", str);
        }

        /// <summary>
        /// 取得異動類別
        /// </summary>
        /// <returns></returns>
        public string GetUpdateCodeType()
        {
            return _XmlData.GetAttribute("異動類別");
        }


        /// <summary>
        /// 設定異動類別
        /// </summary>
        /// <param name="str"></param>
        public void SetUpdateCodeType(string str)
        {
            _XmlData.SetAttribute("異動類別", str);
        }

        /// <summary>
        /// 取得異動代碼
        /// </summary>
        /// <returns></returns>
        public string GetUpdateCode()
        {
            return _XmlData.GetAttribute("異動代號");
        }


        /// <summary>
        /// 設定異動代碼
        /// </summary>
        /// <param name="str"></param>
        public void SetUpdateCode(string str)
        {
            _XmlData.SetAttribute("異動代號", str);
        }

        /// <summary>
        /// 取得異動日期
        /// </summary>
        /// <returns></returns>
        public string GetUpdateDate()
        {
            // 處理舊資料整合
            if (_XmlData.HasAttribute("異動日期1"))
                return _XmlData.GetAttribute("異動日期1");
            else
            {
                // 轉成西元
                string str=_XmlData.GetAttribute("異動日期");

                if (CehckIsChineseDate(str))
                    return ConvertDate1(str);
                else
                    return str;
            }
        }

        /// <summary>
        /// 設定異動日期
        /// </summary>
        /// <param name="str"></param>
        public void SetUpdateDate(string str)
        {
            if (_XmlData.HasAttribute("異動日期1"))
                _XmlData.RemoveAttribute("異動日期1");

            _XmlData.SetAttribute("異動日期", str);
        }

        /// <summary>
        /// 取得學號
        /// </summary>
        /// <returns></returns>
        public string GetStudentNumber()
        {
            return _XmlData.GetAttribute("學號");
        }

        /// <summary>
        /// 設定學號
        /// </summary>
        /// <param name="str"></param>
        public void SetStudentNumber(string str)
        {
            _XmlData.SetAttribute("學號", str);
        }

        /// <summary>
        /// 取得姓名
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return _XmlData.GetAttribute("姓名");
        }

        /// <summary>
        /// 設定姓名
        /// </summary>
        /// <param name="str"></param>
        public void SetName(string str)
        {
            _XmlData.SetAttribute("姓名", str);
        }

        /// <summary>
        /// 取得身分證號
        /// </summary>
        /// <returns></returns>
        public string GetIDNumber()
        {
            return _XmlData.GetAttribute("身分證號");
        }

        /// <summary>
        /// 設定身分證號
        /// </summary>
        /// <param name="str"></param>
        public void SetIDNumber(string str)
        {
            _XmlData.SetAttribute("身分證號", str);
        }

        /// <summary>
        /// 取得性別
        /// </summary>
        /// <returns></returns>
        public string GetGender()
        {
            return _XmlData.GetAttribute("性別");
        }

        /// <summary>
        /// 取得性別
        /// </summary>
        /// <param name="str"></param>
        public void SetGender(string str)
        {
            _XmlData.SetAttribute("性別", str);
        }


        /// <summary>
        /// 取得性別代號(男1,女2)
        /// </summary>
        /// <returns></returns>
        public string GetGenderID()
        {
            return _XmlData.GetAttribute("性別代號");
        }

        /// <summary>
        /// 設定性別代號(男1,女2)
        /// </summary>
        /// <returns></returns>
        public void SetGenderID(string str)
        {
            _XmlData.SetAttribute("性別代號",str);
        }

        /// <summary>
        /// 取得出生年月日
        /// </summary>
        /// <returns></returns>
        public string GetBirthday()
        {
            if (_XmlData.HasAttribute("生日"))
                return _XmlData.GetAttribute("生日");
            else
            {
                // 轉成西元
                string str = _XmlData.GetAttribute("出生年月日");

                if (CehckIsChineseDate(str))
                    return ConvertDate1(str);
                else
                    return str;
            }
        }

        /// <summary>
        /// 設定出生年月日
        /// </summary>
        /// <param name="str"></param>
        public void SetBirthday(string str)
        {
            if (_XmlData.HasAttribute("出生年月日"))
                _XmlData.RemoveAttribute("出生年月日");

            _XmlData.SetAttribute("生日", str);
        }


        /// <summary>
        /// 取得班級
        /// </summary>
        /// <returns></returns>
        public string GetClassName()
        {
            if(_XmlData.HasAttribute("班別"))
                return _XmlData.GetAttribute("班別");
            else
                return _XmlData.GetAttribute("班級");
        }

        /// <summary>
        /// 設定班級
        /// </summary>
        /// <param name="str"></param>
        public void SetClassName(string str)
        {
            if (_XmlData.HasAttribute("班別"))
                _XmlData.RemoveAttribute("班別");

            _XmlData.SetAttribute("班級", str);            
        }


        /// <summary>
        /// 取得地址
        /// </summary>
        /// <returns></returns>
        public string GetAddress()
        {
            return _XmlData.GetAttribute("地址");
        }

        /// <summary>
        /// 設定地址
        /// </summary>
        /// <param name="str"></param>
        public void SetAddress(string str)
        {
            _XmlData.SetAttribute("地址", str);
        }

        
        /// <summary>
        /// 取得入學年月
        /// </summary>
        /// <returns></returns>
        public string GetEnrollmentSchoolYear()
        {
            return _XmlData.GetAttribute("入學年月");
        }


        /// <summary>
        /// 設定入學年月
        /// </summary>
        /// <param name="str"></param>
        public void SetEnrollmentSchoolYear(string str)
        {
            _XmlData.SetAttribute("入學年月", str);
        }

        /// <summary>
        /// 取得學籍核准文號
        /// </summary>
        /// <returns></returns>
        public string GetLastADNumber()
        {
            return _XmlData.GetAttribute("學籍核准文號");
        }

        /// <summary>
        /// 設定學籍核准文號
        /// </summary>
        /// <param name="str"></param>
        public void SetLastADNumber(string str)
        {
            _XmlData.SetAttribute("學籍核准文號", str);
        }

        /// <summary>
        /// 取得異動年級
        /// </summary>
        /// <returns></returns>
        public string GetGradeYear()
        {
            return _XmlData.GetAttribute("異動年級");
        }

        /// <summary>
        /// 設定異動年級
        /// </summary>
        public void SetGradeYear(string str)
        {
            _XmlData.SetAttribute("異動年級",str);
        }

        /// <summary>
        /// 取得備註
        /// </summary>
        /// <returns></returns>
        public string GetComment()
        {
            return _XmlData.GetAttribute("備註");
        }

        /// <summary>
        /// 設定備註
        /// </summary>
        /// <param name="str"></param>
        public void SetComment(string str)
        {
            _XmlData.SetAttribute("備註", str);        
        }

        /// <summary>
        /// 取得監護人
        /// </summary>
        /// <returns></returns>
        public string GetGuardian()
        {
            return _XmlData.GetAttribute("監護人");
        }

        /// <summary>
        /// 設定監護人
        /// </summary>
        /// <param name="str"></param>
        public void SetGuardian(string str)
        {
            _XmlData.SetAttribute("監護人", str);
        }

        /// <summary>
        /// 取得畢業國小
        /// </summary>
        /// <returns></returns>
        public string GetPrimarySchoolName()
        {
            return _XmlData.GetAttribute("畢業國小");
        }

        /// <summary>
        /// 設定畢業國小
        /// </summary>
        /// <param name="str"></param>
        public void SetPrimarySchoolName(string str)
        {
            _XmlData.SetAttribute("畢業國小", str);
        }
       
        /// <summary>
        /// 取得畢業國小所在縣市代號
        /// </summary>
        /// <returns></returns>
        public string GetGraduateSchoolLocationCode()
        {
            return _XmlData.GetAttribute("畢業國小所在縣市代號");        
        }

        /// <summary>
        /// 設定畢業國小所在縣市代號
        /// </summary>
        /// <param name="str"></param>
        public void SetGraduateSchoolLocationCode(string str)
        {
            _XmlData.SetAttribute("畢業國小所在縣市代號", str);
        }
        
        /// <summary>
        /// 取得畢業證書字號
        /// </summary>
        /// <returns></returns>
        public string GetGraduateCertificateNumber()
        {
            return _XmlData.GetAttribute("畢業證書字號");
        }

        /// <summary>
        /// 設定畢業證書字號
        /// </summary>
        /// <param name="str"></param>
        public void SetGraduateCertificateNumber(string str)
        {
            _XmlData.SetAttribute("畢業證書字號", str);
        }

        /// <summary>
        /// 取得出生地
        /// </summary>
        /// <returns></returns>
        public string GetBirthPlace()
        {
            return _XmlData.GetAttribute("出生地");
        }


        /// <summary>
        /// 設定出生地
        /// </summary>
        /// <param name="str"></param>
        public void SetBirthPlace(string str)
        {
            _XmlData.SetAttribute("出生地", str);        
        }

        
        /// <summary>
        /// 取得畢業年月
        /// </summary>
        /// <returns></returns>
        public string GetGraduateSchoolYear()
        {
            return _XmlData.GetAttribute("畢業年月");
        }

        /// <summary>
        /// 設定畢業年月
        /// </summary>
        /// <param name="str"></param>
        public void SetGraduateSchoolYear(string str)
        {
            _XmlData.SetAttribute("畢業年月", str);        
        }

        /// <summary>
        /// 取得學籍核准日期
        /// </summary>
        /// <returns></returns>
        public string GetLastADDate()
        {
            if (_XmlData.HasAttribute("學籍核准日期1"))
                return _XmlData.GetAttribute("學籍核准日期1");
            else
            {
                // 轉成西元
                string str = _XmlData.GetAttribute("學籍核准日期");

                if (CehckIsChineseDate(str))
                    return ConvertDate1(str);
                else
                    return str;
            }
        }

        /// <summary>
        /// 設定學籍核准日期
        /// </summary>
        /// <param name="str"></param>
        public void SetLastADDate(string str)
        {
            if (_XmlData.HasAttribute("學籍核准日期1"))
                _XmlData.RemoveAttribute("學籍核准日期1");

            _XmlData.SetAttribute("學籍核准日期", str);
        }
        
        /// <summary>
        /// 取得戶籍地址
        /// </summary>
        /// <returns></returns>
        public string GetPermanentAddress()
        {
            return _XmlData.GetAttribute("戶籍地址");
        }

        /// <summary>
        /// 設定戶籍地址
        /// </summary>
        /// <param name="str"></param>
        public void SetPermanentAddress(string str)
        {
            _XmlData.SetAttribute("戶籍地址", str);
        }

        /// <summary>
        /// 取得畢修業別
        /// </summary>
        /// <returns></returns>
        public string GetGraduate()
        {
            return _XmlData.GetAttribute("畢修業別");
        }

        /// <summary>
        /// 設定畢修業別
        /// </summary>
        /// <param name="str"></param>
        public void SetGraduate(string str)
        {
            _XmlData.SetAttribute("畢修業別", str);
        }

        /// <summary>
        /// 取得轉入轉出學校
        /// </summary>
        /// <returns></returns>
        public string GetImportExportSchool()
        {
            return _XmlData.GetAttribute("轉入轉出學校");
        }

        /// <summary>
        /// 設定轉入轉出學校
        /// </summary>
        /// <param name="str"></param>
        public void SetImportExportSchool(string str)
        {
            _XmlData.SetAttribute("轉入轉出學校", str);
        }

        /// <summary>
        /// 取得異動原因或事項
        /// </summary>
        /// <returns></returns>
        public string GetUpdateDescription()
        {
            if(_XmlData.HasAttribute("休學原因"))
                return _XmlData.GetAttribute("休學原因");

            return _XmlData.GetAttribute("異動原因或事項");
        }

        /// <summary>
        /// 設定異動原因或事項
        /// </summary>
        /// <param name="str"></param>
        public void SetUpdateDescription(string str)
        {
            _XmlData.SetAttribute("異動原因或事項", str);
        }

        /// <summary>
        /// 取得班級年級
        /// </summary>
        /// <returns></returns>
        public string GetClassYear()
        {
            return _XmlData.GetAttribute("班級年級");
        }

        /// <summary>
        /// 設定班級年級
        /// </summary>
        /// <param name="str"></param>
        public void SetClassYear(string str)
        {
            _XmlData.SetAttribute("班級年級", str);
        }

        /// <summary>
        /// 取得新姓名
        /// </summary>
        public string GetNewName()
        {
            return _XmlData.GetAttribute("新姓名");
        }

        /// <summary>
        /// 設定新姓名
        /// </summary>        
        public void SetNewName(string str)
        {
            _XmlData.SetAttribute("新姓名", str);
        }

        /// <summary>
        /// 取得新性別
        /// </summary>
        /// <returns></returns>
        public string GetNewGender()
        {
            return _XmlData.GetAttribute("新性別");
        }

        /// <summary>
        /// 設定新性別
        /// </summary>
        /// <param name="str"></param>
        public void SetNewGender(string str)
        {
            _XmlData.SetAttribute("新性別", str);
        }

        /// <summary>
        /// 取得新出生年月日
        /// </summary>
        /// <returns></returns>
        public string GetNewBirthday()
        {
            return _XmlData.GetAttribute("新出生年月日");        
        }

        /// <summary>
        /// 設定新出生年月日
        /// </summary>
        /// <param name="str"></param>
        public void SetNewBirthday(string str)
        {
            _XmlData.SetAttribute("新出生年月日", str);
        }


        /// <summary>
        /// 取得新身分證號
        /// </summary>
        /// <returns></returns>
        public string GetNewIDNumber()
        {
            return _XmlData.GetAttribute("新身分證號");        
        }

        /// <summary>
        /// 設定新身分證號
        /// </summary>
        /// <param name="str"></param>
        public void SetNewIDNumber(string str)
        {
            _XmlData.SetAttribute("新身分證號", str);
        }

        // 檢查西元或民國日期
        private bool CehckIsChineseDate(string str)
        {
            if (str.IndexOf('/') > 3)
                return false;
            else
                return true ;
        }

        // 轉換日期
        private string ConvertDate1(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            string str1 = str.Trim();
            int n=str1.IndexOf('/');
            int year;
            if (int.TryParse(str1.Substring(0, n), out year))
            {
                return (year + 1911) + str1.Substring(n, str.Length - n);
            }
            else
                return string.Empty;
        }
    }
}
