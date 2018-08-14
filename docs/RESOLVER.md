# Resolver
Resolvers are responsible for the actual process of translating names into addresses. Any contract that implements the relevant standards may act as a resolver in NNS. General-purpose resolver implementations are offered for users whose requirements are straightforward, such as serving an infrequently changed address for a name.

Each record type - NEO address, IPFS content hash, and so forth - defines a method or methods that a resolver must implement in order to provide records of that kind. 

## [NNS Resolver](./nns/nns_resolver)
Resolver implementation that allows the owner of any domain to configure how its name should resolve. One deployment of this contract allows any number of people to use it, by setting it as their resolver in the registry.
