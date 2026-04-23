using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.__Hardmode.NPCs._Surface._Day
{
    public class BlazePhoenix : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blaze Phoenix");
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
			NPC.width = 30;
			NPC.height = 30;
            NPC.aiStyle = -1;
            NPC.npcSlots = 1;
            NPC.value = Item.sellPrice(0, 1, 25, 0);
            NPC.lifeMax = 200;
            NPC.defense = 5;
            NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
			NPC.buffImmune[BuffID.OnFire] = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.damage = 70;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<BlazePhoenixBanner>();
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
			AAAI.AIShadowflameGhost(NPC, ref NPC.ai, false, 660f, 0.3f, 10f, 0.2f, 6f, 5f, 10f, 0.4f, 0.4f, 0.95f, 5f);
			NPC.spriteDirection = NPC.velocity.X > 0 ? -1 : 1;
			BaseAI.LookAt(NPC.Center + NPC.velocity, NPC, 0);
            NPC.frameCounter++;
            if (NPC.frameCounter > 3)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 76;
                if (NPC.frame.Y > 76 * 7)
                {
                    NPC.frame.Y = 0;
                }
            }
            float num1276 = 120f;
            if (NPC.localAI[0] < num1276)
            {
                NPC.localAI[0] += 1f;
                float num1279 = 1f - NPC.localAI[0] / num1276;
                float num1280 = num1279 * 20f;
                int num1281 = 0;
                while (num1281 < num1280)
                {
                    if (Main.rand.NextBool(5))
                    {
                        int num1282 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.DragonflameDust>(), 0f, 0f, 0);
                        Main.dust[num1282].alpha = 100;
                        Main.dust[num1282].velocity *= 0.3f;
                        Main.dust[num1282].velocity += NPC.velocity * 0.75f;
                        Main.dust[num1282].noGravity = true;
                    }
                    num1281++;
                }
            }
        }
		
				

        public static Color GetGlowAlpha()
        {
            return new Color(220, 150, 150) * (Main.mouseTextColor / 255f);
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonFire>(), 1, 1, 2));
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 0.8f, 1f, 4, false, 0f, 0f, GetGlowAlpha());
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, Color.White);			
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 600);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
			bool isDead = NPC.life <= 0;
            if (isDead)
            {
				for (int m = 0; m < 30; m++)
				{
					int dustID = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, 1, DustID.Torch, -NPC.velocity.X * 0.2f,
						-NPC.velocity.Y * 0.2f, 100, default, 2f);
					Main.dust[dustID].velocity *= 2f;
					dustID = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.BroodmotherDust>(), -NPC.velocity.X * 0.2f,
						-NPC.velocity.Y * 0.2f, 100, default);
					Main.dust[dustID].velocity *= 2f;
				}
            }
			for (int m = 0; m < 5; m++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, 1.3f);
			}
        }	
    }
}
