package xep.samples;
import java.util.Random;

public class ECG implements java.io.Serializable {	
	public  short                     p1;
	public  byte                      p2;
	public  byte                      p3;
	public  short                     p4;
	public  short[]                   p5;
	public  short[]                   p6;
	public  short                     p7;
	public  short                     p8;
	public  byte                      p9=0;
	public  byte                      p10=0;
	public  byte                      p11=0;
	public  short                     seq;
    
    public ECG() {}
    
    public static ECG[] generateSampleData(Random rnd,int count) {
    	ECG[] s = new ECG[count];
        for (int i=0;i<count;i++) {
            s[i] = new ECG();
            s[i].p1=(short)(rnd.nextInt(20)+50);
            s[i].p2=(byte)i;
            s[i].p3=2;
            s[i].p4=(short)(rnd.nextInt(255)+1);
            s[i].p5 = new short[5];
            s[i].p6 = new short[5];
            for (int j=0;j<s[i].p5.length;j++) {
            	s[i].p5[j] = (short)rnd.nextInt(256);
            	s[i].p6[j] = (short)rnd.nextInt(256);
            }
            s[i].p7=(short)rnd.nextInt(256);
            s[i].p8=(short)(rnd.nextInt(255)+1);
            s[i].seq=(short)rnd.nextInt(256);
        }
        return s;
    }

}