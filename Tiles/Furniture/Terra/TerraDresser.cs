using System;
using AAModClassic.Items.Blocks.Terra;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Furniture.Terra;

public class TerraDresser : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolidTop[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileTable[Type] = true;
		Main.tileContainer[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileID.Sets.HasOutlines[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook((Func<int, int, int, int, int, int, int>)Chest.FindEmptyChest, -1, 0, true);
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook((Func<int, int, int, int, int, int, int>)Chest.AfterPlacement_Hook, -1, 0, false);
		TileObjectData.newTile.AnchorInvalidTiles = new int[1] { 127 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile((int)Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Terra Dresser");
		AddMapEntry(new Color(65, 205, 12), val);
		base.DustType = DustID.Terra;
		TileID.Sets.DisableSmartCursor[Type] = true;
		base.AdjTiles = new int[1] { 88 };
		TileID.Sets.BasicDresser[Type] = true;
		RegisterItemDrop(ModContent.ItemType<AAModClassic.Items.Blocks.Terra.TerraDresser>());
	}

    public override LocalizedText DefaultContainerName(int i, int j) => Mod.Find<ModItem>("RazewoodDresser").DisplayName;

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		if (Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY == 0)
		{
			Main.CancelClothesWindow(quiet: true);
			Main.mouseRightRelease = false;
			int num = Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameX / 18;
			num %= 3;
			num = Player.tileTargetX - num;
			int num2 = Player.tileTargetY - Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY / 18;
			if (localPlayer.sign > -1)
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				localPlayer.sign = -1;
				Main.editSign = false;
				Main.npcChatText = string.Empty;
			}
			if (Main.editChest)
			{
				SoundEngine.PlaySound(SoundID.MenuTick);
				Main.editChest = false;
				Main.npcChatText = string.Empty;
			}
			if (localPlayer.editedChestName)
			{
				NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[localPlayer.chest].name), localPlayer.chest, 1f);
				localPlayer.editedChestName = false;
			}
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				if (num == localPlayer.chestX && num2 == localPlayer.chestY && localPlayer.chest != -1)
				{
					localPlayer.chest = -1;
					Recipe.FindRecipes();
					SoundEngine.PlaySound(SoundID.MenuClose);
				}
				else
				{
					NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, num, num2);
					Main.stackSplit = 600;
				}
			}
			else
			{
				//localPlayer.flyingPigChest = -1;
				int num3 = Chest.FindChest(num, num2);
				if (num3 != -1)
				{
					Main.stackSplit = 600;
					if (num3 == localPlayer.chest)
					{
						localPlayer.chest = -1;
						Recipe.FindRecipes();
						SoundEngine.PlaySound(SoundID.MenuClose);
					}
					else if (num3 != localPlayer.chest && localPlayer.chest == -1)
					{
						localPlayer.chest = num3;
						Main.playerInventory = true;
						Main.recBigList = false;
						SoundEngine.PlaySound(SoundID.MenuOpen);
						localPlayer.chestX = num;
						localPlayer.chestY = num2;
					}
					else
					{
						localPlayer.chest = num3;
						Main.playerInventory = true;
						Main.recBigList = false;
						SoundEngine.PlaySound(SoundID.MenuTick);
						localPlayer.chestX = num;
						localPlayer.chestY = num2;
					}
					Recipe.FindRecipes();
				}
			}
		}
		else
		{
			Main.playerInventory = false;
			localPlayer.chest = -1;
			Recipe.FindRecipes();
			Main.interactedDresserTopLeftX = Player.tileTargetX;
			Main.interactedDresserTopLeftY = Player.tileTargetY;
			Main.OpenClothesWindow();
		}
		return true;
	}

	public override void MouseOverFar(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
		int tileTargetX = Player.tileTargetX;
		int num = Player.tileTargetY;
		int x = tileTargetX - tile.TileFrameX % 54 / 18;
		if (tile.TileFrameY % 36 != 0)
		{
			num--;
		}
		int num2 = Chest.FindChest(x, num);
		localPlayer.cursorItemIconID = -1;
		if (num2 < 0)
		{
			localPlayer.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
		}
		else
		{
			if (Main.chest[num2].name != "")
			{
				localPlayer.cursorItemIconText = Main.chest[num2].name;
			}
			else
			{
				localPlayer.cursorItemIconText = DefaultContainerName(tile.TileFrameX, tile.TileFrameY).ToString();
			}
			if (localPlayer.cursorItemIconText == DefaultContainerName(tile.TileFrameX, tile.TileFrameY).ToString())
			{
				localPlayer.cursorItemIconID = ModContent.ItemType<AAModClassic.Items.Blocks.Terra.TerraDresser>();
				localPlayer.cursorItemIconText = "";
			}
		}
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		if (localPlayer.cursorItemIconText == "")
		{
			localPlayer.cursorItemIconEnabled = false;
			localPlayer.cursorItemIconID = ItemID.None;
		}
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
		int tileTargetX = Player.tileTargetX;
		int num = Player.tileTargetY;
		int x = tileTargetX - tile.TileFrameX % 54 / 18;
		if (tile.TileFrameY % 36 != 0)
		{
			num--;
		}
		int num2 = Chest.FindChest(x, num);
		localPlayer.cursorItemIconID = -1;
		if (num2 < 0)
		{
			localPlayer.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
		}
		else
		{
			if (Main.chest[num2].name != "")
			{
				localPlayer.cursorItemIconText = Main.chest[num2].name;
			}
			else
			{
				localPlayer.cursorItemIconText = DefaultContainerName(tile.TileFrameX, tile.TileFrameY).ToString();
			}
			if (localPlayer.cursorItemIconText == DefaultContainerName(tile.TileFrameX, tile.TileFrameY).ToString())
			{
				localPlayer.cursorItemIconID = ModContent.ItemType<AAModClassic.Items.Blocks.Terra.TerraDresser>();
				localPlayer.cursorItemIconText = "";
			}
		}
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		if (Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY > 0)
		{
			localPlayer.cursorItemIconID = ItemID.FamiliarShirt;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 48, 32, ModContent.ItemType<AAModClassic.Items.Blocks.Terra.TerraDresser>(), 1, false, 0, false, false);
		Chest.DestroyChest(i, j);
	}
}
