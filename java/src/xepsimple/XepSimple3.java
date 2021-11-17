package xepsimple;
import com.intersystems.xep.*;
import xep.samples.*;

public class XepSimple3 {
  protected   static  String              pkgName = "xep.samples";
  protected   static  String              schemaName = "xep_samples";
  protected   static  String              className = "DeviceClassListId";
  protected   static String               collectionClassName = "ECGListId";
  protected   static  String              classFullName = pkgName+"."+className;
  protected   static String               collectionClassFullName = pkgName + "." + collectionClassName;
  
  public static void main(String[] args) throws Exception {
    System.out.println("Generating test data...");
    DeviceClassListId[] sampleArray = DeviceClassListId.generateSampleData(12);

    // EventPersister
    System.out.println("Generating schema");
    EventPersister xepPersister = PersisterFactory.createPersister();
    xepPersister.connect("127.0.0.1",1972,"XEP","_SYSTEM","SYS"); // connect to localhost
    xepPersister.deleteClass(classFullName);
    xepPersister.deleteClass(collectionClassFullName);
    xepPersister.importSchemaFull(classFullName);   // import schema

    // Event
    Event xepEvent = xepPersister.getEvent(classFullName,Event.INDEX_MODE_SYNC);
    System.out.println("saving");
/*
    for (int i=0; i < sampleArray.length; i++) {
    	DeviceClassListId sample = sampleArray[i]; 
    	xepEvent.store(sample);
    }
*/
    xepEvent.store(sampleArray);
    System.out.println("saved");

    // EventQuery
    String sqlQuery = "SELECT * FROM "+schemaName+"."+className+" WHERE %ID BETWEEN ? AND ?";
    EventQuery<DeviceClassListId> xepQuery = xepEvent.createQuery(sqlQuery);
    xepQuery.setParameter(1,3);    // assign value 3 to first SQL parameter
    xepQuery.setParameter(2,12);   // assign value 12 to second SQL parameter
    xepQuery.execute();            // get resultset for IDs between 3 and 12

    // EventQueryIterator
    EventQueryIterator<DeviceClassListId> xepIter = xepQuery.getIterator();
    while (xepIter.hasNext()) {
      DeviceClassListId record = xepIter.next();
      System.out.println(record.deviceName+" "+record.deviceId+" "+record.position);
      for (int i=0;i<record.arrayfloat.length;i++) {
        System.out.print("["+i+"]"+record.arrayfloat[i]+" ");
        // comparing with data source
        if (record.arrayfloat[i] != sampleArray[record.position].arrayfloat[i]) { System.out.println("data mismatch!!! Abort."); System.exit(1); }
      }
      System.out.println();
      int elementcount=record.listECG.size();
      for (int i=0;i<elementcount;i++) {
        System.out.print("["+i+"]"+record.listECG.get(i).p1+"/"+record.listECG.get(i).p2+" ");
        // comparing with data source
        if (record.listECG.get(i).p1 != sampleArray[record.position].listECG.get(i).p1) { System.out.println("data mismatch!!! Abort."); System.exit(1); }
        if (record.listECG.get(i).p2 != sampleArray[record.position].listECG.get(i).p2) { System.out.println("data mismatch!!! Abort."); System.exit(1); }
      }
      System.out.println();
    }

    xepQuery.close();
    xepEvent.close();
    xepPersister.close();
  }
}