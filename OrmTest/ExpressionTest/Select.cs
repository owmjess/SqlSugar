﻿using OrmTest.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrmTest.ExpressionTest
{
    public class Select
    {
        internal static void Init()
        {
            DateTime b = DateTime.Now;
            int count = 1;
            for (int i = 0; i < count; i++)
            {
                single();
                Multiple();
                singleDynamic();
                MultipleDynamic();
            }
            DateTime e = DateTime.Now;
            Console.WriteLine("Count: " + count + "\r\nTime:  " + (e - b).TotalSeconds + " Seconds ");
        }

        private static void Multiple()
        {
            Expression<Func<Student, School, object>> exp = (it, school) => new Student() { Name = "a", Id = it.Id, SchoolId = school.Id,TestId=it.Id+1 };
            ExpressionContext expContext = new ExpressionContext(exp, ResolveExpressType.SelectMultiple);
            expContext.Resolve();
            var selectorValue = expContext.Result.GetString();
            var pars = expContext.Parameters;
            if (selectorValue.Trim() != " @constant1 AS Name , it.Id AS Id , school.Id AS SchoolId ".Trim())
            {
                throw new Exception("Multiple Error");
            }
        }
        private static void MultipleDynamic()
        {
            Expression<Func<Student, School, object>> exp = (it, school) => new { Name = "a", Id = it.Id / 2, SchoolId = school.Id };
            ExpressionContext expContext = new ExpressionContext(exp, ResolveExpressType.SelectMultiple);
            expContext.Resolve();
            var selectorValue = expContext.Result.GetString();
            var pars = expContext.Parameters;
        }
        private static void single()
        {
            int p = 1;
            Expression<Func<Student, object>> exp = it => new Student() { Name = "a", Id = it.Id, SchoolId = p };
            ExpressionContext expContext = new ExpressionContext(exp, ResolveExpressType.SelectSingle);
            expContext.Resolve();
            var selectorValue = expContext.Result.GetString();
            var pars = expContext.Parameters;
        }

        private static void singleDynamic()
        {
            string a = "a";
            Expression<Func<Student, object>> exp = it => new { x = it.Id, shoolid = 1, name = a };
            ExpressionContext expContext = new ExpressionContext(exp, ResolveExpressType.SelectSingle);
            expContext.Resolve();
            var selectorValue = expContext.Result.GetString();
            var pars = expContext.Parameters;
        }
    }
}