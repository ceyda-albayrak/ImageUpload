﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Transactions;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using Z.BulkOperations;
using Z.Dapper.Plus;

using Z.Dapper.Plus;

namespace Core.DataAccess.Dapper
{

    public class DapperRepositoryBase<T> : IDapperRepository<T> where T : class, new()

    {

    public DapperRepositoryBase()
    {
        DapperPlusManager.Entity<T>().Table($"{typeof(T).Name}s");
    }

    public void Add(T entity)
    {
      using(TransactionScope scope=new TransactionScope())
          try
          {
              using (var connection = new SqlConnection(Db.connection))
              {

                  connection.BulkInsert(entity);
                  scope.Complete();

              }
          }


          catch (Exception)
          {
             scope.Dispose();
          }
    }
    

    public void Delete(T entity)
    {
        using (var connection = new SqlConnection(Db.connection))
        {
            connection.BulkDelete(entity);
        }
    }

    public T Get(int id)
    {
        using (var connection = new SqlConnection(Db.connection))
        {
            var get= connection.Get<T>(id);
            return get;
        }
    }

    public List<T> GetAll()
    {
        using (var connection = new SqlConnection(Db.connection))
        {
           

            var getall = connection.GetAll<T>().ToList();
            return getall;
        }
    }

    public void Update(T entity)
    {
        using (var connection = new SqlConnection(Db.connection))
        {
            connection.BulkUpdate(entity);
        }
    }


    private IEnumerable<string> GetColumns()
    {
        return typeof(T)
            .GetProperties()
            .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
            .Select(e => e.Name);
    }
    }
}
