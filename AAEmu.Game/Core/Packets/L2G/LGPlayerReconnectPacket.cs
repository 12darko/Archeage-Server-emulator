﻿using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Core.Network.Login;
using AAEmu.Game.Core.Packets.G2C;

namespace AAEmu.Game.Core.Packets.L2G
{
    public class LGPlayerReconnectPacket : LoginPacket
    {
        public LGPlayerReconnectPacket() : base(0x02)
        {
        }

        public override void Read(PacketStream stream)
        {
            var sessionId = stream.ReadUInt32();
            var connection = GameConnectionTable.Instance.GetConnection(sessionId);
            connection?.SendPacket(new SCReconnectAuthPacket(connection.Id));
        }
    }
}
