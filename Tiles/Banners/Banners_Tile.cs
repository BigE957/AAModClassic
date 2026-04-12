
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Tiles.Banners
{
    public class Banners_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
			TileObjectData.newTile.CoordinatePadding = 0;		
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);			
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
			DustType = -1;
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Banner");
			AddMapEntry(new Color(13, 88, 130), name);			
        }

		/*IF YOU ARE ADDING A BANNER:
		 1. add an entry for it's style in GetStyleForName, increasing the style count by one. 0 is used for 'broken' banners so you can easily tell if the banner didn't load right.
		 2. add an entry for it's name in GetBannerName, including spaces. The id used should match the one GetStyleForName gives.
		 */

		public static int GetStyleForName(string name)
		{
			switch(name)
			{
				case "Scout": return 1;
				case "FeralMonster": return 2;
				case "MiniDjinn": return 3;
				case "BlazePhoenix": return 4;
				case "ChaoticDawn": return 5;
				case "Dragron": return 6;
				case "Flamebrute": return 7;
				case "Flamespitter": return 8;
				case "InfernalSlime": return 9;
				case "InfernoSalamander": return 10;
				case "Wyrm": return 11;
				case "Wyrmling": return 12;
				case "Wyvern": return 13;
				case "Magmalgam": return 14;
				case "ChaoticTwilight": return 15;
				case "Kappa": return 16;
				case "Miregron": return 17;
				case "MireSlime": return 18;
				case "Mosster": return 19;
				case "Newt": return 20;
				case "AbyssClaw": return 21;
				case "BlazeClaw": return 22;
				case "ChaosDragon": return 23;
				case "CyberClaw": return 24;
				case "DragonClaw": return 25;
				case "ElderDragon": return 26;
				case "HydraClaw": return 27;
				case "MadnessBat": return 28;
				case "MadnessSlime": return 29;
				case "DimensionDiver": return 30;
				case "RiftShark": return 31;
				case "Squid": return 32;
				case "Snake": return 33; //snow serpent
				case "Bladon": return 34;
				case "TerraDeadshot": return 35;
				case "TerraWarlock": return 36;
				case "TerraWizard": return 37;
				case "Null": return 38;
				case "Searcher": return 39;
				case "Toxitoad": return 40;
                case "SagittariusMini": return 41;
            }
			return 0; //broken banner
		}

		public static string GetBannerName(int frameX, bool spaces = false)
        {
			string dropName = null;
			int style = frameX / 16;
			switch (style)
			{
				case 1: dropName = "Void Scout"; break;
				case 2: dropName = "Feral Monster"; break;		
				case 3: dropName = "Mini Djinn"; break;	
				case 4: dropName = "Blaze Phoenix"; break;		
				case 5: dropName = "Chaotic Dawn"; break;
				case 6: dropName = "Dragron"; break;		
				case 7: dropName = "Flame brute"; break;	
				case 8: dropName = "Flamespitter"; break;	
				case 9: dropName = "Infernal Slime"; break;
				case 10: dropName = "Inferno Salamander"; break;
				case 11: dropName = "Wyrm"; break;
				case 12: dropName = "Wyrmling"; break;		
				case 13: dropName = "Wyvern"; break;	
				case 14: dropName = "Magmalgam"; break;	
				case 15: dropName = "Chaotic Twilight"; break;
				case 16: dropName = "Kappa"; break;		
				case 17: dropName = "Miregron"; break;	
				case 18: dropName = "Mire Slime"; break;					
				case 19: dropName = "Mosster"; break;
				case 20: dropName = "Newt"; break;	
				case 21: dropName = "Abyss Claw"; break;
				case 22: dropName = "Blaze Claw"; break;		
				case 23: dropName = "Chaos Dragon"; break;	
				case 24: dropName = "Cyber Claw"; break;	
				case 25: dropName = "Dragon Claw"; break;
				case 26: dropName = "Elder Dragon"; break;		
				case 27: dropName = "Hydra Claw"; break;	
				case 28: dropName = "Madness Bat"; break;					
				case 29: dropName = "Madness Slime"; break;
				case 30: dropName = "Dimension Diver"; break;	
				case 31: dropName = "Rift Shark"; break;
				case 32: dropName = "Squid"; break;		
				case 33: dropName = "Snake"; break;	
				case 34: dropName = "Bladon"; break;	
				case 35: dropName = "Terra Deadshot"; break;
				case 36: dropName = "Terra Warlock"; break;		
				case 37: dropName = "Terra Wizard"; break;	
				case 38: dropName = "Null"; break;					
				case 39: dropName = "Searcher"; break;
				case 40: dropName = "Toxitoad"; break;
                case 41: dropName = "Shadow Scout"; break;
				case 42: dropName = "Purity Squid"; break;
				case 43: dropName = "Terra Probe"; break;
				case 44: dropName = "Terra Watcher"; break;
				case 45: dropName = "Purity Weaver"; break;
				case 46: dropName = "Purity Sphere"; break;
				case 47: dropName = "Terra Serpent"; break;
				case 48: dropName = "Purity Crawler"; break;
				case 49: dropName = "Terra Squid"; break;
				case 50: dropName = "Terra Crawler"; break;
				case 51: dropName = "Terra Sphere"; break;
				case 52: dropName = "Terra Weaver"; break;
				case 53: dropName = "Vortex"; break;
				case 54: dropName = "Stone Searcher"; break;
				case 55: dropName = "Night Guard"; break;
				case 56: dropName = "Sun Watcher"; break;
				case 57: dropName = "Lung"; break;
				case 58: dropName = "Mushbug"; break;
				case 59: dropName = "ShroomJelly"; break;
				case 60: dropName = "MushroomCrab"; break;
				case 61: dropName = "MushroomZombie"; break;
				case 62: dropName = "InfernoSandShark"; break;
				case 63: dropName = "MireSandShark"; break;
				case 64: dropName = "Mire Ghoul"; break;
				case 65: dropName = "Miresquito"; break;
				case 66: dropName = "Fog Angler"; break;
				case 67: dropName = "Soulsucker"; break;
				case 68: dropName = "Terra Squire"; break;
				case 69: dropName = "Seraph"; break;
				case 70: dropName = "Tiny Toad"; break;
				case 71: dropName = "FungusFrog"; break;
				case 72: dropName = "Infernal Ghoul"; break;
				case 73: dropName = "Mire Skulker"; break;
				case 74: dropName = "Scavenger"; break;
            }
			if(spaces) //fix for display names
			{
				switch (style)
				{
					case 1: dropName = "Scout"; break;
					//case 2: dropName = "Feral Monster"; break;
					case 3: dropName = "Mini Djinn"; break;	
					case 4: dropName = "Blaze Phoenix"; break;			
					case 5: dropName = "Chaotic Dawn"; break;
					case 6: dropName = "Dragron"; break;		
					case 7: dropName = "Flame brute"; break;	
					case 8: dropName = "Flamespitter"; break;	
					case 9: dropName = "Infernal Slime"; break;
					case 10: dropName = "Inferno Salamander"; break;
					case 11: dropName = "Wyrm"; break;
					case 12: dropName = "Wyrmling"; break;		
					//case 13: dropName = "Wyvern"; break;	
					case 14: dropName = "MagmaSwimmer"; break;	
					case 15: dropName = "Chaotic Twilight"; break;
					case 16: dropName = "Kappa"; break;		
					case 17: dropName = "Miregron"; break;	
					case 18: dropName = "Mire Slime"; break;					
					case 19: dropName = "Mosster"; break;
					case 20: dropName = "Newt"; break;	
					case 21: dropName = "Abyss Claw"; break;
					case 22: dropName = "Blaze Claw"; break;		
					case 23: dropName = "Chaos Dragon"; break;	
					//case 24: dropName = "Cyber Claw"; break;	
					case 25: dropName = "Dragon Claw"; break;
					case 26: dropName = "Elder Dragon"; break;		
					case 27: dropName = "Hydra Claw"; break;	
					case 28: dropName = "Madness Bat"; break;					
					case 29: dropName = "Madness Slime"; break;
					//case 30: dropName = "Dimension Diver"; break;	
					//case 31: dropName = "Rift Shark"; break;
					//case 32: dropName = "Squid"; break;		
					case 33: dropName = "SnakeHead"; break;	
					case 34: dropName = "Bladon"; break;	
					case 35: dropName = "Terra Deadshot"; break;
					case 36: dropName = "Terra Warlock"; break;		
					case 37: dropName = "Terra Wizard"; break;	
					case 38: dropName = "Null"; break;					
					case 39: dropName = "Searcher"; break;
					case 40: dropName = "Toxitoad"; break;
					case 41: dropName = "SagittariusMini"; break;
					case 42: dropName = "Purity Squid"; break;
					case 43: dropName = "Terra Probe"; break;
					case 44: dropName = "Terra Watcher"; break;
					case 45: dropName = "Purity Weaver"; break;
					case 46: dropName = "Purity Sphere"; break;
					case 47: dropName = "Terra Serpent"; break;
					case 48: dropName = "Purity Crawler"; break;
					case 49: dropName = "Minion1"; break;
					case 50: dropName = "Minion2"; break;
					case 51: dropName = "Minion3"; break;
					case 52: dropName = "Minion4"; break;
					case 53: dropName = "Vortex"; break;
					case 54: dropName = "Searcher1"; break;
					case 55: dropName = "Nightguard"; break;
					case 56: dropName = "Sunwatcher"; break;
					case 57: dropName = "Lung"; break;
					case 58: dropName = "Mushbug"; break;
					case 59: dropName = "ShroomJelly"; break;
					case 60: dropName = "MushroomCrab"; break;
					case 61: dropName = "MushroomZombie"; break;
					case 62: dropName = "InfernoSandShark"; break;
					case 63: dropName = "MireSandShark"; break;
					case 64: dropName = "Mire Ghoul"; break;
					case 65: dropName = "Miresquito"; break;
					case 66: dropName = "Fog Angler"; break;
					case 67: dropName = "Soulsucker"; break;
					case 68: dropName = "Terra Squire"; break;
					case 69: dropName = "Seraph"; break;
					case 70: dropName = "SmallToad"; break;
					case 71: dropName = "SmallFrog"; break;
					case 72: dropName = "Infernal Ghoul"; break;
					case 73: dropName = "Mire Skulker"; break;
					case 74: dropName = "Scavenger"; break;
				}				
			}
			if(!string.IsNullOrEmpty(dropName)) dropName = dropName.Replace(" ", null);
			return dropName;
        }

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            string dropName = GetBannerName(frameX);
            if (!string.IsNullOrEmpty(dropName))
            {
                Item.NewItem(Item.GetSource_NaturalSpawn(), x * 16, y * 16, 16, 16, Mod.Find<ModItem>(dropName + "Banner").Type, 1, false, -1, false);
            }
        }

        public override void NearbyEffects(int x, int y, bool closer)
        {
            if (closer)
            {
				string name = GetBannerName(Main.tile[x, y].TileFrameX, true);
				if(name == null) return;

                Player player = Main.LocalPlayer;	
				player.HasNPCBannerBuff(Mod.Find<ModNPC>(name).Type);
				//TODO: readd him when the time is right. and when the time is right, remove this line of code
				// bcuz this only exists due to names being similar so when name is changed itll be fiine
				//if(name == "MushroomZombie") player.HasNPCBannerBuff(ModContent.NPCType<MushroomZombie2>());
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
	}
}

