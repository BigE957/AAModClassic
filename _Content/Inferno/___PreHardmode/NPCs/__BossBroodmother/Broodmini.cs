using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother
{
    public class Broodmini : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Broodmini");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 56;
            NPC.aiStyle = -1;
            NPC.damage = 30;
            NPC.defense = 0;
            NPC.lifeMax = 75;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.3f;
            NPC.value = 0f;
            NPC.npcSlots = 0.1f;
            AnimationType = NPCID.MothronSpawn;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoBiome>().Type };
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
			bool isDead = NPC.life <= 0;		
            if (isDead)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                //Gore.NewGore(NPC.GetSource_OnHurt(null), NPC.position, NPC.velocity, Mod.Find<ModGore>("BroodminiGore1").Type, 1f);
                //Gore.NewGore(NPC.GetSource_OnHurt(null), NPC.position, NPC.velocity, Mod.Find<ModGore>("BroodminiGore2").Type, 1f);
                //Gore.NewGore(NPC.GetSource_OnHurt(null), NPC.position, NPC.velocity, Mod.Find<ModGore>("BroodminiGore3").Type, 1f);
                //Gore.NewGore(NPC.GetSource_OnHurt(null), NPC.position, NPC.velocity, Mod.Find<ModGore>("BroodminiGore3").Type, 1f);
                //Gore.NewGore(NPC.GetSource_OnHurt(null), NPC.position, NPC.velocity, Mod.Find<ModGore>("BroodminiGore3").Type, 1f);
            }
			for (int m = 0; m < (isDead ? 35 : 6); m++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead? 2f : 1.5f);
			}			
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScorchedScale>(), 1, 2, 4));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IncineriteOre>(), 1, 5, 6));
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.rand.NextBool(2) || Main.expertMode && Main.rand.Next(0) == 0)       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 120));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }

        public override void AI()
        {
            NPC.noTileCollide = false;
            NPC.knockBackResist = 0.4f * Main.GameModeInfo.KnockbackToEnemiesMultiplier;
            NPC.noGravity = true;
            NPC.rotation = (NPC.rotation * 9f + NPC.velocity.X * 0.1f) / 10f;
            if (Main.player[NPC.target].GetModPlayer<ZAAPlayer>().ZoneInferno == false)
            {
                if (NPC.timeLeft > 5)
					NPC.timeLeft = 5;
                NPC.velocity.Y = NPC.velocity.Y - 0.2f;
                if (NPC.velocity.Y < -8f)
                {
                    NPC.velocity.Y = -8f;
                }
                NPC.noTileCollide = true;
                return;
            }
            if (NPC.ai[0] == 0f || NPC.ai[0] == 1f)
            {
                for (int num1328 = 0; num1328 < 200; num1328++)
                {
                    if (num1328 != NPC.whoAmI && Main.npc[num1328].active && Main.npc[num1328].type == NPC.type)
                    {
                        Vector2 value55 = Main.npc[num1328].Center - NPC.Center;
                        if (value55.Length() < NPC.width + NPC.height)
                        {
                            value55.Normalize();
                            value55 *= -0.1f;
                            NPC.velocity += value55;
                            Main.npc[num1328].velocity -= value55;
                        }
                    }
                }
            }
            if (NPC.target < 0 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
                Vector2 vector209 = Main.player[NPC.target].Center - NPC.Center;
                if (Main.player[NPC.target].dead || vector209.Length() > 3000f)
                {
                    NPC.ai[0] = -1f;
                }
            }
            else
            {
                Vector2 vector210 = Main.player[NPC.target].Center - NPC.Center;
                if (NPC.ai[0] > 1f && vector210.Length() > 1000f)
                {
                    NPC.ai[0] = 1f;
                }
            }
            if (NPC.ai[0] == -1f)
            {
                Vector2 value56 = new Vector2(0f, -8f);
                NPC.velocity = (NPC.velocity * 9f + value56) / 10f;
                NPC.noTileCollide = true;
                NPC.dontTakeDamage = true;
                return;
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.TargetClosest(true);
                NPC.spriteDirection = NPC.direction;
                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.velocity.X * (-NPC.oldVelocity.X * 0.5f);
                    if (NPC.velocity.X > 4f)
                    {
                        NPC.velocity.X = 4f;
                    }
                    if (NPC.velocity.X < -4f)
                    {
                        NPC.velocity.X = -4f;
                    }
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.velocity.Y * (-NPC.oldVelocity.Y * 0.5f);
                    if (NPC.velocity.Y > 4f)
                    {
                        NPC.velocity.Y = 4f;
                    }
                    if (NPC.velocity.Y < -4f)
                    {
                        NPC.velocity.Y = -4f;
                    }
                }
                Vector2 value57 = Main.player[NPC.target].Center - NPC.Center;
                if (value57.Length() > 800f)
                {
                    NPC.ai[0] = 1f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                }
                else if (value57.Length() > 200f)
                {
                    float scaleFactor20 = 5.5f + value57.Length() / 100f + NPC.ai[1] / 15f;
                    float num1329 = 40f;
                    value57.Normalize();
                    value57 *= scaleFactor20;
                    NPC.velocity = (NPC.velocity * (num1329 - 1f) + value57) / num1329;
                }
                else if (NPC.velocity.Length() > 2f)
                {
                    NPC.velocity *= 0.95f;
                }
                else if (NPC.velocity.Length() < 1f)
                {
                    NPC.velocity *= 1.05f;
                }
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= 90f)
                {
                    NPC.ai[1] = 0f;
                    NPC.ai[0] = 2f;
                    return;
                }
            }
            else
            {
                if (NPC.ai[0] == 1f)
                {
                    NPC.collideX = false;
                    NPC.collideY = false;
                    NPC.noTileCollide = true;
                    NPC.knockBackResist = 0f;
                    if (NPC.target < 0 || !Main.player[NPC.target].active || Main.player[NPC.target].dead)
                    {
                        NPC.TargetClosest(true);
                    }
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    else if (NPC.velocity.X > 0f)
                    {
                        NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = (NPC.rotation * 9f + NPC.velocity.X * 0.08f) / 10f;
                    Vector2 value58 = Main.player[NPC.target].Center - NPC.Center;
                    if (value58.Length() < 300f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[0] = 0f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                    }
                    NPC.ai[2] += 0.0166666675f;
                    float scaleFactor21 = 5.5f + NPC.ai[2] + value58.Length() / 150f;
                    float num1330 = 35f;
                    value58.Normalize();
                    value58 *= scaleFactor21;
                    NPC.velocity = (NPC.velocity * (num1330 - 1f) + value58) / num1330;
                    return;
                }
                if (NPC.ai[0] == 2f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    else if (NPC.velocity.X > 0f)
                    {
                        NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = (NPC.rotation * 7f + NPC.velocity.X * 0.1f) / 8f;
                    NPC.knockBackResist = 0f;
                    NPC.noTileCollide = true;
                    Vector2 vector211 = Main.player[NPC.target].Center - NPC.Center;
                    vector211.Y -= 8f;
                    float scaleFactor22 = 9f;
                    float num1331 = 8f;
                    vector211.Normalize();
                    vector211 *= scaleFactor22;
                    NPC.velocity = (NPC.velocity * (num1331 - 1f) + vector211) / num1331;
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    else
                    {
                        NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.ai[1] += 1f;
                    if (NPC.ai[1] > 10f)
                    {
                        NPC.velocity = vector211;
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.direction = -1;
                        }
                        else
                        {
                            NPC.direction = 1;
                        }
                        NPC.ai[0] = 2.1f;
                        NPC.ai[1] = 0f;
                        return;
                    }
                }
                else if (NPC.ai[0] == 2.1f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    else if (NPC.velocity.X > 0f)
                    {
                        NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.velocity *= 1.01f;
                    NPC.knockBackResist = 0f;
                    NPC.noTileCollide = true;
                    NPC.ai[1] += 1f;
                    int num1332 = 45;
                    if (NPC.ai[1] > num1332)
                    {
                        if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                        {
                            NPC.ai[0] = 0f;
                            NPC.ai[1] = 0f;
                            NPC.ai[2] = 0f;
                            return;
                        }
                        if (NPC.ai[1] > num1332 * 2)
                        {
                            NPC.ai[0] = 1f;
                            NPC.ai[1] = 0f;
                            NPC.ai[2] = 0f;
                            return;
                        }
                    }
                }
            }
        }
		
		
		public static Color GetGlowAlpha()
		{
			return ColorUtils.COLOR_GLOWPULSE;
		}

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			BaseDrawing.DrawTexture(spriteBatch, ModContent.Request<Texture2D>(Texture + "_Glow").Value, 0, NPC, GetGlowAlpha());
        }		
		
    }
}