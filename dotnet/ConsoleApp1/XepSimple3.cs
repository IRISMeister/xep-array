using System;
using InterSystems.XEP;
using xep.samples;

namespace XepSimpleNamespace
{
    public class XepSimple3
    {

        protected static String pkgName = "xep.samples";
        protected static String schemaName = "xep_samples";
        protected static String className = "DeviceClassListId";
        protected static String classFullName = pkgName + "." + className;

        public static void Main(string[] args)
        {
            // Generate 12 SingleStringSample objects for use as test data
            DeviceClassListId[] sampleArray = DeviceClassListId.generateSampleData(12);

            // EventPersister
            EventPersister xepPersister = PersisterFactory.CreatePersister();

            String host = "127.0.0.1"; // InterSystems IRIS host
            int port = 1972; // InterSystems IRIS Superserver port
            String irisnamespace = "XEP"; // InterSystems IRIS namespace
            String username = "_system"; // Credentials for InterSystems IRIS
            String password = "SYS"; //Credentials for InterSystems IRIS

            xepPersister.Connect(host, port, irisnamespace, username, password); // connect to localhost
            xepPersister.DeleteExtent(classFullName);   // remove old test data
            xepPersister.DeleteClass(classFullName);   // remove old schema
            xepPersister.ImportSchemaFull(classFullName);   // import full schema

            // Event
            Event xepEvent = xepPersister.GetEvent(classFullName);
            /*
            for (int i=0; i < sampleArray.Length; i++) {
                DeviceClassListId sample = sampleArray[i]; 
                xepEvent.Store(sample);
            }
            */
            xepEvent.Store(sampleArray);

            Console.WriteLine("Stored " +sampleArray.Length + " events");


            // EventQuery
            EventQuery<DeviceClassListId> xepQuery = null;
            String sqlQuery = "SELECT * FROM " + schemaName + "." + className + " WHERE %ID BETWEEN ? AND ? order by ID";

            xepQuery = xepEvent.CreateQuery<DeviceClassListId>(sqlQuery);
            xepQuery.AddParameter(3);    // assign value 3 to first SQL parameter
            xepQuery.AddParameter(12);   // assign value 12 to second SQL parameter
            xepQuery.Execute();            // get resultset for IDs between 3 and 12

            // There is no EventQueryIterator in .NET
            DeviceClassListId record = xepQuery.GetNext();
            while (record != null)
            {
                Console.WriteLine(record.deviceName + " "+ record.deviceId);
                for (int i = 0; i < record.arrayfloat.Length; i++)
                {
                    Console.Write("[" + i + "]" + record.arrayfloat[i] + " ");
                }
                Console.WriteLine();
                for (int i = 0; i < record.listECG.Count; i++)
                {
                    Console.Write("[" + i + "]" + record.listECG[i].p1 + "/" + record.listECG[i].p2 + " ");

                }
                Console.WriteLine();

                record = xepQuery.GetNext();
            }

            xepQuery.Close();
            xepEvent.Close();
            xepPersister.Close();

            Console.ReadLine();

        }
    }
}
 