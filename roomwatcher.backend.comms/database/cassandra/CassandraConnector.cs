using Cassandra;
using roomwatcher.bo.sensors.interfaces;
using roomwatcher.bo.sensors.ir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roomwatcher.backend.comms.database.cassandra
{
    public class CassandraConnector
    {
        public delegate void CassandraEventSaved(object source, CassandraEventArgs e);
        public event CassandraEventSaved OnCassandraEventSaved;
        protected ISession session;
        protected PreparedQueries preparedQ;
        private Cluster cluster;
        private string[] SERVER_POINTS = new string[] { "192.168.139.133" };
        private const string KEYSPACE = "sensors_data";
        private void StartConnection()
        {
            // Create a cluster instance the cassandra node 
            cluster = Cluster.Builder()
              .AddContactPoints(SERVER_POINTS)
                //.WithPort(9160)
              .Build();
            // Create connections to the nodes using a keyspace
            session = cluster.Connect(KEYSPACE);

            // Prepare most frequent queries
            preparedQ = new PreparedQueries((Session)session);

        }

        private void StopConnection()
        {
            cluster.Shutdown();
            cluster.Dispose();
        }

        public void InsertSensorData(ISensor data)
        {
            Type sensorType = data.GetType();
            Statement statement = null;
            if (sensorType == typeof(IRDTO))
            {
                IRDTO irData = (IRDTO)data;
                statement = preparedQ.RecordIR.Bind(DateTime.Now.ToString(), irData.Value);
            }
            
            // Execute the bound statement with the provided parameters
            StartConnection();
            session.Execute(statement);
            StopConnection();
            if (OnCassandraEventSaved != null)
                OnCassandraEventSaved(this, new CassandraEventArgs(data));
        }

        protected class PreparedQueries
        {
            public PreparedStatement RecordIR;
            public PreparedQueries(Session session)
            {
                CassandraConnector connector = new CassandraConnector();
                RecordIR = session.Prepare(SensorQueries.IR);
            }
        }

        protected class SensorQueries
        {
            public const string IR = "INSERT INTO ir (id, datetime, value) VALUES (uuid(), ?, ?);";
        }
    }
}
