using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;


namespace AAMod.NPCs.Enemies.BiomeGuardians
{
    public class Fishron : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fishron");
			Main.npcFrameCount[NPC.type] = 7;
		}

		public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 36;
            NPC.aiStyle = -1;
            NPC.damage = 80;
            NPC.defense = 12;
            NPC.lifeMax = 210;
            NPC.HitSound = SoundID.NPCHit27;
            NPC.DeathSound = SoundID.NPCDeath30;
            NPC.knockBackResist = 0.5f;
            NPC.value = 2000f;
        }

        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public override void AI()
        {
            if (Main.rand.Next(1000) == 0)
            {
                SoundEngine.PlaySound(SoundID.Zombie9, NPC.position);
            }
            NPC.noGravity = true;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[3]++;
                if (NPC.ai[3] > 400)
                {
                    NPC.ai[2] = 1;
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
            }
            if (NPC.ai[2] == 0)
            {
                if (!NPC.noTileCollide)
                {
                    if (NPC.collideX)
                    {
                        NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
                        if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
                        {
                            NPC.velocity.X = 2f;
                        }
                        if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
                        {
                            NPC.velocity.X = -2f;
                        }
                    }
                    if (NPC.collideY)
                    {
                        NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
                        if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
                        {
                            NPC.velocity.Y = 1f;
                        }
                        if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
                        {
                            NPC.velocity.Y = -1f;
                        }
                    }
                }
                NPC.TargetClosest(true);
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    if (NPC.ai[1] > 0f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[1] = 0f;
                        NPC.ai[0] = 0f;
                        NPC.netUpdate = true;
                    }
                }
                else if (NPC.ai[1] == 0f)
                {
                    NPC.ai[0] += 1f;
                }
                if (NPC.ai[0] >= 300f)
                {
                    NPC.ai[1] = 1f;
                    NPC.ai[0] = 0f;
                    NPC.netUpdate = true;
                }
                if (NPC.ai[1] == 0f)
                {
                    NPC.alpha = 0;
                    NPC.noTileCollide = false;
                }
                else
                {
                    NPC.wet = false;
                    NPC.alpha = 200;
                    NPC.noTileCollide = true;
                }
                NPC.rotation = NPC.velocity.Y * 0.1f * NPC.direction;
                NPC.TargetClosest(true);
                if (NPC.direction == -1 && NPC.velocity.X > -4f && NPC.position.X > Main.player[NPC.target].position.X + Main.player[NPC.target].width)
                {
                    NPC.velocity.X = NPC.velocity.X - 0.08f;
                    if (NPC.velocity.X > 4f)
                    {
                        NPC.velocity.X = NPC.velocity.X - 0.04f;
                    }
                    else if (NPC.velocity.X > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - 0.2f;
                    }
                    if (NPC.velocity.X < -4f)
                    {
                        NPC.velocity.X = -4f;
                    }
                }
                else if (NPC.direction == 1 && NPC.velocity.X < 4f && NPC.position.X + NPC.width < Main.player[NPC.target].position.X)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.08f;
                    if (NPC.velocity.X < -4f)
                    {
                        NPC.velocity.X = NPC.velocity.X + 0.04f;
                    }
                    else if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + 0.2f;
                    }
                    if (NPC.velocity.X > 4f)
                    {
                        NPC.velocity.X = 4f;
                    }
                }
                if (NPC.directionY == -1 && NPC.velocity.Y > -2.5 && NPC.position.Y > Main.player[NPC.target].position.Y + Main.player[NPC.target].height)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                    if (NPC.velocity.Y > 2.5)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.05f;
                    }
                    else if (NPC.velocity.Y > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.15f;
                    }
                    if (NPC.velocity.Y < -2.5)
                    {
                        NPC.velocity.Y = -2.5f;
                    }
                }
                else if (NPC.directionY == 1 && NPC.velocity.Y < 2.5 && NPC.position.Y + NPC.height < Main.player[NPC.target].position.Y)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 0.1f;
                    if (NPC.velocity.Y < -2.5)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + 0.05f;
                    }
                    else if (NPC.velocity.Y < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                    }
                    if (NPC.velocity.Y > 2.5)
                    {
                        NPC.velocity.Y = 2.5f;
                    }
                }
            }
            else
            {
                NPC.rotation = NPC.velocity.Y * 0.1f * NPC.direction;
                NPC.TargetClosest(true);
                if (SelectPoint)
                {
                    float Point = 500 * NPC.direction;
                    MovePoint = Main.player[NPC.target].Center + new Vector2(Point, 500f);
                    SelectPoint = false;
                    NPC.netUpdate = true;
                }
                MeleeMovement(MovePoint);
                NPC.netUpdate = true;
                if (Vector2.Distance(MovePoint, NPC.Center) < 20 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                }
            }
        }


        public void MeleeMovement(Vector2 point)
        {
            float MeleeSpeed = 16f;
            if (MeleeSpeed < 16f)
            {
                MeleeSpeed += .5f;
            }
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < MeleeSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / MeleeSpeed);
            }
            if (length < 200f)
            {
                MeleeSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                MeleeSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                MeleeSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= MeleeSpeed;
            NPC.velocity *= velMultiplier;
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            if (NPC.life > 0)
            {
                int num589 = 0;
                while (num589 < damage / NPC.lifeMax * 50.0)
                {
                    int num590 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 0, default, 1.5f);
                    Main.dust[num590].velocity *= 1.5f;
                    Main.dust[num590].noGravity = true;
                    num589++;
                }
                return;
            }
            for (int num591 = 0; num591 < 10; num591++)
            {
                int num592 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num592].velocity *= 2f;
                Main.dust[num592].noGravity = true;
            }
            for (int num593 = 0; num593 < 4; num593++)
            {
                int num594 = Gore.NewGore(new Vector2(NPC.position.X, NPC.position.Y + NPC.height / 2 - 10f), new Vector2(hitDirection, 0f), 99, NPC.scale);
                Main.gore[num594].velocity *= 0.3f;
            }
        }

		public override void OnKill()
		{
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Materials.OceanWhisper>(), 1, false, 0, false, false);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/Fishron_Glow");

            if (NPC.ai[2] == 1f)
            {
                BaseDrawing.DrawAfterimage(spritebatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 6, false, 0f, 0f, Color.Cyan);
            }

            BaseDrawing.DrawTexture(spritebatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, dColor, true);

            if (NPC.ai[2] == 1f)
            {
                BaseDrawing.DrawTexture(spritebatch, glowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, Color.White, true);
            }
            return false;
        }

        public static void DrawAfterimage(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, Vector2[] oldPoints, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default, float distanceScalar = 1.0F, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, bool drawCentered = false, Color? overrideColor = null)
        {
            Color lightColor = overrideColor != null ? (Color)overrideColor : BaseDrawing.GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            Vector2 velAddon = default;
            Vector2 originalpos = position;
            Vector2 offset = new Vector2(offsetX, offsetY);
            for (int m = 1; m <= imageCount; m++)
            {
                scale *= sizeScalar;
                Color newLightColor = lightColor;
                newLightColor.R = (byte)(newLightColor.R * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.G = (byte)(newLightColor.G * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.B = (byte)(newLightColor.B * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.A = (byte)(newLightColor.A * (imageCount + 3 - m) / (imageCount + 9));
                if (useOldPos)
                {
                    position = Vector2.Lerp(originalpos, m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1], distanceScalar);
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset, width, height, scale, rotation, direction, framecount, frame, newLightColor, drawCentered ? true : false);
                }
                else
                {
                    Vector2 velocity = m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1];
                    velAddon += velocity * distanceScalar;
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset - velAddon, width, height, scale, rotation, direction, framecount, frame, newLightColor, drawCentered ? true : false);
                }
            }
        }
    }
}