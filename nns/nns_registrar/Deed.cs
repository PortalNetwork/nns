using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Helper = Neo.SmartContract.Framework.Helper;
using System;
using System.Numerics;

namespace nns_registrar
{
  public class Deed : SmartContract
  {
    public byte[] registrar;
    public byte[] owner;
    public byte[] previousOwner;
    public uint createOn;
    public BigInteger value;
    public Boolean active;

    public Deed(byte[] registrar, byte[] owner, BigInteger value)
    {
      this.registrar = registrar;
      this.owner = owner;
      this.value = value;
      this.createOn =  Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
      this.active = true;
    }

    public void setOwner(byte[] registrar, byte[] newOwner) 
    {
      if(this.registrar != registrar)
      {
        return;
      }

      this.previousOwner = this.owner;
      this.owner = newOwner;
      Runtime.Notify(new Object[] { "set new owner", newOwner});
    }

    public void setRegistrar(byte[] newRegistrar)
    {
      this.registrar = newRegistrar;
    }
  }
}