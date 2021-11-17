package xep.samples;
import java.util.List;
import java.util.Random;
import java.util.Date;
import java.util.Calendar;
import com.intersystems.xep.annotations.Index;
import com.intersystems.xep.annotations.IndexType;
import com.intersystems.xep.annotations.Indices;

@Indices({
	 @Index(name="idx1",fields={"deviceId"},type=IndexType.simple),
	 @Index(name="idx2",fields={"deviceName"},type=IndexType.simple),
	 @Index(name="idx3",fields={"number1"},type=IndexType.simple)
	})
public class DeviceClassListId {
	
    public  int                        position;
	public  String                     deviceName;
    public  String                     deviceId;
    public  Date                       fromTS;
    public  Date                       toTS;
    public  float                      number1;
    public  float                      number2;
    public  float                      number3;
    public  float                      number4;
    public  float                      number5;
    public  float[]                    arrayfloat;
    public  List<ECGListId>				   listECG;
        
    protected static Random       rnd;
    
    public DeviceClassListId() {}
    
    public static DeviceClassListId[] generateSampleData(int count) {
        int NumOfSamplesPerRecord=5;
    	rnd = new Random(528314287391911L);
        Calendar cl = Calendar.getInstance();
    	DeviceClassListId[] s = new DeviceClassListId[NumOfSamplesPerRecord*count];
        for (int dev = 0; dev < NumOfSamplesPerRecord; dev++) {
            for (int i = 0; i < count; i++) {
                cl.set(2020,0,1,0,0,0); // month starts from 0...
                s[dev*count+i] = new DeviceClassListId();
                s[dev*count+i].position = dev * count + i;
                s[dev*count+i].deviceName = "deviceName" + dev;
                s[dev*count+i].deviceId = "id" + dev;
                cl.add(Calendar.SECOND,i*100);
                s[dev*count+i].fromTS = cl.getTime();
                cl.add(Calendar.SECOND,99);
                s[dev*count+i].toTS = cl.getTime();
                s[dev*count+i].number1 = (float)12345;
                s[dev*count+i].number2 = (float)1;
                s[dev*count+i].number3 = (float)2;
                s[dev*count+i].number4 = (float)3;
                s[dev*count+i].number5 = (float)4;
                s[dev*count+i].arrayfloat = new float[10];
                for (int j=0;j<s[dev*count+i].arrayfloat.length;j++) {
                    s[dev*count+i].arrayfloat[j] = rnd.nextFloat();
                }
                s[dev*count+i].listECG = ECGListId.generateSampleData(rnd,20,s[dev*count+i].deviceId,s[dev*count+i].position);
            }
        }
        return s;
    }

}
