using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed;
using AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Hoard.World.Tiles
{
    public class GreedAltar_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            DustType = DustID.Gold;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Direction = TileObjectDirection.None;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Desire Altar");
            AddMapEntry(new Color(80, 50, 0), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<GreedHead>()) || NPC.AnyNPCs(ModContent.NPCType<SparkOfDesire>()) || NPC.AnyNPCs(ModContent.NPCType<GreedAHead>()) || NPC.AnyNPCs(ModContent.NPCType<GreedTransition>()))
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
            if (NPC.AnyNPCs(ModContent.NPCType<GreedHead>()) || NPC.AnyNPCs(ModContent.NPCType<SparkOfDesire>()) || NPC.AnyNPCs(ModContent.NPCType<GreedAHead>()) || NPC.AnyNPCs(ModContent.NPCType<GreedTransition>()))
            {
                return true;
            }
            Player player = Main.LocalPlayer;
            int type = ModContent.ItemType<GoldenGrub>();
            if (BasePlayer.HasItem(player, type, 1))
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        if (AADowned.downedGreed)
                        {
                            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GreedHead>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.Greed"));
                        }
                        else
                        {
                            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<SparkOfDesire>(), false, new Vector2(i * 16, (j * 16) - 200), Language.GetTextValue("Mods.AAModClassic.Common.Greed"));
                        }
                    }
                }
            }
            return true;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanReplace(int i, int j, int tileTypeBeingPlaced) => false;

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<GoldenGrub>();
        }
    }
}
