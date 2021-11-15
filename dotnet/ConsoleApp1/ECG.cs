using System;

namespace xep.samples
{
    [Serializable]
    public class ECG
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

        public ECG() { }

        public static ECG[] generateSampleData(Random rnd, int count)
        {
            ECG[] s = new ECG[count];
            for (int i = 0; i < count; i++)
            {
                s[i] = new ECG();
                s[i].p1 = (byte)(rnd.Next(50,70));
                s[i].p2 = (byte)i;
                s[i].p3 = 2;
                s[i].p4 = (byte)(rnd.Next(1,255));
                s[i].p5 = new byte[5];
                s[i].p6 = new byte[5];
                for (int j = 0; j < s[i].p5.Length; j++)
                {
                    s[i].p5[j] = (byte)rnd.Next(255);
                    s[i].p6[j] = (byte)rnd.Next(255);
                }
                s[i].p7 = (byte)rnd.Next(0,255);
                s[i].p8 = (byte)(rnd.Next(1,255));
                s[i].seq = (ushort)rnd.Next(0,255);

            }
            return s;
        }
    }
}