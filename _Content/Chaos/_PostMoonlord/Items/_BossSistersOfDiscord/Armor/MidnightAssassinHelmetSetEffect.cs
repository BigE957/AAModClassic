using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class MidnightAssassinHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<MidnightAssassinHelmetSetPlayer>().effect = true;
        }
    }

    public class MidnightAssassinHelmetSetPlayer : EquipmentEffectPlayer
    {
        public override void PostUpdate()
        {
            if (effect)
            {
                float RandomX = 50f;
                float RandomY = 25f;
                bool flag = Player.itemAnimation > 0;
                if (flag && Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Melee) && Main.rand.NextBool(200) && Player.whoAmI == Main.myPlayer)
                {
                    Vector2 SpeedVector = Main.MouseWorld - Player.RotatedRelativePoint(Player.MountedCenter, true);
                    SpeedVector.Normalize();
                    if (SpeedVector.HasNaNs())
                    {
                        SpeedVector = Vector2.UnitX * Player.direction;
                    }
                    SpeedVector *= 15f;
                    Vector2[] Spwanposition = new Vector2[3];
                    Spwanposition[0] = new Vector2(Player.Center.X + Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    Spwanposition[1] = new Vector2(Player.Center.X - Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    Spwanposition[2] = new Vector2(Player.Center.X - Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    int i = 0;
                    while (i < 3)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(Player.GetSource_ItemUse(Player.inventory[Player.selectedItem]), Spwanposition[i].X, Spwanposition[i].Y, SpeedVector.X, SpeedVector.Y, ModContent.ProjectileType<MidnightAssassinHelmetSetEffect_AssassinDagger>(), (int)(Player.inventory[Player.selectedItem].damage * 1.3), 2f, Player.whoAmI, 0f, 1f);
                        float round = 16f;
                        int k = 0;
                        while (k < round)
                        {
                            Vector2 vector12 = Vector2.UnitX * 0f;
                            vector12 += -Vector2.UnitY.RotatedBy(k * (6.28318548f / round), default) * new Vector2(1f, 4f);
                            vector12 = vector12.RotatedBy(SpeedVector.ToRotation(), default);
                            int Dusti = Dust.NewDust(Spwanposition[i], 0, 0, ModContent.DustType<Dusts.AcidDust>(), 0f, 0f, 0, default, 1f);
                            Main.dust[Dusti].scale = 1.5f;
                            Main.dust[Dusti].noGravity = true;
                            Main.dust[Dusti].position = Spwanposition[i] + vector12;
                            Main.dust[Dusti].velocity = vector12.SafeNormalize(Vector2.UnitY) * 1f;
                            k++;
                        }
                        i++;
                    }
                }
            }
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.CountsAsClass(DamageClass.Ranged) && effect)
            {
                velocity *= 1.3f;
                if (Main.rand.NextBool(10) && Player.whoAmI == Main.myPlayer)
                {
                    float RandomX = 50f;
                    float RandomY = 25f;
                    Vector2[] Spwanposition = new Vector2[3];
                    Spwanposition[0] = new Vector2(Player.Center.X + Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    Spwanposition[1] = new Vector2(Player.Center.X - Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    Spwanposition[2] = new Vector2(Player.Center.X - Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Spwanposition[i].X, Spwanposition[i].Y, velocity.X, velocity.Y, ModContent.ProjectileType<MidnightAssassinHelmetSetEffect_AssassinArrow>(), (int)(item.damage * 1.3), 2f, Player.whoAmI, 0f, 1f);
                        float round = 16f;
                        int k = 0;
                        while (k < round)
                        {
                            Vector2 vector12 = Vector2.UnitX * 0f;
                            vector12 += -Vector2.UnitY.RotatedBy(k * (6.28318548f / round), default) * new Vector2(1f, 4f);
                            vector12 = vector12.RotatedBy(velocity.ToRotation(), default);
                            int Dusti = Dust.NewDust(Spwanposition[i], 0, 0, ModContent.DustType<Dusts.AcidDust>(), 0f, 0f, 0, default, 1f);
                            Main.dust[Dusti].scale = 1.5f;
                            Main.dust[Dusti].noGravity = true;
                            Main.dust[Dusti].position = Spwanposition[i] + vector12;
                            Main.dust[Dusti].velocity = vector12.SafeNormalize(Vector2.UnitY) * 1f;
                            k++;
                        }
                    }
                }
            }

            return true;
        }
    }
}