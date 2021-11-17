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
        protected static String collectionClassName = "ECGListId";
        protected static String classFullName = pkgName + "." + className;
        protected static String collectionClassFullName = pkgName + "." + collectionClassName;

        public static void Main(string[] args)
        {
            Console.WriteLine("Generating test data.");
            DeviceClassListId[] sampleArray = DeviceClassListId.generateSampleData(12);
            Console.WriteLine("Done.");

            // EventPersister
            EventPersister xepPersister = PersisterFactory.CreatePersister();

            String host = "127.0.0.1";      // InterSystems IRIS host
            int port = 1972;                // InterSystems IRIS Superserver port
            String irisnamespace = "XEP";   // InterSystems IRIS namespace
            String username = "_system";    // Credentials for InterSystems IRIS
            String password = "SYS";        //Credentials for InterSystems IRIS

            var sw = new System.Diagnostics.Stopwatch();
            TimeSpan ts;

            xepPersister.Connect(host, port, irisnamespace, username, password); // connect to localhost
            //Console.WriteLine("Deleting extent.");
            //xepPersister.DeleteExtent(classFullName);             // delete old test data
            //xepPersister.DeleteExtent(collectionClassFullName);   // delete old test data

            Console.WriteLine("Deleting schema and its data.");
            xepPersister.DeleteClass(classFullName);            // remove old schema and its data
            xepPersister.DeleteClass(collectionClassFullName);  // remove old schema and its data
            Console.WriteLine("Importing schema.");
            xepPersister.ImportSchemaFull(classFullName);       // import full schema

            // Event
            Event xepEvent = xepPersister.GetEvent(classFullName);
            Console.WriteLine("Saving data.");
            sw.Start();
            /*
            for (int i=0; i < sampleArray.Length; i++) {
                DeviceClassListId sample = sampleArray[i]; 
                xepEvent.Store(sample);
            }
            */
            xepEvent.Store(sampleArray);
            sw.Stop();

            Console.WriteLine("Stored " +sampleArray.Length + " events");
            ts = sw.Elapsed;
            Console.WriteLine($"　{ts}");

            // EventQuery
            EventQuery<DeviceClassListId> xepQuery = null;
            String sqlQuery = "SELECT * FROM " + schemaName + "." + className + " WHERE deviceId=? and fromTS=?";

            sw.Restart();
            xepQuery = xepEvent.CreateQuery<DeviceClassListId>(sqlQuery);
            xepQuery.AddParameter("id3");
            //xepQuery.AddParameter("2020-01-01 00:16:40");
            xepQuery.AddParameter("2020/01/01 0:16:40");
            xepQuery.Execute();

            float ftemp = 0;
            byte b1temp = 0;
            byte b2temp = 0;
            // There is no EventQueryIterator in .NET
            DeviceClassListId record = xepQuery.GetNext();
            while (record != null)
            {
                Console.WriteLine(record.deviceName + " "+ record.deviceId+" "+record.position);
                for (int i = 0; i < record.arrayfloat.Length; i++)
                {
                    Console.Write("[" + i + "]" + record.arrayfloat[i] + " ");
                    ftemp += record.arrayfloat[i];
                    // comparing with data source
                    if (record.arrayfloat[i] != sampleArray[record.position].arrayfloat[i]) { Console.Write("data mismatch!!! Abort."); Environment.Exit(1); }
                }
                Console.WriteLine();
                for (int i = 0; i < record.listECG.Count; i++)
                {
                    Console.Write("[" + i + "]" + record.listECG[i].p1 + "/" + record.listECG[i].p2 + " ");
                    b1temp += record.listECG[i].p1;
                    b2temp += record.listECG[i].p2;
                    // comparing with data source
                    if (record.listECG[i].p1 != sampleArray[record.position].listECG[i].p1) { Console.Write("data mismatch!!! Abort."); Environment.Exit(1); }
                    if (record.listECG[i].p2 != sampleArray[record.position].listECG[i].p2) { Console.Write("data mismatch!!! Abort."); Environment.Exit(1); }
                }
                Console.WriteLine();

                record = xepQuery.GetNext();
            }

            xepQuery.Close();
            ts = sw.Elapsed;
            Console.WriteLine($"　{ts}");

            xepEvent.Close();
            xepPersister.Close();

            Console.WriteLine("Hit any key");
            Console.ReadLine();

        }
    }
}
 