﻿using System;
using System.Collections.Generic;
using ArcheAgeGame.ArcheAge.Structuring.NPC;
using LocalCommons.Logging;
using LocalCommons.Network;
using LocalCommons.Utilities;

namespace ArcheAgeGame.ArcheAge.Network
{
	public sealed class NP_SCUnitStatePacket_0x0064_debug : NetPacket
    {
        public NP_SCUnitStatePacket_0x0064_debug(List<NPC> list) : base(04, 0x0009)
        {
			/**
			 * DD00
			 * dd01
			 * 6400 opcode
			 * 040F00 liveObjectID
			 * 0000
			 * 01
			 * 3ED500
			 * 0d0e0000
			 * 00000000
			 * 0000
			 * E69479
			 * A4F477
			 * 267A03
			 * 0000803F
			 * 15
			 * 3801
			0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000220C816430200000000000000000000204E0000B8880000FFFF00000001020000000100000000000029000400D812000000730000000000000000000000
			*/
			try
			{


				foreach (NPC npc in list)
				{
					if (npc == null)
					{
						continue;
					}

					//opcode
					ns.Write((short)0x64);
					if (npc.LiveObjectID == 0)
					{
						//获取当前NPC的在线索引
						int index = NPCs.OnlineNPCList.IndexOf(npc);

						//获取LiveObjectID
						npc.LiveObjectID = ArcheAgeGame.LiveObjectUid.Next();
						
						//更新当前NPC
						NPCs.OnlineNPCList[index] = npc;
					}
						
					//liveobjectid
					ns.Write(npc.LiveObjectID);
					//len
					ns.Write((short)0x0000);
					//type
					ns.Write((byte)0x01);
					///bc 我不知道BC代表什么意思
					//ns.Write((Uint24)0x9BE500);
					ns.Write(npc.LiveObjectID);
					ns.Write(npc.ID);
					//tpye 2
					ns.Write(0);
					ns.Write((short)0);
					ns.Write(LocalCommons.Utilities.Helpers.ConvertX(npc.Position.X), 0, 3);
					ns.Write(LocalCommons.Utilities.Helpers.ConvertY(npc.Position.Y), 0, 3);
					ns.Write(LocalCommons.Utilities.Helpers.ConvertZ(npc.Position.Z), 0, 3);
					byte[] Scale = (byte[])BitConverter.GetBytes(npc.Scale);
					ns.Write(Scale);
					//ns.WriteHex("0000803F");//100%=>1065353216  90%=>1064514355
					ns.Write(npc.Level);
					//ns.Write(npc.ModelID);
					ns.Write(10);
					//未知
					ns.WriteHex("62450000000000000000000000000000005363000000000000000000000000000000E0600000000000000000000000514900000000000000000000004863000000000000000000000000000000000000000000000000000000000000000000000000000000000000002607000000000000000000000000000000D9360000000000000000000000000000007E4D0000425E00000000000000000000000000001802000000000000000000000000000003AA0E00000100000000000000000000000000803F0000803F0000000000000000000000000000803F000000000000803F350200000000803F000000000000803F0000000021000000000000003CDA3C3FFFCDC2FFA25F42FFA25F42FF2B250DFF4B4756FF800000FAFDE6F7DFE4553AF82622176437F5009CD934D8FE090800EBF06220BA2325F30E14FDFF02F0DA0FF325D7F516EB0A25C141E1B0D3159CCE0F0315001EFEF545E601043C1427FFED430DD5272A140023FCCB000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
					//未知
					//ns.WriteHex("02B0A473020200000000000000000000");
					//HP  1.00
					ns.Write(0);
					//MP
					ns.Write(0);
					//
					ns.WriteHex("FFFF");
					//姿势 NPC独有  怪兽没有
					ns.Write((short)0x04);//len
					ns.Write(0x3e);//3E000000  3D000000
					//姿势 动作 
					ns.Write((short)0x01);//len
										  //ns.Write((byte)0x0);

					//
					//ns.Write((short)0x0201);
					ns.Write((byte)0x01);
					ns.Write(0x02);
					ns.WriteHex("010000");

					//rot.x  guess
					ns.Write((short)0x0);
					//rot.y guess
					ns.Write((short)0x0);
					//rot.z confirm
					ns.Write((short)0xB0);

					//
					ns.WriteHex("0800A662" +
						"01000000");
					//factionID confirm
					//ns.Write(npc.FactionId);
					ns.Write(0x65);

					ns.WriteHex("0000000000000000");



				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex, "Write NPCs Send Stream Error");
			}

		}
    }
}

