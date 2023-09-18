using System;
using System.Data;
using System.IO;
using System.Threading;
using Mono.Data.Sqlite;
using NUnit.Framework;
using UnityEngine;

// Disable warning about never assigned fields. The values are injected.
#pragma warning disable CS0649

namespace SQLiteUnity.Tests
{
    public class SQLiteTests
    {
        [Test]
        public void CreateTable_InsertValue_ReadValue()
        {
            string dbPath = $"{Application.persistentDataPath}/SQLiteTestDatabase.db";
            Debug.Log($"SQLite database path: '{dbPath}'");

            // Delete old database
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);

                // Wait for file to be deleted
                if (File.Exists(dbPath))
                {
                    Thread.Sleep(100);
                }
            }

            // Open connection, auto close via using statement
            string connectionString = $"URI=file:{dbPath}";
            using IDbConnection dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();

            // Create table
            ExecuteNonQuery(dbConnection, "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, name STRING, age INTEGER)");

            // Insert values in table
            ExecuteNonQuery(dbConnection, "INSERT INTO my_table (id, name, age) VALUES (1, 'Alice', 42)");
            ExecuteNonQuery(dbConnection, "INSERT INTO my_table (id, name, age) VALUES (2, 'Bob', 33)");

            // Read and all values in table
            IDataReader reader = ExecuteQuery(dbConnection, "SELECT * FROM my_table");

            // Check reader found the expected values
            int recordIndex = 0;
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int age = reader.GetInt32(2);
                Debug.Log($"DB reader result (id: {id}, name: {name}, age: {age})");

                if (recordIndex == 0)
                {
                    AssertRecord(id, 1, name, "Alice", age, 42);
                }
                else if (recordIndex == 1)
                {
                    AssertRecord(id, 2, name, "Bob", age, 33);
                }
                else
                {
                    throw new Exception("Unexpected number of rows returned by DB reader.");
                }

                recordIndex++;
            }
        }

        private void AssertRecord(
            int actualId,
            int expectedId,
            string actualName,
            string expectedName,
            int actualAge,
            int expectedAge)
        {
            Assert.AreEqual(expectedId, actualId);
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedAge, actualAge);
        }

        private static Int32 ExecuteNonQuery(IDbConnection dbConnection, string command)
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = command;
            return dbCommand.ExecuteNonQuery();
        }

        private static IDataReader ExecuteQuery(IDbConnection dbConnection, string query)
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            IDataReader reader;
            dbCommand.CommandText = query;
            reader = dbCommand.ExecuteReader();
            return reader;
        }
    }
}
