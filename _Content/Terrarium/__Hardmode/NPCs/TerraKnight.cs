using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic._Content.Terrarium.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.__Hardmode.NPCs
{
    public class TerraKnight : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Knight");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.SolarSolenian];
		}

		public override void SetDefaults()
		{
            NPC.lifeMax = 900;
            NPC.defense = 40;
            NPC.damage = 90;
            NPC.width = 22;
            NPC.height = 56;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            AnimationType = NPCID.SolarSolenian;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.BladonBanner>();
        }
        public override void AI()
        {
            NPC.reflectsProjectiles = false;
            NPC.takenDamageMultiplier = 1f;
            int num27 = 6;
            int num28 = 10;
            float scaleFactor3 = 16f;
            if (NPC.ai[2] > 0f)
            {
                NPC.ai[2] -= 1f;
            }
            if (NPC.ai[2] == 0f)
            {
                if ((Main.player[NPC.target].Center.X < NPC.Center.X && NPC.direction < 0 || Main.player[NPC.target].Center.X > NPC.Center.X && NPC.direction > 0) && Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
                {
                    NPC.ai[2] = -1f;
                    NPC.netUpdate = true;
                    NPC.TargetClosest(true);
                }
            }
            else
            {
                if (NPC.ai[2] < 0f && NPC.ai[2] > -num27)
                {
                    NPC.ai[2] -= 1f;
                    NPC.velocity.X = NPC.velocity.X * 0.9f;
                    return;
                }
                if (NPC.ai[2] == -num27)
                {
                    NPC.ai[2] -= 1f;
                    NPC.TargetClosest(true);
                    Vector2 vec = NPC.DirectionTo(Main.player[NPC.target].Top + new Vector2(0f, -30f));
                    if (vec.HasNaNs())
                    {
                        vec = Vector2.Normalize(new Vector2(NPC.spriteDirection, -1f));
                    }
                    NPC.velocity = vec * scaleFactor3;
                    NPC.netUpdate = true;
                    return;
                }
                if (NPC.ai[2] < -num27)
                {
                    NPC.ai[2] -= 1f;
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.ai[2] = 60f;
                    }
                    else if (NPC.ai[2] < -(float)num27 - num28)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                        if (NPC.velocity.Y > 24f)
                        {
                            NPC.velocity.Y = 24f;
                        }
                    }
                    NPC.reflectsProjectiles = true;
                    NPC.takenDamageMultiplier = 3f;
                    if (NPC.justHit)
                    {
                        NPC.ai[2] = 60f;
                        NPC.netUpdate = true;
                    }
                    return;
                }
            }
            int num36 = 60;

            bool flag5 = false;
            bool flag6 = true;
            bool flag7 = false;
            bool flag8 = true;
            if (NPC.ai[2] > 0f)
            {
                flag8 = false;
            }
            if (!flag7 && flag8)
            {
                if (NPC.velocity.Y == 0f && (NPC.velocity.X > 0f && NPC.direction < 0 || NPC.velocity.X < 0f && NPC.direction > 0))
                {
                    flag5 = true;
                }
                if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= num36 || flag5)
                {
                    NPC.ai[3] += 1f;
                }
                else if (Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f)
                {
                    NPC.ai[3] -= 1f;
                }
                if (NPC.ai[3] > num36 * 10)
                {
                    NPC.ai[3] = 0f;
                }
                if (NPC.justHit)
                {
                    NPC.ai[3] = 0f;
                }
                if (NPC.ai[3] == num36)
                {
                    NPC.netUpdate = true;
                }
            }

            if (NPC.ai[3] < num36)
            {
                NPC.TargetClosest(true);
            }
            float num75 = 5f;
            float num76 = 0.25f;
            float scaleFactor5 = 0.7f;
            num75 = 6f;
            num76 = 0.15f;
            scaleFactor5 = 0.85f;
            if (NPC.velocity.X < -num75 || NPC.velocity.X > num75)
            {
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity *= scaleFactor5;
                }
            }
            else if (NPC.velocity.X < num75 && NPC.direction == 1)
            {
                NPC.velocity.X = NPC.velocity.X + num76;
                if (NPC.velocity.X > num75)
                {
                    NPC.velocity.X = num75;
                }
            }
            else if (NPC.velocity.X > -num75 && NPC.direction == -1)
            {
                NPC.velocity.X = NPC.velocity.X - num76;
                if (NPC.velocity.X < -num75)
                {
                    NPC.velocity.X = -num75;
                }
            }

            if (Main.player[NPC.target].Center.Y + 100f < NPC.position.Y && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            { 
                {
                    NPC.velocity.Y = -5f;
                    NPC.ai[2] = 1f;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.localAI[2] += 1f;
                    if (NPC.localAI[2] >= 360 + Main.rand.Next(360) && NPC.Distance(Main.player[NPC.target].Center) < 400f && Math.Abs(NPC.DirectionTo(Main.player[NPC.target].Center).Y) < 0.5f && Collision.CanHitLine(NPC.Center, 0, 0, Main.player[NPC.target].Center, 0, 0))
                    {
                        NPC.localAI[2] = 0f;
                        Vector2 vector13 = NPC.Center + new Vector2(NPC.direction * 30, 2f);
                        Vector2 vector14 = NPC.DirectionTo(Main.player[NPC.target].Center) * 7f;
                        if (vector14.HasNaNs())
                        {
                            vector14 = new Vector2(NPC.direction * 8, 0f);
                        }
                        int num85 = Main.expertMode ? 50 : 75;
                        for (int num86 = 0; num86 < 4; num86++)
                        {
                            Vector2 vector15 = vector14 + Utils.RandomVector2(Main.rand, -0.8f, 0.8f);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), vector13.X, vector13.Y, vector15.X, vector15.Y, ProjectileID.VortexLaser, num85, 1f, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
            }
            
            bool flag23 = false;
            if (NPC.velocity.Y == 0f)
            {
                int num167 = (int)(NPC.position.Y + NPC.height + 7f) / 16;
                int num168 = (int)NPC.position.X / 16;
                int num169 = (int)(NPC.position.X + NPC.width) / 16;
                for (int num170 = num168; num170 <= num169; num170++)
                {
                    if (Main.tile[num170, num167] == null)
                    {
                        return;
                    }
                    if (Main.tile[num170, num167].HasUnactuatedTile && Main.tileSolid[Main.tile[num170, num167].TileType])
                    {
                        flag23 = true;
                        break;
                    }
                }
            }
            
            if (NPC.velocity.Y >= 0f)
            {
                int num171 = 0;
                if (NPC.velocity.X < 0f)
                {
                    num171 = -1;
                }
                if (NPC.velocity.X > 0f)
                {
                    num171 = 1;
                }
                Vector2 position2 = NPC.position;
                position2.X += NPC.velocity.X;
                int num172 = (int)((position2.X + NPC.width / 2 + (NPC.width / 2 + 1) * num171) / 16f);
                int num173 = (int)((position2.Y + NPC.height - 1f) / 16f);
                if (!(Main.tile[num172, num173] == null &&
                    Main.tile[num172, num173 - 1] == null &&
                    Main.tile[num172, num173 - 2] == null &&
                    Main.tile[num172, num173 - 3] == null &&
                    Main.tile[num172, num173 + 1] == null &&
                    Main.tile[num172 - num171, num173 - 3] == null))
                {
                    if (num172 * 16 < position2.X + NPC.width && num172 * 16 + 16 > position2.X && (Main.tile[num172, num173].HasUnactuatedTile && !Main.tile[num172, num173].TopSlope && !Main.tile[num172, num173 - 1].TopSlope && Main.tileSolid[Main.tile[num172, num173].TileType] && !Main.tileSolidTop[Main.tile[num172, num173].TileType] || Main.tile[num172, num173 - 1].IsHalfBlock && Main.tile[num172, num173 - 1].HasUnactuatedTile) && (!Main.tile[num172, num173 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 1].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 1].TileType] || Main.tile[num172, num173 - 1].IsHalfBlock && (!Main.tile[num172, num173 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 4].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 4].TileType])) && (!Main.tile[num172, num173 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 2].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 2].TileType]) && (!Main.tile[num172, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172, num173 - 3].TileType] || Main.tileSolidTop[Main.tile[num172, num173 - 3].TileType]) && (!Main.tile[num172 - num171, num173 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num172 - num171, num173 - 3].TileType]))
                    {
                        float num174 = num173 * 16;
                        if (Main.tile[num172, num173].IsHalfBlock)
                        {
                            num174 += 8f;
                        }
                        if (Main.tile[num172, num173 - 1].IsHalfBlock)
                        {
                            num174 -= 8f;
                        }
                        if (num174 < position2.Y + NPC.height)
                        {
                            float num175 = position2.Y + NPC.height - num174;
                            float num176 = 16.1f;
                            if (num175 <= num176)
                            {
                                NPC.gfxOffY += NPC.position.Y + NPC.height - num174;
                                NPC.position.Y = num174 - NPC.height;
                                if (num175 < 9f)
                                {
                                    NPC.stepSpeed = 1f;
                                }
                                else
                                {
                                    NPC.stepSpeed = 2f;
                                }
                            }
                        }
                    }
                }
            }
            if (!flag23 && flag6)
            {
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
            }
            
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraKnightGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraKnightGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraKnightGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraKnightGore4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraKnightGore5").Type, 1f);
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.MeleeDust>();
                int dust2 = ModContent.DustType<Dusts.MeleeDust>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override void OnKill()
        {
            if (Main.rand.NextBool(40))
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<TerraPrism>());
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Terrablaze_Buff>(), 300);
        }
    }
}
