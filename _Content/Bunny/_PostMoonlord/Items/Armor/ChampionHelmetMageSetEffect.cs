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
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetMageSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<ChampionHelmetMageSetPlayer>().effect = true;
        }
    }

    public class ChampionHelmetMageSetPlayer : EquipmentEffectPlayer
    {
        public int CarrotBuff = 0;

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (effect)
            {
                if (Main.rand.NextBool(30))
                {
                    int i = Item.NewItem(target.GetSource_OnHurt(Player), target.Hitbox, ModContent.ItemType<ChampionHelmetMageSetEffect_CarrotBooster>(), 1, false, 0, true);
                    Main.item[i].velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                }
            }
        }

        public void CarrotLevelup()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 22; i++)
                {
                    if (Player.buffType[i] == ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost1>() ||
                        Player.buffType[i] == ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost2>() ||
                        Player.buffType[i] == ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost3>())
                    {
                        Player.DelBuff(i);
                    }
                }
                CarrotBuff = (int)MathHelper.Clamp(CarrotBuff + 1, 0f, 3f);
                switch (CarrotBuff)
                {
                    case 0:
  
                        return;
                    case 1:
                        Player.AddBuff(ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost1>(), 480, true);
                        return;
                    case 2:
                        Player.AddBuff(ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost2>(), 480, true);
                        return;
                    case 3:
                        Player.AddBuff(ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost3>(), 480, true);
                        return;
                }
                return;
            }
        }
    }
}