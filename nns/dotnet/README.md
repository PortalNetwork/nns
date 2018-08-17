##### Registry Smart Contract
```Boolean setOwner(string node, byte[] owner, byte[] newOwner) ```
Transfers ownership of a node to a new address. May only be called by the current owner of the node.

```Boolean setResolver(string node, byte[] owner, byte[] resolver)```
Sets the resolver address for the specified node.

```Boolean setTTL(string node, byte[] owner, BigInteger ttl)```
Sets the TTL for the specified node.

```byte[] owner(string node)```
Returns the address that owns the specified node.

```byte[] resolver(string node)```
Returns the address of the resolver for the specified node.

```BigInteger ttl(string node)```
Returns the TTL of a node, and any records associated with it.

##### Resolver Smart Contract
```Boolean setAddr(string node, byte[] owner, byte[] addr)```
Sets the address associated with an node. May only be called by the owner of that node in the registry.

```Boolean addr(string node, byte[] owner, byte[] addr)```
Returns the address associated with an node.

##### Registrar Smart Contract #####
```Boolean startAuctionAndBid(string label, byte[] who, BigInteger value)```
Start an auction for an available node

```String getAuctionState(string label)```
Get an auction state
