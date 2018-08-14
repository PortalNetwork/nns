using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Helper = Neo.SmartContract.Framework.Helper;
using System;
using System.Numerics;

namespace nns_registry
{
  public class Record 
  {
    public byte[] owner;
    public byte[] resolver;
    public BigInteger ttl;
  }

  public class Registry : SmartContract
  {
    public static Object Main(string operation, Object[] args)
    {
      if(operation == "setOwner")
      {
        return setOwner((string)args[0], (byte[])args[1], (byte[])args[2]);
      }
      else if(operation == "setSubnodeOwner")
      {
        return setSubnodeOwner((string)args[0], (string)args[1], (byte[])args[2], (byte[])args[3]);
      }
      else if(operation == "setResolver")
      {
        return setResolver((string)args[0], (byte[])args[1], (byte[])args[2]);
      }
      else if(operation == "setTTL")
      {
        return setTTL((string)args[0], (byte[])args[1], (BigInteger)args[2]);
      }
      else if(operation == "owner")
      {
        return owner((string)args[0]);
      }
      else if(operation == "resolver")
      {
        return resolver((string)args[0]);
      }
      else if(operation == "ttl")
      {
        return ttl((string)args[0]);
      }
      return false;
    }

    public static Record getOwner(string node)
    {
      var data = Storage.Get(Storage.CurrentContext, node);
      var record = Helper.Deserialize(data) as Record;
      return record;
    }

    private static Object setOwner(string node, byte[] owner, byte[] newOwner) 
    {
      Record nodeOwner = getOwner(node);
      if(nodeOwner != null && nodeOwner.owner != owner)
      {
        Runtime.Notify(new Object[] { "set owner", "fail: not owner", node, owner, newOwner});
        return false;
      }

      nodeOwner.owner = newOwner;
      var ownerInfo = Helper.Serialize(nodeOwner);
      Storage.Put(Storage.CurrentContext, node, ownerInfo);
      Runtime.Notify(new Object[] { "set owner", node, owner, newOwner});
      return true;
    }

    private static Object setSubnodeOwner(string parentNode, string subNode, byte[] owner, byte[] newOwner) 
    {
      Record parentNodeOwner = getOwner(parentNode);
      if(parentNodeOwner != null && parentNodeOwner.owner != owner)
      {
        Runtime.Notify(new Object[] { "set sudNodeOwner", "fail: not owner", parentNode, owner, newOwner});
        return false;
      }
      
      var newDomain = parentNode + "." + subNode;
      var record = new Record();
      record.owner = newOwner;
      var ownerInfo = Helper.Serialize(record);
      Storage.Put(Storage.CurrentContext, newDomain, ownerInfo);
      Runtime.Notify(new Object[] { "set subdomain", newDomain, owner, newOwner});
      return true;
    }

    private static Object setResolver(string node, byte[] owner, byte[] resolver)
    {
      Record nodeOwner = getOwner(node);
      if(nodeOwner != null && nodeOwner.owner != owner)
      {
        Runtime.Notify(new Object[] { "set resolver", "fail: not owner", node, owner, resolver});
        return false;
      }

      nodeOwner.resolver = resolver;
      var ownerInfo = Helper.Serialize(nodeOwner);
      Storage.Put(Storage.CurrentContext, node, ownerInfo);
      Runtime.Notify(new Object[] { "set resolver", node, owner, resolver});
      return true;
    }

    private static Object setTTL(string node, byte[] owner, BigInteger ttl)
    {
      Record nodeOwner = getOwner(node);
      if(nodeOwner != null && nodeOwner.owner != owner)
      {
        Runtime.Notify(new Object[] { "set ttl", "fail: not owner", node, owner, ttl});
        return false;
      }

      nodeOwner.ttl = ttl;
      var ownerInfo = Helper.Serialize(nodeOwner);
      Storage.Put(Storage.CurrentContext, node, ownerInfo);
      Runtime.Notify(new Object[] { "set ttl", node, owner, ttl});
      return true;
    }

    private static byte[] owner(string node)
    {
      Record record = getOwner(node);
      return record.owner;
    }

    private static byte[] resolver(string node)
    {
      Record record = getOwner(node);
      return record.resolver;
    }

    private static BigInteger ttl(string node)
    {
      Record record = getOwner(node);
      return record.ttl;
    }

  }
}
