using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.NPCs.Bosses.Athena;
using AAModClassic.NPCs.Bosses.Athena.Olympian;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Boss
{
    public class AcropolisAltar : ModTile
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
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Athena").Type))
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
            Texture2D glowTex = Mod.GetTexture("Glowmasks/AcropolisAltar_Glow");

            int frameY = tile != null && tile.HasTile ? tile.TileFrameY + (Main.tileFrame[Type] * 54) : 0;
            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.TileFrameX, frameY, false, false, false, null, White);
        }

        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            int type = ModContent.ItemType<Items.BossSummons.Owl>();
            bool Athena = NPC.AnyNPCs(ModContent.NPCType<Athena>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaFlee>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaDefeat>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaA>());
            if (BasePlayer.HasItem(player, type, 1) && !Athena)
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        SpawnBoss(player, ModContent.NPCType<Athena>(), player.Center, Language.GetTextValue("Mods.AAMod.Common.Athena"));
                    }
                }
            }
            return true;
        }

        // SpawnBoss(player, mod.NPCType("MyBoss"), true, 0, 0, "DerpyBoi 2", false);
        public static void SpawnBoss(Player player, int bossType, Vector2 Pos = default, string name = "", bool seen = true)
        {
            Vector2 npcCenter = Pos + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * Main.rand.Next(2) == 0 ? -1 : 1, -800f);
            for (int a = 0; a < 8; a++)
            {
                Dust.NewDust(npcCenter, 152, 114, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0);
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(bossType))
                {
                    return;
                }

                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)npcCenter.X, (int)npcCenter.Y, bossType);
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate = true;

                ((Athena)Main.npc[npcID].ModNPC).Seen = seen;

                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", name), 175, 75, 255, false);
                    }
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(
                        NetworkText.FromKey("Announcement.HasAwoken", new object[] { NetworkText.FromLiteral(name) }),
                        new Color(175, 75, 255)
                    );
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, true, (int)npcCenter.X, (int)npcCenter.Y, name, false);
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
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = Mod.Find<ModItem>("Owl").Type;
        }
    }
}