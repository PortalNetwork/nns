using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace nns_registrar
{
  public class Deed {
    public byte[] owner;
    public BigInteger createOn;
    public BigInteger value;

    public Deed(byte[] owner, BigInteger value)
    {
      this.owner = owner;
      this.value = value;
      this.createOn =  Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
    }
  }
}