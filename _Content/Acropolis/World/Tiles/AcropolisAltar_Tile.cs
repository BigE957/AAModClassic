using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena;
using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Unofficial;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Chat;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace AAModClassic._Content.Acropolis.World.Tiles
{
    public class AcropolisAltar_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            DustType = DustID.BlueCrystalShard;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Direction = TileObjectDirection.None;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Owl Altar");
            AddMapEntry(new Color(0, 50, 150), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Athena>()))
            {
                frame = 1;
            }
            else
            {
                frame = 0;
            }
        }

        public static Color White(Color color)
        {
            return AAColor.Sky;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            int frameY = tile != null && tile.HasTile ? tile.TileFrameY + (Main.tileFrame[Type] * 54) : 0;
            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.TileFrameX, frameY, false, false, false, null, White);
        }

        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            int type = ModContent.ItemType<OwlStatue>();
            bool Athena = NPC.AnyNPCs(ModContent.NPCType<Athena>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaFlee>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaDefeat>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaA>());
            if (BasePlayer.HasItem(player, type, 1) && !Athena)
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        if(item.consumable)
                            item.stack--;

                        Vector2 npcCenter = player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * (Main.rand.NextBool() ? -1 : 1), -800f);
                        for (int a = 0; a < 8; a++)
                            Dust.NewDust(npcCenter, 152, 114, ModContent.DustType<FeatherDust>(), Main.rand.Next(-1, 2), 1, 0);

                        AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Athena>(), true, npcCenter, Language.GetTextValue("Mods.AAModClassic.Common.Athena"));
                        break;
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
            player.cursorItemIconID = ModContent.ItemType<OwlStatue>();
        }
    }
}