﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;
using ThinkerThings.Customers.Service.Infra.Options;

namespace ThinkerThings.Customers.Service.Infra.Repositories
{
    public abstract class Repository
    {
        private readonly IOptions<ConnectionStringOptions> _connectionString;

        public Repository(ILogger logger, IOptions<ConnectionStringOptions> connectionString)
        {
            Logger = logger;
            _connectionString = connectionString;
        }

        protected ILogger Logger { get; }
        protected string ConnectionString => _connectionString.Value.MySqlConnection;

        protected IDbConnection GetConnection() => new MySqlConnection(ConnectionString);
    }
}