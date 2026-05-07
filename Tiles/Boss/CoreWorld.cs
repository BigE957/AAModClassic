using AAModClassic._Content.Terrarium.__Hardmode.NPCs._BossBiomiteCore;
using System.IO;
//using AAModClassic.NPCs.Bosses.Core;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Boss;

public class CoreWorld : ModSystem
{
	public static bool PedestalActive;

	public static bool PrismCharged;

	public override void PostUpdateWorld()
	{
		PedestalActive = NPC.AnyNPCs(ModContent.NPCType<Core>());
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
