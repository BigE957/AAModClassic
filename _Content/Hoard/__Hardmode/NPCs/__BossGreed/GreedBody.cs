using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.BossStandard;
using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Tools;
using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Weapons;
using AAModClassic._Content.Hoard.__Hardmode.Items.Materials;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed
{
    [AutoloadBossHead]
    public class GreedBody : GreedHead
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Greed");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 22;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
            NPC.alpha = 255;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (!truehit)
            {
                modifiers.TargetDamageMultiplier *= .3f;
            }
            else
            {
                truehit = false;
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ModContent.ProjectileType<OreChunk>() && projectile.ai[1] == ItemID.GoldOre && NPC.ai[2] == 6)
            {
                damage += (int)(NPC.defense * (Main.expertMode ? .75 : .5f));
                truehit = true;
            }
        }

        public override bool PreAI()
        {
            NPC.defense = Def();
            Vector2 chasePosition = Main.npc[(int)NPC.ai[1]].Center;
            Vector2 directionVector = chasePosition - NPC.Center;
            NPC.spriteDirection = directionVector.X > 0f ? 1 : -1;
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[3]].active || Main.npc[(int)NPC.ai[3]].type != ModContent.NPCType<GreedHead>())
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    NPC.spriteDirection = 1;

                }
                else
                {
                    NPC.spriteDirection = -1;
                }

                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }

            int tileX = (int)(NPC.position.X / 16f) - 1;
            int tileCenterX = (int)(NPC.Center.X / 16f) + 2;
            int tileY = (int)(NPC.position.Y / 16f) - 1;
            int tileCenterY = (int)(NPC.Center.Y / 16f) + 2;
            if (tileX < 0) { tileX = 0; }
            if (tileCenterX > Main.maxTilesX) { tileCenterX = Main.maxTilesX; }
            if (tileY < 0) { tileY = 0; }
            if (tileCenterY > Main.maxTilesY) { tileCenterY = Main.maxTilesY; }
            for (int tX = tileX; tX < tileCenterX; tX++)
            {
                for (int tY = tileY; tY < tileCenterY; tY++)
                {
                    Tile checkTile = WorldGenUtils.GetTileSafely(tX, tY);
                    if (checkTile != null && (checkTile.HasUnactuatedTile && (Main.tileSolid[checkTile.TileType] || Main.tileSolidTop[checkTile.TileType] && checkTile.TileFrameY == 0) || checkTile.LiquidAmount > 64))
                    {
                        Vector2 tPos;
                        tPos.X = tX * 16;
                        tPos.Y = tY * 16;
                        if (NPC.position.X + NPC.width > tPos.X && NPC.position.X < tPos.X + 16f && NPC.position.Y + NPC.height > tPos.Y && NPC.position.Y < tPos.Y + 16f)
                        {
                            if (Main.rand.NextBool(100) && checkTile.HasUnactuatedTile)
                            {
                                WorldGen.KillTile(tX, tY, true, true, false);
                            }
                        }
                    }
                }
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if (NPC.alpha <= 0)
            {
                NPC.alpha = 0;
                return false;
            }
            else
            {
                for (int spawnDust = 0; spawnDust < 4; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GoldCoin, 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
                NPC.alpha -= 3;
                return false;
            }
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<GreedHead>()))
            {
                return false;
            }
            NPC.active = false;
            return true;
        }

        public int Def()
        {
            switch ((int)NPC.ai[2])
            {
                case 0:
                    return NPC.defense = 6; //Copper
                case 1:
                    return NPC.defense = 7; //tin
                case 2:
                    return NPC.defense = 9; //Iron
                case 3:
                    return NPC.defense = 11; //Lead
                case 4:
                    return NPC.defense = 13; //Silver
                case 5:
                    return NPC.defense = 15; //Tungsten
                case 6:
                    return NPC.defense = 16; //Gold
                case 7:
                    return NPC.defense = 20; //Platinum
                case 8:
                    return NPC.defense = 19; //Shadow
                case 9:
                    return NPC.defense = 19; //Crimson
                case 10:
                    return NPC.defense = 15; //Abyssium
                case 11:
                    return NPC.defense = 21; //Incinerite
                case 12:
                    return NPC.defense = 25; //Hellstone
                case 13:
                    return NPC.defense = 26; //Cobalt
                case 14:
                    return NPC.defense = 32; //Paladium
                case 15:
                    return NPC.defense = 37; //Mythril
                case 16:
                    return NPC.defense = 42; //Oricalcum
                case 17:
                    return NPC.defense = 50; //Adamantite
                case 18:
                    return NPC.defense = 49; //Titanium
                case 19:
                    return NPC.defense = 50; //Hallowed
                case 20:
                    return NPC.defense = 56; //Chlorophyte
                default:
                    return NPC.defense = 30; //Tail
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            NPC.position.Y += NPC.height * 0.5f;

            spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            if (Main.LocalPlayer.findTreasure)
            {
                Color color = drawColor;
                byte b2 = 200;
                byte b3 = 170;
                if (color.R < b2)
                {
                    color.R = b2;
                }
                if (color.G < b3)
                {
                    color.G = b3;
                }
                color.A = Main.mouseTextColor;
                spriteBatch.Draw(glow, NPC.Center - screenPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }

            NPC.position.Y -= NPC.height * 0.5f;
            return false;
        }
    }
}