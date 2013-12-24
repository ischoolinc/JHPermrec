using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHPermrec.UpdateRecord.UpdateRecordItemControls
{
    interface IUpdateRecordInfo
    {
         /// <summary>
        /// 讀取學生異動 Data
        /// </summary>
        /// <returns></returns>
        DAL.StudUpdateRecordEntity GetStudUpdateRecordData();
    }
}
