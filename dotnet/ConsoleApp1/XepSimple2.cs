using System;
using InterSystems.XEP;
using xep.samples;

namespace XepSimpleNamespace
{
    public class XepSimple2
    {

        protected static String pkgName = "xep.samples";
        protected static String schemaName = "xep_samples";
        protected static String className = "DeviceClassList";
        protected static String classFullName = pkgName + "." + className;

        public static void Main(string[] args)
        {
            Console.WriteLine("Generating test data.");
            DeviceClassList[] sampleArray = DeviceClassList.generateSampleData(12);
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
            Console.WriteLine("Deleting schema and its data.");
            xepPersister.DeleteClass(classFullName);    // remove old schema and its data
            Console.WriteLine("Importing schema.");
            xepPersister.ImportSchema(classFullName);   // import flat schema

            // Event
            Event xepEvent = xepPersister.GetEvent(classFullName, Event.INDEX_MODE_SYNC);

            Console.WriteLine("Saving data.");
            sw.Start();
            /*
            for (int i=0; i < sampleArray.Length; i++) {
                DeviceClassList sample = sampleArray[i]; 
                xepEvent.Store(sample);
            }
            */
            xepEvent.Store(sampleArray);
            sw.Stop();

            Console.WriteLine("Stored " +sampleArray.Length + " events");
            ts = sw.Elapsed;
            Console.WriteLine($"　{ts}");

            // EventQuery
            EventQuery<DeviceClassList> xepQuery = null;
            String sqlQuery = "SELECT * FROM " + schemaName + "." + className + " WHERE %ID BETWEEN ? AND ?";

            sw.Restart();
            xepQuery = xepEvent.CreateQuery<DeviceClassList>(sqlQuery);
            xepQuery.AddParameter(3);    // assign value 3 to first SQL parameter
            xepQuery.AddParameter(12);   // assign value 12 to second SQL parameter
            xepQuery.Execute();          // get resultset for IDs between 3 and 12

            // There is no EventQueryIterator in .NET
            DeviceClassList record = xepQuery.GetNext();
            while (record != null)
            {
                Console.WriteLine("deviceName:"+record.deviceName+" deviceId:"+record.deviceId+" position:"+record.position+" number1:"+record.number1);
                // comparing with data source
                if (record.number1 != sampleArray[record.position].number1) { Console.WriteLine("data mismatch!!! Abort."); Environment.Exit(1); }
                if (record.number2 != sampleArray[record.position].number2) { Console.WriteLine("data mismatch!!! Abort."); Environment.Exit(1); }

                for (int i = 0; i < record.arrayfloat.Length; i++)
                {
                    Console.Write("[" + i + "]" + record.arrayfloat[i] + " ");
                    // comparing with data source
                    if (record.arrayfloat[i] != sampleArray[record.position].arrayfloat[i]) { Console.Write("data mismatch!!! Abort."); Environment.Exit(1); }
                }
                Console.WriteLine();
                for (int i = 0; i < record.listECG.Count; i++)
                {
                    Console.Write("[" + i + "]" + record.listECG[i].p1 + "/" + record.listECG[i].p2 + " ");
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
 