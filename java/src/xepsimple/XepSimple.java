package xepsimple;
import com.intersystems.xep.*;
import xep.samples.*;

public class XepSimple {
  protected   static  String              pkgName = "xep.samples";
  protected   static  String              schemaName = "xep_samples";
  protected   static  String              className = "DeviceClass";
  protected   static  String              classFullName = pkgName+"."+className;

  public static void main(String[] args) throws Exception {
    System.out.println("Generating test data...");
    DeviceClass[] sampleArray = DeviceClass.generateSampleData(12);

    // EventPersister
    System.out.println("Generating schema");
    EventPersister xepPersister = PersisterFactory.createPersister();
    xepPersister.connect("127.0.0.1",1972,"XEP","_SYSTEM","SYS"); // connect to localhost
    xepPersister.deleteExtent(classFullName);
    xepPersister.deleteClass(classFullName);
    xepPersister.importSchema(classFullName);   // import schema

    // Event
    Event xepEvent = xepPersister.getEvent(classFullName);
    System.out.println("saving");
/*
    for (int i=0; i < sampleArray.length; i++) {
      DeviceClass sample = sampleArray[i]; 
      xepEvent.store(sample);
    }
*/
    xepEvent.store(sampleArray);
    System.out.println("saved");

    // EventQuery
    String sqlQuery = "SELECT * FROM "+schemaName+"."+className+" WHERE %ID BETWEEN ? AND ?";
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