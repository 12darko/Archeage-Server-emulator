﻿using System;

using AAEmu.Commons.Network;

namespace AAEmu.Game.Models.Game.Mails
{
    public class MailHeader : PacketMarshaler
    {
        public long mailId { get; set; }
        public byte Type { get; set; }
        public byte Status { get; set; }
        public string Title { get; set; } // TODO max length 1200
        public uint SenderId { get; set; }
        public string SenderName { get; set; } // TODO max length 128
        public byte Attachments { get; set; }
        public uint ReceiverId { get; set; }
        public string ReceiverName { get; set; } // TODO max length 128
        public DateTime OpenDate { get; set; }
        public byte Returned { get; set; }
        public long Extra { get; set; }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(mailId);
            stream.Write(Type);
            stream.Write(Status);
            stream.Write(Title);
            stream.Write(SenderName);
            stream.Write(Attachments);
            stream.Write(ReceiverName);
            stream.Write(OpenDate);
            stream.Write(Returned);
            stream.Write(Extra);

            return stream;
        }
    }
}
