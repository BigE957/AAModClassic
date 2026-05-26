using System.IO;
//using AAModClassic.NPCs.Bosses.Core;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore;

public class CoreWorld : ModSystem
{
	public static bool PedestalActive;

	public static bool PrismCharged;

	public override void PostUpdateWorld()
	{
		PedestalActive = NPC.AnyNPCs(ModContent.NPCType<BiomiteCore>());
	}

	public override void NetSend(BinaryWriter writer)
	{
		BitsByte bitsByte = default(BitsByte);
		bitsByte[0] = PedestalActive;
		bitsByte[1] = PrismCharged;
		writer.Write(bitsByte);
	}

	public override void NetReceive(BinaryReader reader)
	{
		BitsByte bitsByte = reader.ReadByte();
		PedestalActive = bitsByte[0];
		PrismCharged = bitsByte[1];
	}
}
