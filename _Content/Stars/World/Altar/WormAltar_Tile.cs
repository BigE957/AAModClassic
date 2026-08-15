
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Stars._PostMoonlord.Items.Quest;
using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms;

namespace AAModClassic._Content.Stars.World.Altar
{
    public class WormAltar_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            DustType = DustID.Dirt;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Worm Altar");
            AddMapEntry(new Color(150, 100, 0), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AnimationFrameHeight = 72;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<WormSpawn>()))
            {
                frame = 1;
            }
            else
            {
                frame = 0;
            }
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            int type = ModContent.ItemType<WormIdol>();
            bool Worms = NPC.AnyNPCs(ModContent.NPCType<WormSpawn>());
            if (BasePlayer.HasItem(player, type, 1) && !Worms)
            {
                if (AADowned.StarActive && AADowned.GravActive)
                {
                    for (int m = 0; m < 50; m++)
                    {
                        Item item = player.inventory[m];
                        if (item != null && item.type == type && item.stack >= 1)
                        {
                            item.stack--;
                            player.QuickSpawnItem(Item.GetSource_NaturalSpawn(), ModContent.ItemType<EquinoxWorm>());
                            if (!AADowned.WormActive)
                            {
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormAltarOK"), new Color(75, 175, 255));
                                SpawnBoss(player, ModContent.NPCType<WormSpawn>(), false, player.Center);
                            }
                        }
                    }
                }
                else
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.EquinoxDialogue.WormAltar"), new Color (75, 175, 255));
                }
            }
            return true;
        }

        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 Pos = default, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            Vector2 npcCenter = Pos;
            AAModGlobalNPC.SpawnBoss(player, bossType, spawnMessage, npcCenter, overrideDisplayName, namePlural);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return AADowned.downedEquinoxWorms;
        }

        public override bool CanReplace(int i, int j, int tileTypeBeingPlaced) => AADowned.downedEquinoxWorms;

        public override bool CanExplode(int i, int j)
        {
            return AADowned.downedEquinoxWorms;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<WormIdol>();
        }
    }
}