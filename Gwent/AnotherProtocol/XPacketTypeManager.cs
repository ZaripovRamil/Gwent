using System;
using System.Collections.Generic;

namespace Protocol
{

    public static class XPacketTypeManager
    {
        private static readonly Dictionary<XPacketType, Tuple<byte, byte>> TypeDictionary = new();

        static XPacketTypeManager()
        {
            RegisterType(XPacketType.Handshake, 1, 0);
            RegisterType(XPacketType.GameRequest, 2, 0);
            RegisterType(XPacketType.GameResponse, 3, 0);
            RegisterType(XPacketType.MoveResult, 4, 0);
            RegisterType(XPacketType.PlayerMove, 5, 0);
        }

        public static void RegisterType(XPacketType type, byte btype, byte bsubtype)
        {
            if (TypeDictionary.ContainsKey(type))
            {
                throw new Exception($"Packet type {type:G} is already registered.");
            }

            TypeDictionary.Add(type, Tuple.Create(btype, bsubtype));
        }

        public static Tuple<byte, byte> GetType(XPacketType type)
        {
            if (!TypeDictionary.ContainsKey(type))
            {
                throw new Exception($"Packet type {type:G} is not registered.");
            }

            return TypeDictionary[type];
        }

        public static XPacketType GetTypeFromPacket(XPacket packet)
        {
            var type = packet.PacketType;
            var subtype = packet.PacketSubtype;
            foreach (var tuple in TypeDictionary)
                if (tuple.Value.Item1 == type && tuple.Value.Item2 == subtype)
                    return tuple.Key;
            return XPacketType.Unknown;
        }
    }
}