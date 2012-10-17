﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MVC4ServicesBook.Common;
using MVC4ServicesBook.Data.Model;
using NHibernate;
using NHibernate.Linq;

namespace MVC4ServicesBook.Data.SqlServer
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;
        private readonly ISqlCommandFactory _sqlCommandFactory;
        private readonly IDatabaseValueParser _valueParser;

        public UserRepository(ISession session, ISqlCommandFactory sqlCommandFactory, IDatabaseValueParser valueParser)
        {
            _session = session;
            _sqlCommandFactory = sqlCommandFactory;
            _valueParser = valueParser;
        }

        public IQueryable<User> AllUsers()
        {
            return _session.Query<User>();
        }

        public User GetUser(Guid userId)
        {
            return _session.Get<User>(userId);
        }

        public void SaveUser(Guid userId, string firstname, string lastname)
        {
            using(var command = _sqlCommandFactory.GetCommand())
            {
                command.CommandText = "dbo.SaveUser";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@lastname", lastname);

                command.ExecuteNonQuery();
            }
        }
    }
}
