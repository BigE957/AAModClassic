using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Mire
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Soulsucker : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Soulsucker");
			Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
		{
            NPC.aiStyle = NPCAIStyleID.Slime;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.width = 64;
			NPC.height = 64;
			NPC.damage = 70;
			NPC.defense = 30;
			NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 6000f;
            NPC.lavaImmune = false;
            NPC.knockBackResist = 0.5f;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("SoulsuckerBanner").Type;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 3)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
                if (NPC.frame.Y >= frameHeight * 2)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void AI()
        {
            BaseAI.AIFlier(NPC, ref NPC.ai, false, 0.2f, 0.1f, 3, 2.5f, true, 250);
            NPC.rotation = NPC.velocity.X * 0.05f;
            if (NPC.velocity.X > 0)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = -1;
            }

            if (Collision.SolidCollision(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, NPC.height))
            {
                if (NPC.alpha < 100)
                {
                    NPC.alpha += 2;
                }
            }
            else
            {
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 3;
                }
            }
        }

        public override Color? GetAlpha(Color drawColor)
        {
            if (Collision.SolidCollision(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, NPC.height))
            {
                return Color.PaleVioletRed;
            }
            else
            {
                return drawColor;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            LeadingConditionRule expertRule = new(new Conditions.IsExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TerrorSoul>(), 2));
            expertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TerrorSoul>(), 1, 1, 2));

            npcLoot.Add(notExpertRule);
            npcLoot.Add(expertRule);
        }
    }
}
