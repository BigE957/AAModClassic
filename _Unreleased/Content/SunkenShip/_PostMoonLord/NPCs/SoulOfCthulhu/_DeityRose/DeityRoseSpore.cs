using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose
{
    public class DeityRoseSpore : ModNPC
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ei'Lor's Spore");
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 18;
            NPC.aiStyle = NPCAIStyleID.Spore;
            NPC.damage = 70;
            NPC.defense = 0;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
        }

        public override void AI()
        {
            if (NPC.timeLeft > 5)
            {
                NPC.timeLeft = 5;
            }
            NPC.noTileCollide = true;
            NPC.velocity.Y = NPC.velocity.Y + 0.02f;
            if (NPC.velocity.Y < 0f && !Main.expertMode)
            {
                NPC.velocity.Y = NPC.velocity.Y * 0.99f;
            }
            if (NPC.velocity.Y > 1f)
            {
                NPC.velocity.Y = 1f;
            }
            NPC.TargetClosest(true);
            if (NPC.position.X + NPC.width < Main.player[NPC.target].position.X)
            {
                if (NPC.velocity.X < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.98f;
                }
                if (Main.expertMode && NPC.velocity.X < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.98f;
                }
                NPC.velocity.X = NPC.velocity.X + 0.1f;
                if (Main.expertMode)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.1f;
                }
            }
            else if (NPC.position.X > Main.player[NPC.target].position.X + Main.player[NPC.target].width)
            {
                if (NPC.velocity.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.98f;
                }
                if (Main.expertMode && NPC.velocity.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.98f;
                }
                NPC.velocity.X = NPC.velocity.X - 0.1f;
                if (Main.expertMode)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.1f;
                }
            }
            if (NPC.velocity.X > 5f || NPC.velocity.X < -5f)
            {
                NPC.velocity.X = NPC.velocity.X * 0.97f;
            }
            NPC.rotation = NPC.velocity.X * 0.2f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                int num440 = 0;
                while (num440 < hit.Damage / (double)NPC.lifeMax * 100.0)
                {
                    if (Main.rand.NextBool(3))
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), hit.HitDirection, -1f, 0, default, 1f);
                    }
                    num440++;
                }
                return;
            }
            for (int num441 = 0; num441 < 150; num441++)
            {
                if  (Main.rand.NextBool(3))
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.CthulhuDust>(), 2 * hit.HitDirection, -2f, 0, default, 1f);
                }
            }
        }
    }
}