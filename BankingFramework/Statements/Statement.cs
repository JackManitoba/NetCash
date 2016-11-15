using BankingFramework.DatabaseManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BankingFramework.Statements
{
    public class Statement
    {
        private List<List<string>> _listOfTransactions;
        private DatabaseManager _databaseManager;

        public Statement(string accountNumber)
        {
            _listOfTransactions = new List<List<string>>();
            _databaseManager = DatabaseManager.GetInstance();
            populateListOfTransactions(accountNumber);            
        }

        public List<List<string>> getListOfTransactions()
        {
            return _listOfTransactions;
        }

        public void populateListOfTransactions(string accountNumber)
        {
            _listOfTransactions = _databaseManager.PopulateStatement(accountNumber);
        }
    }
}
