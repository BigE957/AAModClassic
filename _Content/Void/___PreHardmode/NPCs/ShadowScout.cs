using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.NPCs
{
    public class ShadowScout : ModNPC, IBannerNPC
    {
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shadow Scout");
            Main.npcFrameCount[NPC.type] = 12;
        }
		
		public override void SetDefaults()
		{
            NPC.noGravity = true;
            NPC.noTileCollide = true;
			NPC.aiStyle = -1;
            NPC.width = 24;
            NPC.height = 40;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.alpha = 70;
            NPC.value = 700f;
            NPC.knockBackResist = 0.7f;
            NPC.noGravity = true;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<ShadowScoutBanner>();
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

		public int frameCount = 0;
		public int frameCounter = 0;
        public int IdleTimer = 0;

		public override void PostAI()
		{
			NPC.spriteDirection = NPC.velocity.X > 0 ? -1 : 1;
		}

        public override void AI()
        {
            NPC.TargetClosest(true);
            BaseAI.AIElemental(NPC, ref NPC.ai, ref IdleTimer, null, 1, false, true, 800f, 600f, 180, 2f);

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if ((NPC.frame.Y / 44) > 4)
                {
                    NPC.defense = 100;
                    NPC.knockBackResist = 0f;
                }
                else
                {
                    NPC.defense = 5;
                    NPC.knockBackResist = 0.7f;
                }
                if (NPC.frameCounter == 0 && (NPC.frame.Y / 44) == 1)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(Main.player[NPC.target].Center) * 12f, ModContent.ProjectileType<InfinityZero_InfinityZeroShot>(), 25, 0);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 7)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
                if (NPC.frame.Y > frameHeight * 11)
                {
                    NPC.frame.Y = 0;
                }
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, NPC.Center - screenPos, NPC.frame, AAColor.ZeroShield, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DoomiteScrap>()));
        }
    }
}