package xep.samples;
import java.util.Random;
import java.util.Date;
import java.util.Calendar;
import com.intersystems.xep.annotations.Index;
import com.intersystems.xep.annotations.IndexType;
import com.intersystems.xep.annotations.Serialized;
import com.intersystems.xep.annotations.Indices;

@Indices({
 @Index(name="idx1",fields={"deviceId"},type=IndexType.simple),
 @Index(name="idx2",fields={"deviceName"},type=IndexType.simple),
 @Index(name="idx3",fields={"number1"},type=IndexType.simple)
})
public class DeviceClass {

    public  int                        position;
	public  String                     deviceName;
    public  String                     deviceId;
    public  Date                       fromTS;
    public  Date                       toTS;
    public  double                     number1;
    public  float                      number2;
    public  float                      number3;
    public  float                      number4;
    public  float                      number5;
    public  float[]                    arrayfloat;
    @Serialized
    public  ECG[]                      arrayECG;
        
    protected static Random       rnd;
    
    public DeviceClass() {}
    
    public static DeviceClass[] generateSampleData(int count) {
        int NumOfDevice=5;
        int NumOfSamplesPerDev=20;
    	rnd = new Random(528314287391911L);
    	// Date will hold fractional milliseconds which you can't manipulate
    	// So select fromTS will return something like 2020-01-01 00:00:00.118
        Calendar cl = Calendar.getInstance();
    	DeviceClass[] s = new DeviceClass[NumOfDevice*count];
        for (int dev = 0; dev < NumOfDevice; dev++) {
            for (int i = 0; i < count; i++) {
                cl.set(2020,0,1,0,0,0); // month starts from 0...
                s[dev*count+i] = new DeviceClass();
                s[dev*count+i].position = dev * count + i;
                s[dev*count+i].deviceName="deviceName" + dev;
                s[dev*count+i].deviceId="id"+ dev;
                cl.add(Calendar.SECOND,i*100);
                s[dev*count+i].fromTS = cl.getTime();
                cl.add(Calendar.SECOND,99);
                s[dev*count+i].toTS = cl.getTime();
                s[dev*count+i].number1=(double)12345.1;
                s[dev*count+i].number2=(float)1.2;
                s[dev*count+i].number3=(float)2;
                s[dev*count+i].number4=(float)3;
                s[dev*count+i].number5=(float)4;
                s[dev*count+i].arrayfloat = new float[10];
                for (int j=0;j<s[dev*count+i].arrayfloat.length;j++) {
                    s[dev*count+i].arrayfloat[j] = rnd.nextFloat();
                }
                s[dev*count+i].arrayECG = ECG.generateECGData(rnd,NumOfSamplesPerDev);

            }
        }
        return s;
    }

}
