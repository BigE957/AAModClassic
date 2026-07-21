using System.IO;
//using AAModClassic.NPCs.Bosses.Core;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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
		writer.WriteFlags(PedestalActive, PrismCharged);
	}

	public override void NetReceive(BinaryReader reader)
	{
		reader.ReadFlags(out PedestalActive, out PrismCharged);
	}
}
