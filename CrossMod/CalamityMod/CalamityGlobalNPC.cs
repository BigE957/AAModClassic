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
                if(npc.type == Mod.Find<ModNPC>("Athena").Type) CalamityDR = 0.8f;
                if(npc.type == Mod.Find<ModNPC>("OlympianDragon").Type) CalamityDR = 0.8f;

                if(npc.type == Mod.Find<ModNPC>("Greed").Type) CalamityDR = 0.8f;

                if(npc.type == Mod.Find<ModNPC>("ForsakenAnubis").Type) CalamityDR = 0.65f;
                if(npc.type == Mod.Find<ModNPC>("CurseCircle").Type) CalamityDR = 0.65f;
                if(npc.type == Mod.Find<ModNPC>("CursedScarab").Type) CalamityDR = 0.65f;
                if(npc.type == Mod.Find<ModNPC>("CursedLocust").Type) CalamityDR = 0.65f;
                if(npc.type == Mod.Find<ModNPC>("Naddaha").Type) CalamityDR = 0.65f;
                if(npc.type == Mod.Find<ModNPC>("HorusSentry").Type) CalamityDR = 0.65f;

                if(npc.type == Mod.Find<ModNPC>("Ashe").Type) CalamityDR = 0.6f;
                if(npc.type == Mod.Find<ModNPC>("AsheDragon").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("AsheOrbiter").Type) CalamityDR = 0.7f;

                if(npc.type == Mod.Find<ModNPC>("Haruka").Type) CalamityDR = 0.6f;

                if(npc.type == Mod.Find<ModNPC>("AkumaA").Type) CalamityDR = 0.4f;
                if(npc.type == Mod.Find<ModNPC>("Akuma").Type) CalamityDR = 0.4f;
                if(npc.type == Mod.Find<ModNPC>("AwakenedLung").Type) CalamityDR = 0.4f;
                //if(npc.type == Mod.Find<ModNPC>("AncientLung").Type) CalamityDR = 0.4f;

                if(npc.type == Mod.Find<ModNPC>("AthenaA").Type) CalamityDR = 0.7f;
                if(npc.type == Mod.Find<ModNPC>("Seraph").Type) CalamityDR = 0.7f;
                if(npc.type == Mod.Find<ModNPC>("SeraphA").Type) CalamityDR = 0.7f;

                if(npc.type == Mod.Find<ModNPC>("DaybringerHead").Type) CalamityDR = 0.4f;
                if(npc.type == Mod.Find<ModNPC>("NightcrawlerHead").Type) CalamityDR = 0.4f;
                if(npc.type == Mod.Find<ModNPC>("NCCloud").Type) CalamityDR = 0.6f;
                
                if(npc.type == Mod.Find<ModNPC>("GreedA").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("GreedMinion").Type) CalamityDR = 0.7f;

                if(npc.type == Mod.Find<ModNPC>("SupremeRajah").Type) CalamityDR = 0.6f;

                if(npc.type == Mod.Find<ModNPC>("AbyssGrip").Type) CalamityDR = 0.2f;
                if(npc.type == Mod.Find<ModNPC>("BlazeGrip").Type) CalamityDR = 0.2f;
                if(npc.type == Mod.Find<ModNPC>("FuryAshe").Type) CalamityDR = 0.2f;
                if(npc.type == Mod.Find<ModNPC>("WrathHaruka").Type) CalamityDR = 0.2f;
                if(npc.type == Mod.Find<ModNPC>("Shen").Type) CalamityDR = 0.15f;
                if(npc.type == Mod.Find<ModNPC>("ShenA").Type) CalamityDR = 0.1f;
                if(npc.type == Mod.Find<ModNPC>("FuryAsheOrbiter").Type) CalamityDR = 0.2f;
                if(npc.type == Mod.Find<ModNPC>("Shenling").Type) CalamityDR = 0.15f;

                if(npc.type == Mod.Find<ModNPC>("YamataA").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("YamataAHead").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("YamataAHeadF").Type) CalamityDR = 0.5f;
                //if(npc.type == Mod.Find<ModNPC>("YamataAHeadF1").Type) CalamityDR = 0.5f;
                //if(npc.type == Mod.Find<ModNPC>("YamataAHeadF2").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("Yamata").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("YamataHead").Type) CalamityDR = 0.5f;
                //if(npc.type == Mod.Find<ModNPC>("YamataHeadF").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("YamataHeadF1").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("YamataHeadF2").Type) CalamityDR = 0.5f;

                if(npc.type == Mod.Find<ModNPC>("ZeroEcho").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("ZeroMini").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("ZeroProtocol").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("GenocideCannon").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("Neutralizer").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("NovaFocus").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("OmegaVolley").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("RealityCannon").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("RiftShredder").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("SearcherZero").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("Taser").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("TeslaHand").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("VoidStar").Type) CalamityDR = 0.5f;
                if(npc.type == Mod.Find<ModNPC>("Zero").Type) CalamityDR = 0.5f;
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
