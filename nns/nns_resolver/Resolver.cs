using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Helper = Neo.SmartContract.Framework.Helper;
using System;
using System.Numerics;

namespace nns_resolver 
{
  public class Record
  {
    public byte[] owner;
    public byte[] addr;
  }

  public class Resolver: SmartContract 
  {
    public static Object Main(string operation, Object[] args)
    {
      if(operation == "setAddr")
      {
        return setAddr((string)args[0], (byte[])args[1], (byte[])args[2]);
      }
      else if(operation == "addr")
      {
        return getAddr((string)args[0]);
      }
      return false;
    }

    public static Record getOwner(string node)
    {
      var data = Storage.Get(Storage.CurrentContext, node);
      var record = Helper.Deserialize(data) as Record;
      return record;
    }


    public static Boolean setAddr(string node, byte[] owner, byte[] addr)
    {
      Record record = getOwner(node);
      if(record != null && record.owner != owner)
      {
        Runtime.Notify(new Object[] { "set addr fail: not owner", node, owner, addr});
        return false;
      }
      record.addr = addr;
      var recordInfo = Helper.Serialize(record);
      Storage.Put(Storage.CurrentContext, node, recordInfo);
      Runtime.Notify(new Object[] { "set addr", node, owner, addr, recordInfo});
      return true;
    }

    public static byte[] getAddr(string node)
    {
      Record record = getOwner(node);
      return record.addr;
    }
  }
}