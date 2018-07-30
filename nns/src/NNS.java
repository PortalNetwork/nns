public interface NNS {

    void EventNewOwner(byte[] node, byte[] label, byte[] owner);

    void EventTransfer(byte[] node, byte[] owner);

    void EventNewResolver(byte[] node, byte[] resolver);

    void EventNewTTL(byte[] node, int ttl);

    boolean setSubnodeOwner(byte[] node, byte[] label, byte[] owner);

    boolean setResolver(byte[] node, byte[] resolver);

    boolean setOwner(byte[] node, byte[] owner);

    boolean setTTL(byte[] node, int ttl);

    byte[] owner(byte[] node);

    byte[] resolver(byte[] node);

    int ttl(byte[] node);

}
