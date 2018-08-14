using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Messenger.Helpers;
using SQLite;

namespace Messenger.Repository
{
    public class GenericRepository<T> where T: new()
    {
        #region Properties

        public SQLiteConnection DbConnection { get; set; }

        #endregion

        public GenericRepository()
        {
            DbConnection = new SQLiteConnection(ApplicationHelper.DatabasePath);
        }

        #region Get operations

        /// <summary>
        /// Gets the first or default record that matches the specified predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The where clause used to filter records.</param>
        /// <returns>The first record that matched the predicate.</returns>
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            if (predicate == null) return default(T);

            return DbConnection.Table<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets all the records that match the specified predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The where clause used to filter records.</param>
        /// <returns>A list of records that matched the predicate.</returns>
        public List<T> Where(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) return null;

            return DbConnection.Table<T>().Where(predicate).ToList();
        }

        /// <summary>
        /// Gets all the records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A list of all the records.</returns>
        public List<T> GetAll()
        {
            return DbConnection.Table<T>().ToList();
        }

        #endregion

        #region Insert operations

        /// <summary>
        /// Inserts a record into the database.
        /// </summary>
        /// <param name="record">Record to be save.</param>
        /// <returns>Primary Key of newly inserted item.</returns>
        public int Insert(T record)
        {
            return DbConnection.Insert(record);
        }

        /// <summary>
        /// Inserts records into the database.
        /// </summary>
        /// <param name="records">Records to be save.</param>
        /// <returns>Number of inserted rows.</returns>
        public int InsertAll(List<T> records)
        {
            var result = 0;

            if (records != null && records.Count > 0)
            {
                result = DbConnection.InsertAll(records);
            }

            return result;
        }

        #endregion

        #region Update Operations

        /// <summary>
        /// Updates the specified record.
        /// </summary>
        /// <param name="record">The record to be updated.</param>
        /// <returns>True if the record updated successfully.</returns>
        public bool Update(T record)
        {
            if (record == null) return false;

            var rowCount = DbConnection.Update(record);

            return rowCount > 0;
        }

        /// <summary>
        /// Updates the specified records.
        /// </summary>
        /// <param name="records">The records to be updated</param>
        /// <returns>Number of records updated</returns>
        public int UpdateAll(List<T> records)
        {
            var result = 0;

            if (records != null)
            {
                result = DbConnection.UpdateAll(records);
            }

            return result;
        }

        #endregion

        #region Delete operations

        /// <summary>
        /// Deletes a record from the database.
        /// </summary>
        /// <param name="record">The record to be deleted.</param>
        /// <returns>True if the record was successfully deleted.</returns>
        public bool Delete(T record)
        {
            var result = false;

            if (record != null)
            {
                result = DbConnection.Delete(record) == 1;
            }

            return result;
        }

        #endregion
    }
}
