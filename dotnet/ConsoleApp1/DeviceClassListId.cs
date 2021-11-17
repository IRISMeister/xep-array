using System;
using System.Collections.Generic;
using InterSystems.XEP.Attributes;

namespace xep.samples
{
    [Index(name = "idx1", fields = new string[] { "deviceId" }, type = IndexType.simple),
     Index(name = "idx2", fields = new string[] { "fromTS" }, type = IndexType.simple),
     Index(name = "idx3", fields = new string[] { "toTS" }, type = IndexType.simple)
    ]
    public class DeviceClassListId
    {
        public long position;
        public String deviceName;
        public String deviceId;
        public DateTime fromTS;
        public DateTime toTS;
        public double number1;
        public float number2;
        public float number3;
        public float number4;
        public float number5;
        public float[] arrayfloat;
        public List<ECGListId> listECG;

        public DeviceClassListId() { }

        public static DeviceClassListId[] generateSampleData(int count)
        {
            int NumOfDevice=5;
            int NumOfSamplesPerDev=20;
            Random rnd = new Random();
            DateTime baseTS = new DateTime(2020, 1, 1, 0, 0, 0);
            DeviceClassListId[] s = new DeviceClassListId[NumOfDevice*count];
            for (int dev = 0; dev < NumOfDevice; dev++) {
                for (int i = 0; i < count; i++) {
                    s[dev*count+i] = new DeviceClassListId();
                    s[dev*count+i].position = dev * count + i;
                    s[dev*count+i].deviceName = "deviceName" + dev;
                    s[dev*count+i].deviceId = "id" + dev;
                    s[dev*count+i].fromTS = baseTS.AddSeconds(i * 100);
                    s[dev*count+i].toTS = baseTS.AddSeconds(i * 100 + 99);
                    s[dev*count+i].number1 = (double)12345.1;
                    s[dev*count+i].number2 = (float)1.2;
                    s[dev*count+i].number3 = (float)2;
                    s[dev*count+i].number4 = (float)3;
                    s[dev*count+i].number5 = (float)4;
                    s[dev*count+i].arrayfloat = new float[10];
                    for (int j = 0; j < s[dev*count+i].arrayfloat.Length; j++) {
                        s[dev*count+i].arrayfloat[j] = (float)rnd.NextDouble();
                    }
                    s[dev*count+i].listECG = ECGListId.generateECGData(rnd,NumOfSamplesPerDev,s[dev*count+i].deviceId,s[dev*count+i].position);
                }
            }
            return s;
        }
    }
}