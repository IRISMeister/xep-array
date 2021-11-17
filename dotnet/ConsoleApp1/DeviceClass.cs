using System;
using Serialized = InterSystems.XEP.Attributes.Serialized;
using Index = InterSystems.XEP.Attributes.Index;
using IndexType = InterSystems.XEP.Attributes.IndexType;


namespace xep.samples
{
    [Index(name = "idx1", fields = new string[] { "deviceId" }, type = IndexType.simple)]
    public class DeviceClass
    {
        public long position;
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
            int NumOfDevice=5;
            int NumOfSamplesPerDev=20;
            Random rnd = new Random();
            DeviceClass[] s = new DeviceClass[NumOfDevice*count];
            for (int dev = 0; dev < NumOfDevice; dev++) {
                for (int i = 0; i < count; i++) {
                    s[dev*count+i] = new DeviceClass();
                    s[dev*count+i].position = dev * count + i;
                    s[dev*count+i].deviceName = "deviceName" + dev;
                    s[dev*count+i].deviceId = "id" + dev;
                    s[dev*count+i].number1 = (double)12345.1;
                    s[dev*count+i].number2 = (float)1.2;
                    s[dev*count+i].number3 = (float)2;
                    s[dev*count+i].number4 = (float)3;
                    s[dev*count+i].number5 = (float)4;
                    s[dev*count+i].arrayfloat = new float[10];
                    for (int j = 0; j < s[dev*count+i].arrayfloat.Length; j++) {
                        s[dev*count+i].arrayfloat[j] = (float)rnd.NextDouble();
                    }
                    s[dev*count+i].arrayECG = ECG.generateECGData(rnd,NumOfSamplesPerDev);
                    //s[dev*count+i].singleECG = new ECG();
                    //s[dev*count+i].singleECG.p1 = 100;
                }
            }
            return s;
        }
    }
}