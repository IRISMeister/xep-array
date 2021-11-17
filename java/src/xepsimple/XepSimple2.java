package xepsimple;
import com.intersystems.xep.*;
import xep.samples.*;

public class XepSimple2 {
  protected   static  String              pkgName = "xep.samples";
  protected   static  String              schemaName = "xep_samples";
  protected   static  String              className = "DeviceClassList";
  protected   static  String              classFullName = pkgName+"."+className;

  public static void main(String[] args) throws Exception {
    System.out.println("Generating test data...");
    DeviceClassList[] sampleArray = DeviceClassList.generateSampleData(12);

    // EventPersister
    System.out.println("Generating schema");
    EventPersister xepPersister = PersisterFactory.createPersister();
    xepPersister.connect("127.0.0.1",1972,"XEP","_SYSTEM","SYS"); // connect to localhost
    xepPersister.deleteClass(classFullName);
    xepPersister.importSchema(classFullName);   // import schema

    // Event
    Event xepEvent = xepPersister.getEvent(classFullName,Event.INDEX_MODE_SYNC);
    System.out.println("saving");
/*
    for (int i=0; i < sampleArray.length; i++) {
    	DeviceClassList sample = sampleArray[i]; 
    	xepEvent.store(sample);
    }
*/
    xepEvent.store(sampleArray);
    System.out.println("saved");

    // EventQuery
    String sqlQuery = "SELECT * FROM "+schemaName+"."+className+" WHERE %ID BETWEEN ? AND ?";
    EventQuery<DeviceClassList> xepQuery = xepEvent.createQuery(sqlQuery);
    xepQuery.setParameter(1,3);    // assign value 3 to first SQL parameter
    xepQuery.setParameter(2,12);   // assign value 12 to second SQL parameter
    xepQuery.execute();            // get resultset for IDs between 3 and 12

    // EventQueryIterator
    EventQueryIterator<DeviceClassList> xepIter = xepQuery.getIterator();
    while (xepIter.hasNext()) {
      DeviceClassList record = xepIter.next();
      System.out.println("deviceName:"+record.deviceName+" deviceId:"+record.deviceId+" position:"+record.position+" number1:"+record.number1);
      // comparing with data source
      if (record.number1 != sampleArray[record.position].number1) { System.out.println("data mismatch!!! Abort."); System.exit(1); }
      if (record.number2 != sampleArray[record.position].number2) { System.out.println("data mismatch!!! Abort."); System.exit(1); }

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