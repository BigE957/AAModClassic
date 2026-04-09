using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Base.BaseMod.Base
{
    public class BasePlayer
    {
        //------------------------------------------------------//
        //------------------BASE PLAYER CLASS-------------------//
        //------------------------------------------------------//
        // Contains methods relating to players.                //
        //------------------------------------------------------//
        //  Author(s): Grox the Great                           //
        //------------------------------------------------------//

        public static void ReduceSlot(Player player, int slot, int amount)
        {
            player.inventory[slot].stack -= amount;
            if (player.inventory[slot].stack <= 0)
            {
                player.inventory[slot] = new Item();
            }
        }

        public static bool HasHelmet(Player player, int itemType, bool vanity = true) { return HasArmor(player, itemType, 0, vanity); }
        public static bool HasChestplate(Player player, int itemType, bool vanity = true) { return HasArmor(player, itemType, 1, vanity); }
        public static bool HasLeggings(Player player, int itemType, bool vanity = true) { return HasArmor(player, itemType, 2, vanity); }

        /*
         * Returns true if the player is wearing the given armor
         * armorType : 0 == helmet, 1 == chestplate, 2 == leggings.
         * vanity : If true, include vanity slots.
         */
        public static bool HasArmor(Player player, int itemType, int armorType, bool vanity = true)
        {
            if (vanity)
            {
                if (armorType == 0)
                    return player.armor[10] != null && player.armor[10].type == itemType || player.armor[0] != null && player.armor[0].type == itemType;
                if (armorType == 1)
                    return player.armor[11] != null && player.armor[11].type == itemType || player.armor[1] != null && player.armor[1].type == itemType;
                if (armorType == 2)
                    return player.armor[12] != null && player.armor[12].type == itemType || player.armor[2] != null && player.armor[2].type == itemType;
            }
            else
            {
                if (armorType == 0)
                    return player.armor[0] != null && player.armor[0].type == itemType;
                if (armorType == 1)
                    return player.armor[1] != null && player.armor[1].type == itemType;
                if (armorType == 2)
                    return player.armor[2] != null && player.armor[2].type == itemType;
            }
            return false;
        }

        /**
         * Returns true if the given player has any of the given item types in thier inventory.
         * index : Is set to the index of the item found. If it isn't found, it is set to -1.
         * counts : the minimum stack per item needed for HasItem to return true.
         * includeAmmo : true if you wish to include the ammo slots.
         * includeCoins : true if you wish to include the coin slots.
         */
        public static bool HasItem(Player player, int[] types, ref int index, int[] counts = default, bool includeAmmo = false, bool includeCoins = false)
        {
            if (types == null || types.Length == 0) return false; //no types to check!			
            if (counts == null || counts.Length == 0) { counts = BaseUtility.FillArray(new int[types.Length], 1); }
            int countIndex = -1;
            if (includeCoins)
            {
                for (int m = 50; m < 54; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && BaseUtility.InArray(types, item.type, ref countIndex) && item.stack >= counts[countIndex]) { index = m; return true; }
                }
            }
            if (includeAmmo)
            {
                for (int m = 54; m < 58; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && BaseUtility.InArray(types, item.type, ref countIndex) && item.stack >= counts[countIndex]) { index = m; return true; }
                }
            }
            for (int m = 0; m < 50; m++)
            {
                Item item = player.inventory[m];
                if (item != null && BaseUtility.InArray(types, item.type, ref countIndex) && item.stack >= counts[countIndex]) { index = m; return true; }
            }
            return false;
        }

        public static bool HasItem(Player player, int type, int count = 1, bool includeAmmo = false, bool includeCoins = false)
        {
            int dummyIndex = 0;
            bool hasItem = HasItem(player, type, ref dummyIndex, count, includeAmmo, includeCoins);
            return hasItem;
        }

        /**
         * Returns true if the given player has the given item type in thier inventory.
         * 
         * index : Is set to the index of the item found. If it isn't found, it is set to -1.
         * count : the minimum stack needed for HasItem to return true.
         * includeAmmo : true if you wish to include the ammo slots.
         * includeCoins : true if you wish to include the coin slots.
         */
        public static bool HasItem(Player player, int type, ref int index, int count = 1, bool includeAmmo = false, bool includeCoins = false)
        {
            if (includeCoins)
            {
                for (int m = 50; m < 54; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= count) { index = m; return true; }
                }
            }
            if (includeAmmo)
            {
                for (int m = 54; m < 58; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= count) { index = m; return true; }
                }
            }
            for (int m = 0; m < 50; m++)
            {
                Item item = player.inventory[m];
                if (item != null && item.type == type && item.stack >= count) { index = m; return true; }
            }
            index = -1;
            return false;
        }

        public static bool HasAccessory(Player player, int type, bool normal, bool vanity)
        {
            int dummy = 0; bool dummeh = false;
            return HasAccessory(player, type, normal, vanity, ref dummeh, ref dummy);
        }

        /**
         * Returns true if the given player has the given accessory equipped.
         */
        public static bool HasAccessory(Player player, int type, bool normal, bool vanity, ref bool social, ref int index)
        {
            if (vanity)
            {
                for (int m = 13; m < 18 + player.GetAmountOfExtraAccessorySlotsToShow(); m++)
                {
                    Item item = player.armor[m];
                    if (item is { IsAir: false } && item.type == type) { index = m; social = true; return true; }
                }
            }
            if (normal)
            {
                for (int m = 3; m < 8 + player.GetAmountOfExtraAccessorySlotsToShow(); m++)
                {
                    Item item = player.armor[m];
                    if (item is { IsAir: false } && item.type == type) { index = m; social = false; return true; }
                }
            }
            return false;
        }
    }
}


