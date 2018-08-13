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
}