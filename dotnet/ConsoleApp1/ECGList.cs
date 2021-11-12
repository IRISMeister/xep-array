using System;
using System.Collections.Generic;

namespace xep.samples
{
    public class ECGList
    {

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

        public ECGList() { }

        public static List<ECGList> generateSampleData(Random rnd, int count)
        {
            List<ECGList> s = new List<ECGList>();
            for (int i = 0; i < count; i++)
            {
                ECGList element = new ECGList();
                element.p1 = (byte)(rnd.Next(50,70));
                element.p2 = (byte)i;
                element.p3 = 2;
                element.p4 = (byte)(rnd.Next(1,255));
                element.p5 = new byte[5];
                element.p6 = new byte[5];
                for (int j = 0; j < element.p5.Length; j++)
                {
                    element.p5[j] = (byte)rnd.Next(255);
                    element.p6[j] = (byte)rnd.Next(255);
                }
                element.p7 = (byte)rnd.Next(0,255);
                element.p8 = (byte)(rnd.Next(1,255));
                element.seq = (ushort)rnd.Next(0,255);

                s.Add(element);

            }
            return s;
        }
    }
}