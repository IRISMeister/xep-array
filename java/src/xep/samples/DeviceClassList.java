package xep.samples;
import java.util.List;
import java.util.Random;
import com.intersystems.xep.annotations.Index;
import com.intersystems.xep.annotations.IndexType;

@Index(name="idx1",fields={"deviceId"},type=IndexType.simple)
public class DeviceClassList {
	
	public  String                     deviceName;
    public  String                     deviceId;
    public  float                      number1;
    public  float                      number2;
    public  float                      number3;
    public  float                      number4;
    public  float                      number5;
    public  float[]                    arrayfloat;
    public  List<ECGList>				   listECG;
        
    protected static Random       rnd;
    
    public DeviceClassList() {}
    
    public static DeviceClassList[] generateSampleData(int count) {
    	rnd = new Random(528314287391911L);
    	DeviceClassList[] s = new DeviceClassList[count];
        for (int i=0;i<count;i++) {
            s[i] = new DeviceClassList();
            s[i].deviceName="deviceName"+i;
            s[i].deviceId="id"+i;
            s[i].number1=(float)12345;
            s[i].number2=(float)1;
            s[i].number3=(float)2;
            s[i].number4=(float)3;
            s[i].number5=(float)4;
            s[i].arrayfloat = new float[10];
            for (int j=0;j<s[i].arrayfloat.length;j++) {
            	s[i].arrayfloat[j] = rnd.nextFloat();
            }
            s[i].listECG = ECGList.generateSampleData(rnd,20);

        }
        return s;
    }

}
