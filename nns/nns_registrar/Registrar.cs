using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using Helper = Neo.SmartContract.Framework.Helper;
using System;
using System.Numerics;

namespace nns_registrar
{
  public class Entry
  {
    public Deed deed;
    public uint registrationDate;
    public BigInteger bid;
  }

  public class Registrar : SmartContract
  {
    public byte[] rootNode;
    public Registrar(byte[] rootNode)
    {
      this.rootNode = rootNode;
    }

    public static Object Main(string operation, Object[] args)
    {
      var selfHash = ExecutionEngine.ExecutingScriptHash;
      return true;
    }
  }
}