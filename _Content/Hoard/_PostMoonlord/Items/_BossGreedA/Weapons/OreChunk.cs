using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic._Content.Acropolis.Projectiles;
using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Hallow.__Hardmode.Items.Materials;
using AAModClassic._Content.Hoard._PostMoonlord.Items.Materials;
using AAModClassic._Content.Hoard.Projectiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._CrossMod.CalamityMod;
using AAModClassic.Assets;
using AAModClassic.Buffs;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons
{
    public delegate void OnHitDelegate(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers);

    public struct OreProjectileData(int dustType, Action<Projectile> oreEffect = null, Action<Projectile> extraAI = null, OnHitDelegate onHit = null, Action<Projectile> onKill = null, Action<Projectile, Color> extraDraw = null, Action<Projectile> onSpawn = null)
    {
        public int DustType = dustType;
        public Action<Projectile> OreEffect = oreEffect;
        public Action<Projectile> ExtraAI = extraAI;
        public OnHitDelegate OnHit = onHit;
        public Action<Projectile> OnKill = onKill;
        public Action<Projectile, Color> ExtraDraw = extraDraw;
        public Action<Projectile> OnSpawn = onSpawn;
    }

    public static class OreProjectileUtils
    {
        public static int NewProjectile(Projectile sourceProjectile, float x, float y, float speedX, float speedY, int type, int damage, float knockback, int player = 255, float ai0 = 0f, float ai1 = 0f)
        {
            int index = Projectile.NewProjectile(sourceProjectile.GetSource_Death(), x, y, speedX, speedY, type, damage, knockback, player, ai0, ai1);
            Main.projectile[index].hostile = false;
            Main.projectile[index].friendly = true;
            Main.projectile[index].DamageType = sourceProjectile.DamageType;
            Main.projectile[index].minion = false;
            Main.projectile[index].sentry = false;
            return index;
        }

        public static int NewProjectile(Projectile sourceProjectile, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int player = 255, float ai0 = 0f, float ai1 = 0f)
        {
            int index = Projectile.NewProjectile(sourceProjectile.GetSource_Death(), position, velocity, type, damage, knockback, player, ai0, ai1);
            Main.projectile[index].hostile = false;
            Main.projectile[index].friendly = true;
            Main.projectile[index].DamageType = sourceProjectile.DamageType;
            Main.projectile[index].minion = false;
            Main.projectile[index].sentry = false;
            return index;
        }

        public static int HomeOnTarget(Projectile projectile, float maxRangeInPixels = 400f, bool canAimAtWetEnemies = true)
        {
            int selectedTarget = -1;
            float selectedDist = float.MaxValue;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || canAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= maxRangeInPixels && (selectedTarget == -1 || selectedDist > distance))
                    {
                        selectedTarget = i;
                        selectedDist = distance;
                    }
                }
            }
            return selectedTarget;
        }

        public static void TriggerOreOnSpawn(this Projectile projectile)
        {
            if (OreCannonSystem.OreData.TryGetValue((int)projectile.ai[1], out var data))
                data.OnSpawn?.Invoke(projectile);
        }
    }

    public class OreCannonSystem : ModSystem
    {
        public static readonly Dictionary<int, OreProjectileData> OreData = [];

        public override void PostSetupContent()
        {
            RegisterVanillaOres();
            RegisterAAModOres();
            RegisterCalamityOres();
        }

        public static bool TryGetOreData(Projectile projectile, out OreProjectileData data) => OreCannonSystem.OreData.TryGetValue((int)projectile.ai[1], out data);

        private static void RegisterVanillaOres()
        {
            // Copper
            OreData.Add(ItemID.CopperOre, new(DustID.Copper,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    modifiers.TargetDamageMultiplier *= 1.1f;
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.velocity *= 0.5f;
                }
            ));

            // Tin
            OreData.Add(ItemID.TinOre, new(DustID.Tin,
                onSpawn: (Projectile projectile) =>
                {
                    projectile.velocity *= 0.5f;
                    projectile.knockBack *= 1.3f;
                }
            ));

            // Iron
            OreData.Add(ItemID.IronOre, new(DustID.Iron,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.BrokenArmor, 180);
                }
            ));

            // Lead
            OreData.Add(ItemID.LeadOre, new(DustID.Lead,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.Weak, 180);
                }
            ));

            // Silver
            OreData.Add(ItemID.SilverOre, new(DustID.Silver,
                extraAI: (Projectile projectile) =>
                {
                    bool bounced = false;
                    Vector2 newVelocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true, true, 1);
                    if (newVelocity != projectile.velocity)
                        bounced = true;

                    if (bounced && ProjectileLoader.OnTileCollide(projectile, projectile.velocity))
                    {
                        projectile.velocity = -projectile.velocity;
                        projectile.penetrate--;
                    }
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.penetrate = 2;
                }
            ));

            // Tungsten
            OreData.Add(ItemID.TungstenOre, new(DustID.Tungsten,
                extraAI: (Projectile projectile) =>
                {
                    projectile.penetrate = -1;
                    projectile.GetGlobalProjectile<ImplaingProjectile>().CanImpale = true;
                    projectile.GetGlobalProjectile<ImplaingProjectile>().damagePerImpaler = 30;

                    if (projectile.ai[0] != 1f)
                        return;

                    projectile.rotation = 0;
                    projectile.tileCollide = false;

                    const int maxImpaleSeconds = 15;
                    bool shouldKill = false;
                    bool tickEffect = false;

                    projectile.localAI[0]++;
                    if (projectile.localAI[0] % 30f == 0f)
                        tickEffect = true;

                    int impaledNpcIndex = (int)projectile.localAI[1];
                    if (projectile.localAI[0] >= 60 * maxImpaleSeconds)
                        shouldKill = true;
                    else if (impaledNpcIndex < 0 || impaledNpcIndex >= Main.maxNPCs)
                        shouldKill = true;
                    else if (Main.npc[impaledNpcIndex].active && !Main.npc[impaledNpcIndex].dontTakeDamage)
                    {
                        NPC impaled = Main.npc[impaledNpcIndex];
                        projectile.Center = impaled.Center - projectile.velocity * 2f;
                        projectile.gfxOffY = impaled.gfxOffY;
                        projectile.alpha = impaled.alpha;
                        if (tickEffect)
                            impaled.HitEffect(0, 1.0);
                    }
                    else
                        shouldKill = true;

                    if (shouldKill)
                        projectile.Kill();
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(ModContent.BuffType<Impaled_Buff>(), 900);

                    Rectangle hitbox = new((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    if (projectile.owner != Main.myPlayer)
                        return;

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC n = Main.npc[i];
                        bool canHit = n.active && !n.dontTakeDamage &&
                            (projectile.friendly && (!n.friendly
                                || projectile.type == ProjectileID.RottenEgg
                                || (n.type == NPCID.Guide && projectile.owner < 255 && Main.player[projectile.owner].killGuide)
                                || (n.type == NPCID.Clothier && projectile.owner < 255 && Main.player[projectile.owner].killClothier))
                            || (projectile.hostile && n.friendly && !n.dontTakeDamageFromHostiles)) &&
                            (projectile.owner < 0 || n.immune[projectile.owner] == 0 || projectile.maxPenetrate == 1) &&
                            (n.noTileCollide || !projectile.ownerHitCheck || projectile.CanHitWithOwnBody(n));

                        if (!canHit)
                            continue;

                        bool colliding;
                        if (n.type == NPCID.SolarCrawltipedeTail)
                        {
                            Rectangle padded = n.getRect();
                            const int pad = 8;
                            padded.X -= pad;
                            padded.Y -= pad;
                            padded.Width += pad * 2;
                            padded.Height += pad * 2;
                            colliding = projectile.Colliding(hitbox, padded);
                        }
                        else
                            colliding = projectile.Colliding(hitbox, n.getRect());

                        if (!colliding)
                            continue;

                        if (n.reflectsProjectiles && projectile.CanBeReflected())
                        {
                            n.ReflectProjectile(projectile);
                            return;
                        }

                        projectile.ai[0] = 1f;
                        projectile.localAI[1] = i;
                        projectile.velocity = (n.Center - projectile.Center) * 0.75f;
                        projectile.netUpdate = true;
                        projectile.StatusNPC(i);
                        projectile.damage = 0;
                        projectile.timeLeft = 1200;
                    }
                }
            ));

            // Gold
            OreData.Add(ItemID.GoldOre, new(DustID.Gold,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.Midas, 180);
                    modifiers.FlatBonusDamage += (int)(target.defense * (Main.expertMode ? 0.75f : 0.5f));
                }
            ));

            // Platinum
            OreData.Add(ItemID.PlatinumOre, new(DustID.Platinum,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.Midas, 180);
                    if (Main.rand.NextBool(5))
                    {
                        int item = Item.NewItem(projectile.GetSource_DropAsItem(), (int)target.position.X, (int)target.position.Y, 16, 16, ItemID.SilverCoin, Main.rand.Next(15, 20), false, 0, false, false);
                        if (Main.netMode == NetmodeID.MultiplayerClient && item > 0)
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                    }
                }
            ));

            // Meteorite
            OreData.Add(ItemID.Meteorite, new(DustID.t_Meteor,
                onKill: (Projectile projectile) =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.1f);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                onSpawn: (Projectile projectile) =>
                {
                    int count = 3;
                    if (Main.rand.NextBool(3))
                        count++;

                    Player player = Main.player[projectile.owner];

                    for (int i = 0; i < count; i++)
                    {
                        Vector2 spawnPos = new(
                            player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X),
                            player.MountedCenter.Y - 600f);
                        spawnPos.X = (spawnPos.X * 10f + player.Center.X) / 11f + Main.rand.Next(-100, 101);
                        spawnPos.Y -= 150 * i;

                        float diffX = Main.mouseX + Main.screenPosition.X - spawnPos.X;
                        float diffY = Main.mouseY + Main.screenPosition.Y - spawnPos.Y;
                        if (diffY < 0f)
                            diffY *= -1f;
                        if (diffY < 20f)
                            diffY = 20f;

                        float velX = diffX + Main.rand.Next(-40, 41) * 0.03f;
                        float velY = diffY + Main.rand.Next(-40, 41) * 0.03f;
                        velX *= Main.rand.Next(75, 150) * 0.01f;
                        spawnPos.X += Main.rand.Next(-50, 51);

                        Vector2 finalVelocity = Vector2.Normalize(new Vector2(velX, velY)) * 12f;
                        OreProjectileUtils.NewProjectile(projectile, spawnPos, finalVelocity, ModContent.ProjectileType<OreChunk>(), projectile.damage, projectile.knockBack, player.whoAmI, 0f, ItemID.Meteorite);
                    }
                }
            ));

            // Demonite
            OreData.Add(ItemID.DemoniteOre, new(DustID.Demonite,
                oreEffect: (Projectile projectile) =>
                {
                    projectile.extraUpdates = 1;
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    modifiers.FlatBonusDamage += 50;
                    if (Main.rand.NextBool(5))
                        target.AddBuff(BuffID.ShadowFlame, 180);
                },
                extraDraw: (Projectile projectile, Color lightColor) =>
                {
                    int oreType = (int)projectile.ai[1];
                    Main.spriteBatch.Draw(TextureAssets.Item[oreType].Value,
                        projectile.position - Main.screenPosition,
                        null, lightColor, projectile.rotation,
                        new Vector2(TextureAssets.Item[oreType].Value.Width * 0.5f, projectile.height * 0.5f),
                        projectile.scale, SpriteEffects.None, 0f);
                }
            ));

            // Crimtane
            OreData.Add(ItemID.CrimtaneOre, new(DustID.Crimstone,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    if (Main.LocalPlayer.lifeSteal <= 0f)
                        return;
                    Main.LocalPlayer.lifeSteal -= (float)(modifiers.FinalDamage.Flat * 0.02);
                    Projectile.NewProjectile(projectile.GetSource_Death(), target.position.X, target.position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, projectile.owner, projectile.owner, (float)(modifiers.FinalDamage.Flat * 0.02));
                    if (Main.rand.NextBool(5))
                        target.AddBuff(BuffID.Confused, 180);
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.knockBack *= 1.5f;
                }
            ));

            // Hellstone
            OreData.Add(ItemID.Hellstone, new(DustID.Torch,
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Torch, 0f, 0f, 100);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                extraAI: (Projectile projectile) =>
                {
                    if (projectile.ai[0]++ > 800)
                    {
                        projectile.Kill();
                        return;
                    }
                    if (projectile.ai[0] % 20 == 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Vector2 origin = new(projectile.Center.X, projectile.Center.Y + 30f);
                            float velX = projectile.position.X - origin.X;
                            float velY = projectile.position.Y - origin.Y;
                            velX += Main.rand.Next(-20, 51);
                            velY += Main.rand.Next(20, 51);
                            velY *= 0.2f;
                            float length = (float)Math.Sqrt(velX * velX + velY * velY);
                            velX *= length;
                            velY *= length;
                            velX *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                            velY *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                            int p = OreProjectileUtils.NewProjectile(projectile, origin, new Vector2(velX, velY), Main.rand.Next(326, 329), projectile.damage, 0f, Main.myPlayer);
                            Main.projectile[p].DamageType = DamageClass.Ranged;
                        }
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.OnFire, 1200);
                    for (int i = 0; i < 7; i++)
                    {
                        Vector2 origin = new(projectile.Center.X, projectile.Center.Y + 30f);
                        float velX = projectile.position.X - origin.X;
                        float velY = projectile.position.Y - origin.Y;
                        velX += Main.rand.Next(-20, 51);
                        velY += Main.rand.Next(20, 51);
                        velY *= 0.2f;
                        float length = (float)Math.Sqrt(velX * velX + velY * velY);
                        velX *= length;
                        velY *= length;
                        velX *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        velY *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        int p = OreProjectileUtils.NewProjectile(projectile, origin, new Vector2(velX, velY), Main.rand.Next(326, 329), (int)modifiers.FinalDamage.Flat, 0f, Main.myPlayer);
                        Main.projectile[p].DamageType = DamageClass.Ranged;
                    }
                }
            ));

            // Cobalt
            OreData.Add(ItemID.CobaltOre, new(DustID.Cobalt,
                extraAI: (Projectile projectile) =>
                {
                    Vector2 newVelocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true, true, 1);
                    if (newVelocity != projectile.velocity && ProjectileLoader.OnTileCollide(projectile, projectile.velocity))
                    {
                        projectile.velocity = -projectile.velocity;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    if (projectile.tileCollide)
                        projectile.velocity = -projectile.velocity;
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.velocity *= 1.5f;
                }
            ));

            // Palladium
            OreData.Add(ItemID.PalladiumOre, new(DustID.Palladium,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    if (projectile.damage / 2 > 100f)
                        OreProjectileUtils.NewProjectile(projectile, projectile.position, -projectile.velocity, ModContent.ProjectileType<OreChunk>(), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, ItemID.PalladiumOre);
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.velocity *= 1.3f;
                }
            ));

            // Mythril
            OreData.Add(ItemID.MythrilOre, new(DustID.Mythril,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.CursedInferno, 600);
                    ChainToNearbyNPC(projectile, target);
                }
            ));

            // Orichalcum
            OreData.Add(ItemID.OrichalcumOre, new(DustID.Orichalcum,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.Ichor, 600);
                    ChainToNearbyNPC(projectile, target);
                }
            ));

            // Adamantite
            OreData.Add(ItemID.AdamantiteOre, new(DustID.Adamantite,
                extraAI: (Projectile projectile) =>
                {
                    if (projectile.velocity == Vector2.Zero)
                    {
                        projectile.Kill();
                        return;
                    }
                    if (projectile.velocity.Length() < 8f)
                        projectile.velocity = Vector2.Normalize(projectile.velocity) * 8f;

                    Vector2 newVelocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true, true, 1);
                    if (newVelocity != projectile.velocity && ProjectileLoader.OnTileCollide(projectile, projectile.velocity))
                    {
                        if (newVelocity.Y != projectile.velocity.Y)
                            projectile.velocity.Y = 0;
                        if (newVelocity.X != projectile.velocity.X)
                            projectile.velocity.X = 0;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    projectile.scale /= 1.3f;
                    projectile.width = (int)(projectile.width / 1.3);
                    projectile.height = (int)(projectile.height / 1.3);
                    projectile.damage = (int)(projectile.damage / 1.3);
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.scale *= 1.5f;
                    projectile.width *= 2;
                    projectile.height *= 2;
                    projectile.damage = (int)(projectile.damage * 1.3);
                }
            ));

            // Titanium
            OreData.Add(ItemID.TitaniumOre, new(DustID.Titanium,
                onSpawn: (Projectile projectile) =>
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 perturbedSpeed = projectile.velocity.RotatedByRandom(MathHelper.ToRadians(20));
                        OreProjectileUtils.NewProjectile(projectile, projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, projectile.type, (int)(projectile.damage * 0.8), projectile.knockBack, projectile.owner, 0, ItemID.TitaniumOre);
                    }
                }
            ));

            // Chlorophyte
            OreData.Add(ItemID.ChlorophyteOre, new(DustID.Chlorophyte,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    for (int i = 0; i < 4; i++)
                        OreProjectileUtils.NewProjectile(projectile, projectile.Center, projectile.velocity * Main.rand.Next(-3, 3) * 0.1f, 228, projectile.damage, projectile.knockBack, projectile.owner);
                    target.AddBuff(BuffID.Poisoned, 240);
                    target.AddBuff(BuffID.Venom, 240);
                },
                onKill: (Projectile projectile) =>
                {
                    for (int s = 0; s < 3; s++)
                        OreProjectileUtils.NewProjectile(projectile, projectile.position, Vector2.Zero, ModContent.ProjectileType<OreSpores>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0, s);
                }
            ));

            // Luminite
            OreData.Add(ItemID.LunarOre, new(ModContent.DustType<Dusts.LuminiteDust>(),
                oreEffect: (Projectile projectile) =>
                {
                    projectile.extraUpdates = 2;
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    if (projectile.damage / 2 > 100f)
                    {
                        Vector2 perp = Vector2.Normalize(projectile.velocity.RotatedBy(Math.PI / 2));
                        for (int dir = -1; dir <= 1; dir += 2)
                        {
                            int p = OreProjectileUtils.NewProjectile(projectile, projectile.Center + perp * 40f * dir, projectile.velocity, ModContent.ProjectileType<OreChunk>(), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, ItemID.LunarOre);
                            Main.projectile[p].scale /= 2;
                            Main.projectile[p].width /= 2;
                            Main.projectile[p].height /= 2;
                            Main.projectile[p].ai[0] = 1f;
                        }
                    }
                    if (projectile.ai[0] != 1f)
                        OreProjectileUtils.NewProjectile(projectile, projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), (int)(projectile.damage / 2.5), projectile.knockBack, projectile.owner);
                },
                onKill: (Projectile projectile) =>
                {
                    OreProjectileUtils.NewProjectile(projectile, projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), projectile.damage, projectile.knockBack, Main.myPlayer);
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.velocity *= 2;
                },
                extraDraw: (Projectile projectile, Color lightColor) =>
                {
                    int oreType = (int)projectile.ai[1];
                    Main.spriteBatch.Draw(TextureAssets.Item[oreType].Value, projectile.position - Main.screenPosition, null, lightColor, projectile.rotation, new Vector2(TextureAssets.Item[oreType].Value.Width * 0.5f, projectile.height * 0.5f), projectile.scale, SpriteEffects.None, 0f);
                }
            ));
        }

        private static void RegisterAAModOres()
        {
            // Incinerite
            OreData.Add(ModContent.ItemType<IncineriteOre>(), new(ModContent.DustType<Dusts.IncineriteDust>(),
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Torch, 0f, 0f, 100);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.OnFire, 240);
                    if (Main.rand.NextBool(5))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 origin = new(projectile.Center.X, projectile.Center.Y + 30f);
                            float velX = projectile.position.X - origin.X;
                            float velY = projectile.position.Y - origin.Y;
                            velX += Main.rand.Next(-20, 51);
                            velY += Main.rand.Next(20, 51);
                            velY *= 0.2f;
                            float length = (float)Math.Sqrt(velX * velX + velY * velY);
                            velX *= length;
                            velY *= length;
                            velX *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                            velY *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                            int p = OreProjectileUtils.NewProjectile(projectile, origin, new Vector2(velX, velY), Main.rand.Next(326, 329), (int)modifiers.FinalDamage.Flat, 0f, Main.myPlayer);
                            Main.projectile[p].DamageType = DamageClass.Ranged;
                        }
                    }
                }
            ));

            // Abyssium
            OreData.Add(ModContent.ItemType<AbyssiumOre>(), new(ModContent.DustType<Dusts.AbyssiumDust>(),
                oreEffect: (Projectile projectile) =>
                {
                    projectile.extraUpdates = 1;
                },
                extraAI: (Projectile projectile) =>
                {
                    if (projectile.ai[0]++ > 800)
                    {
                        projectile.Kill();
                        return;
                    }
                    if (projectile.ai[0] % 30 == 15)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Vector2 vel = projectile.velocity;
                            vel.Normalize();
                            vel *= Main.rand.Next(70, 91) * 0.1f;
                            vel.X += Main.rand.Next(-30, 31) * 0.04f;
                            vel.Y += Main.rand.Next(-30, 31) * 0.03f;
                            OreProjectileUtils.NewProjectile(projectile, projectile.position, vel, 523, projectile.damage, 0, Main.myPlayer, Main.rand.Next(20), 0f);
                        }
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.Venom, 180);
                },
                onKill: (Projectile projectile) =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 vel = projectile.velocity;
                        vel.Normalize();
                        vel *= Main.rand.Next(70, 91) * 0.1f;
                        vel.X += Main.rand.Next(-30, 31) * 0.04f;
                        vel.Y += Main.rand.Next(-30, 31) * 0.03f;
                        int id = OreProjectileUtils.NewProjectile(projectile, projectile.position, vel, 523, projectile.damage, 0, Main.myPlayer, Main.rand.Next(20), 0f);
                        Main.projectile[id].tileCollide = false;
                    }
                },
                extraDraw: (Projectile projectile, Color lightColor) =>
                {
                    int oreType = (int)projectile.ai[1];
                    Main.spriteBatch.Draw(TextureAssets.Item[oreType].Value, projectile.position - Main.screenPosition, null, lightColor, projectile.rotation, new Vector2(TextureAssets.Item[oreType].Value.Width * 0.5f, projectile.height * 0.5f), projectile.scale, SpriteEffects.None, 0f);
                }
            ));

            // Dynaskull
            OreData.Add(ModContent.ItemType<DynaskullFossil>(), new(FallbackDustType(ModContent.ItemType<DynaskullFossil>()),
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    if (projectile.ai[0] != 1f)
                    {
                        int projType = projectile.type;
                        for (int i = 0; i < 16; i++)
                        {
                            Vector2 shoot = new((float)Math.Sin(i * 0.125f * Math.PI), (float)Math.Cos(i * 0.125f * Math.PI));
                            shoot *= 10f;
                            int p = OreProjectileUtils.NewProjectile(projectile, projectile.position, shoot, projType, (int)(modifiers.FinalDamage.Flat / 2), 5, Main.myPlayer, 0, ModContent.ItemType<DynaskullFossil>());
                            Main.projectile[p].ai[0] = 1f;
                            Main.projectile[p].scale /= 2;
                            Main.projectile[p].width /= 2;
                            Main.projectile[p].height /= 2;
                        }
                    }
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.penetrate = 1;
                }
            ));

            // Hallowed
            OreData.Add(ModContent.ItemType<HallowedOre>(), new(DustID.Gold,
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    Player player = Main.player[projectile.owner];
                    if (projectile.ai[0] < 2f)
                        OreProjectileUtils.NewProjectile(projectile, player.Center, projectile.velocity, ModContent.ProjectileType<OreChunk>(), projectile.damage, projectile.knockBack, projectile.owner, ++projectile.ai[0], ModContent.ItemType<HallowedOre>());
                }
            ));

            // SkyCrystal
            OreData.Add(ModContent.ItemType<SkyCrystal>(), new(FallbackDustType(ModContent.ItemType<SkyCrystal>()),
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    int count = 3;
                    if (Main.rand.NextBool(3)) count++;
                    for (int i = 0; i < count; i++)
                    {
                        Vector2 spawnPos = new(
                            projectile.position.X + projectile.width * 0.5f + Main.rand.Next(201) * -projectile.direction + (projectile.Center.X - projectile.position.X),
                            projectile.Center.Y - 600f);
                        spawnPos.X = (spawnPos.X * 10f + projectile.Center.X) / 11f + Main.rand.Next(-100, 101);
                        spawnPos.Y -= 150 * i;

                        float diffX = projectile.Center.X - spawnPos.X;
                        float diffY = projectile.Center.Y - spawnPos.Y;
                        if (diffY < 0f) diffY *= -1f;
                        if (diffY < 20f) diffY = 20f;
                        diffX += Main.rand.Next(-40, 41) * 0.03f;
                        diffY += Main.rand.Next(-40, 41) * 0.03f;
                        diffX *= Main.rand.Next(75, 150) * 0.01f;
                        spawnPos.X += Main.rand.Next(-50, 51);

                        Vector2 speed = Vector2.Normalize(new Vector2(diffX, diffY)) * 12f;
                        OreProjectileUtils.NewProjectile(projectile, spawnPos, speed, ModContent.ProjectileType<SeraphFeather>(), projectile.damage, 0, projectile.owner, 0f, 1f);
                    }
                }
            ));

            // Covetite
            OreData.Add(ModContent.ItemType<CovetiteOre>(), new(FallbackDustType(ModContent.ItemType<CovetiteOre>()),
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    for (int i = 0; i < 12; i++)
                        OreProjectileUtils.NewProjectile(projectile, projectile.position + new Vector2(30f), new Vector2(Main.rand.Next(-3, 4), Main.rand.Next(-3, 10)), ModContent.ProjectileType<GreedGold>(), projectile.damage / 2, 1, projectile.owner, 0, 1);
                }
            ));

            // Darkmatter
            OreData.Add(ModContent.ItemType<DarkmatterOre>(), new(ModContent.DustType<Dusts.DarkmatterDust>(),
                extraAI: (Projectile projectile) =>
                {
                    int d = Dust.NewDust(projectile.position + projectile.velocity, projectile.width * 3, projectile.height * 3, ModContent.DustType<Dusts.DarkmatterDust>(), 0f, 0f, 200, default, 0.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 0.75f;
                    Main.dust[d].fadeIn = 1.3f;
                    Vector2 offset = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    offset.Normalize();
                    offset *= Main.rand.Next(50, 100) * 0.04f;
                    Main.dust[d].velocity = offset;
                    offset.Normalize();
                    offset *= 34f;
                    Main.dust[d].position = projectile.Center - offset;

                    if (projectile.ai[0]++ > 800)
                    {
                        projectile.Kill();
                        return;
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 circleOffset = new();
                        double angle = Main.rand.NextDouble() * Math.PI * 2;
                        circleOffset.X = (float)(Math.Sin(angle) * 200);
                        circleOffset.Y = (float)(Math.Cos(angle) * 200);
                        Dust dust = Main.dust[Dust.NewDust(projectile.Center - projectile.velocity + circleOffset, 0, 0, ModContent.DustType<Dusts.DarkmatterDust>(), 0, 0, 100, default, 1f)];
                        dust.velocity = projectile.velocity;
                        dust.noGravity = true;
                    }

                    if (projectile.ai[0] % 20 == 10)
                        for (int n = 0; n < Main.maxNPCs; n++)
                            if (!Main.npc[n].townNPC && !Main.npc[n].dontTakeDamage && Vector2.Distance(Main.npc[n].position, projectile.position) < 200f)
                                Main.player[projectile.owner].ApplyDamageToNPC(Main.npc[n], projectile.damage / 10, 0, 1, false);
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 180);
                }
            ));

            // Radium
            OreData.Add(ModContent.ItemType<RadiumOre>(), new(ModContent.DustType<Dusts.RadiumDust>(),
                oreEffect: (Projectile projectile) =>
                {
                    projectile.extraUpdates = 1;
                },
                extraAI: (Projectile projectile) =>
                {
                    projectile.ai[0]++;
                    if (projectile.ai[0] > 600)
                        projectile.ai[0] = 600;
                    else
                        projectile.damage += 4;
                    projectile.velocity += Vector2.Normalize(projectile.velocity) * 0.03f;
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.damage = (int)(projectile.damage / 1.3);
                    projectile.velocity /= 2;
                }
            ));

            // Daybreak Incinerite
            OreData.Add(ModContent.ItemType<DaybreakIncineriteOre>(), new(ModContent.DustType<Dusts.DaybreakIncineriteDust>(),
                extraAI: (Projectile projectile) =>
                {
                    if (projectile.ai[0] == 1f)
                    {
                        if (projectile.localAI[0]++ >= 15f)
                        {
                            projectile.localAI[0] = 0f;
                            OreProjectileUtils.NewProjectile(projectile, projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), projectile.damage, projectile.knockBack * 3, Main.myPlayer);
                        }
                        else if (projectile.localAI[0] <= 0f)
                            projectile.localAI[0] = 0f;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.Daybreak, 400);
                    OreProjectileUtils.NewProjectile(projectile, projectile.Center, projectile.velocity, ModContent.ProjectileType<DaybreakBlast>(), (int)(projectile.damage / 2.5), projectile.knockBack, projectile.owner);
                    projectile.ai[0] = 1f;
                },
                onKill: (Projectile projectile) =>
                {
                    OreProjectileUtils.NewProjectile(projectile, projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), projectile.damage, projectile.knockBack * 3, Main.myPlayer);
                }
            ));

            // Eventide Abyssium
            OreData.Add(ModContent.ItemType<EventideAbyssiumOre>(), new(ModContent.DustType<Dusts.YamataDust>(),
                oreEffect: (Projectile projectile) =>
                {
                    projectile.extraUpdates = 2;
                    projectile.tileCollide = false;
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.Moonraze>(), 0f, 0f, 100);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                extraAI: (Projectile projectile) =>
                {
                    if (projectile.localAI[0] == 1)
                    {
                        const int homingDelay = 20;
                        const float desiredSpeed = 60f;
                        const float lerpAmount = 20f;

                        projectile.ai[0]++;
                        if (projectile.ai[0] > homingDelay)
                        {
                            projectile.ai[0] = homingDelay;
                            int target = OreProjectileUtils.HomeOnTarget(projectile);
                            if (target != -1)
                            {
                                Vector2 desiredVelocity = projectile.DirectionTo(Main.npc[target].Center) * desiredSpeed;
                                projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / lerpAmount);
                            }
                        }
                    }
                    else if (projectile.localAI[0] >= 2)
                    {
                        projectile.ai[0]++;
                        if (projectile.ai[0] > 20)
                            projectile.localAI[0] = 1;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 400);
                    projectile.localAI[0]++;
                    if (projectile.velocity.Length() < 10f)
                        projectile.velocity = 10f * Vector2.Normalize(projectile.velocity);
                },
                onSpawn: (Projectile projectile) =>
                {
                    projectile.aiStyle = -1;
                    projectile.penetrate = 6;
                },
                extraDraw: (Projectile projectile, Color lightColor) =>
                {
                    int oreType = (int)projectile.ai[1];
                    Main.spriteBatch.Draw(TextureAssets.Item[oreType].Value, projectile.position - Main.screenPosition, null, lightColor, projectile.rotation, new Vector2(TextureAssets.Item[oreType].Value.Width * 0.5f, projectile.height * 0.5f), projectile.scale, SpriteEffects.None, 0f);
                }
            ));

            // Apocalyptite
            OreData.Add(ModContent.ItemType<ApocalyptiteOre>(), new(ModContent.DustType<Dusts.VoidDust>(),
                extraAI: (Projectile projectile) =>
                {
                    if (projectile.ai[0]++ % 40 == 20 && projectile.localAI[0] < 3)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 baseVel = projectile.velocity;
                            float ai = Main.rand.Next(100);
                            Vector2 dir = Vector2.Normalize(baseVel.RotatedByRandom(Math.PI * 2));
                            Vector2 shootVel = Vector2.Normalize(dir.RotatedByRandom(0.8)) * 14f;
                            int id = OreProjectileUtils.NewProjectile(projectile, projectile.position + projectile.velocity, shootVel * 2, ModContent.ProjectileType<Gigataser_Gigatase>(), (int)(projectile.damage * 0.02f), 0f, Main.myPlayer, dir.ToRotation(), ai);
                            Main.projectile[id].timeLeft = 30;
                        }
                        projectile.localAI[0]++;
                    }
                    if (projectile.ai[0] > 800)
                        projectile.Kill();
                },
                onKill: (Projectile projectile) =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int x = Main.rand.Next(-6, 6);
                        int y = -Main.rand.Next(3, 5);
                        int p = OreProjectileUtils.NewProjectile(projectile, projectile.position, new Vector2(x, y), ModContent.ProjectileType<AFrag>(), projectile.damage, 0, projectile.owner, 0, Main.rand.Next(23));
                        Main.projectile[p].Center = projectile.Center;
                    }
                }
            ));
        }

        private static void RegisterCalamityOres()
        {
            if (!CalamityMod.IsEnabled)
                return;

            // Aerialite
            int aerialiteType = CalamityMod.GetModItem("AerialiteOre");
            OreData.Add(aerialiteType, new(DustID.t_Slime,
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.t_Slime, 0f, 0f, 100);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        float x = target.position.X + Main.rand.Next(-400, 400);
                        float y = target.position.Y - Main.rand.Next(500, 800);
                        Vector2 spawn = new(x, y);
                        float diffX = target.Center.X - spawn.X;
                        float diffY = target.Center.Y - spawn.Y;
                        diffX += Main.rand.Next(-100, 101);
                        float length = 20f;
                        float inv = length / (float)Math.Sqrt(diffX * diffX + diffY * diffY);
                        diffX *= inv;
                        diffY *= inv;
                        int projType = CalamityMod.GetModProjectileType("StickyFeatherAero");
                        OreProjectileUtils.NewProjectile(projectile, spawn, new Vector2(diffX, diffY), projType, projectile.damage, 1f, projectile.owner);
                    }
                }
            ));

            // Cryonic
            int cryonicType = CalamityMod.GetModItem("CryonicOre");
            OreData.Add(cryonicType, new(DustID.BlueCrystalShard,
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.BlueCrystalShard, 0f, 0f, 100);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.OnFire, 240);
                    target.AddBuff(BuffID.Frostburn, 240);
                    target.AddBuff(CalamityMod.GetModBuffType("GlacialState"), 120);
                },
                onKill: (Projectile projectile) =>
                {
                    SoundEngine.PlaySound(SoundID.Item27, projectile.position);
                    float spread = 0.783f;
                    double baseAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2f;
                    double delta = spread / 8f;
                    for (int i = 0; i < 8; i++)
                    {
                        float rand1 = Main.rand.Next(1, 7);
                        float rand2 = Main.rand.Next(1, 7);
                        double angle = baseAngle + delta * (i + i * i) / 2.0 + 32f * i;
                        int p1 = OreProjectileUtils.NewProjectile(projectile, projectile.Center, new Vector2((float)(Math.Sin(angle) * 5.0), (float)(Math.Cos(angle) * 5.0) + rand1), 90, projectile.damage, 1f, projectile.owner);
                        int p2 = OreProjectileUtils.NewProjectile(projectile, projectile.Center, new Vector2((float)(-Math.Sin(angle) * 5.0), (float)(-Math.Cos(angle) * 5.0) + rand2), 90, projectile.damage, 1f, projectile.owner);
                    }
                }
            ));

            // Astral
            int astralType = CalamityMod.GetModItem("AstralOre");
            OreData.Add(astralType, new(CalamityMod.AstralChunkDust,
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, CalamityMod.AstralChunkDust, 0f, 0f, 100);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                extraAI: (Projectile projectile) =>
                {
                    if (projectile.ai[0]++ > 800)
                    {
                        projectile.Kill();
                        return;
                    }
                    if (Main.rand.NextBool(40))
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            float x = projectile.position.X + Main.rand.Next(-400, 400);
                            float y = projectile.position.Y - Main.rand.Next(500, 800);
                            Vector2 spawn = new(x, y);
                            float diffX = projectile.Center.X - spawn.X;
                            float diffY = projectile.Center.Y - spawn.Y;
                            diffX += Main.rand.Next(-100, 101);
                            float speed = 25f;
                            int projType = Main.rand.Next(3) switch
                            {
                                0 => CalamityMod.GetModProjectileType("AstralStar"),
                                1 => 92,
                                _ => 12
                            };
                            float inv = speed / (float)Math.Sqrt(diffX * diffX + diffY * diffY);
                            diffX *= inv;
                            diffY *= inv;
                            int p = OreProjectileUtils.NewProjectile(projectile, spawn, new Vector2(diffX, diffY), projType, projectile.damage, 5f, projectile.owner);
                        }
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(CalamityMod.GetModBuffType("AstralInfectionDebuff"), 360);
                    for (int j = 0; j < 6; j++)
                    {
                        float x = target.position.X + Main.rand.Next(-400, 400);
                        float y = target.position.Y - Main.rand.Next(500, 800);
                        Vector2 spawn = new(x, y);
                        float diffX = target.Center.X - spawn.X;
                        float diffY = target.Center.Y - spawn.Y;
                        diffX += Main.rand.Next(-100, 101);
                        float speed = 25f;
                        int projType = Main.rand.Next(3) switch
                        {
                            0 => CalamityMod.GetModProjectileType("AstralStar"),
                            1 => 92,
                            _ => 12
                        };
                        float inv = speed / (float)Math.Sqrt(diffX * diffX + diffY * diffY);
                        diffX *= inv;
                        diffY *= inv;
                        int p = OreProjectileUtils.NewProjectile(projectile, spawn, new Vector2(diffX, diffY), projType, projectile.damage, 5f, projectile.owner);
                        Main.projectile[p].DamageType = DamageClass.Ranged;
                        Main.projectile[p].noDropItem = true;
                    }
                }
            ));

            // Scoria
            int scoriaOre = CalamityMod.GetModItem("ScoriaOre");
            OreData.Add(scoriaOre, new(DustID.Torch,
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Torch, 0f, 0f, 100);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                extraAI: (Projectile projectile) =>
                {
                    if (projectile.ai[0]++ > 800)
                    {
                        projectile.Kill();
                        return;
                    }
                    if (Main.rand.NextBool(30))
                    {
                        int lavaChunkType = CalamityMod.GetModProjectileType("LavaChunk");
                        OreProjectileUtils.NewProjectile(projectile, projectile.Center.X + projectile.velocity.X, projectile.Center.Y + projectile.velocity.Y, 0f, 0.1f, lavaChunkType, projectile.damage, 2f, projectile.owner);
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(BuffID.OnFire, 720);
                },
                onKill: (Projectile projectile) =>
                {
                    SoundEngine.PlaySound(SoundID.Item74, projectile.position);
                    int chaosBlazeType = CalamityMod.GetModProjectileType("DeepseaBlaze");
                    OreProjectileUtils.NewProjectile(projectile, projectile.Center, Vector2.Zero, chaosBlazeType, projectile.damage / 3, 1f, projectile.owner);
                }
            ));

            // Infernal Suevite
            int charredType = CalamityMod.GetModItem("InfernalSuevite");
            OreData.Add(charredType, new(DustID.LifeDrain,
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.LifeDrain, 0f, -1f, 90, default, 3f);
                        Main.dust[d].velocity *= 2f;
                        Main.dust[d].noGravity = true;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(CalamityMod.GetModBuffType("BrimstoneFlames"), 720);
                },
                onKill: (Projectile projectile) =>
                {
                    Vector2 pos = projectile.position;
                    int hellblastType = CalamityMod.GetModProjectileType("BrimstoneHellblast");
                    for (int m = 0; m < 6; m++)
                    {
                        Vector2 vel = Vector2.Normalize(new Vector2(projectile.velocity.X + Main.rand.Next(-4, 4), projectile.velocity.Y + Main.rand.Next(-4, 4))) * Main.rand.Next(6, 12);
                        int p = OreProjectileUtils.NewProjectile(projectile, pos, vel, hellblastType, projectile.damage, 0f, projectile.owner, 1f, 0f);
                        Main.projectile[p].timeLeft = 300;
                        Main.projectile[p].tileCollide = false;
                    }
                    int count = 12;
                    float spread = MathHelper.ToRadians(30f);
                    double baseAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2f;
                    double delta = spread / count;
                    float speed = 6f;
                    int barrageType = CalamityMod.GetModProjectileType("BrimstoneBarrage");
                    for (int n = 0; n < 6; n++)
                    {
                        double angle = baseAngle + delta * (n + n * n) / 2.0 + 32f * n + 0.5 * Main.rand.NextDouble();
                        OreProjectileUtils.NewProjectile(projectile, pos, new Vector2((float)(Math.Sin(angle) * speed), (float)(Math.Cos(angle) * speed)), barrageType, projectile.damage, 0f, projectile.owner, 1f, 0f);
                        OreProjectileUtils.NewProjectile(projectile, pos, new Vector2((float)(-Math.Sin(angle) * speed), (float)(-Math.Cos(angle) * speed)), barrageType, projectile.damage, 0f, projectile.owner, 1f, 0f);
                    }
                }
            ));

            // Perennial
            int perennialType = CalamityMod.GetModItem("PerennialOre");
            OreData.Add(perennialType, new(DustID.GreenFairy,
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.GreenFairy, projectile.velocity.X * 0.2f + projectile.direction * 3, projectile.velocity.Y * 0.2f, 100, default, 0.75f);
                        Main.dust[d].noGravity = true;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    SoundEngine.PlaySound(SoundID.NPCHit1, projectile.position);
                    float spread = 0.783f;
                    double baseAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2f;
                    double delta = spread / 8f;
                    for (int i = 0; i < 4; i++)
                    {
                        float x = Main.rand.NextBool() ? projectile.Center.X + 100f : projectile.Center.X - 100f;
                        Vector2 spawn = new(x, projectile.Center.Y + Main.rand.Next(-100, 101));
                        double angle = baseAngle + delta * (i + i * i) / 2.0 + 32f * i;
                        int p1 = OreProjectileUtils.NewProjectile(projectile, spawn, new Vector2((float)(Math.Sin(angle) * 5.0), (float)(Math.Cos(angle) * 5.0)), 567, projectile.damage, 2f, projectile.owner);
                        Main.projectile[p1].DamageType = DamageClass.Ranged;
                        Main.projectile[p1].usesLocalNPCImmunity = true;
                        Main.projectile[p1].localNPCHitCooldown = 60;
                        int p2 = OreProjectileUtils.NewProjectile(projectile, spawn, new Vector2((float)(-Math.Sin(angle) * 5.0), (float)(-Math.Cos(angle) * 5.0)), 568, projectile.damage, 2f, projectile.owner);
                        Main.projectile[p2].DamageType = DamageClass.Ranged;
                        Main.projectile[p2].usesLocalNPCImmunity = true;
                        Main.projectile[p2].localNPCHitCooldown = 60;
                    }
                }/*,
                //TODO: ReaverBlast is dead in a ditch.
                onKill: (Projectile projectile) =>
                {
                    int reaverBlastType = CalamityMod.GetModProjectileType("ReaverBlast");
                    int id = OreProjectileUtils.NewProjectile(projectile, projectile.Center, Vector2.Zero, reaverBlastType, projectile.damage, 0f, projectile.owner);
                    Main.projectile[id].DamageType = DamageClass.Ranged;
                }
                */
            ));

            // Uelibloom
            int uelibloomType = CalamityMod.GetModItem("UelibloomOre");
            OreData.Add(uelibloomType, new(DustID.ChlorophyteWeapon,
                oreEffect: (Projectile projectile) =>
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.ChlorophyteWeapon, 0f, -1f, 90, default, 3f);
                        Main.dust[d].noGravity = true;
                    }
                },
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    int num = 9 + Main.rand.Next(3);
                    for (int i = 0; i < num; i++)
                    {
                        float factor = 0.025f * i;
                        float velX = projectile.velocity.X + Main.rand.Next(-25, 26) * factor;
                        float velY = projectile.velocity.Y + Main.rand.Next(-25, 26) * factor;
                        float len = projectile.velocity.Length();
                        len = 14f / len;
                        velX *= len;
                        velY *= len;
                        OreProjectileUtils.NewProjectile(projectile, Main.player[projectile.owner].position, new Vector2(velX, velY), 206, projectile.damage / 2, projectile.knockBack, projectile.owner);
                    }
                    if (!target.SpawnedFromStatue && (target.damage > 5 || target.boss) && target.lifeMax > 100 && Main.rand.NextBool(5))
                    {
                        int item = Item.NewItem(projectile.GetSource_DropAsItem(), (int)target.position.X, (int)target.position.Y,
                            16, 16, 58, 1);
                        if (Main.netMode == NetmodeID.MultiplayerClient && item > 0)
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                        if (Main.bloodMoon)
                        {
                            int orbType = CalamityMod.GetModItem("BloodOrb");
                            item = Item.NewItem(projectile.GetSource_DropAsItem(), (int)target.position.X, (int)target.position.Y, 16, 16, orbType, 1);
                            if (Main.netMode == NetmodeID.MultiplayerClient && item > 0)
                                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                        }
                    }
                },
                onKill: (Projectile projectile) =>
                {
                    int count = Main.rand.Next(2, 4);
                    for (int i = 0; i < count; i++)
                    {
                        Vector2 vel = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        while (vel.X == 0f && vel.Y == 0f)
                            vel = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                        vel.Normalize();
                        vel *= Main.rand.Next(70, 101) * 0.1f;
                        OreProjectileUtils.NewProjectile(projectile, projectile.Center, vel, 206, projectile.damage / 2, 0f, projectile.owner);
                    }
                }
            ));

            // Exodium Cluster
            int exodiumType = CalamityMod.GetModItem("ExodiumClusterOre");
            OreData.Add(exodiumType, new(FallbackDustType(exodiumType),
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    target.AddBuff(CalamityMod.GetModBuffType("Horror"), 240);
                    target.AddBuff(CalamityMod.GetModBuffType("MarkedforDeath"), 240);
                }
            ));

            // Auric
            int auricType = CalamityMod.GetModItem("AuricOre");
            OreData.Add(auricType, new(FallbackDustType(auricType),
                onHit: (Projectile projectile, NPC target, ref NPC.HitModifiers modifiers) =>
                {
                    //TODO: Wtf is an element ball, added Auric Rebuke as replacement
                    target.AddBuff(CalamityMod.GetModBuffType("AuricRebuke"), 240);
                    /*
                    float speed = Main.rand.Next(22, 30);
                    int count = 4;
                    for (int i = 0; i < count; i++)
                    {
                        Vector2 spawn = projectile.Center;
                        spawn.X = (spawn.X + projectile.Center.X) / 2f;
                        spawn.Y -= 100 * i;
                        float diffX = projectile.position.X - spawn.X;
                        float diffY = projectile.position.Y - spawn.Y;
                        float len = (float)Math.Sqrt(diffX * diffX + diffY * diffY);
                        len = speed / len;
                        diffX *= len;
                        diffY *= len;
                        diffX += Main.rand.Next(-360, 361) * 0.02f;
                        diffY += Main.rand.Next(-360, 361) * 0.02f;
                        int projType = CalamityMod.GetModProjectileType("ElementBall");
                        OreProjectileUtils.NewProjectile(projectile, spawn, new Vector2(diffX, diffY), projType, projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, Main.rand.Next(3));
                    }
                    */
                }

            ));
        }

        private static void RegisterRedemptionOres()
        {
            //TODO: You may be wondering why this totally empty function is here, well there was a totally empty if statement checking for if
            // Mod of Redemption was active so now thats here too.
        }

        private static void ChainToNearbyNPC(Projectile projectile, NPC hitTarget)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n == hitTarget || !n.active || n.friendly || n.townNPC || n.dontTakeDamage)
                    continue;
                if (Vector2.Distance(n.Center, hitTarget.Center) < 200f)
                {
                    projectile.velocity = hitTarget.DirectionTo(n.Center) * projectile.velocity.Length();
                    break;
                }
            }
        }

        private static int FallbackDustType(int oreItemType)
        {
            if (AALuckyConfig.LuckyOre.TryGetValue(oreItemType, out int value))
            {
                if (value <= 300) return DustID.Copper;
                if (value <= 700) return DustID.Gold;
            }

            return WorldGen.genRand.Next(18) switch
            {
                0 => DustID.Copper,
                1 => DustID.Tin,
                2 => DustID.Iron,
                3 => DustID.Lead,
                4 => DustID.Silver,
                5 => DustID.Tungsten,
                6 => DustID.Gold,
                7 => DustID.Platinum,
                8 => DustID.t_Meteor,
                9 => ModContent.DustType<Dusts.LuminiteDust>(),
                10 => ModContent.DustType<Dusts.DarkmatterDust>(),
                11 => ModContent.DustType<Dusts.RadiumDust>(),
                12 => ModContent.DustType<Dusts.DaybreakIncineriteDust>(),
                13 => ModContent.DustType<Dusts.YamataDust>(),
                14 => ModContent.DustType<Dusts.VoidDust>(),
                15 => ModContent.DustType<Dusts.IncineriteDust>(),
                16 => ModContent.DustType<Dusts.AbyssiumDust>(),
                _ => DustID.Torch,
            };
        }
    }

    public class OreChunk : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 6;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            bool hasData = OreCannonSystem.TryGetOreData(Projectile, out var data);

            if (hasData)
                data.OreEffect?.Invoke(Projectile);

            Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.rotation += 0.2f * Projectile.direction;

            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            Projectile.oldPos[0] = Projectile.position;

            if (hasData)
                data.ExtraAI?.Invoke(Projectile);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new(TextureAssets.Item[(int)Projectile.ai[1]].Value.Width * 0.5f, Projectile.height * 0.5f);

            for (int k = 0; k < 3; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((3 - k) / 3f);
                Main.spriteBatch.Draw(TextureAssets.Item[(int)Projectile.ai[1]].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }

            if (OreCannonSystem.TryGetOreData(Projectile, out var data) && data.ExtraDraw != null)
                data.ExtraDraw(Projectile, lightColor);

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            bool hasData = OreCannonSystem.TryGetOreData(Projectile, out var data);
            int dustType = hasData ? data.DustType : FallbackDustType((int)Projectile.ai[1]);

            for (int i = 0; i < 5; i++)
            {
                float velX = -Projectile.velocity.X * 0.2f;
                float velY = -Projectile.velocity.Y * 0.2f;
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, dustType, velX, velY);
            }

            if (hasData)
                data.OnKill?.Invoke(Projectile);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (OreCannonSystem.TryGetOreData(Projectile, out var data))
                data.OnHit?.Invoke(Projectile, target, ref modifiers);
        }

        private static int FallbackDustType(int oreItemType)
        {
            if (AALuckyConfig.LuckyOre.TryGetValue(oreItemType, out var luckyVal) && luckyVal <= 300)
                return DustID.Copper;

            if (AALuckyConfig.LuckyOre.TryGetValue(oreItemType, out luckyVal) && luckyVal <= 700)
                return DustID.Gold;

            return WorldGen.genRand.Next(18) switch
            {
                0 => DustID.Copper,
                1 => DustID.Tin,
                2 => DustID.Iron,
                3 => DustID.Lead,
                4 => DustID.Silver,
                5 => DustID.Tungsten,
                6 => DustID.Gold,
                7 => DustID.Platinum,
                8 => DustID.t_Meteor,
                9 => ModContent.DustType<Dusts.LuminiteDust>(),
                10 => ModContent.DustType<Dusts.DarkmatterDust>(),
                11 => ModContent.DustType<Dusts.RadiumDust>(),
                12 => ModContent.DustType<Dusts.DaybreakIncineriteDust>(),
                13 => ModContent.DustType<Dusts.YamataDust>(),
                14 => ModContent.DustType<Dusts.VoidDust>(),
                15 => ModContent.DustType<Dusts.IncineriteDust>(),
                16 => ModContent.DustType<Dusts.AbyssiumDust>(),
                _ => DustID.Torch,
            };
        }
    }

    public class GravityAffectedOreChunk : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = ProjAIStyleID.GroundProjectile;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                Projectile.timeLeft = 600;
        }

        public float rotationspeed = 0.2f;

        public override void AI()
        {
            bool hasData = OreCannonSystem.TryGetOreData(Projectile, out var data);

            if (hasData)
            {
                data.OreEffect?.Invoke(Projectile);

                data.ExtraAI?.Invoke(Projectile);
            }

            if (Projectile.velocity.X > 0)
            {
                Projectile.direction = 1;
            }
            else
            {
                Projectile.direction = -1;
            }

            bool flag = false;
            Vector2 velocity = Collision.TileCollision(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height, true, true, 1); ;
            if (velocity != Projectile.velocity)
            {
                flag = true;
            }
            if (flag && ProjectileLoader.OnTileCollide(Projectile, Projectile.velocity))
            {
                rotationspeed -= .021f;
            }

            if (rotationspeed <= 0)
            {
                rotationspeed = 0f;
            }

            Projectile.rotation += rotationspeed * Projectile.direction;

            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new(TextureAssets.Item[(int)Projectile.ai[1]].Value.Width * 0.5f, Projectile.height * 0.5f);

            for (int k = 0; k < 3; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((3 - k) / 3f);
                Main.spriteBatch.Draw(TextureAssets.Item[(int)Projectile.ai[1]].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }

            if (OreCannonSystem.TryGetOreData(Projectile, out var data) && data.ExtraDraw != null)
                data.ExtraDraw(Projectile, lightColor);

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            bool hasData = OreCannonSystem.TryGetOreData(Projectile, out var data);
            int dustType = hasData ? data.DustType : FallbackDustType((int)Projectile.ai[1]);

            for (int i = 0; i < 5; i++)
            {
                float velX = -Projectile.velocity.X * 0.2f;
                float velY = -Projectile.velocity.Y * 0.2f;
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, dustType, velX, velY);
            }

            if (hasData)
                data.OnKill?.Invoke(Projectile);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (OreCannonSystem.TryGetOreData(Projectile, out var data))
                data.OnHit?.Invoke(Projectile, target, ref modifiers);
        }

        private static int FallbackDustType(int oreItemType)
        {
            if (AALuckyConfig.LuckyOre.TryGetValue(oreItemType, out var luckyVal) && luckyVal <= 300)
                return DustID.Copper;

            if (AALuckyConfig.LuckyOre.TryGetValue(oreItemType, out luckyVal) && luckyVal <= 700)
                return DustID.Gold;

            return WorldGen.genRand.Next(18) switch
            {
                0 => DustID.Copper,
                1 => DustID.Tin,
                2 => DustID.Iron,
                3 => DustID.Lead,
                4 => DustID.Silver,
                5 => DustID.Tungsten,
                6 => DustID.Gold,
                7 => DustID.Platinum,
                8 => DustID.t_Meteor,
                9 => ModContent.DustType<Dusts.LuminiteDust>(),
                10 => ModContent.DustType<Dusts.DarkmatterDust>(),
                11 => ModContent.DustType<Dusts.RadiumDust>(),
                12 => ModContent.DustType<Dusts.DaybreakIncineriteDust>(),
                13 => ModContent.DustType<Dusts.YamataDust>(),
                14 => ModContent.DustType<Dusts.VoidDust>(),
                15 => ModContent.DustType<Dusts.IncineriteDust>(),
                16 => ModContent.DustType<Dusts.AbyssiumDust>(),
                _ => DustID.Torch,
            };
        }

    }
}