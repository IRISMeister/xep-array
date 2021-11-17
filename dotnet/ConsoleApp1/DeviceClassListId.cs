using System;
using System.Collections.Generic;
using InterSystems.XEP.Attributes;

namespace xep.samples
{
    // Cannot add the third index...
    [Index(name = "idx1", fields = new string[] { "deviceId" }, type = IndexType.simple),
     Index(name = "idx2", fields = new string[] { "fromTS" }, type = IndexType.simple)]
    public class DeviceClassListId
    {
        public long position;
        public String deviceName;
        public String deviceId;
        // There is no limit of the number of fields in a composite IdKey, but the fields must be String, int, or long, or their corresponding System types
        // DateTimeはidkeyの一部を構成できないので、Stringで保存。
        // Stringで保持するのも、日時表現のゆらぎや、where条件下で不利なので避けたいところ。
        // xepQuery.GetNext()で例外発生。理由不明。
        // +		$exception	{"オブジェクト参照がオブジェクト インスタンスに設定されていません。"}	InterSystems.XEP.XEPException
        public String fromTS;
        public String toTS;
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
            int NumOfSamplesPerRecord=5;
            Random rnd = new Random();
            DateTime baseTS = new DateTime(2020, 1, 1, 0, 0, 0);
            DeviceClassListId[] s = new DeviceClassListId[NumOfSamplesPerRecord*count];
            for (int dev = 0; dev < NumOfSamplesPerRecord; dev++) {
                for (int i = 0; i < count; i++) {
                    s[dev*count+i]= new DeviceClassListId();
                    s[dev*count+i].position = dev * count + i;
                    s[dev*count+i].deviceName = "deviceName" + dev;
                    s[dev*count+i].deviceId = "id" + dev;
                    s[dev*count+i].fromTS = baseTS.AddSeconds(i * 100).ToString();
                    s[dev*count+i].toTS = baseTS.AddSeconds(i * 100 + 99).ToString();
                    s[dev*count+i].number1 = (float)12345;
                    s[dev*count+i].number2 = (float)1;
                    s[dev*count+i].number3 = (float)2;
                    s[dev*count+i].number4 = (float)3;
                    s[dev*count+i].number5 = (float)4;
                    s[dev*count+i].arrayfloat = new float[10];
                    for (int j = 0; j < s[dev*count+i].arrayfloat.Length; j++) {
                        s[dev*count+i].arrayfloat[j] = (float)rnd.NextDouble();
                    }
                    s[dev*count+i].listECG = ECGListId.generateSampleData(rnd, 400, s[dev * count + i].deviceId, s[dev*count+i].position);
                }
            }
            return s;
        }
    }
}