﻿using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSResurrectCharacterPacket : GamePacket
    {
        public CSResurrectCharacterPacket() : base(CSOffsets.CSResurrectCharacterPacket, 5)
        {
        }

        public override void Read(PacketStream stream)
        {
            var inPlace = stream.ReadBoolean();

            _log.Debug("ResurrectCharacter, InPlace: {0}", inPlace);

            Connection.ActiveChar.Hp = (int)(Connection.ActiveChar.MaxHp * 0.1);
            Connection.ActiveChar.Mp = (int)(Connection.ActiveChar.MaxMp * 0.1);

            Connection.ActiveChar.BroadcastPacket(
                new SCCharacterResurrectedPacket(
                    Connection.ActiveChar.ObjId,
                    Connection.ActiveChar.Position.X,
                    Connection.ActiveChar.Position.Y,
                    Connection.ActiveChar.Position.Z,
                    0
                ),
                true
            );

            Connection.ActiveChar.BroadcastPacket(
                new SCUnitPointsPacket(
                    Connection.ActiveChar.ObjId,
                    Connection.ActiveChar.Hp,
                    Connection.ActiveChar.Mp,
                    Connection.ActiveChar.HighAbilityRsc
                ),
                true
            );
            Connection.ActiveChar.StartRegen();
        }
    }
}
