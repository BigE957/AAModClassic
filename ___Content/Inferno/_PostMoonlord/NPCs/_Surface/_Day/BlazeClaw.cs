using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria.ModLoader.Utilities;
using AAModClassic.Globals;
using AAModClassic.Items.Banners;
using AAModClassic.___Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.___Content.Inferno.Buffs;

namespace AAModClassic.___Content.Inferno._PostMoonlord.NPCs._Surface._Day
{
    public class BlazeClaw : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blaze Claw");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 24;
            NPC.friendly = false;
            NPC.damage = 90;
            NPC.defense = 40;
            NPC.lifeMax = 800;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<BlazeClawBanner>();
        }

        public override void AI()
        {
            AAAI.AIClaw(NPC, ref NPC.ai, false, true, 0.1f, 0.04f, 9f, 5f, 1f, 1f);
            if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 3.14f;
            }

            NPC.frameCounter++;
            if (NPC.frameCounter >= 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 26;
                if (NPC.frame.Y > 26 * 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (AAWorld.downedSisters)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.04f;
            }
            return 0;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaDust>());
                }
            }
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 180);
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<DaybreakIncineriteOre>());
        }
    }
}