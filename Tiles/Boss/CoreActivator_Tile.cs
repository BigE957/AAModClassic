using AAModClassic._Content.Terrarium.__Hardmode.NPCs._BossBiomiteCore;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Materials;
//using AAModClassic.NPCs.Bosses.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Boss;

public class CoreActivator_Tile : ModTile
{
	public Vector2 Origin = new Vector2((float)(int)((float)Main.maxTilesX * 0.65f), 100f) * 16f;

	public override void SetStaticDefaults()
	{
		Main.tileSolidTop[Type] = false;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		base.DustType = DustID.Terra;
		Main.tileLavaDeath[Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Origin = new Point16(0, 0);
		TileObjectData.newTile.Direction = TileObjectDirection.None;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Core Engine");
		AddMapEntry(new Color(0, 150, 50), val);
		TileID.Sets.DisableSmartCursor[Type] = true;
		base.AnimationFrameHeight = 36;
	}

	public Color White(Color color)
	{
		return AAColor.COLOR_WHITEFADE1;
	}

	public override void PostDraw(int x, int y, SpriteBatch sb)
	{
		Tile tile = Main.tile[x, y];
		Texture2D texture = Mod.GetTexture("Glowmasks/CoreActivator_Glow");
		int frameY = ((tile != null && tile.HasTile) ? (tile.TileFrameY + Main.tileFrame[Type] * 36) : 0);
		BaseDrawing.DrawTileTexture(sb, texture, x, y, 16, 16, tile.TileFrameX, frameY, slopeDraw: false, flipTex: false, ignoreHalfBricks: false, null, White);
	}

	public override bool RightClick(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		int num = ModContent.ItemType<BiomePrism>();
		if (CoreWorld.PrismCharged)
		{
			localPlayer.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<AAModClassic.Items.Materials.TerraPrism>(), 1);
			CoreWorld.PrismCharged = false;
			return true;
		}
		if (BasePlayer.HasItem(localPlayer, num) && !NPC.AnyNPCs(ModContent.NPCType<Core>()))
		{
			for (int k = 0; k < 50; k++)
			{
				Item item = localPlayer.inventory[k];
				if (item != null && item.type == num && item.stack >= 1)
				{
					item.stack--;
					SpawnBoss(localPlayer, ModContent.NPCType<Core>(), localPlayer.Center);
				}
			}
		}
		return true;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		if (CoreWorld.PedestalActive)
		{
			frame = 1;
		}
		else if (CoreWorld.PrismCharged)
		{
			frame = 2;
		}
		else
		{
			frame = 0;
		}
	}

	public static void SpawnBoss(Player player, int bossType, Vector2 Pos = default(Vector2))
	{
		Vector2 val = Pos + new Vector2((float)((MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * (float)Main.rand.Next(2) != 0f) ? 1 : (-1)), -800f);
		if (Main.netMode != NetmodeID.MultiplayerClient)
		{
			if (!NPC.AnyNPCs(bossType))
			{
				int num = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)val.X, (int)val.Y, bossType, 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num].Center = val;
				Main.npc[num].netUpdate = true;
				BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.CoreSpawn"), 175, 75, byte.MaxValue, sync: false);
			}
		}
		else
		{
			AANet.SendNetMessage<SummonNPCFromClient>((byte)player.whoAmI, (short)bossType, true, (int)val.X, (int)val.Y, "The Biome Core whirs to life!", false);
		}
	}

	public override bool CanKillTile(int i, int j, ref bool blockDamaged)
	{
		return false;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<BiomePrism>();
	}
}
