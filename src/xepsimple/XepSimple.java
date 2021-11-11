package xepsimple;
import com.intersystems.xep.*;
import xep.samples.DeviceClass;

public class XepSimple {
  public static void main(String[] args) throws Exception {
	System.out.println("Generating test data...");
	DeviceClass[] sampleArray = DeviceClass.generateSampleData(12);

// EventPersister
	System.out.println("Generating schema");
    EventPersister xepPersister = PersisterFactory.createPersister();
    xepPersister.connect("127.0.0.1",1972,"XEP","_SYSTEM","SYS"); // connect to localhost
    xepPersister.deleteExtent("xep.samples.DeviceClass");   // remove old test data
    xepPersister.importSchemaFull("xep.samples.DeviceClass");   // import schema

// Event
    Event xepEvent = xepPersister.getEvent("xep.samples.DeviceClass");
	System.out.println("saving");

    for (int i=0; i < sampleArray.length; i++) {
      DeviceClass sample = sampleArray[i]; 
      xepEvent.store(sample);
    }
	System.out.println("saved");

// EventQuery
    String sqlQuery = "SELECT * FROM xep_samples.DeviceClass WHERE %ID BETWEEN ? AND ?";
    EventQuery<DeviceClass> xepQuery = xepEvent.createQuery(sqlQuery);
    xepQuery.setParameter(1,3);    // assign value 3 to first SQL parameter
    xepQuery.setParameter(2,12);   // assign value 12 to second SQL parameter
    xepQuery.execute();            // get resultset for IDs between 3 and 12

// EventQueryIterator
    EventQueryIterator<DeviceClass> xepIter = xepQuery.getIterator();
    while (xepIter.hasNext()) {
    	DeviceClass record = xepIter.next();
    	System.out.println(record.deviceName+" "+record.deviceId);
    	for (int i=0;i<record.arrayfloat.length;i++) {
    		System.out.print("["+i+"]"+record.arrayfloat[i]+" ");
    	}
    	System.out.println();
    	for (int i=0;i<record.arrayECG.length;i++) {
    		System.out.print("["+i+"]"+record.arrayECG[i].p1+"/"+record.arrayECG[i].p2+" ");
    	}
    	System.out.println();
    }

    xepQuery.close();
    xepEvent.close();
    xepPersister.close();
  }
}