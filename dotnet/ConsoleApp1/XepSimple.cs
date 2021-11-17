﻿using System;
using InterSystems.XEP;
using xep.samples;

namespace XepSimpleNamespace
{
    public class XepSimple
    {

        protected static String pkgName = "xep.samples";
        protected static String schemaName = "xep_samples";
        protected static String className = "DeviceClass";
        protected static String classFullName = pkgName + "." + className;

        public static void Main(string[] args)
        {
            // Generate 12 SingleStringSample objects for use as test data
            DeviceClass[] sampleArray = DeviceClass.generateSampleData(12);

            // EventPersister
            EventPersister xepPersister = PersisterFactory.CreatePersister();

            String host = "127.0.0.1"; // InterSystems IRIS host
            int port = 1972; // InterSystems IRIS Superserver port
            String irisnamespace = "XEP"; // InterSystems IRIS namespace
            String username = "_system"; // Credentials for InterSystems IRIS
            String password = "SYS"; //Credentials for InterSystems IRIS

            xepPersister.Connect(host, port, irisnamespace, username, password); // connect to localhost
            Console.WriteLine("Deleting schema and its data.");
            xepPersister.DeleteClass(classFullName);    // remove old schema and its data
            Console.WriteLine("Importing schema.");
            xepPersister.ImportSchema(classFullName);   // import flat schema

            // Event
            Event xepEvent = xepPersister.GetEvent(classFullName);

            /* 
             * Arrayをserialized指定すると下記のエラーが出る。
             *  [Serialized]
             *  public ECG[] arrayECG;
             * 
             * Singel valueだと成功するので、ECG内で使用している型は問題無いものと判断する。ArrayがNG？
             *  [Serialized]
             *  public ECG singleECG;
             *  
             * "型 'InterSystems.XEP.Metadata.PrimitiveSchema' のオブジェクトを型 'InterSystems.XEP.Metadata.ArraySchema' にキャストできません。"
             * StackTrace	
             * "場所 InterSystems.XEP.Metadata.SerialSchema.Write(Object obj, IRISListBuilder vList, EventPersister persister)\r\n   
             * 場所 InterSystems.XEP.Metadata.PersistentSchema.Write(Object obj, IRISListBuilder vList, EventPersister persister, Boolean isArray)\r\n   
             * 場所 InterSystems.XEP.EventPersister.writeObject(Object obj, PersistentSchema schema, Int32 indexMode)\r\n   
             * 場所 InterSystems.XEP.Event.Store(Object obj)\r\n   
             * 場所 XepSimpleNamespace.XepSimple.Main(String[] args) 
             * 場所 C:\\Users\\iwamoto\\Source\\repos\\ConsoleApp1\\ConsoleApp1\\XepSimple.cs:行 50"	string
             * TargetSite	{Void Write(System.Object, InterSystems.Data.IRISClient.List.IRISListBuilder, InterSystems.XEP.EventPersister)}	System.Reflection.MethodBase {System.Reflection.RuntimeMethodInfo}
            */
            for (int i=0; i < sampleArray.Length; i++) {
                DeviceClass sample = sampleArray[i]; 
                xepEvent.Store(sample);
            }
            //xepEvent.Store(sampleArray);

            Console.WriteLine("Stored " +sampleArray.Length + " events");


            // EventQuery
            EventQuery<DeviceClass> xepQuery = null;
            String sqlQuery = "SELECT * FROM " + schemaName + "." + className + " WHERE %ID BETWEEN ? AND ?";

            xepQuery = xepEvent.CreateQuery<DeviceClass>(sqlQuery);
            xepQuery.AddParameter(3);    // assign value 3 to first SQL parameter
            xepQuery.AddParameter(12);   // assign value 12 to second SQL parameter
            xepQuery.Execute();            // get resultset for IDs between 3 and 12

            // There is no EventQueryIterator in .NET
            DeviceClass record = xepQuery.GetNext();
            while (record != null)
            {
                Console.WriteLine(record.deviceName + " "+ record.deviceId);
                for (int i = 0; i < record.arrayfloat.Length; i++)
                {
                    Console.Write("[" + i + "]" + record.arrayfloat[i] + " ");
                }
                Console.WriteLine();

                //Console.Write("[" + 0 + "]" + record.singleECG.p1 + " ");
                record = xepQuery.GetNext();
            }

            xepQuery.Close();
            xepEvent.Close();
            xepPersister.Close();
        } // end main()
    } // end class xepSimple
}
 