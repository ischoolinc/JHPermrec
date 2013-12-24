using System;
using System.Collections.Generic;
using System.Text;

namespace Student_JSBT
{
    class StudInfoEntity
    {
        private Dictionary<string,DataCellEntity> _DataCellEntityDic= new Dictionary<string,DataCellEntity> ();

        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string StudentID {get;set;}
        /// <summary>
        /// 學生姓名
        /// </summary>
        public string Name {get;set;}
        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName {get;set;}
        /// <summary>
        /// 座號
        /// </summary>
        public string SeatNo {get;set;}
        /// <summary>
        /// 身分證號
        /// </summary>
        public string IDNumber {get;set;}
        /// <summary>
        /// 學生學號
        /// </summary>
        public string StudentNumber {get;set;}
        /// <summary>
        /// 性別
        /// </summary>
        public string Gender { get; set; }


        /// <summary>
        /// 設定欄位資料
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="FieldLenght"></param>
        /// <param name="Value"></param>
        public void SetDataCellEntity(string FieldName, int FieldLenght, string Value)
        {
            if (_DataCellEntityDic.ContainsKey(FieldName))
            {
                _DataCellEntityDic[FieldName].FieldLenght = FieldLenght;
                _DataCellEntityDic[FieldName].Value = Value;
            }
            else
            {
                DataCellEntity dec = new DataCellEntity();
                dec.FieldName = FieldName;
                dec.FieldLenght = FieldLenght;
                dec.Value = Value;
                _DataCellEntityDic.Add(FieldName,dec);
            }
        
        }

        /// <summary>
        /// 取得欄位資料
        /// </summary>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public DataCellEntity GetDataCellEntity(string FieldName)
        {
            DataCellEntity dce = new DataCellEntity();
            if (_DataCellEntityDic.ContainsKey(FieldName))
                dce = _DataCellEntityDic[FieldName];
            
            return dce;
        }
    }
}
