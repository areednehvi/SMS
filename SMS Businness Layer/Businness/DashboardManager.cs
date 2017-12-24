﻿using SMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SMS_Models.Models.DBModels;

namespace SMS_Businness_Layer.Businness
{
    public class DashboardManager
    {
    
        public static List<Keyvalue> GetStudentGenderRatio()
        {
            List<Keyvalue> lstKeyValues = new List<Keyvalue>();
            try
            {   
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentGenderRatio);
                foreach(DataRow row in objDatable.Rows)
                {
                    lstKeyValues.Add(new Keyvalue() { Key = row["Gender"].ToString(), Value  = Convert.ToInt64(row["Count"]) });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstKeyValues;

        }
        public static List<Keyvalue> GetStudentPaymentsAsPerMonthRatio()
        {
            List<Keyvalue> lstKeyValues = new List<Keyvalue>();
            try
            {
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentPaymentsSumAsPerMonth);
                foreach (DataRow row in objDatable.Rows)
                {
                    lstKeyValues.Add(new Keyvalue() { Key = Convert.ToDateTime(row["Month"]).ToString("MMM yyyy") + " ", Value = Convert.ToInt64(row["Amount"]) });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstKeyValues;

        }


    }
}