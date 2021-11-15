package xep.samples;
import java.util.Random;

import com.intersystems.xep.annotations.Index;
import com.intersystems.xep.annotations.IndexType;

import java.util.ArrayList;
import java.util.List;

@Index(name="idkey",fields={"deviceId","id"},type=IndexType.idkey)
public class ECGListId  {	
	public  long                      id;
	public  String                    deviceId;
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
    
    public ECGListId() {}
    
    public static List<ECGListId> generateSampleData(Random rnd,int count,String deviceId) {
    	List<ECGListId> s = new ArrayList<ECGListId>();
        for (int i=0;i<count;i++) {
        	ECGListId e = new ECGListId();
        	e.id=i+1;
        	e.deviceId=deviceId;
            e.p1=(short)(rnd.nextInt(20)+50);
            e.p2=(byte)i;
            e.p3=2;
            e.p4=(short)(rnd.nextInt(255)+1);
            e.p5 = new short[5];
            e.p6 = new short[5];
            for (int j=0;j<e.p5.length;j++) {
            	e.p5[j] = (short)rnd.nextInt(256);
            	e.p6[j] = (short)rnd.nextInt(256);
            }
            e.p7=(short)rnd.nextInt(256);
            e.p8=(short)(rnd.nextInt(255)+1);
            e.seq=(short)rnd.nextInt(256);
            s.add(e);
        }
        return s;
    }

}