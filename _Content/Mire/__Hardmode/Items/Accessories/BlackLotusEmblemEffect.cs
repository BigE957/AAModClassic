using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Accessories
{
    public class BlackLotusEmblemEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<BlackLotusEmblemPlayer>().effect = true;
        }
    }

    public class BlackLotusEmblemPlayer : EquipmentEffectPlayer
    {
        public override void PostUpdate()
        {
            if (effect && Player.inventory[Player.selectedItem].mana > 0 && Player.statMana < (int)(Player.inventory[Player.selectedItem].mana * Player.manaCost))
            {
                BlackLotusQuickMana();
            }
        }

        //TODO: what the fuck is this doing and how can we make it not do that
        public void BlackLotusQuickMana()
        {
            if (Player.noItems)
            {
                return;
            }
            if (Player.statMana == Player.statManaMax2)
            {
                return;
            }
            for (int i = 0; i < 58; i++)
            {
                if (Player.inventory[i].stack > 0 && Player.inventory[i].type > ItemID.None && Player.inventory[i].healMana > 0 && (Player.potionDelay == 0 || !Player.inventory[i].potion) && ItemLoader.CanUseItem(Player.inventory[i], Player))
                {
                    SoundEngine.PlaySound(Player.inventory[i].UseSound, Player.position);
                    if (Player.inventory[i].potion)
                    {
                        if (Player.inventory[i].type == ItemID.RestorationPotion)
                        {
                            Player.potionDelay = Player.restorationDelayTime;
                            Player.AddBuff(BuffID.PotionSickness, Player.potionDelay, true);
                        }
                        else
                        {
                            Player.potionDelay = Player.potionDelayTime;
                            Player.AddBuff(BuffID.PotionSickness, Player.potionDelay, true);
                        }
                    }
                    ItemLoader.UseItem(Player.inventory[i], Player);
                    Player.statLife += Player.inventory[i].healLife;
                    Player.statMana += Player.inventory[i].healMana;
                    if (Player.statLife > Player.statLifeMax2)
                    {
                        Player.statLife = Player.statLifeMax2;
                    }
                    if (Player.statMana > Player.statManaMax2)
                    {
                        Player.statMana = Player.statManaMax2;
                    }
                    if (Player.inventory[i].healLife > 0 && Main.myPlayer == Player.whoAmI)
                    {
                        Player.HealEffect(Player.inventory[i].healLife, true);
                    }
                    if (Player.inventory[i].healMana > 0)
                    {
                        Player.AddBuff(BuffID.ManaSickness, 60, true);
                        if (Main.myPlayer == Player.whoAmI)
                        {
                            Player.ManaEffect(Player.inventory[i].healMana);
                        }
                    }
                    if (ItemLoader.ConsumeItem(Player.inventory[i], Player))
                    {
                        Player.inventory[i].stack--;
                    }
                    if (Player.inventory[i].stack <= 0)
                    {
                        Player.inventory[i].TurnToAir();
                    }
                    //TODO: WHAT????? WHAT???????????????????
                    Recipe.FindRecipes();
                    return;
                }
            }
        }
    }
}