using AAModClassic._Content.Acropolis.__Hardmode.NPCs;
using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.GripsOfDiscord.GripOfAbyssalWrath;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.GripsOfDiscord.GripOfBlazingFury;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.FuryAshe;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.FuryAshe.Shenling;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.WrathHaruka;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Ashe;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Ashe.AshenDragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Haruka;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed;
using AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened.AwakenedLung;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Daybringer;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Nightcrawler;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.CrossMod.CalamityMod
{
    public class CalamityGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public float CalamityDR = 1f;

        public override void SetDefaults(NPC npc)
        {
            if (ModSupport.GetMod("CalamityMod") != null)
			{
                if(npc.type == ModContent.NPCType<Athena>()) CalamityDR = 0.8f;
                if(npc.type == ModContent.NPCType<OlympianDragon>()) CalamityDR = 0.8f;

                if(npc.type == ModContent.NPCType<GreedHead>()) CalamityDR = 0.8f;

                if(npc.type == ModContent.NPCType<AnubisA>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<CursedMinionCircle>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<CursedScarab>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<CursedLocust>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<Naddaha>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<HorusSentry>()) CalamityDR = 0.65f;

                if(npc.type == ModContent.NPCType<Ashe>()) CalamityDR = 0.6f;
                if(npc.type == ModContent.NPCType<AshenDragonHead>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<FlameVortex>()) CalamityDR = 0.7f;

                if(npc.type == ModContent.NPCType<Haruka>()) CalamityDR = 0.6f;

                if(npc.type == ModContent.NPCType<AkumaA>()) CalamityDR = 0.4f;
                if(npc.type == ModContent.NPCType<AkumaHead>()) CalamityDR = 0.4f;
                if(npc.type == ModContent.NPCType<AwakenedLungHead>()) CalamityDR = 0.4f;
                //if(npc.type == ModContent.NPCType<AncientLung>()) CalamityDR = 0.4f;

                if(npc.type == ModContent.NPCType<AthenaA>()) CalamityDR = 0.7f;
                if(npc.type == ModContent.NPCType<Seraph>()) CalamityDR = 0.7f;
                if(npc.type == ModContent.NPCType<SeraphA>()) CalamityDR = 0.7f;

                if(npc.type == ModContent.NPCType<DaybringerHead>()) CalamityDR = 0.4f;
                if(npc.type == ModContent.NPCType<NightcrawlerHead>()) CalamityDR = 0.4f;
                if(npc.type == ModContent.NPCType<NightCloud>()) CalamityDR = 0.6f;
                
                if(npc.type == ModContent.NPCType<GreedAHead>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<OreConstruct>()) CalamityDR = 0.7f;

                if(npc.type == ModContent.NPCType<RajahRabbitA>()) CalamityDR = 0.6f;

                if(npc.type == ModContent.NPCType<AbyssGrip>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<BlazeGrip>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<FuryAshe>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<WrathHaruka>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<ShenDoragon>()) CalamityDR = 0.15f;
                if(npc.type == ModContent.NPCType<ShenDoragonA>()) CalamityDR = 0.1f;
                if(npc.type == ModContent.NPCType<FuryAsheOrbiter>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<ShenlingHead>()) CalamityDR = 0.15f;

                if(npc.type == ModContent.NPCType<YamataABody>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataAHead>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataAHeadFake>()) CalamityDR = 0.5f;
                //if(npc.type == ModContent.NPCType<YamataAHeadF1>()) CalamityDR = 0.5f;
                //if(npc.type == ModContent.NPCType<YamataAHeadF2>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataBody>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataHead>()) CalamityDR = 0.5f;
                //if(npc.type == ModContent.NPCType<YamataHeadF>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataHeadFake1>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataHeadFake2>()) CalamityDR = 0.5f;

                if(npc.type == ModContent.NPCType<ZeroEcho>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroMini>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroA>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroGenocideCannon>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroNeutralizer>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroNovaFocus>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroOmegaVolley>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroRealityCannon>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroRiftShredder>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<SearcherZero>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroGigataser>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroBrokenWeapon>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroVoidStar>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<Zero>()) CalamityDR = 0.5f;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (ModSupport.GetMod("CalamityMod") != null)
			{
                if (npc.realLife > 0 && Main.npc[npc.realLife].GetGlobalNPC<CalamityGlobalNPC>().CalamityDR < 1f) CalamityDR = Main.npc[npc.realLife].GetGlobalNPC<CalamityGlobalNPC>().CalamityDR;
            }
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
			{
                if (npc.type >= NPCID.Count && npc.ModNPC.Mod == AAMod.instance && npc.boss)
                {
                    bool revenge = (bool)calamity.Call("GetDifficultyActive", "revengeance");
                    bool Death = (bool)calamity.Call("GetDifficultyActive", "death");
                    if(!NPC.downedMoonlord)
                    {
                        modifiers.IncomingDamageMultiplier *= (1.1f + (revenge? 0.2f:0f) + (Death? 0.3f:0f));
                    }
                    else
                    {
                        modifiers.IncomingDamageMultiplier *= (1.2f + (revenge? 0.4f:0f) + (Death? 0.6f:0f));
                    }
                }
            }
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (ModSupport.GetMod("CalamityMod") != null)
			{
                if (npc.type >= NPCID.Count && npc.boss && npc.ModNPC.Mod == AAMod.instance)
                {
                    if (item.type > ItemID.Celeb2 && item.ModItem.Mod == ModLoader.GetMod("CalamityMod"))
                    {
                        modifiers.TargetDamageMultiplier *= CalamityDR * (NPC.downedPlantBoss? 0.8f : 1f) * (NPC.downedMoonlord? 0.7f : 1f);
                    }
                }
                if (npc.type >= NPCID.Count && npc.boss && npc.ModNPC.Mod == ModLoader.GetMod("CalamityMod"))
                {
                    if (item.type > ItemID.Celeb2 && item.ModItem.Mod == AAMod.instance)
                    {
                        modifiers.TargetDamageMultiplier *= (NPC.downedPlantBoss? 1.25f : 1f) * (NPC.downedMoonlord? 1.42f : 1f);
                    }
                }
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{
            if (ModSupport.GetMod("CalamityMod") != null)
			{
                if (npc.type >= NPCID.Count && npc.boss && npc.ModNPC.Mod == AAMod.instance)
                {
                    if (projectile.type >= ProjectileID.Count && projectile.ModProjectile.Mod == ModSupport.GetMod("CalamityMod"))
                    {
                        modifiers.TargetDamageMultiplier *= CalamityDR * (NPC.downedPlantBoss? 0.8f : 1f) * (NPC.downedMoonlord? 0.7f : 1f);
                    }
                }
                if (npc.type >= NPCID.Count && npc.boss && npc.ModNPC.Mod == ModSupport.GetMod("CalamityMod"))
                {
                    if (projectile.type >= ProjectileID.Count && projectile.ModProjectile.Mod == AAMod.instance)
                    {
                        modifiers.TargetDamageMultiplier *= (NPC.downedPlantBoss? 1.25f : 1f) * (NPC.downedMoonlord? 1.42f : 1f);
                    }
                }
            }
		}
    }

    public class CalamityGlobalProjectile : GlobalProjectile
    {
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
		{
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                if (projectile.hostile && !projectile.friendly && projectile.type >= ProjectileID.Count && projectile.ModProjectile.Mod == AAMod.instance)
                {
                    bool revenge = (bool)calamity.Call("GetDifficultyActive", "revengeance");
                    bool Death = (bool)calamity.Call("GetDifficultyActive", "death");
                    if (!NPC.downedMoonlord)
                    {
                        modifiers.IncomingDamageMultiplier *= (1.1f + (revenge ? 0.2f : 0f) + (Death ? 0.3f : 0f));
                    }
                    else
                    {
                        modifiers.IncomingDamageMultiplier *= (1.2f + (revenge ? 0.4f : 0f) + (Death ? 0.6f : 0f));
                    }
                }
            }
		}
    }
}
