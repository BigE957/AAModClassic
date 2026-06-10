using System;
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

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraChest_Tile : ModTile
{
	private int LockFrame;

	public override void SetStaticDefaults()
	{
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSpelunker[Type] = true;
		Main.tileContainer[Type] = true;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 1200;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileOreFinderPriority[Type] = 500;
		TileID.Sets.HasOutlines[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook((Func<int, int, int, int, int, int, int>)Chest.FindEmptyChest, -1, 0, true);
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook((Func<int, int, int, int, int, int, int>)Chest.AfterPlacement_Hook, -1, 0, false);
		TileObjectData.newTile.AnchorInvalidTiles = new int[1] { 127 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile((int)Type);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Terra Chest");
		AddMapEntry(new Color(63, 160, 0), val, (Func<string, int, int, string>)MapChestName);
		base.DustType = DustID.Terra;
		TileID.Sets.DisableSmartCursor[Type] = true;
		base.AdjTiles = new int[1] { 21 };
		TileID.Sets.BasicChest[Type] = true;
		RegisterItemDrop(ModContent.ItemType<TerraChest>());
	}

    public override LocalizedText DefaultContainerName(int i, int j) => Mod.Find<ModItem>("TerraChest").DisplayName;

    public override ushort GetMapOption(int i, int j)
	{
		return (ushort)(Main.tile[i, j].TileFrameX / 36);
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool IsLockedChest(int i, int j)
	{
		return Main.tile[i, j].TileFrameX / 36 == 1;
	}

	public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
	{
		dustType = base.DustType;
		return true;
	}

	public string MapChestName(string name, int i, int j)
	{
		int num = i;
		int num2 = j;
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameX % 36 != 0)
		{
			num--;
		}
		if (tile.TileFrameY != 0)
		{
			num2--;
		}
		int num3 = Chest.FindChest(num, num2);
		if (Main.chest[num3].name == "")
		{
			return name;
		}
		return name + ": " + Main.chest[num3].name;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = 1;
	}

	public override bool CanKillTile(int i, int j, ref bool blockDamaged)
	{
		Tile tile = Main.tile[i, j];
		int num = i;
		int num2 = j;
		if (tile.TileFrameX % 36 != 0)
		{
			num--;
		}
		if (tile.TileFrameY != 0)
		{
			num2--;
		}
		return Chest.CanDestroyChest(num, num2);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Chest.DestroyChest(i, j);
	}

	public override bool RightClick(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		Main.mouseRightRelease = false;
		int num = i;
		int num2 = j;
		if (tile.TileFrameX % 36 != 0)
		{
			num--;
		}
		if (tile.TileFrameY != 0)
		{
			num2--;
		}
		if (localPlayer.sign >= 0)
		{
			SoundEngine.PlaySound(SoundID.MenuClose);
			localPlayer.sign = -1;
			Main.editSign = false;
			Main.npcChatText = "";
		}
		if (Main.editChest)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.editChest = false;
			Main.npcChatText = "";
		}
		if (localPlayer.editedChestName)
		{
			NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[localPlayer.chest].name), localPlayer.chest, 1f);
			localPlayer.editedChestName = false;
		}
		bool flag = IsLockedChest(num, num2);
		if (Main.netMode == NetmodeID.MultiplayerClient && !flag)
		{
			if (num == localPlayer.chestX && num2 == localPlayer.chestY && localPlayer.chest >= 0)
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
		else if (flag)
		{
			int num3 = ModContent.ItemType<TerraKey>();
			if (localPlayer.ConsumeItem(num3, false) && Chest.Unlock(num, num2) && Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendData(MessageID.LockAndUnlock, -1, -1, null, localPlayer.whoAmI, 1f, num, num2);
			}
		}
		else
		{
			int num4 = Chest.FindChest(num, num2);
			if (num4 >= 0)
			{
				Main.stackSplit = 600;
				if (num4 == localPlayer.chest)
				{
					localPlayer.chest = -1;
					SoundEngine.PlaySound(SoundID.MenuClose);
				}
				else
				{
					localPlayer.chest = num4;
					Main.playerInventory = true;
					Main.recBigList = false;
					localPlayer.chestX = num;
					localPlayer.chestY = num2;
					SoundEngine.PlaySound((localPlayer.chest < 0) ? SoundID.MenuOpen : SoundID.MenuClose);
				}
				Recipe.FindRecipes();
			}
		}
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		int num = i;
		int num2 = j;
		if (tile.TileFrameX % 36 != 0)
		{
			num--;
		}
		if (tile.TileFrameY != 0)
		{
			num2--;
		}
		int num3 = Chest.FindChest(num, num2);
		localPlayer.cursorItemIconID = -1;
		if (num3 < 0)
		{
			localPlayer.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
		}
		else
		{
			localPlayer.cursorItemIconText = ((Main.chest[num3].name.Length > 0) ? Main.chest[num3].name : "Terra Chest");
			if (localPlayer.cursorItemIconText == "Terra Key")
			{
				localPlayer.cursorItemIconID = ModContent.ItemType<TerraKey>();
				if (Main.tile[num, num2].TileFrameX / 36 == 1)
				{
					localPlayer.cursorItemIconID = ModContent.ItemType<TerraKey>();
				}
				localPlayer.cursorItemIconText = "";
			}
		}
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
	}

	public override void MouseOverFar(int i, int j)
	{
		MouseOver(i, j);
		Player localPlayer = Main.LocalPlayer;
		if (localPlayer.cursorItemIconText == "")
		{
			localPlayer.cursorItemIconEnabled = false;
			localPlayer.cursorItemIconID = ItemID.None;
		}
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		if (++frameCounter >= 6)
		{
			frameCounter = 0;
			if (++LockFrame >= 8)
			{
				LockFrame = 0;
			}
		}
	}
}
