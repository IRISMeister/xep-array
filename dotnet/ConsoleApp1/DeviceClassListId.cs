using System;
using System.Collections.Generic;
using InterSystems.XEP.Attributes;

namespace xep.samples
{
    [Index(name = "idx1", fields = new string[] { "deviceId" }, type = IndexType.simple)]
    public class DeviceClassListId
    {

        public String deviceName;
        public String deviceId;
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
            Random rnd = new Random();
            DeviceClassListId[] s = new DeviceClassListId[count];
            for (int i = 0; i < count; i++)
            {
                s[i] = new DeviceClassListId();
                s[i].deviceName = "deviceName" + i;
                s[i].deviceId = "id" + i;
                s[i].number1 = (float)12345;
                s[i].number2 = (float)1;
                s[i].number3 = (float)2;
                s[i].number4 = (float)3;
                s[i].number5 = (float)4;
                s[i].arrayfloat = new float[10];
                for (int j = 0; j < s[i].arrayfloat.Length; j++)
                {
                    s[i].arrayfloat[j] = (float)rnd.NextDouble();
                }
                s[i].listECG = ECGListId.generateSampleData(rnd, 20, s[i].deviceId);
            }
            return s;
        }
    }
}