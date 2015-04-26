using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mono.Data;
using Mono.Data.Sqlite;
using Mono.Data.Tds;
using Android.Database.Sqlite;
using System.IO;
using Android.Database;


namespace DatabaseApp
{
	public class MyDatabase
	{
		private SQLiteDatabase dbMain;
		public SQLiteDatabase DBMain
		{
			get{ return dbMain;}
			set{dbMain = value; }
		}
		public string Message
		{
			get{return sMessage; }
			set{sMessage = value; }
		}
		private string sMessage;
		private bool DbIsAvailable;
		private string myFileName;
		public string MyFileName
		{
			get{return myFileName; }
			set{ myFileName = value;}
		}

//		string db=System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
//		string dbpath=Path.Combine(db,dbname);
		private string sSQLQuery;
		public bool DatabaseAvailable {
			get{ return DbIsAvailable;}
			set{ DbIsAvailable = value;}
		}
		public MyDatabase()
		{
			sMessage = "";
			DbIsAvailable = false;
		}
		public MyDatabase(string dbname)
		{
			try{
				sMessage="";
				DbIsAvailable=false;
				CreateDatabase(dbname);
				MyFileName=dbname;
			}
			catch(SQLiteException ex) {
				sMessage = ex.Message;
			}
		}

		public void UpdateDatabase(int Id,string nName,int nAge,string ncountry)
		{
			string db=System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			string dbpath=Path.Combine(db,myFileName);
			bool isExist=File.Exists(dbpath);

			dbMain=SQLiteDatabase.OpenOrCreateDatabase(dbpath,null);
			sSQLQuery = "UPDATE MyTable SET Name='"+nName+"',Age='"+nAge+"',Country='"+ncountry+"' WHERE _id='"+Id+"';  ";
			dbMain.ExecSQL (sSQLQuery);
			sMessage="This record is updated";
		}

		public void CreateDatabase (string dbname)
		{
			try{
				string db=System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				string dbpath=Path.Combine(db,dbname);
				bool isExist=File.Exists(dbpath);
				if(!isExist)
				{
					dbMain=SQLiteDatabase.OpenOrCreateDatabase(dbpath,null);
					sSQLQuery = "CREATE TABLE IF NOT EXISTS " +
					            "MyTable " +
					            "(_id INTEGER PRIMARY KEY AUTOINCREMENT,Name VARCHAR,Age INT,Country VARCHAR);";
					dbMain.ExecSQL(sSQLQuery);
					sMessage="New Database has been created";
				


				}
				else
				{
					var opendb=SQLiteDatabase.OpenDatabase(dbpath,null,DatabaseOpenFlags.OpenReadwrite);
					DBMain=opendb;
					sMessage="Database is opened";
				}
				DbIsAvailable=true;
			}
			catch(SQLiteException ex) {
				sMessage = ex.Message;
			}
		}

		public void DeleteRecord(int iId)
		{
			try{
				dbMain=DBMain;
				sSQLQuery="DELETE FROM MyTable WHERE _id='"+iId+"';";
				dbMain.ExecSQL(sSQLQuery);
				sMessage="Record is deleted";

			}
			catch(SQLiteException ex) {
				sMessage = ex.Message;
			}
		}

		public ICursor GetRecord(string sColumn,string value)
		{
			ICursor c = null;
			try{
			sSQLQuery="select * from MyTable where"+sColumn+" like '"+value+" %';";
				c = DBMain.RawQuery (sSQLQuery, null);
				if (!(c != null)) {
				sMessage = "Record not found";
			}
			}
			catch(SQLException ex) {
				sMessage = ex.Message;
			}
			return c;
		}

		public void AddRecord(string sName, int iAge, string sCountry)
		{
			try{
				string db=System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				string dbpath=Path.Combine(db,myFileName);
				bool isExist=File.Exists(dbpath);
			
					dbMain=SQLiteDatabase.OpenOrCreateDatabase(dbpath,null);
					sSQLQuery = "CREATE TABLE IF NOT EXISTS " +
					            "MyTable " +
					            "(_id INTEGER PRIMARY KEY AUTOINCREMENT,Name VARCHAR,Age INT,Country VARCHAR);";
				
				string	sSQLQuery2="INSERT INTO MyTable (Name,Age,Country)" +
				           "VALUES('" + sName + "'," + iAge + ",'" + sCountry + "');";
				dbMain.ExecSQL(sSQLQuery);
				dbMain.ExecSQL(sSQLQuery2);
				sMessage = "Record is saved.";


			}
			catch(SQLiteException ex) {
				sMessage = ex.Message;
			}
		}


	}
}

