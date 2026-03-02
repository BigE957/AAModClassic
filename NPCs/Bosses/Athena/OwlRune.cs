using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.NPCs.Enemies.Sky;

namespace AAModClassic.NPCs.Bosses.Athena
{
    public class OwlRune : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.alpha = 255;
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.damage = Main.expertMode ? 50 : 84;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.width = 82;
            NPC.height = 82;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.scale = .001f;
            NPC.friendly = false;
            NPC.damage = 50;
        }

        public override void AI()
        {
            if (NPC.ai[1] == 0)
            {
                NPC.alpha -= 5;
                NPC.scale += .019f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0]++;
                    if (NPC.ai[0] >= 51)
                    {
                        NPC.alpha = 0;
                        NPC.scale = 1;
                        NPC.ai[0] = 0;
                        NPC.ai[1] = 1;
                        NPC.netUpdate = true;
                    }
                }
            }
            else if (NPC.ai[1] == 1)
            {
                if (NPC.alpha <= 0)
                {
                    NPC.alpha = 0;
                }
                if (NPC.scale > 1)
                {
                    NPC.scale = 1;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0]++;
                    if (NPC.ai[0] >= 300)
                    {
                        NPC.ai[0] = 0;
                        NPC.ai[1] = 2;
                        NPC.netUpdate = true;
                    }
                }
                NPC.TargetClosest();
                if (NPC.ai[2]++ == 15)
                {
                    Projectile.NewProjectile(NPC.position, new Vector2(8f, 8f), ModContent.ProjectileType<SeraphFeather>(), 0, 0);
                    Projectile.NewProjectile(NPC.position, new Vector2(-8f, 8f), ModContent.ProjectileType<SeraphFeather>(), 0, 0);
                    Projectile.NewProjectile(NPC.position, new Vector2(-8f, -8f), ModContent.ProjectileType<SeraphFeather>(), 0, 0);
                    Projectile.NewProjectile(NPC.position, new Vector2(8f, -8f), ModContent.ProjectileType<SeraphFeather>(), 0, 0);
                }
                else if (NPC.ai[2] >= 30)
                {
                    Projectile.NewProjectile(NPC.position, new Vector2(0f, 8f), ModContent.ProjectileType<SeraphFeather>(), 0, 0);
                    Projectile.NewProjectile(NPC.position, new Vector2(-8f, 0f), ModContent.ProjectileType<SeraphFeather>(), 0, 0);
                    Projectile.NewProjectile(NPC.position, new Vector2(0f, -8f), ModContent.ProjectileType<SeraphFeather>(), 0, 0);
                    Projectile.NewProjectile(NPC.position, new Vector2(8f, 0f), ModContent.ProjectileType<SeraphFeather>(), 0, 0);
                    NPC.ai[2] = 0;
                }
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[0]++;
                    if (NPC.ai[0] >= 51)
                    {
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
                NPC.alpha += 5;
                NPC.scale -= .019f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(sb, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}