namespace Protocol.Serializator;

public class XFieldAttribute : Attribute
{
    public byte FieldID { get; }

    public XFieldAttribute(byte fieldId)
    {
        FieldID = fieldId;
    }
}