using System;
using System.Collections.Generic;
using InterSystems.XEP.Attributes;

namespace xep.samples
{
    [Index(name = "idkey", fields = new string[] { "deviceId", "fromTS","id" }, type = IndexType.idkey)]
    public class ECGListId
    {
        public long id;
        public String deviceId;
        public String fromTS;
        public byte p1;
        public byte p2;
        public byte p3;
        public byte p4;
        public byte[] p5;
        public byte[] p6;
        public byte p7;
        public byte p8;
        public byte p9 = 0;
        public byte p10 = 0;
        public byte p11 = 0;
        public ushort seq;

        public ECGListId() { }

        public static List<ECGListId> generateSampleData(Random rnd, int count, String deviceId, String fromTS)
        {
            List<ECGListId> s = new List<ECGListId>();
            for (int i = 0; i < count; i++)
            {
                ECGListId e = new ECGListId();
                e.id = i + 1;
                e.deviceId = deviceId;
                e.fromTS = fromTS;
                e.p1 = (byte)(rnd.Next(50,70));
                e.p2 = (byte)i;
                e.p3 = 2;
                e.p4 = (byte)(rnd.Next(1,255));
                e.p5 = new byte[5];
                e.p6 = new byte[5];
                for (int j = 0; j < e.p5.Length; j++)
                {
                    e.p5[j] = (byte)rnd.Next(255);
                    e.p6[j] = (byte)rnd.Next(255);
                }
                e.p7 = (byte)rnd.Next(0,255);
                e.p8 = (byte)(rnd.Next(1,255));
                e.seq = (ushort)rnd.Next(0,255);

                s.Add(e);

            }
            return s;
        }
    }
}