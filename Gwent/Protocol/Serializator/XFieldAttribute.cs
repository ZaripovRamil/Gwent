using System;

namespace Protocol.Serializator
{
    public class XFieldAttribute : Attribute
    {
        public byte FieldId { get; }

        public XFieldAttribute(byte fieldId)
        {
            FieldId = fieldId;
        }
    }
}