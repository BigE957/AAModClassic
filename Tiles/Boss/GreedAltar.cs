using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.NPCs.Bosses.Greed;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Boss
{
    public class GreedAltar : ModTile
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
            disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Greed").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("GreedSpawn").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("GreedA").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("GreedTransition").Type))
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
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Greed").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("GreedSpawn").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("GreedA").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("GreedTransition").Type))
            {
                return true;
            }
            Player player = Main.LocalPlayer;
            int type = ModContent.ItemType<Items.BossSummons.GoldenGrub>();
            if (BasePlayer.HasItem(player, type, 1))
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        if (AAWorld.downedGreed)
                        {
                            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Greed>(), true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.Greed"));
                        }
                        else
                        {
                            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<GreedSpawn>(), false, new Vector2(i * 16, (j * 16) - 200), Language.GetTextValue("Mods.AAMod.Common.Greed"));
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

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = Mod.Find<ModItem>("GoldenGrub").Type;
        }
    }
}