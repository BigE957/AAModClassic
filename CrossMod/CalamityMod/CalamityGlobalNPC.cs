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

                if(npc.type == ModContent.NPCType<Greed>()) CalamityDR = 0.8f;

                if(npc.type == ModContent.NPCType<ForsakenAnubis>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<CurseCircle>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<CursedScarab>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<CursedLocust>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<Naddaha>()) CalamityDR = 0.65f;
                if(npc.type == ModContent.NPCType<HorusSentry>()) CalamityDR = 0.65f;

                if(npc.type == ModContent.NPCType<Ashe>()) CalamityDR = 0.6f;
                if(npc.type == ModContent.NPCType<AsheDragon>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<AsheOrbiter>()) CalamityDR = 0.7f;

                if(npc.type == ModContent.NPCType<Haruka.Haruka>()) CalamityDR = 0.6f;

                if(npc.type == ModContent.NPCType<AkumaA>()) CalamityDR = 0.4f;
                if(npc.type == ModContent.NPCType<Akuma>()) CalamityDR = 0.4f;
                if(npc.type == ModContent.NPCType<AwakenedLung>()) CalamityDR = 0.4f;
                //if(npc.type == ModContent.NPCType<AncientLung>()) CalamityDR = 0.4f;

                if(npc.type == ModContent.NPCType<AthenaA>()) CalamityDR = 0.7f;
                if(npc.type == ModContent.NPCType<Seraph>()) CalamityDR = 0.7f;
                if(npc.type == ModContent.NPCType<SeraphA>()) CalamityDR = 0.7f;

                if(npc.type == ModContent.NPCType<DaybringerHead>()) CalamityDR = 0.4f;
                if(npc.type == ModContent.NPCType<NightcrawlerHead>()) CalamityDR = 0.4f;
                if(npc.type == ModContent.NPCType<NCCloud>()) CalamityDR = 0.6f;
                
                if(npc.type == ModContent.NPCType<GreedA>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<GreedMinion>()) CalamityDR = 0.7f;

                if(npc.type == ModContent.NPCType<SupremeRajah>()) CalamityDR = 0.6f;

                if(npc.type == ModContent.NPCType<AbyssGrip>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<BlazeGrip>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<FuryAshe>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<WrathHaruka>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<Shen>()) CalamityDR = 0.15f;
                if(npc.type == ModContent.NPCType<ShenA>()) CalamityDR = 0.1f;
                if(npc.type == ModContent.NPCType<FuryAsheOrbiter>()) CalamityDR = 0.2f;
                if(npc.type == ModContent.NPCType<Shenling>()) CalamityDR = 0.15f;

                if(npc.type == ModContent.NPCType<YamataA>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataAHead>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataAHeadF>()) CalamityDR = 0.5f;
                //if(npc.type == ModContent.NPCType<YamataAHeadF1>()) CalamityDR = 0.5f;
                //if(npc.type == ModContent.NPCType<YamataAHeadF2>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<Yamata>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataHead>()) CalamityDR = 0.5f;
                //if(npc.type == ModContent.NPCType<YamataHeadF>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataHeadF1>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<YamataHeadF2>()) CalamityDR = 0.5f;

                if(npc.type == ModContent.NPCType<ZeroEcho>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroMini>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<ZeroProtocol>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<GenocideCannon>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<Neutralizer>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<NovaFocus>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<OmegaVolley>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<RealityCannon>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<RiftShredder>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<SearcherZero>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<Taser>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<TeslaHand>()) CalamityDR = 0.5f;
                if(npc.type == ModContent.NPCType<VoidStar>()) CalamityDR = 0.5f;
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
