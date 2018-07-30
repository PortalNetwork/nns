public interface NNSResolver {

    boolean setAddr(byte[] node, byte[] addr);

    boolean setContent(byte[] node, byte[] hash);

    boolean setMultihash(byte[] node, byte[] hash);

    boolean setName(byte[] node, String name);

    boolean setABI(byte[] node, int contentType, byte[] data);

    boolean setPubkey(byte[] node, byte x, byte y);

    boolean setText(byte[] node, String key, String value);

    String text(byte[] node, String key);

    byte pubkeyX(byte[] node);

    byte pubkeyY(byte[] node);

    byte[] ABI(byte[] node, int contentTypes);

    String name(byte[] node);

    byte[] content(byte[] node);

    byte[] multihash(byte[] node);

    byte[] addr(byte[] node);

}