using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using Helper = Neo.SmartContract.Framework.Helper;
using System;
using System.Numerics;

namespace nns_registrar
{

  public class Registrar : SmartContract
  {
    const int aDay = 86400;  // seconds
    const int totalAuctionLength = aDay * 5;
    const int revealPeriod = aDay * 2;
    const int minPrice = 1; // NeoVM 不支援浮點數

    //TODO: Registry 部署後需要宣吿 Appcall
    [Appcall("77e193f1af44a61ed3613e6e3442a0fc809bb4b8")]
    static extern object registryCall(string method, object[] arr);

    public static readonly byte[] rootNode = SmartContract.Sha256("neo".AsByteArray());

    public Registrar()
    {
    }

    public static byte[] nameHash(string label)
    {
      var bs = label.AsByteArray();
      if (bs.Length == 0) {
        return bs;
      }
      var domain = SmartContract.Sha256(bs).Concat(rootNode);
      return SmartContract.Sha256(domain);
    }
    
    static byte[] bidkey(string label, byte[] who)
    {
      var bs = label.AsByteArray();
      var bsWho = SmartContract.Sha256(bs).Concat(who);
      return SmartContract.Sha256(bsWho);
    }

    public enum DomainUseState
    {
      Open, 
      Auction, 
      Owned, 
      Forbidden, 
      Reveal, 
      NotYetAvailable
    }

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

    private static Auction getAuction(string label) {
      byte[] key = nameHash(label); 
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

    private static void saveDeed(string label, byte[] who, Deed deed) {
      byte[] key = bidkey(label, who);
      byte[] doublezero = new byte[] { 0, 0 };

      // deed.owner
      var data = deed.owner;
      var datalen = ((BigInteger)data.Length).AsByteArray().Concat(doublezero).Range(0, 2);
      var value = datalen.Concat(data);

      // deed.createOn
      data = deed.createOn.AsByteArray();
      datalen = ((BigInteger)data.Length).AsByteArray().Concat(doublezero).Range(0, 2);
      value = datalen.Concat(data);

      // deed.value
      data = deed.value.AsByteArray();
      datalen = ((BigInteger)data.Length).AsByteArray().Concat(doublezero).Range(0, 2);
      value = datalen.Concat(data);

      Runtime.Notify(new Object[] { "save deed", label});
      Storage.Put(Storage.CurrentContext, key, value);
    }

    private static Deed getDeed(string label, byte[] who) {
      byte[] key = bidkey(label, who);
      byte[] data = Storage.Get(Storage.CurrentContext, key);

      Deed deed = new Deed(null, 0);
      if(data.Length == 0) {
        return deed;
      }

      int seek = 0;
      int len = 0;

      // deed.owner
      len = (int)data.Range(seek, 2).AsBigInteger();
      seek += 2;
      deed.owner = data.Range(seek, len);
      seek += len;

      // deed.createOn
      len = (int)data.Range(seek, 2).AsBigInteger();
      seek += 2;
      deed.createOn = data.Range(seek, len).AsBigInteger();
      seek += len;

      // deed.value
      len = (int)data.Range(seek, 2).AsBigInteger();
      seek += 2;
      deed.value = data.Range(seek, len).AsBigInteger();
      seek += len;

      return deed;
    }

    // get now timestamp
    private static uint now () {
      return Blockchain.GetHeader(Blockchain.GetHeight()).Timestamp;
    }

    private static DomainUseState DomainState(string label) {
      Auction auction = getAuction(label);
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
      // byte[] who = ExecutionEngine.GetCallingScriptHash;

      if(operation == "startAuctionAndBid") {
        return startAuctionAndBid((string)args[0], (byte[])args[1], (BigInteger)args[2]);
      } else if(operation == "getAuctionState") {
        return getAuctionState((string)args[0]);
      }

      return false;
    }

    public static Boolean isAvailable(string label) {
      DomainUseState state = DomainState(label);
      if(state == DomainUseState.Auction) {
        return true;
      }

      if(state == DomainUseState.Owned || state == DomainUseState.Forbidden || 
         state == DomainUseState.Reveal || state == DomainUseState.NotYetAvailable) {
        return false;
      }
      return true;
    }

    /**
     * @ 建立二級域名的拍賣
     * @ param string label 拍賣的二級域名
     * @ return boolean 
     */
    public static Boolean startAuction(string label) {
      if(isAvailable(label) == false) {
        return false;
      }

      Auction newAuction = new Auction();
      newAuction.domain = label;
      newAuction.registrationDate = now() + totalAuctionLength;
      newAuction.value = 0;
      newAuction.highestBid = 0;
      saveAuction(newAuction);
      Runtime.Notify(new Object[] { "create auction", label });
      return true;
    }

    public static Boolean newBid(string label, byte[] who, BigInteger value) {
      if(isAvailable(label) == false) {
        return false;
      }

      var checkBid = getDeed(label, who);
      if(checkBid.value != 0) {
        // already bid
        return false;
      }
      Deed newDeed = new Deed(who, value);
      saveDeed(label, who, newDeed);
      Runtime.Notify(new Object[] { "create new bid", label });
      return true;
    }

    public static Boolean startAuctionAndBid(string label, byte[] who, BigInteger value) {
      var result = startAuction(label);
      if(result == false) {
        return false;
      }
      return newBid(label, who, value);
    }

    public static DomainUseState getAuctionState(string label) {
      return DomainState(label);
    }
    



  }
}