using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using Helper = Neo.SmartContract.Framework.Helper;
using System;
using System.Numerics;

namespace nns_registrar
{
  public class Auction {
    public string domain;                 // include tld
    public BigInteger registrationDate;   // now + totalAuctionLength
    public BigInteger value;
    public BigInteger highestBid;
  }

  public class Registrar : SmartContract
  {
    const int aDay = 86400;  // second unit
    const int totalAuctionLength = aDay * 5;
    const int revealPeriod = aDay * 2;
    const int minPrice = 1; // neovm 不支援浮點數, 顆顆

    public byte[] rootNode;
    public Registrar(byte[] rootNode)
    {
      this.rootNode = rootNode;
    }

    #region domain name to hash
    //域名转hash算法
    // e.g. aaa.bb.test =>{"test","bb","aa"}
    static byte[] nameHash(string domain)
    {
      return SmartContract.Sha256(domain.AsByteArray());
    }
    static byte[] nameHashSub(byte[] roothash, string subdomain)
    {
      var bs = subdomain.AsByteArray();
      if (bs.Length == 0) {
        return roothash;
      }
      var domain = SmartContract.Sha256(bs).Concat(roothash);
      return SmartContract.Sha256(domain);
    }
    static byte[] nameHashArray(string[] domainarray)
    {
      byte[] hash = nameHash(domainarray[0]);
      for (var i = 1; i < domainarray.Length; i++)
      {
          hash = nameHashSub(hash, domainarray[i]);
      }
      return hash;
    }
    #endregion

    #region mode enum
    public enum DomainUseState
    {
      Open, 
      Auction, 
      Owned, 
      Forbidden, 
      Reveal, 
      NotYetAvailable
    }
    #endregion

    #region save/get auction data
    private static void saveAuction(Auction auction) {
      byte[] key = nameHash(auction.domain);  // use domain hash as storage's key
      byte[] doublezero = new byte[] { 0, 0 };
      
      // auction.domain
      var data = auction.domain.AsByteArray();
      var datalen = ((BigInteger)data.Length).AsByteArray().Concat(doublezero).Range(0, 2);
      var value = datalen.Concat(data);

      // auction.registrationDate
      data = auction.registrationDate.AsByteArray();
      datalen = ((BigInteger)data.Length).AsByteArray().Concat(doublezero).Range(0, 2);
      value = datalen.Concat(data);

      // auction.value
      data = auction.value.AsByteArray();
      datalen = ((BigInteger)data.Length).AsByteArray().Concat(doublezero).Range(0, 2);
      value = datalen.Concat(data);

      // auction.highestBid
      data = auction.highestBid.AsByteArray();
      datalen = ((BigInteger)data.Length).AsByteArray().Concat(doublezero).Range(0, 2);
      value = datalen.Concat(data);

      Runtime.Notify(new Object[] { "save auction", auction.domain});
      Storage.Put(Storage.CurrentContext, key, value);
    }

    private static Auction getAuction(string domain) {
      byte[] key = nameHash(domain);  // use domain hash as storage's key
      byte[] data = Storage.Get(Storage.CurrentContext, key);
      Auction auction = new Auction();
      if(data.Length == 0) {
        return auction;
      }

      int seek = 0;
      int len = 0;

      // auction.domain
      len = (int)data.Range(seek, 2).AsBigInteger();
      seek += 2;
      auction.domain = data.Range(seek, len).AsString();
      seek += len;

      // auction.registrationDate
      len = (int)data.Range(seek, 2).AsBigInteger();
      seek += 2;
      auction.registrationDate = data.Range(seek, len).AsBigInteger();
      seek += len;

      // auction.value
      len = (int)data.Range(seek, 2).AsBigInteger();
      seek += 2;
      auction.value = data.Range(seek, len).AsBigInteger();
      seek += len;

      // auction.highestBid
      len = (int)data.Range(seek, 2).AsBigInteger();
      seek += 2;
      auction.highestBid = data.Range(seek, len).AsBigInteger();
      seek += len;

      return auction;
    }
    #endregion

    // get now timestamp
    private static uint now () {
      return Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
    }

    private static DomainUseState DomainState(string domain) {
      Auction auction = getAuction(domain);
      if(auction.domain == null) {
        return DomainUseState.Open;
      }

      var nowtime = now();
      var revealLine = auction.registrationDate - revealPeriod;
      if(nowtime < auction.registrationDate && nowtime < revealLine) {
        return DomainUseState.Auction;
      }

      if(nowtime < auction.registrationDate && nowtime >= revealLine) {
        return DomainUseState.Reveal;
      }

      if(auction.highestBid == 0) {
        return DomainUseState.Open;
      }

      return DomainUseState.Owned;
    }

    public static Object Main(string operation, Object[] args)
    {
      var selfHash = ExecutionEngine.ExecutingScriptHash;
      if(operation == "startAuction") {
        startAuction((string)args[0]);
      }

      return true;
    }

    public static Boolean startAuction(string domain) {
      var state = DomainState(domain);
      if(state == DomainUseState.Auction) {
        return true;
      }

      Auction newAuction = new Auction();
      newAuction.domain = domain;
      newAuction.registrationDate = now() + totalAuctionLength;
      newAuction.value = 0;
      newAuction.highestBid = 0;
      saveAuction(newAuction);
      return true;
    }
  }
}