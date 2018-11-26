using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Varlab.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseWrapper
    {
        public static string DB_NAME = "bodyprotector.db";
        public static string CONN = "URI=file:" + Application.dataPath + "/Database/Source/" + DB_NAME;

        private IDbConnection db;


    }
}
