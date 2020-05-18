using Neo4jClient;
using System;

namespace Neo4J
{
    public class ConfigurationManagerNeo4J
    {
        public static GraphClient GetDefaultClient()
        {
            var connectionString = GetDefaultConnectionString();
            var password = "12344321";
            var client = new GraphClient(new Uri(connectionString), "neo4j", password);
            client.Connect();
            return client;
        }

        private static string GetDefaultConnectionString()
        {
            return "http://localhost:7474/db/data";
        }

        private static string GetDefaultDatabaseName()
        {
            return "socialnetworkusersdb";
        }
    }
}
