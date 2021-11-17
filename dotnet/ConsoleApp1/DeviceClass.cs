using System;
using Serialized = InterSystems.XEP.Attributes.Serialized;
using Index = InterSystems.XEP.Attributes.Index;
using IndexType = InterSystems.XEP.Attributes.IndexType;


namespace xep.samples
{
    [Index(name = "idx1", fields = new string[] { "deviceId" }, type = IndexType.simple)]
    public class DeviceClass
    {

        public String deviceName;
        public String deviceId;
        public double number1;
        public float number2;
        public float number3;
        public float number4;
        public float number5;
        public float[] arrayfloat;
        [Serialized]
        public ECG[] arrayECG;
        //[Serialized]
        //public ECG singleECG;

        public DeviceClass() { }

        public static DeviceClass[] generateSampleData(int count)
        {
            Random rnd = new Random();
            DeviceClass[] s = new DeviceClass[count];
            for (int i = 0; i < count; i++) {
                s[i] = new DeviceClass();
                s[i].deviceName = "deviceName" + i;
                s[i].deviceId = "id" + i;
                s[i].number1 = (float)12345;
                s[i].number2 = (float)1;
                s[i].number3 = (float)2;
                s[i].number4 = (float)3;
                s[i].number5 = (float)4;
                s[i].arrayfloat = new float[10];
                for (int j = 0; j < s[i].arrayfloat.Length; j++) {
                    s[i].arrayfloat[j] = (float)rnd.NextDouble();
                }
                s[i].arrayECG = ECG.generateECGData(rnd,20);
                //s[i].singleECG = new ECG();
                //s[i].singleECG.p1 = 100;
            }
            return s;
        }
    }
}