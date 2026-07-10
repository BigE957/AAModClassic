using Terraria.Localization;

namespace AAModClassic.Localization
{

    //Kept onlt for the purpose of MAYBE eventually porting the other two supported languages
    public class LangFileArchived
    {
        public static string Worldtext(string WorldInfo)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(WorldInfo)
                    {
                        case "YttriumInfo":
                        return "你的世界获得了钇土!";
                        case "UraniumInfo":
                        return "你的世界获得了铀绿!";
                        case "TechneciumInfo":
                        return "你的世界获得了闪锝!";
                        case "downedEquinoxInfo":
                        return "上神之礼在天空中闪耀...";
                        case "StarIdolInfo":
                        return "天空上已经有一堆星星了, 孩子.";
                        case "downedMoonlordInfo1":
                        return "真正的远古觉醒!";
                        case "downedMoonlordInfo2":
                        return "月球领主的精华在洞穴中闪耀...";
                        case "downedMechBossAnyInfo":
                        return "洞穴中有光闪过...";
                        case "downedSistersInfo":
                        return "混沌能量从地下最深出开始涌出. ";
                        case "downedBoss2Info":
                        return "打败强大的恶魔之后你听见泰拉心球中的靡靡之音...";
                        case "downedBoss3Info1":
                        return "远古之骨已经因能量而爆裂!";
                        case "downedBoss3Info2":
                        return "沙漠之风开始酝酿...";
                        case "downedBoss3Info3":
                        return "凛冬之山隆隆作响...";
                        case "downedPlantBossInfo1":
                        return "泰拉心球中的和谐之音回响";
                        case "downedPlantBossInfo2":
                        return "虚空枯萎机器重新运转";
                        case "downedPlantBossInfo3":
                        return "地下的恶魔开始密谋";
                        case "downedPlantBossInfo4":
                        return "嘿, 小子, 是我, 阿努比斯. 回城找我帮我个忙, 我想和你说点事.";
                        case "downedStormAnyInfo":
                        return "山洞里一道霹雳轰鸣...";
                        case "hardModeInfo":
                        return "愤怒和怨恨的灵魂被释放了!";
                        case "downedAllAncientsInfo":
                        return "真正的冥昧之息在大气中翻涌...";
                    }
                }
            if (Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
            {
                switch (WorldInfo)
                {
                    case "YttriumInfo":
                    return "Ваш мир благословлен иттрием!";
                    case "UraniumInfo":
                    return "Ваш мир благословлен ураном!";
                    case "TechneciumInfo":
                    return "Ваш мир благословлен технецием!";
                    case "downedEquinoxInfo":
                    return "Дар небожителей искрится в атмосфере...";
                    case "StarIdolInfo":
                    return "В небе множество звезд, дитя";
                    case "downedMoonlordInfo1":
                    return "Древние Пробудились!";
                    case "downedMoonlordInfo2":
                    return "Эссенция Лунного Лорда искрится в пещерах под вами...";
                    case "downedMechBossAnyInfo":
                    return "Пещеры искрятся светом на долю секунды.";
                    case "downedSistersInfo":
                    return "Хаотическая энергия растет в самых глубоких частях этого мира... ";
                    case "downedBoss2Info":
                    return "Вы слышите гармоничный гул, доносящийся из Террариума после поражения великого демона...";
                    case "downedBoss3Info1":
                    return "Кости древнего прошлого разрываются энергией!";
                    case "downedBoss3Info2":
                    return "Ветры пустыни зашевелились..";
                    case "downedBoss3Info3":
                    return "Холмы тундры грохочут...";
                    case "downedPlantBossInfo1":
                    return "Хор единства звучит из Террариума.";
                    case "downedPlantBossInfo2":
                    return "Побитые машины пустоты снова активируются...";
                    case "downedPlantBossInfo3":
                    return "Красные Дьяволы подземного мира начинают строить козни...";
                    case "downedPlantBossInfo4":
                    return "Эй, малой, это Анубис. Сделай мне одолжение и встреться со мной в городе. Нам нужно поговорить.";
                    case "downedStormAnyInfo":
                    return "Хлопок молнии слышен в пещерах.";
                    case "hardModeInfo":
                    return "На волю выпущены духи гнева и ярости!";
                    case "downedAllAncientsInfo":
                    return "Хаус начинает шевелиться в атмосфере...";
                }
            }
            else
                {
                    switch(WorldInfo)
                    {
                        case "YttriumInfo":
                        return "Your world bursts with Yttrium!";
                        case "UraniumInfo":
                        return "Your world bursts with Uranium!";
                        case "TechneciumInfo":
                        return "Your world bursts with Technecium!";
                        case "downedEquinoxInfo":
                        return "The gift of the celestials sparkle in the atmosphere...";
                        case "StarIdolInfo":
                        return "There are plenty of stars in the sky, child.";
                        case "downedMoonlordInfo1":
                        return "The Ancients have Awakened!";
                        case "downedMoonlordInfo2":
                        return "The Essence of the Moon Lord sparkles in the caves below...";
                        case "downedMechBossAnyInfo":
                        return "The caves shine with light for a brief moment...";
                        case "downedSistersInfo":
                        return "Chaotic energy grows in the deepest parts of the world.";
                        case "downedBoss2Info":
                        return "You hear a hum of harmony from the Terrarium after the defeat of a great demon...";
                        case "downedBoss3Info1":
                        return "Bones of the ancient past burst with energy!";
                        case "downedBoss3Info2":
                        return "The desert winds stir...";
                        case "downedBoss3Info3":
                        return "The winter hills rumble...";
                        case "downedPlantBossInfo1":
                        return "The choirs of unity hum from the terrarium.";
                        case "downedPlantBossInfo2":
                        return "The withered machines of the emptiness reactivate.";
                        case "downedPlantBossInfo3":
                        return "Devils in the underworld begin to plot.";
                        case "downedPlantBossInfo4":
                        return "Hey kid, it's me, Anubis. Do me a favor and meet me back in town, I wanna talk to ya about somethin'.";
                        case "downedStormAnyInfo":
                        return "The clap of a thunderbolt roars in the caverns...";
                        case "hardModeInfo":
                        return "The Souls of Fury and Wrath are unleashed upon the world!";
                        case "downedAllAncientsInfo":
                        return "Chaos begins to stir in the atmosphere...";
                    }
                }
            return "";
        }
        public static string AAPlayerChat(string PlayerInfo)
        {
            if (Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
            {
                switch (PlayerInfo)
                    {
                        case "UnstableSoulInfo":
                        return "你的灵魂激荡着...";
                        case "InfinityGauntletInfo":
                        return "完美的平衡, 万物本该如此...";
                        case "WorldgenReminderInfo1":
                        return "嘿, 呃…小子？ 如果我哪里错了, 请纠正我, 但我认为你的世界不是由远古觉醒的内容而生成的。 如果我是你, 我会完成一个新的世界。 ";
                        case "WorldgenReminderInfo2":
                        return "你这个笨蛋！你没有生成远古觉醒的内容！快创造一个新世界！呃啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊";
                        case "WorldgenReminderInfo3":
                        return "错误。 世界并没有包含aamod. tmod的内容。 请创造一个新世界。 ";
                        case "WorldgenReminderInfo4":
                        return "嘿！你没有在这个世界上创造出远古觉醒的东西！在我把你炸到火星之前赶紧创造一个新世界！";
                        case "WorldgenReminderInfo5":
                        return "嘿, 呃……这个世界里我看不到任何远古觉醒的内容。 聪明点赶紧创一个新世界。 ";
                        case "WorldgenReminderInfo6":
                        return "嘿。 说你呢。 另一个次元的人。 你可能在下载了mod之后忘记了创造一个新的世界。 如果你想要所有的mod的内容, 创造一个新的世界。 ";
                        case "WorldgenReminderInfo7":
                        return "创造……新世界……否则疯狂的蘑菇人……会压扁……你这个小泰拉人……";
                        case "WorldgenReminderInfo8":
                        return "…凡人。 如果我没有老眼昏花的话, 你的世界就没有远古觉醒的内容。 创造一个新的世界最吼的。 ";
                    }
                }
            else if (Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
            {
                switch (PlayerInfo)
                {
                    case "UnstableSoulInfo":
                    return "Вашу душу разрывает...";
                    case "InfinityGauntletInfo":
                    return "Безупречный баланс. Эталон гармонии...";
                    case "WorldgenReminderInfo1":
                    return "Эй, эх...дитя? Поправь меня если я не прав, но я думаю, что в твоем мире отсутствуют биомы из нашего мода. Я бы сделал новый мир.";
                    case "WorldgenReminderInfo2":
                    return "ТЫ ИМБЕЦИЛ!!! В ТВОЕМ МИРЕ НЕТ КОНТЕНТА ИЗ МОЕГО МОДА!!! СЕЙЧАС-ЖЕ ДЕЛАЙ НОВЫЙ! РЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕ";
                    case "WorldgenReminderInfo3":
                    return "КРИТИЧЕСКАЯ 0ШИБКА. В МИРЕ 0ТСУТСВУЕТ К0НТЕНТ ИЗ AAmod.tmod. ПОЖАЛУЙСТА, СГЕНЕРИРУЙТЕ НОВЫЙ МИР.";
                    case "WorldgenReminderInfo4":
                    return "ЭЙ! У тебя нет контента из нашего мода! Сделай новый, или полетишь на Марс!";
                    case "WorldgenReminderInfo5":
                    return "Эй, эх... Я не вижу у тебя контента из мода. Может быть сделаешь новый мир или что-то тип того?";
                    case "WorldgenReminderInfo6":
                    return "Эй, ты, межпространственное существо. Похоже, что ты забыл сделать новый мир. Сделай новый, если хочешь весь контент из мода.";
                    case "WorldgenReminderInfo7":
                    return "Делай новый мир...или грибное безумие тебя...убьет...маленький терраниан";
                    case "WorldgenReminderInfo8":
                    return "... Смертный, если мой старые глаза меня не обманывают, то в твоем мире нет контента из Ancients Awakened. Сгенерировать новый мир будет оптимальным решением.";
                }
            }
            else
                {
                    switch(PlayerInfo)
                    {
                        case "UnstableSoulInfo":
                        return "Your soul ripples...";
                        case "InfinityGauntletInfo":
                        return "Perfectly Balanced, as all things should be...";
                        case "WorldgenReminderInfo1":
                        return "Hey uh...kid? Correct me if I'm wrong, but I think your world didn't generate with Ancients Awakened stuff in it. I'd make a new one if I were you.";
                        case "WorldgenReminderInfo2":
                        return "YOU IMBECILE! YOU DIDN'T GENERATE ANCIENTS AWAKENED CONTENT! MAKE A NEW WORLD NOW! REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE";
                        case "WorldgenReminderInfo3":
                        return "ERR0R. W0RLD D0ES N0T APPEAR T0 C0NTAIN AAM0D.TM0D C0NTENT. PLEASE GENERATE A NEW W0RLD.";
                        case "WorldgenReminderInfo4":
                        return "HEY! You didn't generate Ancients Awakened stuff in this world! Generate a new world before I blast you to mars!";
                        case "WorldgenReminderInfo5":
                        return "Hey, uh...I don't see any Ancients Awakened content in this world. Might be smart to make a new world or whatever...";
                        case "WorldgenReminderInfo6":
                        return "Hey. You. Interdimensional being. You might have forgotten to make a new world after downloading the mod. Make a new world if you want all the mod's content.";
                        case "WorldgenReminderInfo7":
                        return "Make...new world....or mushmad...will squish...little terrarian...";
                        case "WorldgenReminderInfo8":
                        return "...Mortal. Your world doesn't have Ancients Awakened content if my old eyes do not deceive me. Generating a new world would be optimal.";
                    }
                }
            return"";
        }

        public static string GreedChest(string Greed)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(Greed)
                    {
                        case "GreedChest1":
                        return "你脚下的地面轻微震动着...";
                        case "GreedChest2":
                        return "你听到一声尖啸在洞穴中回荡...";
                        case "GreedChest3":
                        return "把 老 子 的 东 西 放 下， 你 这 个 野 猴 子 小 偷!!!";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(Greed)
                    {
                        case "GreedChest1":
                        return "Земля под вами немного грохочет";
                        case "GreedChest2":
                        return "Вы слышите, как крик раздается эхом через пещеры...";
                        case "GreedChest3":
                        return "УБРАЛ РУКИ ОТ МОИХ ПРЕЛЕСТЕЙ, ТЫ, ВОРУЮЩАЯ ОБЕЗЬЯНА!";
                    }
                }
            else
                {
                    switch(Greed)
                    {
                        case "GreedChest1":
                        return "The ground below you trembles slightly...";
                        case "GreedChest2":
                        return "You hear a scream echo through the caverns...";
                        case "GreedChest3":
                        return "HANDS OFF MY LOOT YOU THIEVING APE!!!";
                    }
                }
            return"";
        }

        public static string BossSummonName(string Boss)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                }
            else
                {
                }
            return"";
        }

        public static string Hotkey(string key)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(key)
                    {
                        case "Rifthotkey":
                        return "裂位回家";
                        case "RiftReturnhotkey":
                        return "裂位返程";
                        case "AccessoryAbilityKey":
                        return "远古觉醒饰品能力";
                        case "ArmorAbilityKey":
                        return "远古觉醒套装能力";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(key)
                    {
                        case "Rifthotkey":
                        return "Телепортирут домой";
                        case "RiftReturnhotkey":
                        return "Телепортирует обратно";
                        case "AccessoryAbilityKey":
                        return "AA Активировать способность аксессуара";
                        case "ArmorAbilityKey":
                        return "AA Активировать способность брони";
                    }
                }
            else
                {
                    switch(key)
                    {
                        case "Rifthotkey":
                        return "Rift Home";
                        case "RiftReturnhotkey":
                        return "Rift Back";
                        case "AccessoryAbilityKey":
                        return "AA Accessory Ability";
                        case "ArmorAbilityKey":
                        return "AA Armor Ability";
                    }
                }
            return"";
        }

        public static string GlobalNPCSInfo(string NPCsummon)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(NPCsummon)
                    {
                        case "NPCarrive":
                        return " 到了!";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(NPCsummon)
                    {
                        case "NPCarrive":
                        return " пробудился!";
                    }
                }
            else
                {
                    switch(NPCsummon)
                    {
                        case "NPCarrive":
                        return " have awoken!";
                    }
                }
            return"";
        }

        public static string TownNPCAlpha(string Alpha)
        {
            switch (Alpha)
            {
                case "AlphaButton1":
                    return "Vanity Shop";
                case "AlphaButton2":
                    return "Weapon Shop";

                case "AlphaChat1":
                    return "Hope you don't mind me hanging around. I had to deal with a LOT of bad code to get here. Could do with a break.";
                case "AlphaChat2":
                    return "A fun fact? Hmm, well I've never met my father. They say he vanished a long time ago. He's a real...\nMr. E.";
                case "AlphaChat3":
                    return "You're looking for a fish? No clue who you're talking about, pal.";
                case "AlphaChat4":
                    return "Look at me! I'm Big E! I've always been here, and I'll always be!";
            }
            return "";
        }

        public static string TownNPCAnubis(string Anubis)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(Anubis)
                    {
                        case "AnubisName":
                        return "传说书记员";
                        case "SetChatButtons1":
                        return "切换信息";
                        case "SetChatButtons2":
                        return "我要做什么? ";
                        case "SetChatButtons3":
                        return "蘑菇. 我是认真的. ";
                        case "SetChatButtons4":
                        return "更多的蘑菇. ";
                        case "SetChatButtons5":
                        return "淡定";
                        case "SetChatButtons6":
                        return "死胖子龙";
                        case "SetChatButtons7":
                        return "三头怪胎";
                        case "SetChatButtons8":
                        return "三个愿望";
                        case "SetChatButtons9":
                        return "冰凉";
                        case "SetChatButtons10":
                        return "了解";
                        case "SetChatButtons11":
                        return "机械质料";
                        case "SetChatButtons12":
                        return "两个头;没脑子";
                        case "SetChatButtons13":
                        return "更多蠕虫boss, 该死";
                        case "SetChatButtons14":
                        return "远古裁决";
                        case "SetChatButtons15":
                        return "麻烦双胞胎";
                        case "SetChatButtons16":
                        return "远古之怒";
                        case "SetChatButtons17":
                        return "远古之怨";
                        case "SetChatButtons18":
                        return "远古劫难";
                        case "SetChatButtons19":
                        return "冥昧末日预言者";
                        case "SetChatButtons20":
                        return "宝石持有者";
                        case "SetChatButtons21":
                        return "吵得头疼的婊子";
                        case "SetChatButtons22":
                        return "一条大虫子";
                        case "SetChatButtons23":
                        return "兔子恶棍";
                        case "SetChatButtons24":
                        return "鸟妖女人又来了";
                        case "SetChatButtons25":
                        return "拿钱, 送命";
                        case "SetChatButtons26":
                        return "战兔之怒";
                        case "SetChatButtons27":
                        return "我要求重比一场";

                        case "GetBookChat":
                        return @"你找到啦! 我的限量典藏版传记! 太感谢了, 哥们. 知道吗? 这书作为礼物, 就送给你了. 这, 我再给你签个名.
...哎呀，我不小心用了我的符文羽毛笔签了名。哦，没事，这东西有魔力.";
                        case "GetSummonItemChat":
                        return "嘿，感谢你回来找我. 我想试试你的能力. 你打败那些机械怪物之后，我想看看是不是有实力和像我一样的人战斗. 拿着这把权杖，准备好了就去沙漠里用. 我时刻准备就绪.";
                        case "GetSummonItemChat2":
                        return "拿着这个. 这个老工艺品很快会派上用场. 它会指引你去一个...这个世界上特殊的地方. 读一下那里的提示. 他们会告诉你应该做什么. 如果要清除这个世界上的邪恶，我们还有很多事情要做.";

                        case "CombatTextChat":
                        return "我回家了. 真安静.";

                        case "downedMonarchY":
                        return "…就这样? ";
                        case "downedMonarchN":
                        return "嘿, 你知道这些到处都是的小蘑菇吗？ 我听说, 如果你把他们放在一块, 挥一下, 他们的国王或什么东西就会来试图把你赶走。 我很想看一看。 ";
                        case "downedFungusY":
                        return "干得好。 老实说, 你现在有多高？ ";
                        case "downedFungusN":
                        return "绚烂的蘑菇洞总让我觉得有点毛骨悚然。 无论如何, 你想要更好的魔法能力？ 有一个巨大的蘑菇怪物, 身体内充满了强大的魔法能力。 只不过你在它下面的时候需要堵住你的鼻子。 ";
                        case "downedGripsY":
                        return "把那些大爪子打下来很不错。 也许小东西们能让我好好歇一歇。 ";
                        case "downedGripsN":
                        return "那些飞着的爪子在晚上简直就是一场让我害怕的噩梦。 我旅行中, 遇到过两个大大的。 也许如果你杀了他们, 小东西们就会被赶走。 也许你杀一点小爪子, 就能显示召唤出他们的方法。 ";
                        case "downedBroodY":
                        return "那是我有生以来见过的最胖的龙。 ";
                        case "downedBroodN":
                        return "喜欢龙吗？ 不？ 那可不好。 想要更好的装备？ 你得去杀死这条熔岩 “巨”龙。 她又大又吓人, 太太太太太太太胖了飞不高, 但她还是会飞。 ";
                        case "downedHydraY":
                        return "总算解脱了。 那只九头渊蛇似乎并不会善罢甘休。 至少她女儿更成熟点……嗯？ 谁？ 我以后再解释, 总之干得好。 ";
                        case "downedHydraN":
                        return "潭渊一直是所有令人作呕的蜥蜴聚集地。 不过那有个相当大的, 三个头。 她总是很不高兴, 每次我想进她的窝, 她都想吃掉我！";
                        case "downedDjinnY":
                        return "哈！你这个沙子废物, 看看现在谁强！";
                        case "downedDjinnN":
                        return "那 个 狗 娘 养--哦, 嗨。 对不起, 我只是有点生气, 因为我和沙漠巨灵发生了一场小争执。 你能去教教那个会魔法的傻瓜和他的蠢货打手们别对着我秀肌肉了吗？ ";
                        case "downedSerpentY":
                        return "希望你没有被“冻伤”！*砰-咚-锵*（译者注：打击乐中用来活跃气氛的节拍伴奏）...我知道那得一瘸一拐。 ";
                        case "downedSerpentN":
                        return "蛇, 为什么总是蛇？ 我讨厌他们！不管怎样, 最近在苔原上, 有这么多雪蛇还真是不让我孤单。 你能当一次除害人, 看看他们在干什么吗？ ";
                        case "downedRetrieverY":
                        return "你有没有机会找回我第三版的《神奇狗阿努比斯的生命传奇历险记》？ ";
                        case "downedRetrieverN":
                        return "还记得混乱之爪？ 那些恶心的脏手？ 现在有个机器版的还老偷我东西。 你能帮我个忙, 教训一下它什么的吗？ ";
                        case "downedRaiderY":
                        return "所以它只是个大机器人？ 好吧, 有时候只是人们想多了。 那东西确实和育母炎龙一样胖。 ";
                        case "downedRaiderN":
                        return "人们说有一天晚上他们看到了奇怪的阴影。 它太大了甚至还把月亮遮住了一会儿。 你应该发现这个秘密并且做点什么。 ";
                        case "downedOrthrusY":
                        return "我猜双头狗现在正在吃自己。 ";
                        case "downedOrthrusN":
                        return "记得九头渊蛇？ 外面有一个更大的。 是个机器人。 能发闪电。 所以呃…祝你好运！";
                        case "downedEquinoxY":
                        return "除掉昼夜虫干得不错。 不过我可以告诉你现在好像过了一个星期。 希望我没有错过护士的预约…";
                        case "downedEquinoxN":
                        return "喜欢虫子？ 我也不喜欢, 你猜怎么着？ 有两个虫子控制日夜的流动, 而且很强力。 祝你好运。 ";
                        case "AnubisScapterLost":
                        return "你把杖给弄 丢 了?! 我可没法像发糖一样天天给你弄一个, 懂吗! 还好, 我还有一个.";
                        case "downedAnubisBY":
                        return "你出手可以轻点，懂吗。我现在背还疼着";
                        case "downedAnubisBN":
                        return "我听说有个万人迷家伙帅的一批, 而且所有的女人都爱他, 因为他有惊人的灵魂判断力。 真尼玛牛逼！";
                        case "downedAthenaY":
                        return "感天谢地, 你终于让那堆烦人的臭鸟闭嘴了! 他们要是再敢冲我嚷嚷，我就抓一只做成烤鸡当晚餐";
                        case "downedAthenaN":
                        return "你知道那些整天在天上叽叽喳喳的臭鸟吗? 好，有一种叫六翼鸟妖的完全就不会闭嘴!!! 在东边的天宫里有一个她们的头. 要是你能给她来两拳，也许她们就可以闭上她们的臭嘴";
                        case "downedRajahY":
                        return "你打败了王公兔? 漂亮, 是, 我以前一直以为它是神, 所以我觉得你是没法打败它的..!";
                        case "downedRajahN":
                        return "嘿，你知道那些总是蹦蹦跳跳的兔子吗？如果我是你，我不会骚扰他们。这里有一个传说，有一个“守护者”，一直在保护它们远离危险。为什么是传说？因为很明显没人能活着从它手里出来。简洁的故事，嗯？";
                        case "downedGreedYBookY":
                        return "嘿，谢谢你把我的书拿回来。前一阵子金食饕餮偷它可能是因为我用来装裱它用了金亮的外皮。看看那个山洞，也许他还偷了别的东西？";
                        case "downedGreedYBookN":
                        return "嘿 啊...你找到我的东西了吗? 没? 把那的赃物堆好好翻翻, 我敢确定就在那.";
                        case "downedGreedN":
                        return "嘿 啊, 在地下有一个巨大的金洞里面充满了琳琅满目的财宝, 但它被一条很小气的虫子看着. 你应该去瞅瞅有没有什么战利品, 不过嗯...那里面还有我的东西. 你可以去帮我取回来吗? 别担心你不认识, 当你看见它的时候, 你就会知道那是我的东西.";
                        case "downedAthenaAY":
                        return "哈? 你问我Varian? 我有些年头没听过这个词了...太久远我都忘了这个名字了. 虽然我好像能记起来什么人一直在四处闲逛...";
                        case "downedAthenaAN":
                        return "嘿, 那个烦人的小鸟妖找你了吗? 很抱歉向她透露了你的位置, 主要是她一直在我耳边嚷嚷. 不管怎么样, 雅典娜好像确实想重比一场. 保持警惕, 哥们. 我觉得这是个陷阱";
                        case "GreedACalamityMod":
                        return "你应该知道, 你和神明吞噬者的战斗让我有点想到了金食饕餮...我是说想象一下. 它们都可以虫洞穿越而且喜欢吃东西. 它们可能是...啊, 那可太不可思议了...或者..?";
                        case "downedFAnubisY":
                        return "做的不错, 伙计. 我印象非常深刻, 虽然我还在犹豫. 嗯哼? 我的全力..? 就像我之前所说, 为你自己好有些东西你还是不知道为妙.";
                        case "downedFAnubisN1":
                        return "我想再比一场, ";
                        case "downedFAnubisN2":
                        return ". 当然不只是为了我的名誉. 我必须要确信你能够面对即将发射的一些事情. 在沙漠见，和我再打一场. 准备好了.";
                        case "downedGreedAY":
                        return "所以它一直在隐藏它的真实实力. 我想知道为什么, 虽然...大概是, 它想躲着什么东西..?";
                        case "downedGreedAN":
                        return "你知道吗, 我依稀记得一个有关贪婪的虫子偷走你的垃圾的故事. 当它再一次出现时，它会比你把它打的屁滚尿流的时候强得多. 也许随着时间的推移它变得越来越菜...？或者...不，那不可能...";
                        case "downedSistersY":
                        return "漂亮！你给那俩宠坏的小孩好好上了一课！那俩没尝过苦头！";
                        case "downedSistersN":
                        return "还记得育母炎龙和九头渊蛇？ 好, 那俩有女儿。 而且她们讨厌“男 人”。 。 ！每次我去混沌之地的时候, 这俩都会等在那毁了我的一天！你去给她们俩点教训？ ";
                        case "downedAkumaY":
                        return "邪鬼巨龙觉得他很激昂。 对我来说, 他只是过于想冷静然后失败了。 不管怎么说, 想要给你头发上倒点水, 你头上有点烧焦了。 ";
                        case "downedAkumaN":
                        return "为什么有人会把一只太阳蛇称为恶魔？ 我个人不知道...不过邪鬼巨龙必须得走。 他老用火焰一样的呼吸觊觎我的沙漠, 这很让我恼火。 ";
                        case "downedYamataY":
                        return "感谢你让那个七头娘娘腔闭嘴。 他让我想把我的毛撕下来。 ";
                        case "downedYamataN":
                        return "八歧大蛇, 牢骚鬼！他什么都抱怨, 而且 不 会 闭 嘴！说 真 的, 你可以去试试对付七个不停互相交谈的吵闹的头。 ";
                        case "downedZeroY":
                        return "…老实说。 我不喜欢那东西死后说的话。 ";
                        case "downedZeroN":
                        return "你知道虚空吗？ 东边那些恐怖的浮岛？ 那里有一个 大 的可怕的机器总是漂浮在那。 不论怎样, 你击杀月球领主之后, 我听到一股巨大的冲击波从虚空涌出, 你能帮我查一查吗？ ";
                        case "downedShenY":
                        return "天哪——我知道你有这种感觉, 伙计！太棒了！尽管……你打他时他看起来很生气……几乎和他被打时一样——呃, 别再生气了。  ";
                        case "downedShenN":
                        return "邪鬼巨龙和八歧大蛇...你知道吗, 他们两个曾经是一个生物。 很糟糕的是, 它很强。 他曾经一度把两个文明世界合二为一。 不说了, 你需要啥？ ";
                        case "downedRajahCY":
                        return "所以巨兔王公最终决定移交权力？似乎他认为你是保护那些需要帮助的人的合适人选。如果是我的话我会照他说的做。在我看来，这是正确的做法。";
                        case "downedRajahCN":
                        return "他们兔子最近很生气。有点不对劲。我只是感觉到…";
                        case "Stones":
                        return "你知道……在你与上神应龙交战之后, 我感觉到一些……非常古老的魔法在激活。 也许你应该去拜访一下你遇到的那些强硬的BOSS？ 顺便问一下, 你最近见过哥布林召唤师吗？ 天哪, 她变得很强。 不知道她会掉落什么东西。 ";
                        case "else":
                        return "我不知道？ 你可以随便问我你目前能打的任何强力生物。  ";
                        case "AkumaGuideChat":
                        return "如果我是你, 我会在白天离潭渊远点. 雾大的要死, 你都看不见什么鬼东西蹲在里面. 如果我记得没错的话, 你可以带一个用龙爪做的灯笼, 就能看的清楚了. ";
                        case "YamataGuideChat":
                        return "因为某种原因, 燎狱里的火山在晚上活动频繁. 白天就会比较安静. 如果你能用那些九头蛇之爪做个掩护物品, 也许你能走过去？ 他们似乎不受那灰烬的影响…";
                        case "JungleGuideChat":
                        return "我注意到丛林的生物会掉一些奇怪的植物。 它们硬得像钉子， 也许可以用它们来打造装备。 ";
                        case "BroodMotherGuideChat":
                        return "你知道燎狱火山底部的那些蛋吗？ 是啊， 如果我是你， 我可不会碰那些， 除非你想对付一条非常愤怒的龙。 ";
                        case "HydraGuideChat":
                        return "在潭渊的湖底有一些……的蒴荚。 我真的不想知道那个九头蛇怪吃什么。 我也不想破坏它们， 那只蜥蜴可能会因为食物不好而变得暴躁。 ";
                        case "VoidGuideChat":
                        return "那些东边的浮岛把我吓坏了。 他们只是……在那里。 我想他们上面有些小隔间， 但我不会去那里。 ";
                        case "HardModeGuideChat1":
                        return "嘿， 如果你去地下混沌之地的话， 我想你可以得到一些魂， 就像你在邪恶环境中得到的那样。 这些应该有用。 ";
                        case "HardModeGuideChat2":
                        return "嘿， 你知道潭渊里的蟾蜍吗？ 我听说他们囤积了很多硬币。 快去打那些恶心的怪物， 然后砰的一声！都是现金。 ";
                        case "PlantBossGuideChat":
                        return "你听到泰拉心球里传来的音乐了吗？ 我觉得下面有些新东西。 看看你能不能弄到他们持有的绿色棱镜。 我想你可以用它建造一个牛逼的制作台。 ";
                        case "EquinoxBossGuideChat":
                        return "你知道天空中那些发光的球吗？ 如果我没记错， 它们会随时间而变化。 如果你一天不同的时间去， 也许你能得到不同的东西？ ";
                        case "AnubisChatMask":
                        return "嘿, 看上去帅的一批.";
                        case "AnubisChat1":
                        return "你不会恰巧擅长揉肚子吧? ";
                        case "AnubisChat2":
                        return "你知道去海滩看到沙滩上有沙子多可怕吗? 想象一下一直都有. 这就是我的生活. ";
                        case "AnubisChat3":
                        return "第一千次, 别 来 摸 我 的 毛!";
                        case "AnubisChat4":
                        return "嘿, 我背后很痒. 能(嘶)帮我挠挠吗? ";
                        case "AnubisChat5":
                        return "别, 我不带跳骚项圈. \n \n*挠 挠*";
                        case "AnubisChat6":
                        return "每个人都问我谁是一个好孩子, 但我很沮丧, 因为他们从来没有告诉我什么是好孩子. ";
                        case "AnubisChat7":
                        return "你见过我的尾巴吗? 我需要好好教教它. ";
                        case "AnubisChat8":
                        return "沙漠巨灵被打败了但是它没从我这得到任何东西! 核实一下!";
                        case "AnubisChat9":
                        return "顺便感谢你让我在这落脚. 在沙漠里走了几千年的滋味真不好受. ";
                        case "AnubisChat10":
                        return "对了, 我写了泰拉史记. 但是我也写了另外一本伟大的书, 《阿努比斯生命史诗历险记》! 要看看? ";
                        case "AnubisChat11":
                        return "你不觉得讨厌吗, 当";
                        case "AnubisChat12":
                        return "红色的血垃圾";
                        case "AnubisChat13":
                        return "紫色的脏东西";
                        case "AnubisChat14":
                        return "占领了你的世界的时候? 想想就真tm恶心. ";
                        case "AnubisChat15":
                        return "我最讨厌什么生物? 很简单, 史莱姆之王. 如果它刚好掉在你身上, 祝你能不用喷灯把你身上的凝胶洗掉. ";
                        case "AnubisChat16":
                        return "天哪, 我之前试过和他们搭讪, ";
                        case "AnubisChat17":
                        return "不想让我加入他们的聊天. 我想知道, 这些女人和血月是怎么回事? ";
                        case "AnubisChat18":
                        return "我试着和";
                        case "AnubisChat19":
                        return "搭讪, 她对我很有礼貌. 很...奇怪...尤其是在血月的时候. ";
                        case "AnubisChat20":
                        return "嗨, 不错的衣服. ";
                        case "AnubisChat21":
                        return "嗨, 喜欢我飘起来的派对帽吗? 魔法真好玩. ";
                        case "AnubisChat22":
                        return "你这些泰拉瑞亚的朋友很喜欢迪斯科吗? 我会跳布吉舞. ";
                        case "AnubisChat23":
                        return "嘿嗯...如果有人问, ";
                        case "AnubisChat24":
                        return "是从我这获得他的所有信息, 懂? ";
                        case "AnubisChat25":
                        return "那个";
                        case "AnubisChat26":
                        return "太可怕了. 知道很多boss的消息...实际上, 我只希望他不会跟踪我或是怎么样. . ";
                        case "AnubisChat27":
                        return "我认为, ";
                        case "AnubisChat28":
                        return "不会让人知道他到底有多聪明. 我是说, 看看那张脸. 又纯真, 又原始, 又天才. ";
                        case "AnubisChat29":
                        return "一直对我大喊大叫. 只是因为我把他做的鞋子都吃光了. 这不是我的错, 谁让他用上好的皮革做的. ";
                        case "AnubisChat30":
                        return "别问";
                        case "AnubisChat31":
                        return ", 他的商品哪来的. 很讨厌. ";
                        case "AnubisChat32":
                        return "...然后他把它翻了过来，原来他把他自己变成了一根腌咸菜，这是我见过的最搞笑的臭狗*。我在和谁说话？我也不知道";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(Anubis)
                    {
                       case "AnubisName":
                        return "Летописец легенд";
                        case "SetChatButtons1":
                        return "Поменять";
                        case "SetChatButtons2":
                        return "Что мне теперь делать? ";
                        case "SetChatButtons3":
                        return "Грибы. Я серьезно. ";
                        case "SetChatButtons4":
                        return "Больше грибов. ";
                        case "SetChatButtons5":
                        return "Тиски Хаоса.";
                        case "SetChatButtons6":
                        return "Жирная Дракониха";
                        case "SetChatButtons7":
                        return "Трехголовый Урод";
                        case "SetChatButtons8":
                        return "Три желанья";
                        case "SetChatButtons9":
                        return "Крута как лед";
                        case "SetChatButtons10":
                        return "Попался";
                        case "SetChatButtons11":
                        return "Механическая Масса";
                        case "SetChatButtons12":
                        return "Две головы, но нуль мозгов";
                        case "SetChatButtons13":
                        return "Еще один босс-червь, черт";
                        case "SetChatButtons14":
                        return "Древний Суда";
                        case "SetChatButtons15":
                        return "Ужасные близняшки";
                        case "SetChatButtons16":
                        return "Древний Ярости";
                        case "SetChatButtons17":
                        return "Древний Гнева";
                        case "SetChatButtons18":
                        return "Древний Гибели";
                        case "SetChatButtons19":
                        return "Вестник Раздора";
                        case "SetChatButtons20":
                        return "Каменные хранители";
                        case "SetChatButtons21":
                        return "Скрипящая головная боль";
                        case "SetChatButtons22":
                        return "Что за червь";
                        case "SetChatButtons23":
                        return "Прыгающий громила";
                        case "SetChatButtons24":
                        return "Карга-гарпия вернулась";
                        case "SetChatButtons25":
                        return "Богатеет и убивает тебя";
                        case "SetChatButtons26":
                        return "Гнев Кролика";
                        case "SetChatButtons27":
                        return "Я требую реванш.";

                        case "GetBookChat":
                        return  @"Ты достал ее! Мою лимитированную копию моей-же почитаемой биографии! Спасибо, друг. А знаешь что? Можешь забирать ее как подарок. Смотри, я даже афтограф оставлю...
Ой, кажется, я использовал свое рунное перо для этого. Ну, теперь она волшебная.";
                        case "GetSummonItemChat":
                        return "После того, как ты разнес те механические болванки, мне стало интересно насколько ты будешь хорош против меня. Вот, возьми этот скипетр и используй его в пустыне, когда будешь готов. ";
                        case "GetSummonItemChat2":
                        return "Возьми это. Это старый артифакт, который скоро станет полезным. Он укажет тебе на...особое место для людей этого мира. Они объяснят, что делать. Нам кое-что нужно сделать, если хотим очистить мир от зла.";

                        case "CombatTextChat":
                        return "Я пошел домой, мир тебе.";

                        case "downedMonarchY":
                        return "... и все? ";
                        case "downedMonarchN":
                        return "Эй, видел эти маленькие красные грибы повсюду? Я слышал, что если объединить их в одну массу и помахать немного, то придет их король и попытается тебя убить. Я обязан его видеть. ";
                        case "downedFungusY":
                        return "Хорошая работа. А теперь честно, ты на грибах сейчас? ";
                        case "downedFungusN":
                        return "Пещеры светящихся грибов всегда заставляли меня чувствовать себя странно по какой-то причине. В любом случае, хочешь магические способности получше? Там обитает огромный гриб-монстр, в котором сокрыты отличные возможности для магии. Просто не забудь заткнуть свой нос, пока ты там. ";
                        case "downedGripsY":
                        return "Неплохо сработано с гигантскими руками. Может быть, мелкие, наконец, отстанут. ";
                        case "downedGripsN":
                        return "Эти летающие ночные когти - это кошмар. Они меня бесят. В своих путешествиях я сталкивался с двумя ОООООООЧЕНЬ крупными когтями. Может, если убить их, то и более меньшие уберутся отсюда. Наверное, если ты убьешь несколько когтей и покажешь это, то привлечешь их больших собратьев. ";
                        case "downedBroodY":
                        return "Она была самым жирным драконом, которого я видел. ";
                        case "downedBroodN":
                        return "Драконы нравятся? Нет? Вот беда! Хочешь вооружение получше? Тогда тебе нужно убить ОГРОМНУЮ дракониху сделанную из лавы. Она большая, страшная и слишком ЖИРНААЯЯЯ, чтобы летать, но она все равно летает. ";
                        case "downedHydraY":
                        return "Эта Гидра была ужасно надоедливой, скатертью ей дорога. Ну, хотя бы ее дочка помягче нравом... хмм? Кто? Объясню тебе позже. ";
                        case "downedHydraN":
                        return "Трясина всегда была местом собрания для всех самых омерзительных тварей, но там есть одна большая, и у нее 3 головы. Она всегда ворчливая, и неважно, когда я пытаюсь зайти к ней в логово, она всегда пытается СЪЕСТЬ меня!";
                        case "downedDjinnY":
                        return "Ха! И кто теперь крутой парень, ты, мешок с песком!";
                        case "downedDjinnN":
                        return "ЭТА СУ-- Ой, привет! Извини, я был просто немного зол насчет драки, которая у меня была с Пустынным Джинном. Этот волшебный идиот и его придурки зафлексили меня своими мускулами. Не мог бы ты пойти и преподать им пару уроков? ";
                        case "downedSerpentY":
                        return "Надеюсь, что ты себе ничего не 'ОТМОРОЗИЛ'! Бадум-тсс... Да, я знаю, что это было плохо";
                        case "downedSerpentN":
                        return "Змеи! И откуда там только змеи? Я ненавижу их! В любом случае, недавно, в тундре, встретились эти снежные змеи, которые не оставляют меня в покое. Можешь поиграть в истребителя и узнать по пути, что они делали? ";
                        case "downedRetrieverY":
                        return "Тебе не попадалась моя третья версия ’Жизнь и Эпические приключения Анубиса, собаки-путешественника!’? ";
                        case "downedRetrieverN":
                        return "Помнишь Руки Хаоса? Те мерзкие, желающие схватиться за все ручонки? Так вот, есть их робо-версия и она ворует мои вещи. Можешь оказать мне услугу и швырнуть в нее гаечный ключ или что-то тип того? ";
                        case "downedRaiderY":
                        return "Так это был всего лишь огромный робот? Ну, иногда люди просто раздувают из мухи слона. Кстати, эта штука была почти такая же жирная, как Мать Стаи. ";
                        case "downedRaiderN":
                        return "Люди говорили, что видели в небе огромную тень в одну из ночей. Она была настолько большая, что закрыла собой всю луну на секунду. Ты должен разгадать эту тайну и сделать что-нибудь. ";
                        case "downedOrthrusY":
                        return "Я думаю, что Орф-X сейчас - это то, что он ест... ";
                        case "downedOrthrusN":
                        return "Помнишь Гидру? Так вот, появилась еще одна и она еще больше. И еще она робот. И... э-... она стреляет... молниями? Так что... э -... удачи!";
                        case "downedEquinoxY":
                        return "Неплохо ты справился с Червями Равноденствия. Я уверен в этом, потому что уже неделя прошла. Надеюсь, что не пропустил встречу с моей медсестрой...";
                        case "downedEquinoxN":
                        return "Любишь червей? Я тоже нет, но угадай что. В небе два больших червя, которые контролируют смену дня и ночи, и они оба конченые и жестокие. Удачи";
                        case "AnubisScapterLost":
                        return "Ты ПОТЕРЯЛ скипетр?! Я не могу раздавать их как конфеты! В любом случае, вот еще один.";
                        case "downedAnubisBY":
                        return "Мог быть и подобрее со мной, знаешь ли. До сих пор спина болит.";
                        case "downedAnubisBN":
                        return "Я слышал, что есть летописец легенд, который очень накаченный и красивый. А еще все леди любят его за потрясающие способности судьи душ. Что за парень!";
                        case "downedAthenaY":
                        return "Слава равноденствию, ты заткнул этих маленьких засранцев! Если бы они бы накричали на меня хотя бы еще раз, я бы поджарил одного из них на вертеле и поел бы жареной курочки на ужин.";
                        case "downedAthenaN":
                        return "Ты знаешь о визжащих гарпиях в небесах? Они очень противные существа, называющие себя серафимами, КОТОРЫЕ ПРОСТО НЕ МОГУТ ЗАТКНУТЬСЯ! У них есть лидер в небесном дворце на востоке, может, если ты ее проучишь, то они, наконец заткнут свои рты!";
                        case "downedRajahY":
                        return "Ты победил Раджу?! Пффт, ага, конечно. Я видел, как он наказывал мнимых богов. Невозможно!";
                        case "downedRajahN":
                        return "Ты видел этих кроликов, прыгающих вокруг все время? Я... ну... я бы их не трогал на твоем месте. Есть легенда о 'Защитнике', который их оберегает от угроз нашего мира. Почему легенда? Потому что нет существа, которое с ним сражалось и выжило. Неплохая история, а? ";
                        case "downedGreedYBookY":
                        return "Эй, спасибо, что вернул мою книгу. Жадность украл ее когда-то, скорее всего из-за блестяшек, которые я наколдовал на нее. Покопайся в этой пещере еще. Может быть, есть еще полезные вещи, которые он украл. ";
                        case "downedGreedYBookN":
                        return "Эй..э-... ты нашел мою книгу? Нет? Покопайся в горе наворованного барахла в пещерах. Я уверен, что она там.";
                        case "downedGreedN":
                        return "Эй, ем, под землей есть огромная куча сокровищ, но она охраняется скупым червем. Ты должен пойти и проверить это место на наличие кучи сокровищ, но... ем, там есть кое-что мое. Можешь сбегать туда  и забрать это для меня? Не волнуйся, когда ты увидишь, то сразу поймешь, что это мое. ";
                        case "downedAthenaAY":
                        return "Хм? Кто такой Вариан спрашиваешь? Я этого слова годами не слышал... так давно, что еле помню имя. Хотя я припоминаю что-то про еще одного вариана, который где-то здесь.";
                        case "downedAthenaAN":
                        return "Эй, мелкая надоедливая ведьма нашла тебя? Извини, что рассказал ей где ты. Она не переставала визжать мне в ухо. В любом случае, похоже, что Афина хочет реванш. Будь осторожен, друг, звучит как ловушка.";
                        case "GreedACalamityMod":
                        return "Знаешь, твое сражение с Пожирателем Богов напомнило мне Жадность немного... Я имею ввиду, подумай над этим. У них обоих есть возможность создавать червоточины, и они оба адаптируются к тому, что они едят. Могут ли они быть... не, это бред...или..?";
                        case "downedGreedAY":
                        return "Так все это время он скрывал свое истинную силу. Интересно почему... может ли он от кого то скрываться..?";
                        case "downedGreedAN":
                        return "Знаешь, я помню историю о старом добром хвататель-воровать-твое-барахло. Когда он только появился, то был намного сильнее, чем когда ты победил его. Может он стал слабее со временем..? Или, может... нет, невозможно. ";
                        case "downedSistersY":
                        return "Прекрасно ты задал этим двум зажравшимся крысам урок! Они этого явно не ожидали!";
                        case "downedSistersN":
                        return "Помнишь старых добрых Гидру и Мать Стаи? Ну, у этих двух есть дочери. БОЖЕ, какие же они надоедливые..! Каждый раз, когда я иду в биомы хаоса, эти двое так и ждут момента, что бы испортить мой день! Можешь преподать им урок или два? ";
                        case "downedAkumaY":
                        return "Акума думает, что он весь такой суровый и дерзкий. Как по мне, пролетая мимо, он пытается выглядеть круто, но полностью проваливается в этом. В любом случае, может, ты хочешь налить себе немного воды на волосы. Ты там немного подгорел. ";
                        case "downedAkumaN":
                        return "Кто вообще будет называть змея солнца демоном? Вот я вообще без понятия... Но Акуме уже пора уйти. Он постоянно превращает мои пустыни в стекло своим огненным дыханием. Это меня бесит! ";
                        case "downedYamataY":
                        return "Спасибо за то, что заткнул этого 7-голового гада! Из-за него я хотел вырвать себе всю шкуру! ";
                        case "downedYamataN":
                        return "Ямата-сопливый нытик! Он жалуется на все И НИКОГДА, БЛ###, НЕ ЗАТЫКАЕТСЯ! СЕРЬЕЗНО, ДАВАЙ ТЫ попробуй разобраться с 7-ю противными кричащими головами дракона, которые еще и постоянно перекрикивают друг друга!";
                        case "downedZeroY":
                        return "…Буду честен. Мне не нравиться, что эта штука сказала после смерти... ";
                        case "downedZeroN":
                        return "Бывал в Пустоте? Ну, Летающие страшные острова на востоке? Там есть БОЛЬШАЯ страшная машина, которая всегда там просто летает! В любом случае, после того как ты пришлепнул Лунного Лорда, я услышал массивную шоковую волну из Пустоты. Можешь сходить туда и проверить, хотя бы для меня? ";
                        case "downedShenY":
                        return "БОЖЕ-- Я знал, что в тебе что-то есть! Потрясающе! Хотя... он был очень зол, когда ты победил его... такой же злой, как когда его победила--эхм, забудь.  ";
                        case "downedShenN":
                        return "Акума и Ямата... знаешь ли, эти двое когда-то были одним целым. И боже, этот парень был силен. Он стер с лица земли 2 цивилизаций. В любом случае, ты что-то хотел? ";
                        case "downedRajahCY":
                        return "Так старый добрый Раджа отдал свой скипетр? Похоже, что он считает тебя идеальным выбором для следующего защитника. Я бы, наверное, так же поступил. Он правильно поступил.";
                        case "downedRajahCN":
                        return "Кролики были взбешены в последнее время. Что-то не так, я чувствую...";
                        case "Stones":
                        return "После смерти старика Шена, я почувствовал как... очень древняя магия активировалась. Может попробовать снова сразиться с сильнейшими врагами, которых ты встретил? Кстати, ты видел гоблина призывателя? О боже, она стала такой сильной. Интересно, что это с ней. ";
                        case "else":
                        return "Я не знаю? Спрашивай меня о любых сильных существах, с которыми ты можешь сразиться.  ";
                        case "AkumaGuideChat":
                        return "На твоем месте, я бы не приходил в Трясину днем. Там становится очень туманно и ничего не видно. Я думаю, что ты можешь сделать фонарь из когтей пламени. Он может помочь. ";
                        case "YamataGuideChat":
                        return "По какой-то причине, Вулкан в Инферно становится активным ночью. Может быть, ты сможешь пройти там ночью, если сделаешь подобие прикрытия из когтей гидры. Их пепел не обжигает.";
                        case "JungleGuideChat":
                        return "Я заметил, что враги в джунглях оставляют странные листья после смерти. Они очень жесткие. Может сделать из них обмундирование? ";
                        case "BroodMotherGuideChat":
                        return "Ты видел яйца в глубине вулкана Инферно? Ага, ем, будь я на твоем месте, не трогал бы их. Если конечно не хочешь биться с очень злой драконихой. ";
                        case "HydraGuideChat":
                        return "Под озером, в трясине, есть коконы... чего-то. Я даже знать не хочу, что эта Гидра ест. Но я не ломал бы их. Эта ящерица очень раздражается, когда что-то трогает ее еду. ";
                        case "VoidGuideChat":
                        return "Летающие острова на востоке пугают меня. Они... просто есть. Мне кажется, что там какие-то дома, но подыматься туда я не собираюсь.  ";
                        case "HardModeGuideChat1":
                        return "Эй, если ты спустишься в пещеры биомов хаоса, то можешь достать души, по типу тех, которые можно собрать в биомах зла. ";
                        case "HardModeGuideChat2":
                        return "Эй, встречал лягушек в трясине? Я слышал, что в них накоплена куча монет. Так что идешь, бьеш их немного и БУМ! Деньжата! ";
                        case "PlantBossGuideChat":
                        return "Ты слышал музыку, идущую из Террариума? Мне кажется, что там появилось что-то новенкое. Посмотрим, сможешь ли ты положить свои ручонки на их новые зеленые призмы. Думаю, что из них ты можешь сделать новую безумную крафтинговую станцию. ";
                        case "EquinoxBossGuideChat":
                        return "Видел эти светящиеся сферы в атмосфере? Если я точно помню, то они меняются в зависимости от времени суток. Может ты получишь что-то новое с них, если вскопаешь их в другое время суток? ";
                        case "AnubisChatMask":
                        return "Эй, неплохо выглядишь, очаровашка!";
                        case "AnubisChat1":
                        return "А ты хорош в чесании пузика? ";
                        case "AnubisChat2":
                        return "Знаешь  ужасное чувство, когда на пляже  песок попадает в твои штаны. А теперь представь, что с тобой такое постоянно. Добро пожаловать в мою жизнь! ";
                        case "AnubisChat3":
                        return "В сотый раз говорю, Я НЕ ФУРРИ!!!";
                        case "AnubisChat4":
                        return "Эй, можешь почесать у меня за спинкой? ";
                        case "AnubisChat5":
                        return @"Нет, я не надену ошейник от блох.
						*чешет себя*";
                        case "AnubisChat6":
                        return "Все у меня спрашивают 'кто хороший мальчик', но никогда не дают ответа. ";
                        case "AnubisChat7":
                        return "Ты видел мой хвост? Мне надо его проучить! ";
                        case "AnubisChat8":
                        return "Пустынный Джинн может и ходячая груда мышц, но посмотри на это!";
                        case "AnubisChat9":
                        return "Кстать, спасибо, что разрешил поселиться здесь. Устаешь, когда бродишь по пустыне несколько сотен лет. ";
                        case "AnubisChat10":
                        return "Да, я написал ’История Террарии’. Но еще я написал великую книгу ’Жизнь и Эпические приключения Анубиса, собаки-путешественника!’.  ";
                        case "AnubisChat11":
                        return "А тебя не выбешивает, когда";
                        case "AnubisChat12":
                        return "Красная мясная зараза";
                        case "AnubisChat13":
                        return "Фиолетовая гнилая зараза";
                        case "AnubisChat14":
                        return "Захватывает твой биом? Отвратительно! ";
                        case "AnubisChat15":
                        return "Какое существо я больше всего ненавижу? Ах, это просто. Королевский Слизень. Если он упадет на тебя, то удачи с очисткой шкуры от слизи без паяльной лампы. ";
                        case "AnubisChat16":
                        return "Боже, я попытался познакомиться с девушкой недавно, ";
                        case "AnubisChat17":
                        return "а она ударила меня со всей силы. Что не так с девушками во время Кровавой Луны?  ";
                        case "AnubisChat18":
                        return "Я попытался познакомиться с девушкой,";
                        case "AnubisChat19":
                        return "и она была со мной такая добрая. Это... странно... учитывая, что тогда была кровавая луна... ";
                        case "AnubisChat20":
                        return "Эй, неплохой костюмчик! ";
                        case "AnubisChat21":
                        return "Эй, нравиться моя летающая праздничная шапка? Магия может быть забавной. ";
                        case "AnubisChat22":
                        return "Диско все еще нравиться террарианам, да? Я буги умею танцевать. ";
                        case "AnubisChat23":
                        return "Эй, ем... если кто спросит. ";
                        case "AnubisChat24":
                        return "достал всю его информацию от меня, понял? ";
                        case "AnubisChat25":
                        return "Это";
                        case "AnubisChat26":
                        return "немного пугает. Он тоже много знает о боссах... надеюсь, что он не преследует меня. ";
                        case "AnubisChat27":
                        return "Я думаю, ";
                        case "AnubisChat28":
                        return "что он не признает, насколько он умный на самом деле. Я имею в виду, посмотри на это лицо. Просто. Чистый. Гений.  ";
                        case "AnubisChat29":
                        return "продолжает кричать на меня за то, что я ем всю обувь, которую он делает. Это не моя вина, что все его работы сделана из особого материала. ";
                        case "AnubisChat30":
                        return "Не спрашивай";
                        case "AnubisChat31":
                        return "где он достает свое барахло? Оно такое мерзкое. ";
                        case "AnubisChat32":
                        return "... И затем он перевернул все, и оказалось, что он превратил себя в огурец. Самое смешное дерьмо, которое я видел. Хах? С кем я говорю? Без понятия.";
                    }
                }
            else
                {
                    switch(Anubis)
                    {
                        case "AnubisName":
                        return "Legendscribe";
                        case "SetChatButtons1":
                        return "Switch Info";
                        case "SetChatButtons2":
                        return "What Do I do now?";
                        case "SetChatButtons3":
                        return "Mushrooms. I'm serious.";
                        case "SetChatButtons4":
                        return "More Mushrooms.";
                        case "SetChatButtons5":
                        return "Get a Grip";
                        case "SetChatButtons6":
                        return "Deadweight Dragon";
                        case "SetChatButtons7":
                        return "Three-Headed Freak";
                        case "SetChatButtons8":
                        return "3 Wishes";
                        case "SetChatButtons9":
                        return "Cool as Ice";
                        case "SetChatButtons10":
                        return "Gotcha'";
                        case "SetChatButtons11":
                        return "Mechanical Mass";
                        case "SetChatButtons12":
                        return "Two heads; zero brains";
                        case "SetChatButtons13":
                        return "More worm bosses god dammit.";
                        case "SetChatButtons14":
                        return "Ancient of Judgement";
                        case "SetChatButtons15":
                        return "Terrible Twins";
                        case "SetChatButtons16":
                        return "Ancient of Fury";
                        case "SetChatButtons17":
                        return "Ancient of Wrath";
                        case "SetChatButtons18":
                        return "Ancient of Doom";
                        case "SetChatButtons19":
                        return "Discordian Doomsayer";
                        case "SetChatButtons20":
                        return "The Stonekeepers";
                        case "SetChatButtons21":
                        return "Squakin' Headache";
                        case "SetChatButtons22":
                        return "What a Worm";
                        case "SetChatButtons23":
                        return "Hopping Hoodlum";
                        case "SetChatButtons24":
                        return "Harpy Hags are back";
                        case "SetChatButtons25":
                        return "Riches and R.I.P. you";
                        case "SetChatButtons26":
                        return "Wrath of the Wabbit";
                        case "SetChatButtons27":
                        return "I demand a rematch.";

                        case "GetBookChat":
                        return @"You got it! My limited edition copy of my esteemed biogrophy! Thanks, pal. You know what? As a gift, you can have it. Here, I'll even autograph it for you.
...Whoops, I accidentally used my runic quill to sign it. Oh well, now it's magic.";
                        case "GetSummonItemChat":
                        return "Hey, thanks for getting back to me. I wanna test your strength. After you thrashed those mechanical meatheads, I'm interested in seeing how you fair against someone like me. Here, take this scepter and go use it in the desert on the surface whenever you're ready. I'm ready whenever.";
                        case "GetSummonItemChat2":
                        return "Take this. It's an old artifact that will come in handy soon. It should direct you towards a...special place to the people of this world. read the plaques there. They'll tell you what to do. We got some work to do if we want to clear out some of the major evils in the world.";

                        case "CombatTextChat":
                        return "I'm headed home. Peace.";

                        case "downedMonarchY":
                        return "...that was it?";
                        case "downedMonarchN":
                        return "Hey, you know all these little red mushrooms growing everywhere? I hear if you squish a bunch of them together and wave them around, their king or something will come and attempt to run you down. I gotta see that.";
                        case "downedFungusY":
                        return "Nice work. Now be honest, how high are you right now?";
                        case "downedFungusN":
                        return "The glowing mushroom caves always make me feel loopy for some reason. Anyways, you want better magic abilities? There's a big mushroom monster that has some great magic abilities infused into it. Just plug your nose while your down there.";
                        case "downedGripsY":
                        return "Nice job taking down those giant hands. Maybe the little ones will finally leave me alone for once.";
                        case "downedGripsN":
                        return "Those flying claws at night are a nightmare to deal with, and they freak me out. In my travels, I've come across these two REEEEEEEEALLY big ones. Maybe if you kill them, the little ones will bugger off. Maybe killing a few of them and showing that you have in some way will call them down.";
                        case "downedBroodY":
                        return "That was the fattest dragon I've ever seen in my life.";
                        case "downedBroodN":
                        return "Like dragons? No? Too bad. You want better gear? You gotta go kill of this HUGE dragon made of lava. She's big, scary, and WAAAAAAAAAAAY too fat to fly, but she does anyways.";
                        case "downedHydraY":
                        return "Good riddance. That hydra can't seem to lay off. At least her daughter is a bit more mellow...huh? Who? I'll explain later, good job.";
                        case "downedHydraN":
                        return "The Mire has always been a gathering spot for all the nastiest lizards, but there's a really big one there, and it's got 3 heads. She's really grouchy all the time, and any time I try to go into her den, she tries to EAT me!";
                        case "downedDjinnY":
                        return "Hah! Who's tough now you sandy sadsack!";
                        case "downedDjinnN":
                        return "THAT SON OF A-- Oh hi. Sorry, I was just a bit angry about a little tussle I had with desert djinn. That magical meathead and his goons to stop flexing their muscles on me. Could you go teach em' a thing or two?";
                        case "downedSerpentY":
                        return "Hope you didn't get any 'FROSTBITES'! *buh-dum-tish* ...yeah I know that was lame.";
                        case "downedSerpentN":
                        return "Snakes, why does it always have to be snakes? I hate 'em! Whatever, in the tundra recently, there have been these snow snerpents that won't leave me alone. Could ya play exterminator and find out what they're doing?";
                        case "downedRetrieverY":
                        return "Did you get my 3rd edition of 'The Life and Epic Adventures of Anubis the Wonder Dog!' back by any chance?";
                        case "downedRetrieverN":
                        return "Remember the Grips of Chaos? Those nasty grabby hands? There's a robotic one and it keeps stealing my stuff. Can you do me a favor and go throw a wrench at it or something?";
                        case "downedRaiderY":
                        return "So it was a giant robot after all? Well, sometimes people just overthinking the problem. That thing was almost as fat as the Broodmother.";
                        case "downedRaiderN":
                        return "People said that they saw strange shade in one of the nights. It was so big that it even closed the moon for a moment. You should discover this mystery and do something.";
                        case "downedOrthrusY":
                        return "I guess orthrus is what it eats, now.";
                        case "downedOrthrusN":
                        return "Remeber the Hydra? There's a bigger one out there. And it's a robot. And it uh...shoots lightning. So uh...good luck!";
                        case "downedEquinoxY":
                        return "Nice job taking out the Equinox worms. I could tell you did because it's like a week later now. I hope I didn't miss my nurse's appointment...";
                        case "downedEquinoxN":
                        return "Like worms? Me neither, but guess what? There are 2 big ones that control the flow of day and night, and they're tough buggers. Good luck.";
                        case "AnubisScapterLost":
                        return "You LOST the scepter?! I can't go handing these things out like candy, you know! Anyways, here's another one.";
                        case "downedAnubisBY":
                        return "You could have gone a little easier on me, ya know. My back still hurts from that.";
                        case "downedAnubisBN":
                        return "I hear there’s this lorekeeper guy that’s really jacked and handsome, and all the ladies love him for his amazing soul-judging abilities. What a guy.";
                        case "downedAthenaY":
                        return "Thank the Equinox, you shut those annoying little squakers up! I was about to roast one of them over a spit and have fried chicken for dinner if they shrieked at me one more time.";
                        case "downedAthenaN":
                        return "You know those screechin' harpies up in the sky? Well there are these REALLY obnoxious ones called seraphs who just WILL NOT SHUT UP!!! They have a leader in that sky palace to the east. Maybe if you give her the ol' one-two, they'll shut their yappers.";
                        case "downedGreedYBookY":
                        return "Hey thanks for getting my book back. Greed stole it a while ago, probably because of the gold highlights I used to bind it. Look around that cave, maybe there's some other stuff he's stolen?";
                        case "downedGreedYBookN":
                        return "Hey uh...did you find my thing yet? No? Just dig around in that loot pile down there, I'm sure it's there somewhere.";
                        case "downedGreedN":
                        return "Hey uh, there's this HUGE hoard of treasure underground somewhere with lots of gold in it, but it's guarded by this really stingy worm. You should go check it out for a boatload of booty, but uh...there's something of mine down there. Could you go get it for me? Don't worry, when you see it, you'll know it's mine.";
                        case "downedRajahY":
                        return "You bested Rajah? Pft, yeah right, I've seen him trounce supposed gods before, there is no way you beat him..!";
                        case "downedRajahN":
                        return "Hey, you know those bunnies that hop around all the time? I uh...I wouldn't harass them if I were you. There's a legend around here of a sort of 'Guardian' of sorts that prtotects them from danger. Why is it a legend? Because apparently nobody has ever fought this thing and lived. Neat story, eh?";
                        case "downedFAnubisY":
                        return "You did good, pal. I'm pretty impressed, even though I was still holding back. Huh? My full power..? Like I said earlier, some things should stay locked away for your own good.";
                        case "downedFAnubisN1":
                        return "I want a rematch, ";
                        case "downedFAnubisN2":
                        return ". Not because of my pride, either. I need to make sure you're ready for what's ahead. Meet me in the desert and fight me again. And be ready.";
                        case "downedAthenaAY":
                        return "Huh? What's a Varian you ask? That's something I haven't heard in years...so long ago that I barely even remember the name. Although I do recall something about another one kicking around somewhere...";
                        case "downedAthenaAN":
                        return "Hey, did that annoying little witch find you? Sorry for telling her where you were, she wouldn't stop screeching in my ear. Anyways, looks like Athena wants a rematch. Stay on your guard, bud. This seems like a trap...";
                        case "GreedACalamityMod":
                        return "Ya know, you duking it out with the Devourer of Gods reminded me of Greed a bit...I mean think about it. They both have wormhole capabilities and they both adapt to what they eat. Could they possibly be...nah, that'd be rediculous...or..?";
                        case "downedGreedAY":
                        return "So he WAS hiding his true power all along. I wonder why, though...could he be hiding from something, perhaps..?";
                        case "downedGreedAN":
                        return "You know, I seem to remember a story about ol' grabby-mc-steal-your-crap. When he first showed up in these parts, he was much stronger than he was when you kicked his rear end. Maybe he got weaker as time went on..? Or maybe...nah, that couldn't be it.";
                        case "downedSistersY":
                        return "Nice, you taught those two spoiled brats a lesson! Those two didn't see it coming!";
                        case "downedSistersN":
                        return "Remember ol' Brood and Hydra? Well, those two have daughters. And MAN they're annoying..! Every time I go into the chaos biomes, those two are just waiting to ruin my day! Can you go give em' the ol' one-two?";
                        case "downedAkumaY":
                        return "Akuma thinks he's edgy. To me, he just comes across as trying to be way too cool and failing. Anyways, might wanna run some water through your hair. You got a little singed up there.";
                        case "downedAkumaN":
                        return "Why would anyone call a sun serpent a demon? I have no idea personally...but Akuma has got to go. He always glasses my deserts with his flame breath and it pisses me off.";
                        case "downedYamataY":
                        return "Thanks for shutting up that 7-headed sissy. He makes me want to tear my fur out.";
                        case "downedYamataN":
                        return "Yamata, the whiny nit! He complains about everything, and he WONT SHUT UP! LIKE SERIOUSLY, YOU try and deal with seven obnoxiously loud dragon heads that chatter constantly and talk over eachother!";
                        case "downedZeroY":
                        return "...I'll be honest. I don't like what that thing said after it died one bit.";
                        case "downedZeroN":
                        return "You know the void? Those spooky floating islands to the east? There's a BIG scary machine there that's always just floating there. Anyways, after you slammed the moon lord, I heard a massive shockwave come from the void. Could you check it out for me?";
                        case "downedShenY":
                        return "Holy-- I knew you had it in you, man! Awesome job! Although...he seemed pretty angry when you beat him...almost as angry as when he got beat by-- er, nevermind that.";
                        case "downedShenN":
                        return "Akuma and Yamata...you know, those two were once one being. And hot dang, that guy was powerful. He leveled 2 civilizations one time. Anyways, so what was it that you needed?";
                        case "downedRajahCY":
                        return "So ol' Rajah finally decided to hang up the scepter? Seems like he thought you were the right pick to protect those in need. I'd do what he says. It's the right thing to do, in my opinion.";
                        case "downedRajahCN":
                        return "Them bunnies have been getting pretty riled up lately. Something ain't right. I just feel it...";
                        case "Stones":
                        return "You know...after you whooped ol' Shen, I felt some...very old magic activate. Maybe you should pay a visit to some of the tougher bosses you've come across? By the way, have you seen the Goblin Summoner recently? Jeeze she got tough. Wonder what her deal is.";
                        case "else":
                        return "I dunno? Ask me about any strong creatures you can fight currently.";
                        case "AkumaGuideChat":
                        return "If I were you, I'd stay out of the Mire during the day. It gets really foggy and you can't see jack squat in there. I think you can make a lantern out of blaze claws if I remember correctly to help see through it...";
                        case "YamataGuideChat":
                        return "The Volcano in the inferno gets pretty active at night for some reason. During the day, it seems to calm down. Maybe you could get through if you made some kind of cover out of those Hydra claws? They seem to not be affected by that ash...";
                        case "JungleGuideChat":
                        return "Creatures in the jungle drop these really weird leaves I've noticed. They're tough as nails, though, so making gear out of it could be a pretty interesting idea...";
                        case "BroodMotherGuideChat":
                        return "You know those eggs at the bottom of the inferno volcano? Yeah uh, I wouldn't touch those if I were you, unless you want to deal with a very angry dragon.";
                        case "HydraGuideChat":
                        return "Underneath the lake in the mire are some pods of...something. I don't really want to know what that Hydra thing eats. I wouldn't break them either, that lizard gets feisty when something messes with her food.";
                        case "VoidGuideChat":
                        return "Those floating islands to the east freak me out. They're just...there. I think there's some houses on them, but I'm not going up there.";
                        case "HardModeGuideChat1":
                        return "Hey, if you go to the underground chaos biomes, I think you can get some souls like the ones you get in the evil biomes. Those should be useful.";
                        case "HardModeGuideChat2":
                        return "Hey you know those frogs in the mire? I hear they hoard tons of coins. Just go beat the bajeezus out of them and BAM! Cash money.";
                        case "PlantBossGuideChat":
                        return "Did you hear that music coming from the Terrarium? I think there's something new down there. See if you can get your hands on one of those green prisms they have. I think you can make a crazy new crafting station with it.";
                        case "EquinoxBossGuideChat":
                        return "You know those glowing spheres in the sky? If I remember correctly, they change depending on the time of day. Maybe you can get something different from them if you go at different points in the day?";
                        
                        case "AnubisChatMask":
                        return "Hey, lookin' good handsome.";
                        case "AnubisChat1":
                        return "You wouldn’t happen to be good at belly rubs would you?";
                        case "AnubisChat2":
                        return "You know that awful feeling of getting sand in your swim trunks after going to the beach? Imagine having that all the time. Welcome to my life.";
                        case "AnubisChat3":
                        return "For the thousandth time, I AM NOT A FURRY!";
                        case "AnubisChat4":
                        return "Hey, I got this really bad itch on my back. Could ya get it for me?";
                        case "AnubisChat5":
                        return @"No. I won’t wear a flea collar.
 
*Scratch Scratch*";
                        case "AnubisChat6":
                        return "Everyone asks me who's a good boy, but I'm upset because they never tell me who it is.";
                        case "AnubisChat7":
                        return "Have you seen my tail? I need to teach it a thing or two.";
                        case "AnubisChat8":
                        return "The Desert Djinn may be ripped but he's got nothing on me! Check it!";
                        case "AnubisChat9":
                        return "Thanks for letting me crash here by the way. Walking around the desert for a couple thousand years really tuckers ya out.";
                        case "AnubisChat10":
                        return "I wrote the Terraria Historia, yes. But I also wrote another great book. 'The Life and Epic Adventures of Anubis the Wonder Dog!' Want a copy?";
                        case "AnubisChat11":
                        return "Don't you hate it when ";
                        case "AnubisChat12":
                        return "red fleshy crap";
                        case "AnubisChat13":
                        return "purple muggy crap";
                        case "AnubisChat14":
                        return " takes over your biome? it's disgusting.";
                        case "AnubisChat15":
                        return "What creature do I hate most? Oh that's easy, King Slime. If that thing lands on you, good luck washing the slime out of your clothes or fur without a blowtorch.";
                        case "AnubisChat16":
                        return "Geeze, I tried hitting on ";
                        case "AnubisChat17":
                        return " earlier and they kicked the bajeezus out of me. What is it with these ladies and blood moons?";
                        case "AnubisChat18":
                        return "I tried hitting on ";
                        case "AnubisChat19":
                        return " and she was totally polite to me. That's...odd...especially during a blood moon.";
                        case "AnubisChat20":
                        return "Hey, nice outfit.";
                        case "AnubisChat21":
                        return "Hey, like my floating party hat? Magic is fun.";
                        case "AnubisChat22":
                        return "Disco is still popular with you terrarians, right? I can do a mean boogie.";
                        case "AnubisChat23":
                        return "Hey uh...if anyone asks, ";
                        case "AnubisChat24":
                        return " got all of his info from me, got it?";
                        case "AnubisChat25":
                        return "That ";
                        case "AnubisChat26":
                        return " is pretty chill. Knows a lot about bosses too...actually I hope he isn't stalking me or anything.";
                        case "AnubisChat27":
                        return "I think ";
                        case "AnubisChat28":
                        return " isn't letting in on how smart he actually is. I mean look at that face. Pure. Raw. Genius.";
                        case "AnubisChat29":
                        return " keeps yelling at me for eating all the shoes he makes. It's not my fault he makes them with premium lether.";
                        case "AnubisChat30":
                        return "Don't ask ";
                        case "AnubisChat31":
                        return " where he gets his merch. It's nasty.";
                        case "AnubisChat32":
                        return "...and then, he flips it over, and it turns out this guy turned himself into get this; a pickle. Bloody funniest sh*t I've ever seen. Huh? Who am I talking to? I dunno.";
                    }
                }
            return"";
        }

        public static string TownNPCGoblinSlayer(string GoblinSlayer)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(GoblinSlayer)
                    {
                        case "GoblinSlayerName":
                        return "哥布林杀手";
                        case "GoblinSlayerChat1":
                        return "我不相信";
                        case "GoblinSlayerChat2":
                        return ". 他身上有些我不喜欢的东西...";
                        case "GoblinSlayerChat3":
                        return "告诉我他们从哪来的, 他们杀哥布林杀的真多. 我希望有时间去看看那个地方. 听起来很荣幸. ";
                        case "GoblinSlayerChat4":
                        return "我不喜欢哥布林. ";
                        case "GoblinSlayerChat5":
                        return "看到什么我能杀了的哥布林吗? ";
                        case "GoblinSlayerChat6":
                        return "哥布林是这片土地上的祸害. ";
                        case "GoblinSlayerChat7":
                        return "突击几个哥布林的聚集地? ";
                        case "GoblinSlayerChat8":
                        return "为什么我讨厌哥布林? 因为他们是哥布林. ";
                        case "GoblinSlayerChat9":
                        return "嘿, 你在外面能帮我杀些哥布林吗? 把他们的灵魂给我, 我给你我额外的哥布林杀手装备. ";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(GoblinSlayer)
                    {
                        case "GoblinSlayerName":
                        return "Убийца Гоблинов";
                        case "GoblinSlayerChat1":
                        return "Я не доверяю";
                        case "GoblinSlayerChat2":
                        return ". Есть что-то в нем, что мне не нравиться.";
                        case "GoblinSlayerChat3":
                        return "рассказал мне, что в землях, откуда он пришел, они убивают очень много гоблинов. Как-нибудь посещу это место. ";
                        case "GoblinSlayerChat4":
                        return "Я не люблю гоблинов. ";
                        case "GoblinSlayerChat5":
                        return "Видел каких-нибудь гоблинов, которых я могу убить? ";
                        case "GoblinSlayerChat6":
                        return "Гоблины-бедствие этой земли. ";
                        case "GoblinSlayerChat7":
                        return "Видели какие-нибудь лагеря гоблинов? ";
                        case "GoblinSlayerChat8":
                        return "Почему я ненавижу гоблинов? Потому что они гоблины. ";
                        case "GoblinSlayerChat9":
                        return "Эй, пока ты будешь там, можешь убить парочку гоблинов? Я обменяю их души на новое вооружение для уничтожения гоблинов. ";
						case "GoblinSlayerChat10":
                        return "Сока.";
						case "GoblinSlayerChat11":
                        return "Есть и другие армии, вторгающийся в этот мир. К счастью, избиение гоблинов подготовило меня. Принеси мне особый добычу с них, и я дам тебе особое вооружение, которое достал с них.";
                    }
                }
            else
                {
                    switch(GoblinSlayer)
                    {
                        case "GoblinSlayerName":
                        return "Goblin Slayer";
                        case "GoblinSlayerChat1":
                        return "I don't trust ";
                        case "GoblinSlayerChat2":
                        return ". There's just something about him that I don't like...";
                        case "GoblinSlayerChat3":
                        return " tells me that where he's from, they kill goblins a lot. I wish to visit this place sometime. Sounds glorious.";
                        case "GoblinSlayerChat4":
                        return "I don't like goblins.";
                        case "GoblinSlayerChat5":
                        return "Seen any goblins I can kill?";
                        case "GoblinSlayerChat6":
                        return "Goblins are a scourge on this earth.";
                        case "GoblinSlayerChat7":
                        return "Find any good goblin dens to raid?";
                        case "GoblinSlayerChat8":
                        return "Why do I hate goblins? Because they're goblins.";
                        case "GoblinSlayerChat9":
                        return "Hey, while you're out there, can you kill some goblins for me? Give me their souls and I'll trade you for some of my extra goblin slaying gear.";
                        case "GoblinSlayerChat10":
                        return "Souka.";
                        case "GoblinSlayerChat11":
                        return "There are other armies invading this land like those dreaded goblins. Fortunately, beating the snot out of those little twerps has prepared me. Bring me some special things from them and I'll give you some of the stuff I've picked up from them.";

                }
            }
            return"";
        }
        public static string TownNPCLovecraftian(string Lovecraftian)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(Lovecraftian)
                    {
                        case "button1":
                        return "商店";
                        case "button2":
                        return "提供材料";
                        case "LovecraftianChat1":
                        return "你知道我从哪来, 我就是你的世界所称的“顶尖工匠”";
                        case "LovecraftianChat2":
                        return "我不是唯一来这里的人. 我的世界出现时空裂缝时, 一大堆其他的东西都跟着来了. 但是, 像克苏鲁之眼和克苏鲁之脑早已经在这了, 不知道从哪里来的. ";
                        case "LovecraftianChat3":
                        return "...你看什么呢? 好像你以前没见过乌贼人一样. ";
                        case "LovecraftianChat4":
                        return "没错, 我是个女人. 那又怎么样? 是触须让你远离我? ";
                        case "LovecraftianChat5":
                        return "如果你有自我保护的意识, 我会远离海岸边那艘沉船. 我脖子上的可怕东西就在那儿, 尤其是…啊, 别在意. ";
                        case "LovecraftianChat6":
                        return "有没有在你的触须里发现它是怎么长出来的? 你没有? 只有我有? ";
                        case "LovecraftianChat7":
                        return "嘿, 你的世界很有趣. 你能给我带些不同生物群落的样本来研究吗? 如果你愿意的话, 我可以做些东西来和你交易. ";
                        case "LovecraftianChat8":
                        return "哦!这可真尴尬!不幸的 ";
                        case "LovecraftianChat9":
                        return ". 他的船是我从裂缝里掉出来时损坏的那艘. ";
                        case "LovecraftianChat10":
                        return "那个";
                        case "LovecraftianChat11":
                        return "是瞎扯. 克苏鲁在他说“*你*”(脏话:cnm)之前就会把他给压扁";
                        case "LovecraftianChat12":
                        return "那个晃晃悠悠的死人吓死我了, 说什么我就是个行走的恐怖故事. 我不知道, 我觉得他知道的太多了...";
                        case "LovecraftianChat13":
                        return "有趣的事实：月亮领主和克苏鲁是兄弟. 至少有一次我遇到的粉红小仙女是这么说的";
                        case "PurityFlaskChat":
                        return "哦!这些就是纯净碎片吗? 完美!拿着这个. 你可以用我做的这个特殊的瓶子来净化大多数生物圈. ";
                        case "AshJarChat":
                        return "龙, 啊哈? 我见过更可怕的. 不管怎样, 这是一个新烧瓶. 小心, 很烫. ";
                        case "DarkwaterFlaskChat":
                        return "这是什么. . ? 它实际上是一个……东西的球. 我会调查的. 给, 新烧瓶. 去吧. ";
                        case "CorruptionFlaskChat":
                        return "你捂着嘴干什么? 有些东西闻起来比这更难闻. 只不过是腐肉在分解而已. ";
                        case "CrimsonFlaskChat":
                        return "嗯…骨头? 我以前见过这样的. 我来的地方有类似的. ";
                        case "HallowFlaskChat":
                        return "这是什么东西? 它……闪闪发光……? 不管怎样, 我会做更多的研究. 这是一个新烧瓶. ";
                        case "VoidFlaskChat":
                        return "哇, 这太重了!这是啥? 我以前从未见过这种金属. 哦, 对了. 新烧瓶. 在这里. ";
                        case "FungicideChat":
                        return "嗯…发光的孢子? 除了发光的蘑菇, 我从没见过这样的东西. 说到蘑菇, 我在这里找到了一些非常好的杀蘑菇配方. 我把它做成了一个烧瓶. ";
                        case "SporeSacChat1":
                        return "这是什么东西? 太黏了……我想我能让它起到点作用. 顺便说一下, ";
                        case "SporeSacChat2":
                        return "教了我如何制作蘑菇孢子. 你可以随意使用它. ";
                        case "GlowingSporeSacChat1":
                        return "嗯, 这东西有点炫酷. *嗅* 妈呀, 这东西真刺鼻. 不管怎么说, 我做了一些";
                        case "GlowingSporeSacChat2":
                        return "的发光孢子. ";
                        case "JungleFlaskChat":
                        return "哦, 非常感谢!这些刺针非常适合做一些临时注射器. 这, 是我开发的一套全新的将森林变成丛林的方案. 漂亮吗, 嗯? ";
                        case "IceFlaskChat":
                        return "现在这东西就很有用了. 感谢. 嘿, 说到冰, 看看这个. 造雪和除雪烧瓶? 喜欢吗? 两个换一个!";
                        case "ForestFlaskChat":
                        return "我真希望这些东西能跟我回到我来的地方. 超可爱~!哦, 但如果我是你, 我会小心对待这些小家伙的. 树精告诉我有一个巨大的怪物保护他们……不管怎样, 当你在外面的时候, 我做了一个新的烧瓶. 它能把丛林变成森林. 小心点. ";
                        case "SquidListChat":
                        return "这是我研究需要的物品清单. 如果你丢了, 我很乐意为你写一篇新的. ";
                        case "NothingChat":
                        return "嗯……没东西? 我需要一些研究的东西. 我想要一些来自各种环境的重要材料. 怪物碎片、植物等. ";
                    }
                }
                else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(Lovecraftian)
                    {
                        case "button1":
                        return "Магазин";
                        case "button2":
                        return "Отдать ингредиенты";
                        case "LovecraftianChat1":
                        return "Ты знаешь, откуда я. Из места, что в вашем мире называется 'горячим'~";
                        case "LovecraftianChat2":
                        return "Я не единственное существо, пришедшее в этот мир. Куча всего другого пришло сюда со мной. Но Глаз и Мозг Ктулху всегда были тут. Я без понятия, кто они такие. ";
                        case "LovecraftianChat3":
                        return "... на что ты смотришь? Ведешь себя так, будто никогда не видел женщину с щупальцами. ";
                        case "LovecraftianChat4":
                        return "Да, я женщина. А что такого? Тебя моя тентаклевая борода смущает? ";
                        case "LovecraftianChat5":
                        return "Если у тебя есть хотя бы какое-то чувство самосохранения, то не лезь в затонувший корабль. Ужасные вещи таятся там, особенно опасен... неважно. ";
                        case "LovecraftianChat6":
                        return "Никогда не находил вещи в своих тентаклях? Нет? Только я? ";
                        case "LovecraftianChat7":
                        return "Хей, твой мир очень интересный. Не мог бы ты принести мне некоторые образцы из разных биомов мне для изучения? Если хочешь, то я могу сделать некоторые крутые штуки для обмена с тобой ";
                        case "LovecraftianChat8":
                        return "Ох. Это странно. Бедняга. ";
                        case "LovecraftianChat9":
                        return ". это его корабль был уничтожен, когда я выпала из разрыва. ";
                        case "LovecraftianChat10":
                        return "Этот";
                        case "LovecraftianChat11":
                        return "говорит своей з#######. Ктулху бы раздавил его еще до того, как он скажет 'ауч' ";
                        case "LovecraftianChat12":
                        return "Этот мертвый чувак, шатающийся вокруг, меня пугает. А это о чем то-да говорит, учитывая, что я ходячий древний ужас. У меня просто такое чувство, что он знает слишком много.";
                        case "LovecraftianChat13":
                        return "Интересный факт; Ктулху и Лунный Лорд- братья. По крайней мере, мне так сказала розовая леди-пикси.";
                        case "PurityFlaskChat":
                        return "Ох! Это что, осколки чистоты? Идеально! Вот, держи. Ты можешь очищать большиство биомов этим новым типом раствора. ";
                        case "AshJarChat":
                        return "Драконы, эх? Я и пострашнее вещи видела. В любом случае, вот новый раствор. Осторожно, он горячий. ";
                        case "DarkwaterFlaskChat":
                        return "Что это..? Это буквально шар... чего то. Я исследую это. Вот, новый раствор. Делай с ним что хочешь. ";
                        case "CorruptionFlaskChat":
                        return "Почему ты затыкаешь нос? Есть вещи, которые пахнут намного хуже чем эта. Это же просто гниющая плоть. ";
                        case "CrimsonFlaskChat":
                        return "Хм... Кости? Я уже видела такие. Похожи на кости из моего мира ";
                        case "HallowFlaskChat":
                        return "Что это? Оно... блестящее? В любом случае, я исследую это. Вот новый раствор. ";
                        case "VoidFlaskChat":
                        return "Вау! Вот это тяжелый металл! Никогда такого еще не видела. Ах, да. Новый раствор. Вот. ";
                        case "FungicideChat":
                        return "Хмм... светящиеся споры? Никогда такого не видела, ну, кроме светящихся грибов. Говоря о грибах, я нашла рецепт для очень хорошего фунгицида. Сделала его раствором.  ";
                        case "SporeSacChat1":
                        return "Что это? Оно такое мягкое... Я поработаю над этим, полагаю. А, кстати, ";
                        case "SporeSacChat2":
                        return "научил меня делать грибные споры. Используй их, когда хочешь. ";
                        case "GlowingSporeSacChat1":
                        return "Хм, эти светящиеся. *Нюхает* О великий Ктулху! Моb ноздри горят! Ладно, вот, я сделала светящуюся версию спор";
                        case "GlowingSporeSacChat2":
                        return " ";
                        case "JungleFlaskChat":
                        return "Ох! Спасибо! Эти маленькие жала очень хорошо сработают для самодельных шприцов. Вот, я разработала совершенно новый раствор, который превращает лес в джунгли! Вонючий, да? ";
                        case "IceFlaskChat":
                        return "А вот ЭТО будет полезно! Спасибо. Насчет льда. Смотри. Раствор тундры И удаляющий раствор! Нравиться? Два по цене одного!";
                        case "ForestFlaskChat":
                        return "Хотелось бы их в моем мире. Они такие милахи~! Ох, но я была бы поаккуратней с ними. Дриада рассказала мне, что их защищает огромный монстер... в любом случае, пока ты уходил, я сделала новый раствор. Он превращает джунгли в лес. Будь с ним поосторожнее. ";
                        case "SquidListChat":
                        return "Вот лист всего, что нужно для моих исследований. Если потеряешь этот, то я просто напишу новый. ";
                        case "NothingChat":
                        return "Хмм... ничего? Для исследований мне нужны вещи из списка. Мне могут подойти материалы из биомов. Части монстров, растения и так далее. ";
                    }
                }
                else
                {
                    switch(Lovecraftian)
                    {
                        case "button1":
                        return "Shop";
                        case "button2":
                        return "Supply Ingredients";
                        case "LovecraftianChat1":
                        return "You know, where I’m from, I’m what your world would call ‘hot stuff.’";
                        case "LovecraftianChat2":
                        return "I wasn’t the only thing that came here. A whole bunch of other stuff came through with me when a spacial rift opened up in my world. Stuff like the Eye of Cthulhu and the Brain of Cthulhu were already here though. No clue where those two came from.";
                        case "LovecraftianChat3":
                        return "...What are you looking at? You act like you've never seen a squid-person before.";
                        case "LovecraftianChat4":
                        return "Yes I’m a woman. What about it? Is it the tentacle beard that threw you off?";
                        case "LovecraftianChat5":
                        return "If you have any sense of self preservation, I’d avoid that sunken ship in the ocean just off the coast. Scary things from my neck of the woods hang out there, especially... nevermind.";
                        case "LovecraftianChat6":
                        return "Ever just find things in your tentacles that you don’t know how they got there? No? Just me?";
                        case "LovecraftianChat7":
                        return "Hey, your world is pretty interesting. Could you bring me some samples from different biomes for me to study? If you do, I can make some neat stuff to trade with you.";
                        case "LovecraftianChat8":
                        return "Oh. This is awkward. Poor ";
                        case "LovecraftianChat9":
                        return ". His ship was the one that got destroyed when I fell out of that rift.";
                        case "LovecraftianChat10":
                        return "That ";
                        case "LovecraftianChat11":
                        return " is talking out of his ass. Cthulhu would most likely squash him before he could even say *ech*.";
                        case "LovecraftianChat12":
                        return "That dead guy shambling around freaks me out, and that’s saying something considering I’m a walking horror story. I don’t know, I just feel like he knows too much...";
                        case "LovecraftianChat13":
                        return "Fun fact; The Moon Lord and Cthulhu are brothers. At least that’s what some pink pixie lady I met one time told me.";
                        case "PurityFlaskChat":
                        return "Oh! Are those purity shards? Perfect! Here, take this. You can purify most biomes with this special flask I made.";
                        case "AshJarChat":
                        return "Dragons, eh? I've seen scarier. Anyways, here's a new flask. Careful, it's hot.";
                        case "DarkwaterFlaskChat":
                        return "What is this..? It's literally a ball of...something. I'm gonna look into it. Here, new flask. Go crazy.";
                        case "CorruptionFlaskChat":
                        return "Why are you gagging? There are things that smell way worse than this. It's only decomposing flesh.";
                        case "CrimsonFlaskChat":
                        return "Hm...bones? I've seen ones like these before. Similar to ones from where I came.";
                        case "HallowFlaskChat":
                        return "What is this stuff? It's...sparkly..? Whatever, I'll research it a bit more. Here's a new flask.";
                        case "VoidFlaskChat":
                        return "Wow this is heavy! What is this? I've never seen this kind of metal before. Oh right. New flask. Here.";
                        case "FungicideChat":
                        return "Hmm...glowing spores? I've never seen something like this aside from glowing mushrooms. Speaking of mushrooms, here, I found a recipe for some really good fungicide. I made it into a flask.";
                        case "SporeSacChat1":
                        return "What is this stuff? It's so squishy...I'll make it work I guess. Oh by the way, ";
                        case "SporeSacChat2":
                        return " showed me how to make mushroom spores. Feel free to use it how you see fit.";
                        case "GlowingSporeSacChat1":
                        return "Hm, this stuff is pretty glowy. *Sniff* Yowza that burns my nostrils. Anyways, here, I made a glowing version of  ";
                        case "GlowingSporeSacChat2":
                        return "'s spores.";
                        case "JungleFlaskChat":
                        return "Oh thank you so much! These stingers will work nicely for some makeshift syringes. Here, I've developed a brand new solution that changes forest into jungle. Nifty, huh?";
                        case "IceFlaskChat":
                        return "Now THIS will come in handy. Thank you. Hey, speaking of ice, check this out. Snow creation AND removal flasks? You like it? Two for the price of one!";
                        case "ForestFlaskChat":
                        return "I wish we had these back where I came from. They're adorable~! Oh, but I'd be careful with these little guys if I were you. The dryad told me there's some giant monster that protects them...anyways, while you were out, I made a new flask. It turns Jungle into forest. Careful with it.";
                        case "SquidListChat":
                        return "Here's a list of some things I need for my research. If you lose it, I'll happily write up a new one for you";
                        case "NothingChat":
                        return "Hmm...nothing? I need stuff to study. I'd like some important materials from biomes. Monster pieces, plants, etc.";
                    }
                }
            return"";
        }
        public static string TownNPCMushman(string Mushman)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(Mushman)
                    {
                        case "button1":
                        return "商店";
                        case "button2":
                        return "诡异植物";
                        case "MushmanChat1":
                        return "那些荧光松露看着真让人难受. ";
                        case "MushmanChat2":
                        return "提议让我用一次他的热水澡缸, 我拒绝了, 因为我有更好的事情要做. ";
                        case "MushmanChat3":
                        return "你知道, 赤孢皇并不是看起来那样. ";
                        case "MushmanChat4":
                        return "别问我从哪儿买的蘑菇炼药. ";
                        case "MushmanChat5":
                        return "我有药, 你有钱, 交易? ";
                        case "MushmanChat6":
                        return "有一次有人问我红松露的味道是否和蓝松露一样好. 当然不是. 蓝松露要咸得多. ";
                        case "NoMushroomChat1":
                        return "我需要一些奇怪的植物. 给我拿些来, 我给你一些特殊的炼金蘑菇. 很适合做药水. ";
                        case "NoMushroomChat2":
                        return "...没有? ";
                        case "NoMushroomChat3":
                        return "请给我植物. 没有植物我没法给你药水. ";
                        case "SpecialChat1":
                        return "癫狂蘑菇? 太好了!这是一种特殊的蘑菇. 这个真的很有用. 只是…不要直接吃. ";
                        case "SpecialChat2":
                        return "哦, 一只癫狂蘑菇!这些我很喜欢, 因为它们有特殊作用. 这里有一些彩虹炼药菇. ";
                        case "SpecialChat3":
                        return "你应该知道, 在两个蘑菇地里都可以找到这些东西. 以防万一, 一定要仔细检查. 它们真的很有用. ";
                        case "MushroomChat1":
                        return "感谢. 这些蘑菇比毫无价值的染料更有用, 对吗? ";
                        case "MushroomChat2":
                        return "这里. 更多的彩色蘑菇满足你的炼药需要. 只是…不要吃它们. ";
                        case "MushroomChat3":
                        return "我要这些染料做什么用? 呃…麻烦. 离我远点, 我有事情要做!";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(Mushman)
                    {
                        case "button1":
                        return "Магазин";
                        case "button2":
                        return "Странные растения";
                        case "MushmanChat1":
                        return "Эти светящиеся трюфели такие тупицы. ";
                        case "MushmanChat2":
                        return "предложил мне залезть в его джакузи. Я отказался, потому что у меня были дела поважнее. ";
                        case "MushmanChat3":
                        return "Знаешь, грибной Монарх не тот, кем он кажется. ";
                        case "MushmanChat4":
                        return "Не спрашивай, где я беру грибы на зелья. ";
                        case "MushmanChat5":
                        return "У меня есть зелья, у тебя есть деньги. Обменяемся? ";
                        case "MushmanChat6":
                        return "как то раз спросил меня, настолько ли красные трюфели вкусны, как синие. Конечно нет. Синие более мерзкие.  ";
                        case "NoMushroomChat1":
                        return "Мне нужны странные растения для кое-чего. Принеси мне парочку, и я дам тебе редкие алхимические грибы.  ";
                        case "NoMushroomChat2":
                        return "... нет растений? ";
                        case "NoMushroomChat3":
                        return "Растения, пожалуйста. Я не дам тебе грибы без них. ";
                        case "SpecialChat1":
                        return "Гриб безумия? Миленько! Дам тебе в награду особый тип гриба. Он очень полезный... просто не ешь его. ";
                        case "SpecialChat2":
                        return "Ох, гриб безумия! Люблю их за особые свойства! Вот, держи парочку радужных грибов.";
                        case "SpecialChat3":
                        return "Знаешь ли, ты можешь находить грибы безумия в обоих типах грибного биома. Так что проверяй оба, на всякий случай.  ";
                        case "MushroomChat1":
                        return "Спасибо. Эти грибы намного полезнее, чем никчемные красители, верно? ";
                        case "MushroomChat2":
                        return "Вот. Больше разноцветных грибов для всех твоих алхимических нужд. Просто... не ешь их.";
                        case "MushroomChat3":
                        return "Для чего я использую странные растения? Эм... для чего-то. А теперь оставь меня, у меня есть незаконченные дела!";
                    }
                }
            else
                {
                    switch(Mushman)
                    {
                        case "button1":
                        return "Shop";
                        case "button2":
                        return "Strange Plants";
                        case "MushmanChat1":
                        return "Those glowing truffles are all just such downers.";
                        case "MushmanChat2":
                        return " offered to let me get in his hot tub one time. I denied because I had better things to do";
                        case "MushmanChat3":
                        return "The Mushroom Monarch isn't all he seems, you know.";
                        case "MushmanChat4":
                        return "Don't ask where I get the mushrooms for my potions.";
                        case "MushmanChat5":
                        return "I got potions, you got money. Wanna trade?";
                        case "MushmanChat6":
                        return " asked me one time if red truffles tasted as good as blue ones. Obviously not. Blue truffles are way saltier.";
                        case "NoMushroomChat1":
                        return "I need strange plants for something. Bring me some and I'll give you some special alchemical mushrooms. Good for making potions.";
                        case "NoMushroomChat2":
                        return "...no plants?";
                        case "NoMushroomChat3":
                        return "Plants please. I won't give you mushrooms without them.";
                        case "SpecialChat1":
                        return "A Madness Mushroom? Sweet! Here's a special kind mushroom for payment. This one is really useful. Just...don't eat it directly.";
                        case "SpecialChat2":
                        return "Oh, a Madness Mushroom! These ones I like a lot because of their special properties. Here, have a few rainbow shrooms.";
                        case "SpecialChat3":
                        return "You can find these in both mushroom biomes, you know. Make sure to check both of them just in case. They're really useful.";
                        case "MushroomChat1":
                        return "Thank you. These mushrooms are way more useful than worthless dyes, am I right?";
                        case "MushroomChat2":
                        return "Here. More colored mushrooms for all your brewing needs. Just...don't eat them.";
                        case "MushroomChat3":
                        return "What do I use these dye materials for? Uh...things. Now leave me be, I have stuff to do!";

                    }
                }
            return"";
        }
        public static string TownNPCSamurai(string Samurai)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(Samurai)
                    {
                        case "SamuraiChat1":
                        return "我认识";
                        case "SamuraiChat2":
                        return "有段时间了. 他很有文化素养. ";
                        case "SamuraiChat3":
                        return "我不太喜欢";
                        case "SamuraiChat4":
                        return "的麦酒. 太浓了. 诚实的说, 我喜欢清酒. ";
                        case "SamuraiChat5":
                        return "混沌之地并不总是这么.... emmm.... 混乱. ";
                        case "SamuraiChat6":
                        return "你见过我的剑吗? 我哪儿也找不到. ";
                        case "SamuraiChat7":
                        return "我们曾经是泰拉瑞亚最强大的民族 ...直到...他 来了...";
                        case "SamuraiChat8":
                        return "我记得我的老主人留给了我一些明智的话, 例如：“你在看Flash, 然而你没有安装Flash播放器”...我依旧在试图弄懂. ";
                        case "SamuraiChat9":
                        return "老实说, 我有一半的台词来自于幸运饼干. 来一块? ";
                        case "SamuraiChat10":
                        return "如果你只接受最好的, 你经常会得到最好的. \n(译者：William Somerset Maugham 威廉·萨默塞特·毛姆)";
                        case "SamuraiChat11":
                        return "改变可能会受伤, 但会带我们走向更好的道路. ";
                        case "SamuraiChat12":
                        return "只有体验过不幸, 你才会热爱生活. ";
                        case "SamuraiChat13":
                        return "大地永远是飞鸟的怀抱. ";
                        case "SamuraiChat14":
                        return "什么样的人便决定了做什么样的事. 同样, 做什么样的事也决定了是什么样的人. \n(译者：George Eliot 乔治·艾略特)";
                        case "SamuraiChat15":
                        return "别放弃. 还未放弃就不曾失败. ";
                        case "SamuraiChat16":
                        return "你已经知道在萦绕在你脑海中的问题答案了. ";
                        case "SamuraiChat17":
                        return "当下世界中最重要的是， 我们还活着. ";
                        case "SamuraiChat18":
                        return "你可以创造自己的幸福. ";
                        case "SamuraiChat19":
                        return "冬天来了, 春天还会远吗? \n(译者：Percy Bysshe Shelley 珀西·比希·雪莱)";
                        case "SamuraiChat20":
                        return "陌生人是你还没有和之交谈过的朋友. ";
                        case "SamuraiChat21":
                        return "今天你的鞋子让你快乐. ";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(Samurai)
                    {
                        case "SamuraiChat1":
                        return "Я общался с";
                        case "SamuraiChat2":
                        return "он культурный человек. ";
                        case "SamuraiChat3":
                        return "Я не большой фанат эля";
                        case "SamuraiChat4":
                        return ". Как по мне, он слишком крепкий. Если честно, предпочитаю саке. ";
                        case "SamuraiChat5":
                        return "Биома хаоса не всегда были такие... хаотичные. ";
                        case "SamuraiChat6":
                        return "Ты видел мой меч? Я не могу его нигде найти. ";
                        case "SamuraiChat7":
                        return "Когда то, мы были самой могущественной нацией в террарии... потом... пришел ОН...";
                        case "SamuraiChat8":
                        return "Я помню, как мастер мне сказал слова истины 'ВЫ ПЫТАЕТЕСЬ СМОТРЕТЬ FLASH КОНТЕНТ, НО У ВАС НЕ УСТАНОВЛЕН FLASH PLAYER'... я до сих пор думаю над этими словами. ";
                        case "SamuraiChat9":
                        return "Буду честен, я понимаю только половину строк из печенек с предсказаньем. Хочешь одно? ";
                        case "SamuraiChat10":
                        return "Забавно в жизни вот что: если вы отказываетесь принимать любые вещи, кроме самых лучших, то очень часто именно их и получаете.";
                        case "SamuraiChat11":
                        return "Изменения могут ранить, но они всегда ведут к лучшему. ";
                        case "SamuraiChat12":
                        return "Ты не можешь любить жизнь, пока не начнешь ей жить. ";
                        case "SamuraiChat13":
                        return "Земля всегда в голове летающей птицы. ";
                        case "SamuraiChat14":
                        return "Наши поступки определяют нас так же, как мы определяем наши поступки.";
                        case "SamuraiChat15":
                        return "Никогда не сдавайся. Ты не неудачник, если не сдаешься. ";
                        case "SamuraiChat16":
                        return "Ты уже знаешь ответ на вопросы в твоей голове. ";
                        case "SamuraiChat17":
                        return "Все происходит сейчас, в этом мире, ты должен жить. ";
                        case "SamuraiChat18":
                        return "Ты сам можешь сделать себе счастье. ";
                        case "SamuraiChat19":
                        return "Если зима пришла, то так ли далеко весна?";
                        case "SamuraiChat20":
                        return "Незнакомец, это просто друг, с которым ты еще не говорил. ";
                        case "SamuraiChat21":
                        return "Твои ботинки тебя сегодня осчастливят. ";
                    }
                }
            else
                {
                    switch(Samurai)
                    {
                        case "SamuraiChat1":
                        return "I've known ";
                        case "SamuraiChat2":
                        return " for a while. He's quite the man of culture.";
                        case "SamuraiChat3":
                        return "I'm not really a fan of ";
                        case "SamuraiChat4":
                        return "'s ale. It's a bit strong for my taste. I prefer Sake to be entirely honest.";
                        case "SamuraiChat5":
                        return "The chaos biomes weren't always so.... err.... chaotic.";
                        case "SamuraiChat6":
                        return "Have you seen my sword? I can't seem to find it anywhere.";
                        case "SamuraiChat7":
                        return "We used to be the most powerful nation in all of terraria... then... HE came...";
                        case "SamuraiChat8":
                        return "I remember my old master giving me wise words such as: 'YOU ARE TRYING TO VIEW FLASH CONTENT BUT YOU DO NOT HAVE A FLASH PLAYER INSTALLED'... I’m still trying to figure that one out.";
                        case "SamuraiChat9":
                        return "I'll be honest, I get half my lines from fortune cookies. Want one?";
                        case "SamuraiChat10":
                        return "If you refuse to accept anything but the best, you very often get it.";
                        case "SamuraiChat11":
                        return "Change can hurt, but it leads a path to something better.";
                        case "SamuraiChat12":
                        return "You cannot love life until you live the life you love.";
                        case "SamuraiChat13":
                        return "Land is always on the mind of a flying bird.";
                        case "SamuraiChat14":
                        return "Our deeds determine us, as much as we determine our deeds.";
                        case "SamuraiChat15":
                        return "Never give up. You're not a failure if you don't give up.";
                        case "SamuraiChat16":
                        return "You already know the answer to the questions lingering inside your head.";
                        case "SamuraiChat17":
                        return "It is now, and in this world, that we must live.";
                        case "SamuraiChat18":
                        return "You can make your own happiness.";
                        case "SamuraiChat19":
                        return "If winter comes, can spring be far behind?";
                        case "SamuraiChat20":
                        return "A stranger is a friend you have not spoken to yet.";
                        case "SamuraiChat21":
                        return "Your shoes will make you happy today.";
                    }
                }
            return"";
        }
        public static string BossChat(string BossInfo)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(BossInfo)
                    {
                        case "male":
                        return "他";
                        case "fimale":
                        return "她";
                        case "male2":
                        return " 他 ";
                        case "fimale2":
                        return " 她 ";

                        case "boy":
                        return ", 少年";
                        case "girl":
                        return ", 少女";

                        case "FatPixie":
                        return "扑哧";

                        case "AHDeath1":
                        return "啊啊啊啊啊! 别又这样了!!!";
                        case "AHDeath2":
                        return "艾希你看? 我告诉你这主意很蠢, 但是你就是不听...";
                        case "AHDeath3":
                        return "我为什么要跟你一块. . ? 我真应该让你自己和他们打. ";
                        case "AHDeath4":
                        return "闭嘴吧! 我觉得我们合力攻击";
                        case "AHDeath5":
                        return ", 我们应该可以把他打趴下!";
                        case "AHDeath6":
                        return "啊. . ! 闭嘴. . !";
                        case "AHDeath7":
                        return "无论怎样...我要回去了. 必须有人告诉父亲这臭小子的事. ";
                        case "AHDeath8":
                        return "好好好. . ! 好吧! 从这边走! 我要回燎狱!";
                        case "AHSpawn1":
                        return "好的, 你好, 在这看见你真惊喜~!";
                        case "AHSpawn2":
                        return "啊对, 我听说过你的一些事, 小子...你就是那个把我母亲痛打的常温动物. . !";
                        case "AHSpawn3":
                        return "啊对, 我听说过你的一些事, 小子...你在这些地方可搞了不少的麻烦...";
                        case "AHSpawn4":
                        return "以及我的...";
                        case "AHSpawn5":
                        return "...你还打伤了我妈...";
                        case "AHSpawn6":
                        return "你应该知道, 你就是个非常讨厌的家伙...";
                        case "AHSpawn7":
                        return "所以现在. . !";
                        case "AHSpawn8":
                        return "我们决定打的你嗷嗷叫. . ! 来吧, 遥酱, 让我们来教训教训这个常温东西~!";
                        case "AHSpawn9":
                        return "别再叫我遥酱了...从现在起. ";
                        case "AsheDowned":
                        return "哦啊. . ! 很疼, 你这个小鬼!";
                        case "HarukaDowned":
                        return "啊. . ! 哦啊...";
                        case "Akuma1":
                        return "水? ! 卧槽. . ! 我没法呼吸了!";
                        case "Akuma2":
                        return "呀呀呀呀呀呀. 我累了, 小子, 咱俩的比试要推迟了. 明天再来吧. ";
                        case "Akuma3":
                        return "我以为你们这些泰拉人更喜欢打架. 看来猜错了. ";
                        case "Akuma4":
                        return "嗨小子!天塌了, 注意点!";
                        case "Akuma5":
                        return "烈火与愤怒从天而降!";
                        case "Akuma6":
                        return "燎狱火山之灵啊!助我压扁这小子!";
                        case "Akuma7":
                        return "嘿小子!小心!";
                        case "Akuma8":
                        return "我来了!";
                        case "Akuma9":
                        return "小子, 阳光普照之处没有阴凉可乘!";
                        case "Akuma10":
                        return "热起来了, 对吗?";
                        case "Akuma11":
                        return "燎狱的火山终于平息了...";
                        case "Akuma12":
                        return "哈...你小子还不错, 但还不够强. 等你再强一点再回来找我. ";
                        case "Akuma13":
                        return "面对绝望之火吧!小子!";
                        case "Akuma14":
                        return "注意集中, 小子!";

                        case "AkumaA1":
                        return "艾希? 再来帮你一次亲爱的老爸料理这小子!";
                        case "AkumaA2":
                        return "懂了, 爸爸. . !";
                        case "AkumaA3":
                        return "嘿! 放开我爸!";
                        case "AkumaA4":
                        return "艾女...!";
                        case "AkumaA5":
                        return "明白吗? 你有火焰般炽热的精神! 小子, 我很喜欢这样!";
                        case "AkumaA6":
                        return "什么? !你怎么坚持这么长时间的? !为什么你这个小...我拒绝再被一个泰拉人打败! 攻击!";
                        case "AkumaA7":
                        return "啊啊. . ! 水! 我讨厌水!!!";
                        case "AkumaA8":
                        return "这次, 晚上可救不了你, 小子!已经是新的一天了! ";
                        case "AkumaA9":
                        return "你烧伤了, 小子";
                        case "AkumaA10":
                        return "嘿, 小子这次很光明磊落. 我印象深刻. 这儿. 拿走你的战利品. ";
                        case "AkumaA11":
                        return "啊…!怎么回事!我怎么会输给一个普通的泰拉人? !嗯……好小子, 你赢了, 公平公正. 这是你的奖励. ";
                        case "AkumaA12":
                        return "好好好. 你作弊. 你应该像一个真正的男子汉一样来专家模式挑战我. ";
                        case "AkumaA13":
                        return "天又要塌了! 集中注意!";
                        case "AkumaA14":
                        return "愤怒之火再次从天而降!";
                        case "AkumaA15":
                        return "你低估了龙的火力, 小子!";
                        case "AkumaA16":
                        return "烈火永不熄灭, 小子!";
                        case "AkumaA17":
                        return "注意点! 火山喷发了, 小子!";
                        case "AkumaA18":
                        return "来了!";
                        case "AkumaA19":
                        return "嘿小子? 喜欢烟花吗? 不? 真糟糕!";
                        case "AkumaA20":
                        return "该完美收场了, 小子!";
                        case "AkumaA21":
                        return "白昼不息烈日不止, 小子!";
                        case "AkumaA22":
                        return "面对烈日的怒火吧!";

                        case "AkumaAAshe1":
                        return "爸, 不! 啊! 你! 下次我们再见的时候, 我要把你活烤了. . !";
                        case "AkumaAAshe2":
                        return "哦啊， 混蛋...! 我先撤了!";
                        case "AkumaTransition1":
                        return "呵呵...";
                        case "AkumaTransition2":
                        return "你知道的, 小子...";
                        case "AkumaTransition3":
                        return "扇风可不能灭火...";
                        case "AkumaTransition4":
                        return "邪鬼巨龙已经觉醒!";
                        case "AkumaTransition5":
                        return "只会让他们更猛烈!";
                        case "AkumaTransition6":
                        return "你周围的空气开始升温...";

                        case "AnubisFalse":
                        return "哈! 再回去练练吧-- 或者, 打磨一下自己.";

                        case "AnubisGuys":
                        return "伙计们";
                        case "Anubisbud":
                        return "哥们";

                        case "Anubis1":
                        return "好, ";
                        case "Anubis2":
                        return ". 到地方了.";
                        case "Anubis3":
                        return "我希望你已经准备好了真正的战斗.";
                        case "Anubis4":
                        return "尤其是因为我现在处于我的超强形态.";
                        case "Anubis5":
                        return "准备好了吗? 我拿巴掌抽你的时候可不会手下留情!";
                        case "Anubis6":
                        return "开始吧!";
                        case "Anubis7":
                        return "再比一次吗? 好, 那可太有意思了!";

                        case "AnubisCombat1":
                        return "我靠!";
                        case "AnubisCombat2":
                        return "小心点! 被我的弹幕打中可有点疼.";
                        case "AnubisCombat3":
                        return "让我摇点人来";
                        case "AnubisCombat4":
                        return "接好了!";
                        case "AnubisCombat5":
                        return "砸烂你的头, 如何?";

                        case "AnubisTransition1":
                        return "...呼哈...";
                        case "AnubisTransition2":
                        return "...好了.";
                        case "AnubisTransition3":
                        return "我觉得...是时候了.";
                        case "AnubisTransition4":
                        return "有些事情已经无法阻止了.";
                        case "AnubisTransition5":
                        return "如果你想要和这个世界上的黑暗势力搏杀的话...";
                        case "AnubisTransition6":
                        return "我就必须确认你已经完全准备好了, 因为...倘若你还没准备好...";
                        case "AnubisTransition7":
                        return "...为你好，有些东西你还是不碰为妙.";

                        case "FAnubis":
                        return "...对不起, 看来你还没准备好.";

                        case "FAnubisCombat":
                        return "这次可没有预警.";

                        case "FAnubisWin":
                        return "...你做的很好，孩子. 让我们来制定一个前进的计划，准备好了就来找我. ";

                        case "Athena1":
                        return "哈..!";
                        case "Athena2":
                        return "们";
                        case "Athena3":
                        return "你! 地上的凡人";
                        case "Athena4":
                        return "我的六翼小天使们告诉我你一直在攻击他们! 为什么?!";
                        case "Athena5":
                        return "那我要给你好好上一课, 你这个不懂规矩的小混蛋!";
                        case "Athena6":
                        return "准备开战!";
                        case "Athena7":
                        return "呼...";
                        case "Athena8":
                        return "咱们快点搞完. 我整天没这么多闲空.";
                        case "Athena9":
                        return "那就滚远点...白痴.";
                        case "Athena10":
                        return "不跟你闹着玩了, 电闪雷鸣, 风暴降临!";
                        case "Athena11":
                        return "哦! 好, 好..! 我不打扰你了! 看在上帝的份上, 你给我小心着点, 懂吗?";
                        case "Athena12":
                        return "...那么. 你来啦.";
                        case "Athena13":
                        return "是时候夺回我的荣耀了..!";
                        case "Athena14":
                        return "准备开战!";

                        case "AthenaA1":
                        return "那就滚远点...白痴.";
                        case "AthenaA2":
                        return "哦! 好, 好..! 我不打扰你了! 看在上帝的份上, 你给我小心着点, 懂吗?";

                        case "AthenaDefeat1":
                        return "...呼...呼...";
                        case "AthenaDefeat2":
                        return "...我还是输了.";
                        case "AthenaDefeat3":
                        return "不.";
                        case "AthenaDefeat4":
                        return "我不会这样轻易的放弃.";
                        case "AthenaDefeat5":
                        return "有一句我的人民赖以为生的名言, 地上的凡人.";
                        case "AthenaDefeat6":
                        return "最耀眼的黎明...";
                        case "AthenaDefeat7":
                        return "最漆黑的深夜...";
                        case "AthenaDefeat8":
                        return "即便失败...";
                        case "AthenaDefeat9":
                        return "战 士 不 会 在 最 后 一 战 前 倒 下!!!";

                        case "Athena2Defeat1":
                        return "...为什么?!";
                        case "Athena2Defeat2":
                        return "...我怎么就是赢不了?!";
                        case "Athena2Defeat3":
                        return "...这事咱俩还没完, 地上的凡人";
                        case "Athena2Defeat4":
                        return "们";
                        case "Athena2Defeat5":
                        return "我最终会夺回属于我的荣耀...";
                        case "Athena2Defeat6":
                        return "...那时, 你给我小心着点.";
                        case "Athena2Defeat7":
                        return "黑暗, 混沌的力量在最近苏醒.";
                        case "Athena2Defeat8":
                        return "希望“他”不会回来...";
                        case "Athena2Defeat9":
                        return "一切平安.";

                        case "SeraphHerald1":
                        return "嘿! 地 上 的 凡 人! 对, 就是你, 你 这 个 臭 傻 X!";
                        case "SeraphHerald2":
                        return "雅典娜女王要求你立即再次去天穹卫城觐见!";
                        case "SeraphHerald3":
                        return "她要求重比一次, 而且这次, 她不会让你这么轻易把她打败!";
                        case "SeraphHerald4":
                        return "我也许会说祝你好运, 如果你能出现的话!";
                        case "SeraphHerald5":
                        return "不见不散你个傻X!";
                        case "SeraphHerald6":
                        return "啊对了, 呃, 那个喜欢偷别人东西的恶心虫子也想跟你再打一架.";

                        case "GreedFalse1":
                        return "呃呃呃呃呃呃 这 是 地 表 的 光! 太 亮 了! 太 刺 眼 了!";
                        case "GreedFalse2":
                        return "离 我 琳 琅 满 目 的 财 宝 远 点 你 这 个 臭 小 偷!";

                        case "Greed1":
                        return "谁在打扰我数钱?! 我忙的要死--";
                        case "Greed2":
                        return "...哦哦哦哦哦哦...那是...?";
                        case "Greed3":
                        return "你把我最喜欢吃的带来了..!";
                        case "Greed4":
                        return "金色大餐...";
                        case "Greed5":
                        return "以 及 一 个 泰 拉 人!!!";
                        case "GreedName":
                        return "金食饕餮";

                        case "GreedTransition1":
                        return "你..! 你 这 个 小--";
                        case "GreedTransition2":
                        return "好 了, 我 受 够 了!";
                        case "GreedTransition3":
                        return "你 想 偷 我 的 财 宝, 那 现 在...!";
                        case "GreedTransition4":
                        return "我 要 你 的 命! 呵 呵 呵 呵 呵!!!";
                        case "GreedAName":
                        return "鎏金万蟲王";

                        case "Rajah1":
                        return "正 义 不 会 被 欺 骗";
                        case "Rajah2":
                        return "正义得到伸张……";
                        case "Rajah3":
                        return "懦夫. ";
                        case "Rajah4":
                        return "你这次赢了, 杀人犯……但我会为那些被你残忍杀害的生物报仇的……";
                        case "Rajah5":
                        return "这 事 没 完, ";
                        case "Rajah6":
                        return "!我 会 和 你 打 到 最 后!";
                        case "Rajah7":
                        return "给我趴下. ";
                        case "Rajah8":
                        return "打得不错, ";
                        case "Rajah9":
                        return ". 拿着你的奖励. ";

                        case "SupremeRajahChat":
                        return "王公兔在攻击时散发巨大的愤怒能量, 强化了他的防御";

                        case "SupremeRajahDefeat1":
                        return "呼...";
                        case "SupremeRajahDefeat2":
                        return "...那么...";
                        case "SupremeRajahDefeat3":
                        return "甚至， 每次当我达到最强状态时...";
                        case "SupremeRajahDefeat4":
                        return "...我也无法打败你. ";
                        case "SupremeRajahDefeat5":
                        return "...泰拉人...也许...";
                        case "SupremeRajahDefeat6":
                        return "也许这就是一个信号...或许我作为保护者的时代...";
                        case "SupremeRajahDefeat7":
                        return "...已经结束了. 该交出我的位置了. ";
                        case "SupremeRajahDefeat8":
                        return "...我代表你所有杀过的兔子原谅你, 但是作为交换...我希望你能代替我的位置...";
                        case "SupremeRajahDefeat9":
                        return "...成为它们的捍卫者. 它们的保护者. ";
                        case "SupremeRajahDefeat10":
                        return "我只需要世界上最强的生物. . 只需要如果你比我强大...";
                        case "SupremeRajahDefeat11":
                        return "谁会比你更适合代替我的位置呢, ";
                        case "SupremeRajahDefeat12":
                        return "成为一个在无辜者需要时能保护它们的人. ";
                        case "SupremeRajahDefeat13":
                        return "考虑一下. ";
                        case "SupremeRajahDefeat14":
                        return "如果你还想和我过两招...用一下那个特殊的萝卜. 我会很高兴夺回我的荣耀. ";
                        case "SupremeRajahDefeat15":
                        return "...看你了, 年轻人. ";
                        case "SupremeRajahDefeat16":
                        return "王公兔的讲话温暖了你的内心. 你决定从此不再伤害兔子. 成为他的骄傲. ";

                        case "SagChat":
                        return "检 测 到 身 体 严 重 损 伤. 谨 慎 行 动!";

                        case "YamataAHead":
                        return "哦呃!!!";
                        case "Yamata1":
                        return "哈! 我对你很宽容! 当你真正强大的时候再来, 我们来真正的比试!";
                        case "Yamata2":
                        return "八歧大蛇被打败, 潭渊中的雾气消散";
                        case "Yamata3":
                        return "潭渊里没有太阳!!! 呵呵呵呵呵呵!!!";
                        case "Yamata4":
                        return "非非非非常讨厌!!! 这太阳! 我走了!";
                        case "Yamata5":
                        return "你 觉 我 已 经 不 行 了 对 吗? ! 我 可 不 这 么 认 为!!!";
                        case "Yamata6":
                        return "哦可别想飞! 我自己就是巨大的引力, 会吸引一切包括它自己! 噫哈哈哈哈哈哈哈哈!!!";
                        case "Yamata7":
                        return "潭渊里无处可逃!";
                        case "Yamata8":
                        return "想走? ! 没这么容易！";
                        case "Yamata9":
                        return "噫哈哈哈哈哈哈哈哈. . ! 别回来了!";
                        case "Yamata10":
                        return "这点防御在这可救不了你! 别当个小淘气鬼了, 让我毁了你!";
                        case "Yamata11":
                        return "别来回跑了, 让我宰了你!!!";
                        case "Yamata12":
                        return "呵呵呵呵呵, 你真的很烦你知道吗. . !";
                        case "Yamata13":
                        return "我不懂你为什么要一直和我打! 我各方面都比你强!";
                        case "Yamata14":
                        return "真 让 人 摸 不 着 头 脑!";
                        case "Yamata15":
                        return "我讨厌和你打! 非常非常非常讨厌!!!";
                        case "YamataHead":
                        return "哦啊!!!";
                        case "YamataTransition1":
                        return "呀哈哈哈哈哈哈哈哈哈~";
                        case "YamataTransition2":
                        return "你觉得我已经完了...? !";
                        case "YamataTransition3":
                        return "哈!说的和真的一样!";
                        case "YamataTransition4":
                        return "深渊蠢动着...";
                        case "YamataTransition5":
                        return "八歧大蛇已经觉醒!";
                        case "YamataTransition6":
                        return "七头重生！啊哈哈哈哈哈哈哈哈哈!!!!!";
                        case "YamataTransition7":
                        return "你开始感到来自灵魂的压力...";
                        case "YamataA1":
                        return "呃啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊";
                        case "YamataA2":
                        return "不? 不可能的!即使在我清醒的时候? !你一定是作弊了!呃啊……!好吧!拿走你的战利品!我要走了……!";
                        case "YamataA3":
                        return "八歧大蛇被打败, 潭渊的雾气消散. ";
                        case "YamataA4":
                        return "不————!!! 你这个臭小子!!! 这次我差点就碰到你了!!! 好吧, 拿走你的东西, 反正我也不在乎!";
                        case "YamataA5":
                        return "哈! 得了吧! 当你不用作弊的时候再回到专家模式来打败我! 你所有的战利品都是我的!";
                        case "YamataA6":
                        return "你还能打? ! 你有毛病吗? !";
                        case "YamataA7":
                        return "我受够了你的恶作剧!!!吃我毒液你这个小混蛋!!!";
                        case "YamataA8":
                        return "停停停停停!!!我不会让你赢的!!!";
                        case "YamataA9":
                        return "你知道吗, 你就像个讨厌的小臭虫!";
                        case "YamataA10":
                        return "死!你怎么还不死? !";
                        case "YamataA11":
                        return "停!!! 我可不想再输了!!!";
                        case "YamataA12":
                        return "什-什么? !死!去死吧, 你这个小东西!死死死死死死死!!!!";
                        case "YamataA13":
                        return "不不不!!! 别了!!! 这次我要把你踩在地上!!!";
                        case "YamataA14":
                        return "爸爸, 看来我得再来救你一次. ";
                        case "YamataA15":
                        return "我的好闺女. . !";
                        case "YamataA16":
                        return "哦, 亲爱的. . ! 想帮爸爸拍死这个小虫子吗? ";
                        case "YamataA17":
                        return "哎...是的爸爸";
                        case "HarukaY1":
                        return "爸爸, 你这个白痴……不论怎样, 我不能说我没预料到. ";
                        case "HarukaY2":
                        return "我尽力了. 我输了, 交给你了, 爸爸. ";
                        case "YamataHead1":
                        return "尝尝酸液的滋味, 你这讨厌的蛆虫!!!!";
                        case "YamataHead2":
                        return "别动, 让我把你融化了!!!!";
                        case "YamataHead3":
                        return "毒液往下流呀往下流!!!!什么时候停止? 谁知道? !咿哈哈哈哈哈哈哈哈哈!!!!";
                        case "YamataHead4":
                        return "死吧死吧死吧死吧死死死死死死吧!!!";
                        case "YamataHead5":
                        return "啪!砰!我要送你上西天!!!";
                        case "YamataHead6":
                        return "咿哈哈哈哈哈哈哈哈哈!!!";
                        case "YamataHead7":
                        return "抓住他!吃掉他!让";
                        case "YamataHead8":
                        return "从我眼前消失!!!";
                        case "YamataHead9":
                        return"我吃过比你更吓人的兔子";
                        case "YamataHead10":
                        return "希望你带了雨伞!因为雨下得疼!!!!咿哈哈哈哈哈哈哈哈!!!!";
                        case "YamataHead11":
                        return "毒液降下!!!咿哈哈哈哈哈哈哈哈!";
                        case "YamataHead12":
                        return "尝尝幽魂的厉害吧你个小混蛋";
                        case "YamataHead13":
                        return "咿哈哈哈哈哈哈!!!";
                        case "YamataHead14":
                        return "哎呀!流下的酸液!希望你不会被溶解…. ";
                        case "YamataHead15":
                        return "哎呀!又是流下的酸液!咿哈哈哈哈哈哈哈哈";
                        case "YamataHead16":
                        return "哇啊啊啊啊!你没法活下来!";
                        case "YamataHead17":
                        return "快 点!!!!站 着 别 动, 好 让 我 送 你 上 火 星!";
                        case "YamataHead18":
                        return "呀啊啊啊啊啊啊啊啊啊啊啊啊啊啊 别动啊啊啊啊啊啊啊!!";
                        case "YamataHead19":
                        return "有酸液的正常味道!";
                        case "YamataHead20":
                        return "我要把你撕成碎片!!!!你个小混蛋!";
                        case "YamataHead21":
                        return "噫噫噫呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃!!!!";
                        case "Sagittarius1":
                        return "目 标 已 被 消 灭. 返 回 隐 形 模 式. ";
                        case "Sagittarius2":
                        return "目 标 丢 失. 返 回 隐 形 模 式. ";
                        case "SagittariusFree1":
                        return "目 标 已 被 消 灭. 返 回 隐 形 模 式. ";
                        case "SagittariusFree2":
                        return "目 标 丢 失. 返 回 隐 形 模 式. ";
                        case "SagittariusFree3":
                        return "自我维护程序初始化";
                        case "ZeroBoss1":
                        return "零 械 物 理 单 元 处 于 临 界 状 态. 调 用 并 执 行 末 日 注 册 协 议. ";
                        case "ZeroBoss2":
                        return "末 日 注 册 协 议 报 错. MAIN. EXPERT M0DE = FALSE. （主程序未检测到专家模式）";
                        case "ZeroBoss3":
                        return "零械的暗能量影响已经解除，厄劫石可以挖掘了";
                        case "ZeroBoss4":
                        return "初 始 化 备 用 武 器 协 议. ";
                        case "ZeroBoss5":
                        return "关 键 错 误: 未 找 到 手 臂 单 元. 重 新 调 用 资 源 链 接 到 攻 击 协 议. 护 罩 降 级. ";
                        case "ZeroBoss6":
                        return "目 标 被 毁 灭. 返 回 初 始 轨 道. ";
                        case "ZeroBoss7":
                        return "目 标 丢 失. 返 回 初 始 轨 道. ";
                        case "ZeroBoss8":
                        return "初 始 化 探 针 创 建 系 统. ";
                        case "ZeroBoss9":
                        return "旋 转 协 议 加 载. ";
                        case "ZeroBoss10":
                        return "武 器 单 元 重 新 加 载";
                        case "ZeroBoss11":
                        return "重 载 武 器 单 元";
                        case "ZeroAwakened1":
                        return "厄劫石停止发光. 现在你可以挖掘了. ";
                        case "ZeroAwakened2":
                        return "警 告. 检 测 到 严 重 损 坏, 任 务 即 将 失 败. 启 动 全 面 抹 杀 协 议";
                        case "ZeroAwakened3":
                        return "警 告. 检 测 到 严 重 损 坏, 再 次 任 务 即 将 失 败. 启 动 终 极 全 面 抹 杀 协 议";
                        case "ZeroAwakened4":
                        return "作 弊 警 告!作 弊 警 告! 释 放 4 个 零 单 位";
                        case "ZeroAwakened5":
                        return "你 的 作 弊 工 具 无 法 拯 救 你";
                        case "ZeroAwakened6":
                        return "目 标 被 毁 灭. 返 回 初 始 轨 道. ";
                        case "ZeroAwakened7":
                        return "目 标 丢 失. 返 回 初 始 轨 道. ";
                        case "ZeroAwakened8":
                        return @"自 组 织 充 能 完 成...";
                        case "ZeroAwakened9":
                        return @"以 自 组 织 模 式 启 动 系 统...";
                        case "ZeroAwakened10":
                        return @"允 许 程 序 C:\TERRARIA\AAMOD\ZER0\PR0T0C0L\SELF-ORGNAZATION.EXE ? <Y/N>...";
                        case "ZeroAwakened11":
                        return @"=== 屠 杀 开 始 ===";

                        case "ZeroDeath1":
                        return "任 务 失 败. 向 基 地 发 送 遇 难 信 号. ";
                        case "ZeroDeath2":
                        return "任 务 失 败. 再 次 尝 试 发 送 遇 难 信 号. ";
                        case "ZeroDeath3":
                        return "信 号 发 送 中...";
                        case "ZeroDeath4":
                        return "收 到 遇 难 信 号. ";
                        
                        case "FuryAshe1":
                        return "爸, 不! 你要为此付出代价, ";
                        case "FuryAshe2":
                        return "啊! 对不起, 爸. . ! 我必须离开了!";
                        case "WrathHaruka1":
                        return "父亲! 呃啊. . ! 下次再见的时候, 我要让你命丧于此!";
                        case "WrathHaruka2":
                        return "呃...对不起, 父亲...我坚持不住了...";

                        case "ShenA1":
                        return "你将面临一次真正的战斗, 孩子.";
                        case "ShenA2":
                        return "搞清楚你的位置, 泰拉人. 你自作自受. 仪式, 符文? 全都是你自己.";
                        case "ShenA3":
                        return "一切总将在争端中结束.";
                        case "ShenA4":
                        return "你为何要再起争端? 为力量? 还是荣耀?";
                        case "ShenA5":
                        return "你有点让我回想起了自己...";
                        case "ShenA6":
                        return "不管怎样，我会说这将是你的最后一次!";
                        case "ShenA7":
                        return "你也很强. 鼓起勇气和你所拥有的东西战斗";
                        case "ShenA8":
                        return "你想打败我几次? 或者说你会放弃?";
                        /* 
                        case "ShenA9":
                        return "But today, we clash! Now show me what you got!";
                        case "ShenA10":
                        return "DIE ALREADY YOU INSIGNIFICANT LITTLE WORM!!";
                        */
                        case "ShenA11":
                        return "呼..! 又到这个地步, 离终点越来越近了. 我可不会心慈手软!";
                        case "ShenA12":
                        return "哈..?! 你还没当场去世? 那好...我们换种玩法!";
                        case "ShenA13":
                        return "游戏收尾才越发动人心弦, 记住了, 孩子!";
                        case "ShenA14":
                        return "你这个小..! 等死吧!";
                        case "ShenA15":
                        return "正是如此! 尾声将至, 所以你不敢退缩!";
                        case "ShenA16":
                        return "这不可能--! 怎么回事?!";

                        case "ShenAThorium":
                        return "你知道吗, 我一直关注着你击败灾难之灵和它的三个使徒. 不得不说, 非常令人深刻.";
                        case "ShenACalamity":
                        return "考虑到你把那个狂怒的至尊灾厄之影女巫安排的明明白白, 我还是由衷地恭喜你.";
                        case "ShenAGRealm":
                        return "我看到你在丛林里踩扁了那只让人头疼的昆虫了.";
                        case "ShenARedemption":
                        return "不过, 在你击溃了宇宙天使之后, 连我都被你的水平吓到了.";
                        case "ShenASpirit":
                        return "现在我一想, 虽然, 你把魂灵的监察者像一个鸡蛋一样踩碎了, 那也是相当需要力气的.";
                        case "ShenANoMod":
                        return "你所击败的一切, 无论神明或是怪兽, 都使我尊敬你.";

                        case "ShenDeath1":
                        return "重新分离...";
                        case "ShenDeath2":
                        return "这是你的错， 你这个傲慢的虫子. . !我知道我们应该更加大力度的攻击， 但是...不不不不. . !你说我们可以不费吹灰之力就把它们压扁!";
                        case "ShenDeath3":
                        return "勇者";
                        case "ShenDeath4":
                        return ", 总有一天你会再次面对我们的怒火...等我们再次准备好充足的精力...";
                        case "ShenDeath5":
                        return "...或者你决定再用一次印迹. . !";
                        case "ShenDeath6":
                        return "你自己选择, 孩子. ";
                        case "ShenDeath7":
                        return "你这个笨蛋!我们又双叒叕!输了!!";
                        case "ShenDeath8":
                        return "啊, 我的头...";
                        case "ShenDeath9":
                        return "跳 梁 小 丑";
                        case "ShenDeath10":
                        return "那么你, ";
                        case "ShenDeath11":
                        return "! 下 次 我 会 把 你 的 头 拧 下 来!!!";
                        case "ShenDeath12":
                        return "相信我们, 小子. ";
                        case "ShenDeath13":
                        return "总会有下次的. ";
                        case "ShenDoragon1":
                        return "泰 拉 魔 法? ! 不 会! 我 一 度 认 为 它 已 经 从 这 片 土 地 上 消 失 了!";
                        case "ShenDoragon2":
                        return "上神之爪! 助我!";
                        case "ShenDoragon3":
                        return "艾希? 遥香? 我再次需要你们帮忙. . !";
                        case "ShenDoragon4":
                        return "收到, 爸爸~!";
                        case "ShenDoragon5":
                        return "又来. . ? ";
                        case "ShenDoragon6":
                        return "女儿们. . ? 帮助你父亲对付这个蝼蚁. ";
                        case "ShenDoragon7":
                        return "很乐意, 爸爸~!";
                        case "ShenDoragon8":
                        return "好, 父亲. ";
                        case "ShenDoragon9":
                        return "孩子， 你很固执. 我喜欢. ";
                        case "ShenDoragon10":
                        return "这什么? ...能力? 我从来没想过...";
                        case "ShenDoragon11":
                        return "真的勇士从不仁慈！我不会， 我认为你也不会……";
                        case "ShenDoragon12":
                        return "放弃吧, 孩子. 世界终将归于混沌!";
                        case "ShenDoragon13":
                        return "毫 不 留 情!";
                        case "ShenDoragon14":
                        return "什么? 你还能打? 为什么? !";
                        case "ShenDoragon15":
                        return "一个超远古之神的失败给予了宝石持有者们新的力量";
                        case "ShenDoragon16":
                        return "呵呵, 好吧. 我想我会让你歇息一会. 但是如果你回来变得更强, 我会给你展示展示永劫混沌的真实实力...";
                        case "ShenDoragon17":
                        return "一个超远古之神的失败给予了宝石持有者们新的力量";
                        case "ShenDoragon18":
                        return "一场好戏, 孩子, 真是一场好戏. 你的战斗力仍然让我印象深刻!也许有一天我会给你看看我的真正实力. ";
                        case "ShenDoragon19":
                        return "颇有水平, 孩子.";
                        case "ShenDoragon20":
                        return "这是? 什么样的能力? 难以置信.";
                        case "ShenDoragon21":
                        return "你应该知道你是个真正的勇士. 那就站好让我烤熟了你.";
                        case "ShenDoragon22":
                        return "天地草创, 混沌君临, 孩子. 你们泰拉人从未领悟其意.";
                        case "ShenDoragon23":
                        return "看上去你还想力挽狂澜, 那并不会太久!";
                        case "ShenDoragon24":
                        return "哼嗯嗯...还要打, 嗯?";

                        case "ShenSpawn1":
                        return "又看到我们了是不是很惊讶, 小子? ";
                        case "ShenSpawn2":
                        return "噫哈哈哈哈. . ! 对. . ! 这里看见我们一定惊得说不出话了. . ! 但这次， 我们还藏了一手. . !";
                        case "ShenSpawn3":
                        return "你刚才使用的那个印迹使我们恢复了全部的力量， 这将使我们达到真正的强大形态. . !";
                        case "ShenSpawn4":
                        return "我们曾是同一个生物. . ! 但是后来有一个像你一样的泰拉混蛋将我们的灵魂一分为二. . ! 但是现在...呵呵呵呵呵...";
                        case "ShenSpawn5":
                        return "我 们 合 二 为 一";
                        case "ShenSpawn6":
                        return "呵呵.... 哈哈哈哈哈哈...";
                        case "ShenSpawn7":
                        return "你的致命失误, 小子...";
                        case "ShenSpawn8":
                        return "正如你所见.... ";
                        case "ShenSpawn9":
                        return "吾 乃 上 神 应 龙, 混 乱 与 动 荡 的 原 初 之 皇!";
                        case "ShenSpawn10":
                        return "而你, 我的孩子. . !";
                        case "ShenSpawn11":
                        return "神 形 俱 灭!!!";
                        case "ShenTransition1":
                        return "呵呵...";
                        case "ShenTransition2":
                        return "呵呵呵呵...";
                        case "ShenTransition3":
                        return "哈哈哈哈哈哈哈!!!";
                        case "ShenTransition4":
                        return "你是不是已经忘了我们的上次战斗...? ";
                        case "ShenTransition5":
                        return "咱们之间的争斗才刚刚开始, 天真的孩子...";
                        case "ShenTransition6":
                        return "然而现在, 你挡住了我的去路...";
                        case "ShenTransition7":
                        return "上神应龙觉醒了!";
                        case "ShenTransition8":
                        return "你 将 在 混 律 业 火 中 被 焚 烧 殆 尽!!!";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(BossInfo)
                    {
						case "male":
                        return "его";
                        case "fimale":
                        return "ее";
                        case "male2":
                        return "ЕГО";
                        case "fimale2":
                        return "ЕЕ";
                        case "boy":
                        return ", МАЛЬЧИК";
                        case "girl":
                        return ", ДЕВОЧКА";

                        case "FatPixie":
                        return "EEEE";

                        case "AHDeath1":
                        return "РРРРРРРГХ! НЕТ, ТОЛЬКО НЕ СНОВА!!!";
                        case "AHDeath2":
                        return "Видишь, Аше?? Я говорила тебе, что это тупая идея, но ТЫ не слушаешь...";
                        case "AHDeath3":
                        return "Почему я снова и снова иду с тобой..? Если честно, то я уже просто должна оставить тебя один на один с ними.";
                        case "AHDeath4":
                        return "Заткнись! Я думала, что если мы объединимся ";
                        case "AHDeath5":
                        return ", то сможем уничтожить их!";
                        case "AHDeath6":
                        return "Рхг..! Заткнись..!";
                        case "AHDeath7":
                        return "В любом случае... Я иду домой. КТО-ТО должен расказать папе об этом.";
                        case "AHDeath8":
                        return "Хмпф..! Ладно! Я возвращаюсь в Инферно!";

                        case "AHSpawn1":
                        return "Ну привет, какой сюрприз увидеть ТЕБЯ здесь~!";
                        case "AHSpawn2":
                        return "Ах, да, я про тебя много слышала, сопляк... ты тот теплокровный, который убил мою мать..!";
                        case "AHSpawn3":
                        return "Ах, да, я про тебя много слышала, сопляк... ты создал нам кучу проблем...";
                        case "AHSpawn4":
                        return "И мою...";
                        case "AHSpawn5":
                        return "... ты убил и мою мать...";
                        case "AHSpawn6":
                        return "Ты очень надоедлив";
                        case "AHSpawn7":
                        return "Так что теперь..! Хехе...";
                        case "AHSpawn8":
                        return "Мы заставим тебя кричать от боли! Давай, Хаки, сожжем этого теплокровного~!";
                        case "AHSpawn9":
                        return "Пожалуйста, больше никогда не называй меня Хаки...";

                        case "AsheDowned":
                        return "АУЧ..! ВООБЩЕ ТО БОЛЬНО, ПРИДУРОК!";
                        case "HarukaDowned":
                        return "Ргх..! Ау...";

                        case "Akuma1":
                        return "Вода?! АК..! Я НЕ МОГУ ДЫШАТЬ!";
                        case "Akuma2":
                        return "*Зевает* Прости, сопляк, я устал. Мне надо выспаться. Возвращайся завтра.";
                        case "Akuma3":
                        return "А я думал что вы, террарианы, даете более серьезный бой. Похоже, нет.";
                        case "Akuma4":
                        return "Эй, сопляк! Смотри, небеса падают!";
                        case "Akuma5":
                        return "Вниз спускаются огонь и ярость!";
                        case "Akuma6":
                        return "Духи вулкана! Помогите мне раздавить этого сопляка!";
                        case "Akuma7":
                        return "Эй, сопляк! Смотри!";
                        case "Akuma8":
                        return "Смотри!";
                        case "Akuma9":
                        return "Солнце светит, и тени нигде не видно, сопляк!";
                        case "Akuma10":
                        return "Становится жарковато, не так ли?";
                        case "Akuma11":
                        return "Вулканы Инферно наконец успокоены...";
                        case "Akuma12":
                        return "Хмпф... ты хорош, сопляк, но не достаточно хорош. Возвращайся, когда будешь получше.";
                        case "Akuma13":
                        return "Встань лицом к лицу с огнями безнадеги, сопляк!";
                        case "Akuma14":
                        return "Спасайся, сопляк!";

                        case "AkumaA1":
                        return "Аши? Помоги своему старому папе справиться с этим сопляком снова!";
                        case "AkumaA2":
                        return "У тебя получиться, папочка..!";
                        case "AkumaA3":
                        return "Эй! Убрал руки от моего папы!";
                        case "AkumaA4":
                        return "Моя девочка!";
                        case "AkumaA5":
                        return "Все еще жив, а? А в тебе есть стержень! Мне это нравиться, сопляк!";
                        case "AkumaA6":
                        return "Что?! Как ты держишься так долго?! Почему ты... Я отказываюсь быть побежденным террарианом снова! Получай!";
                        case "AkumaA7":
                        return "АК..! ВОДА! Я НЕНАВИЖУ ВОДУ!!!";
                        case "AkumaA8":
                        return "В этот раз ночь тебя не спасет, сопляк! День перерождается!";
                        case "AkumaA9":
                        return "Ты только что сгорел, сопляк.";
                        case "AkumaA10":
                        return "Хех, не такой убогий в этот раз, сопляк. Я впечатлен. Вот. Бери свой приз.";
                        case "AkumaA11":
                        return "ГРАХ..! КАК!? КАК Я МОГ ПРОЙГРАТЬ СМЕРТНОМУ ТЕРРАРИАНУ?! Хмпф... ладно, сопляк, ты выиграл честно. Вот твой приз.";
                        case "AkumaA12":
                        return "Мило. Ты сжульничал. А теперь сразись со мной в эксперт моде, как настоящий мужчина.";
                        case "AkumaA13":
                        return "Небеса вновь падают!";
                        case "AkumaA14":
                        return "И вот снова вниз спускаются огонь и ярость!";
                        case "AkumaA15":
                        return "Ты недооцениваешь силу дракона, сопляк!";
                        case "AkumaA16":
                        return "Огни не сдаются до самого конца, сопляк!";
                        case "AkumaA17":
                        return "Осторожно! Вулканы извергаются снова";
                        case "AkumaA18":
                        return "СПАСАЙСЯ, СОПЛЯК!";
                        case "AkumaA19":
                        return "Эй, сопляк? Любишь фейерверки? Нет? Очень жаль!";
                        case "AkumaA20":
                        return "А вот и твой грандиозный финал, сопляк!";
                        case "AkumaA21":
                        return "Солнце не сдастся, пока день не закончиться, сопляк!";
                        case "AkumaA22":
                        return "Сразись лицом к лицу с яростью солнца!";

                        case "AkumaAAshe1":
                        return "Папочка, НЕТ! ЭЙ! ТЫ! Я зажарю тебя в следующий раз..!";
                        case "AkumaAAshe2":
                        return "АУ, урод..! Я все!";

                        case "AkumaTransition1":
                        return "Хех...";
                        case "AkumaTransition2":
                        return "Знаешь, сопляк";
                        case "AkumaTransition3":
                        return "То, что ты подул на огни, не потушило их...";
                        case "AkumaTransition4":
                        return "Акума пробудился!";
                        case "AkumaTransition5":
                        return "ЭТО ИХ ТОЛЬКО УСИЛИЛО!";
                        case "AkumaTransition6":
                        return "Воздух вокруг вас нагревается";

                        case "AnubisFalse":
                        return "ХАХ! Кого теперь облили вод-песком!";

                        case "AnubisGuys":
                        return "ребят";
                        case "Anubisbud":
                        return "дружок";

                        case "Anubis1":
                        return "Что же, ";
                        case "Anubis2":
                        return ". вот и тот самый момент.";
                        case "Anubis3":
                        return "Я надеюсь, ты готов к настоящему бою.";
                        case "Anubis4":
                        return "Учти, что я в своей самой сильной форме";
                        case "Anubis5":
                        return "Ты готов? Я не буду стесняться бить тебя!";
                        case "Anubis6":
                        return "Поехали!";
                        case "Anubis7":
                        return "Хочешь реванш, а? Ладненько, будет весело!";

                        case "AnubisCombat1":
                        return "ЕЕЕЕЕ";
                        case "AnubisCombat2":
                        return "Осторожно! Снаряды бьют больно.";
                        case "AnubisCombat3":
                        return "Давай я позову друзей.";
                        case "AnubisCombat4":
                        return "Лови!";
                        case "AnubisCombat5":
                        return "Сокрушительно, не так ли?";

                        case "AnubisTransition1":
                        return "... хргх...";
                        case "AnubisTransition2":
                        return "... ладно.";
                        case "AnubisTransition3":
                        return "Я думаю... что время пришло.";
                        case "AnubisTransition4":
                        return "Больше никаких детских игр.";
                        case "AnubisTransition5":
                        return "Если ты будешь сражаться с темными силами этого мира...";
                        case "AnubisTransition6":
                        return "Мне надо быть уверен, что ты готов... потому что если ты не готов...";
                        case "AnubisTransition7":
                        return "... некоторые вещи должны быть скрыты от тебя";

                        case "FAnubis":
                        return "... Извини, ты еще не готов.";

                        case "FAnubisCombat":
                        return "В этот раз без предупреждений.";

                        case "FAnubisWin":
                        return "... Ты справился хорошо, приятель. Давай продвинемся дальше. Поговори со мной, когда будешь готов..";

                        case "Athena1":
                        return "Хмпф..!";
                        case "Athena2":
                        return "";
                        case "Athena3":
                        return "Ты! Землянин";
                        case "Athena4":
                        return "Мои серафимы доложили мне, что ты атаковал их! Зачем?!";
                        case "Athena5":
                        return "Я преподам тебе урок, крысенышь!";
                        case "Athena6":
                        return "En Garde!";
                        case "Athena7":
                        return "*Вздох*...";
                        case "Athena8":
                        return "Давай просто покончим с этим. У меня мало времени.";
                        case "Athena9":
                        return "И держись подальше... идиот";
                        case "Athena10":
                        return "Никаких игр, штормы зовут, и они идут за тобой!";
                        case "Athena11":
                        return "АУ! Ладно, ладно..! Я оставлю тебя в покое! Боже, ты никогда не сдаешься, не так ли?";
                        case "Athena12":
                        return "... Так ты все таки пришел.";
                        case "Athena13":
                        return "Пришло время мне вернуть свою честь..!";
                        case "Athena14":
                        return "En Garde!";

                        case "AthenaA1":
                        return "И держись подальше... идиот";
                        case "AthenaA2":
                        return "АУ! Ладно, ладно..! Я оставлю тебя в покое! Боже, ты никогда не сдаешься, не так ли?";

                        case "AthenaDefeat1":
                        return "... ха... хах...";
                        case "AthenaDefeat2":
                        return "... я опять проиграла";
                        case "AthenaDefeat3":
                        return "Нет.";
                        case "AthenaDefeat4":
                        return "Я так просто не сдамся.";
                        case "AthenaDefeat5":
                        return "Есть выражение, по которому живет мой народ, землянин.";
                        case "AthenaDefeat6":
                        return "В сиянии рассветной...";
                        case "AthenaDefeat7":
                        return "Во тьме ночной...";
                        case "AthenaDefeat8":
                        return "Даже в поражении....";
                        case "AthenaDefeat9":
                        return "ВАРИАНЕЦ ВСЕГДА ДАЕТ ПОСЛЕДНИЙ БОЙ";

                        case "Athena2Defeat1":
                        return "...ПОЧЕМУ?!";
                        case "Athena2Defeat2":
                        return "... Почему я не могу победить тебя?!";
                        case "Athena2Defeat3":
                        return "... Это еще не конец, землянин ";
                        case "Athena2Defeat4":
                        return "";
                        case "Athena2Defeat5":
                        return "Я еще верну свою честь...";
                        case "Athena2Defeat6":
                        return "... а пока что, оглядывайся по сторонам.";
                        case "Athena2Defeat7":
                        return "Темные силы хаоса снова пробуждаются.";
                        case "Athena2Defeat8":
                        return "Надеюсь, что хотя бы ОН не вернется...";
                        case "Athena2Defeat9":
                        return "Будь осторожен.";

                        case "SeraphHerald1":
                        return "ЭЙ! ЗЕМЛЯНИН! АГА, ТЫ, ТУПАЯ ОБЕЗЬЯНА!";
                        case "SeraphHerald2":
                        return "Королева Афина желает видеть тебя в Акрополе немедленно!";
                        case "SeraphHerald3":
                        return "Она требует реванш, и вот этот раз, она не даст так легко себя победить!";
                        case "SeraphHerald4":
                        return "Я бы пожелала, что бы ты ноги себе обломал, но мы сами их обломаем, когда ты явишься!";
                        case "SeraphHerald5":
                        return "Увидимся, придурок!";
                        case "SeraphHerald6":
                        return "Ах, да, и ем, тот противный червь-клептоман тоже хочет с тобой сразиться или что то тип того.";

                        case "GreedFalse1":
                        return "АААААААААА, СВЕТ С ПОВЕРХНОСТИ! СЛИШКОМ ЯРКО! СЛИШКОМ ЯРКО!";
                        case "GreedFalse2":
                        return "И ДЕРЖИСЬ ПОДАЛЬШЕ ОТ МОИХ ДРАГОЦЕННЫХ СОКРОВИЩ, ВОРИШКА!";

                        case "Greed1":
                        return "Кто мешает мне считать монетки?! Я занят--";
                        case "Greed2":
                        return "... Оооо... это что...?";
                        case "Greed3":
                        return "Ты принес мою любимую вкусняшку..!";
                        case "Greed4":
                        return "Золотая гусеничка...";
                        case "Greed5":
                        return "ВМЕСТЕ С ТЕРРАРИАНОМ!!!";
                        case "GreedName":
                        return "Жадность";

                        case "GreedTransition1":
                        return "ТЫ..! ТЫ МАЛЕНЬКИЙ--";
                        case "GreedTransition2":
                        return "ВСЕ, ДОСТАТОЧНО!";
                        case "GreedTransition3":
                        return "ТЫ СМЕЕШЬ КРАСТЬ МОЮ ПРЕЛЕСТЬ, ПОЭТОМУ...!";
                        case "GreedTransition4":
                        return "Я УКРАДУ ТВОЮ ЖИЗНЬ! ХЕХЕХЕХЕХЕ!!!";
                        case "GreedAName":
                        return "Жадность";

                        case "Rajah1":
                        return "НЕЛЬЗЯ СЖУЛЬНИЧАТЬ В БОЮ С СПРАВЕДЛИВОСТЬЮ";
                        case "Rajah2":
                        return "Справедливость восторжествовала...";
                        case "Rajah3":
                        return "Трус.";
                        case "Rajah4":
                        return "Ты выиграл в этот раз, убийца... но я отомщу за тех, кого ты беспощадно убил...";
                        case "Rajah5":
                        return "ЭТО ЕЩЕ НЕ КОНЕЦ, ";
                        case "Rajah6":
                        return "! СОПЕРНИКИ СРАЖАЮТСЯ ДО КОНЦА!";
                        case "Rajah7":
                        return "И не вылезай.";
                        case "Rajah8":
                        return "Неплохо сражался, ";
                        case "Rajah9":
                        return ". Бери награду.";

                        case "SupremeRajahChat":
                        return "Атакуя, Раджа горит яростной энергией, усиливая свою защиту";

                        case "SupremeRajahDefeat1":
                        return "Ргх...";
                        case "SupremeRajahDefeat2":
                        return "... так...";
                        case "SupremeRajahDefeat3":
                        return "Даже в моей самой сильной форме...";
                        case "SupremeRajahDefeat4":
                        return "... Я не могу победить тебя.";
                        case "SupremeRajahDefeat5":
                        return "... Террариан... может быть...";
                        case "SupremeRajahDefeat6":
                        return "Может быть, все это просто знак... может быть, это знак, что мое время в роли защитника...";
                        case "SupremeRajahDefeat7":
                        return "... наконец закончилось. Может быть, пришло время передать эстафету.";
                        case "SupremeRajahDefeat8":
                        return "... Я прощаю тебя за каждого кролика, которого ты убил, но взамен... Я хочу, что бы ты встал на мое место...";
                        case "SupremeRajahDefeat9":
                        return "... как их защитник.";
                        case "SupremeRajahDefeat10":
                        return "Я желаю только всего наилучшего существам этого мира... и если ты сильнее меня...";
                        case "SupremeRajahDefeat11":
                        return "То кому еще занять мое место, ";
                        case "SupremeRajahDefeat12":
                        return "Будь тем, к кому невинные смогут обратиться в трудную минуту.";
                        case "SupremeRajahDefeat13":
                        return "Хотя бы подумай об этом.";
                        case "SupremeRajahDefeat14":
                        return "И если ты хочешь еще подраться... просто используй одну из тех особых морковок. Я буду рад восстановить свою честь.";
                        case "SupremeRajahDefeat15":
                        return "... Увидимся, дитя.";
                        case "SupremeRajahDefeat16":
                        return "Речь Кролика Раджи согревает ваше сердце. У вас больше нет желания вредить кроликам. Сделай так, что бы он гордился тобой.";

                        case "SagChat":
                        return "ОБНАРУЖЕНО ОБШИРНОЕ РАНЕНИЕ ТЕЛА. ПРОДОЛЖАТЬ С ОСТОРОЖНОСТЬЮ";

                        case "YamataAHead":
                        return "АУЧЬ!!!";
                        case "Yamata1":
                        return "ХАХ! Я поддался тебе! Возвращайся, когда будешь по-настоящему хорош и сможем устроить настоящий бой";
                        case "Yamata2":
                        return "Победа над Яматой заставляет туман в трясине подняться.";
                        case "Yamata3":
                        return "СОЛНЦЕ НЕ СВЕТИТ В ГЛУБИНАХ!!! НЬЕХЕХЕХЕХЕ!!!";
                        case "Yamata4":
                        return "ХССССС!!! СОООЛНЦЕЕЕЕ! Я ВСЕ!";
                        case "Yamata5":
                        return "ТЫ ДУМАЕШЬ, СО МНОЙ ПОКОНЧЕНО?! Я ТАК НЕ ДУМАЮ!!!!";
                        case "Yamata6":
                        return "Ох, и даже не думай о полете! Мое эго настолько массивное, что оно притягивает все к себе! НЬЕХЕХЕХЕХЕ!!!";
                        case "Yamata7":
                        return "НЕВОЗМОЖНО СБЕЖАТЬ ИЗ БЕЗДНЫ!";
                        case "Yamata8":
                        return "Убежать?! Я ТАК НЕ ДУМАЮ!";
                        case "Yamata9":
                        return "НЬЕХЕХЕХЕХЕХЕХЕХЕ..! И не возвращайся";
                        case "Yamata10":
                        return "Защита не спасет тебя! А теперь хватит быть маленьким уродцем и дай мне уничтожить тебя!";
                        case "Yamata11":
                        return "ХВАТИТ УВОРАЧИВАТЬСЯ И ДАЙ МНЕ РАСТОПТАТЬ ТЕБЯ!!";
                        case "Yamata12":
                        return "НГААААААААААААААААААААААХ, ТЫ ОЧЕНЬ НАДОЕДЛИВЫЙ, ЗНАЕШЬ ЛИ..!";
                        case "Yamata13":
                        return "Я не понимаю, зачем ты продолжаешь сражаться со мной! Я лучше тебя во всем!";
                        case "Yamata14":
                        return "МЕНЯ СНОВА ПОБЕЖДАЮТ";
                        case "Yamata15":
                        return "Я НЕНАВИЖУ С ТОБОЙ СРАЖАТЬСЯ! НЕНАВИЖУ НЕНАВИЖУ НЕНАВИЖУ!!!";

                        case "YamataHead":
                        return "АУЧЬ!!!";
                        case "YamataTransition1":
                        return "НЬЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕ~!";
                        case "YamataTransition2":
                        return "Ты думал, что со мной ПОКОНЧЕНО..?!";
                        case "YamataTransition3":
                        return "АГА! ЕЩЕ БЫ!";
                        case "YamataTransition4":
                        return "Бездна голодает...";
                        case "YamataTransition5":
                        return "Ямата пробудился!";
                        case "YamataTransition6":
                        return "И У НЕГО СНОВА 7 ГОЛОВ! НЬЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕ!!!";
                        case "YamataTransition7":
                        return "Вы чувстуете, как ваша душа тянет вас вниз...";

                        case "YamataA1":
                        return "РЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕ";
                        case "YamataA2":
                        return "НЕТ...! НЕВОЗМОЖНО! ДАЖЕ В МОЕЙ ПРОБУЖДЕННОЙ ФОРМЕ?! ДА ТЫ ЖУЛИК! ГИААААААААХ..! ЛАДНО! БЕРИ СВОЙ ЛУТ! Я ПОШЕЛ..!";
                        case "YamataA3":
                        return "Победа над Яматой заставляет туман в трясине подняться.";
                        case "YamataA4":
                        return "НЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕТ!!! ТЫ, МАЛЕНЬКИЙ ВЫРОДОК!!! Я ПОЧТИ ВЫЙГРАЛ В ЭТОТ РАЗ!!! ЛАДНО, бери свое барахло, мне плевать!";
                        case "YamataA5":
                        return "ХАХ! Неплохая попытка! Возвращайся в эксперт моде, где ты не жульничаешь! Весь твой лут все еще МОЙ!";
                        case "YamataA6":
                        return "ТЫ ВСЕ ЕЩЕ СРАЖАЕШЬСЯ?! ДА ЧТО С ТОБОЙ НЕ ТАК?!";
                        case "YamataA7":
                        return "МНЕ НАДОЕЛИ ТВОИ ПРОДЕЛКИ!!! ЖРИ ЯД, МАЛЕНЬКИЙ ЗАСРАНЕЦ!!!";
                        case "YamataA8":
                        return "ПРЕКРАТИ ПРЕКРАТИ ПРЕКРАТИ!!! Я НЕ ДАМ ТЕБЕ ВЫЙГРАТЬ!!!";
                        case "YamataA9":
                        return "Ты маленький надоедливый жучара, знаешь ли!";
                        case "YamataA10":
                        return "УМРИ! ПОЧЕМУ ТЫ ПРОСТО НЕ УМРЕШЬ?!";
                        case "YamataA11":
                        return "ПРЕКРАТИ!!! Я СНОВА ПРОИГРАЮ!!!";
                        case "YamataA12":
                        return "Чт-ЧТА?! УМРИ! УМРИ ТЫ, МАЛЕЬКИЙ ХАМ! УМРИУМРИУМРИУМРИУМРИУМРИУМРИУМРИУМРИ!!!!";
                        case "YamataA13":
                        return "НЕТ НЕТ НЕТ!!! ТОЛЬКО НЕ СНОВА!!! В ЭТОТ РАЗ Я ТЕБЯ РАСТОПЧУ!!!";
                        case "YamataA14":
                        return "Похоже, мне придется вмешаться и спасти твой зад, па..";
                        case "YamataA15":
                        return "Вот это моя девочка!";
                        case "YamataA16":
                        return "Ох, милая..! Поможешь папе уничтожить этого мелкого червя?!";
                        case "YamataA17":
                        return "*Вздох*... конечно, па.";

                        case "HarukaY1":
                        return "Пап, ты придурок..! В любом случае, не могу сказать, что не ожидала этого.";
                        case "HarukaY2":
                        return "Достаточно. Я все, ТЫ сам с ним справляйся, как хочешь, па!";

                        case "YamataHead1":
                        return "ПОЧУСВУЙ КИСЛОТУ, ТЫ, НЕВЫНОСИМЫЙ ЛИЧИНУС!!!";
                        case "YamataHead2":
                        return "ХВАТИТ ДВИГАТЬСЯ И ДАЙ МНЕ РАСПЛАВИТЬ ТЕБЯ!!!";
                        case "YamataHead3":
                        return "ВНИЗ, ВНИЗ, ВНИЗ НА БЕЗДНЫ ДНООООО.  ТАМ ТВОЙ ДРУГ ЯМАТА, ЖДЕТ УЖЕ ДАААВНОООО. НЬЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕ";
                        case "YamataHead4":
                        return "УМРИУМРИУМРИУМРИУМРИУМРИУМРИУМРИУМРИИИИИИИИИИИИИИИИИИИИИИИИИИИИИИИИИ!!!";
                        case "YamataHead5":
                        return "БАМ! БУМ! ТЫ У МЕНЯ СЕЙЧАС ПОЛЕТИШЬ В СЛЕДУЮЩЕЕ ВОСКРЕСЕНЬЕ!!!";
                        case "YamataHead6":
                        return "НГААААААААААААААААААААААХ!!!";
                        case "YamataHead7":
                        return "ДОСТАНЬТЕ ИХ! СЬЕШТЕ ИХ! ПРОСТО ДОСТАНЬТЕ ";
                        case "YamataHead8":
                        return " ПРОЧЬ ОТ МОЕГО ЛИЦА!!!";
                        case "YamataHead9":
                        return "Я ЕЛ КРОЛИКОВ, КОТОРЫЕ БЫЛИ ПОКРУЧЕ, ЧЕМ ТЫ!!!!";
                        case "YamataHead10":
                        return "Я НАДЕЮСЬ, ТЫ ВЗЯЛ С СОБОЙ ЗОНТИК! ПОТОМУ ЧТО У НАС ТУТ ДОЖДЬ ИЗ БОЛИ!!! НЬЕХЕХЕХЕХЕ!!!";
                        case "YamataHead11":
                        return "ВНИЗ ТЕЧЕТ ЯД!!!НЬЕХЕХЕХЕХЕХЕХЕХЕ!";
                        case "YamataHead12":
                        return "ЖРИ ЭКТОПЛАЗМУ, НЕГОДНИК!";
                        case "YamataHead13":
                        return "НЬЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯХ!!!";
                        case "YamataHead14":
                        return "УПС! УРОНИЛ КИСЛОТУ! Надеюсь, что ты не разлагаешься..!";
                        case "YamataHead15":
                        return "УПС! СНОВА КИСЛОТУ УРОНИЛ! НЬЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕХЕ";
                        case "YamataHead16":
                        return "НЬЯЯЯЯЯЯЯЯЯЯЯЯХ! ЭТО ТЫ НЕ ПЕРЕЖИВЕШЬ!";
                        case "YamataHead17":
                        return "НУ ЖЕ!!! ВСТАНЬ РОВНО, ЧТО БЫ Я МОГ ОТПРАВИТЬ ТЕБЯ НА МАРС!";
                        case "YamataHead18":
                        return "НГААААААААААААХ ХВАААААААААААААТИИИИИИИТ ДВИГАААААААААААТЬСЯЯЯЯЯЯЯЯЯЯЯ!!!!!";
                        case "YamataHead19":
                        return "ВОТ ТЕБЕ ПОЛЕЗНАЯ ДЛЯ ЗДОРОВЬЯ ДОЗА КИСЛОТЫ!";
                        case "YamataHead20":
                        return "Я ТЕБЯ НА ЧАСТИ РАЗОВРУ, МАЛЕНЬКИЙ НЕГОДЯЙ!!!";
                        case "YamataHead21":
                        return "РЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕ!!!";

                        case "Sagittarius1":
                        return "ЦЕЛЬ НЕЙТРАЛИЗ0ВАНА. В0ЗВРАЩАЮСЬ В СКРЫТНЫЙ РЕЖИМ.";
                        case "Sagittarius2":
                        return "ЦЕЛЬ П0ТЕРЯНА. В0ЗВРАЩАЮСЬ В СКРЫТНЫЙ РЕЖИМ.";
                        case "SagittariusFree1":
                        return "ЦЕЛЬ НЕЙТРАЛИЗ0ВАНА. В0ЗВРАЩАЮСЬ В СКРЫТНЫЙ РЕЖИМ.";
                        case "SagittariusFree2":
                        return "ЦЕЛЬ П0ТЕРЯНА. В0ЗВРАЩАЮСЬ В СКРЫТНЫЙ РЕЖИМ.";
                        case "SagittariusFree3":
                        return "ЗАПУСКАЮ ПР0ГРАМУ РЕМ0НТА";

                        case "ZeroBoss1":
                        return "ФИЗИЧЕСКИЙ М0ДУЛЬ ЗИР0 В КРИТИЧЕСК0М С0СТ0ЯНИИ. ЗАПУСКАЮ ПР0Т0КОЛ СУДН0Г0 ДНЯ.";
                        case "ZeroBoss2":
                        return "В ЗАПУСКЕ ПР0Т0КОЛА СУДН0Г0 ДНЯ ОТКАЗАН0. MAIN.EXPERT M0DE = FALSE.";
                        case "ZeroBoss3":
                        return "Темное влияние Зир0 на пустоту ушло. Теперь вы можете добывать камень рока.";
                        case "ZeroBoss4":
                        return "ЗАПУСКАЮ ПР0Т0К0Л ЗАПАСНЫХ М0ДУЛЕЙ.";
                        case "ZeroBoss5":
                        return "КРИТИЧЕСКАЯ 0ШИБКА: М0ДУЛИ РУК НЕ 0БНАРУЖЕНЫ. ПЕРЕАДРИСУЮ РЕСУРСЫ НА АТАКУЮЩИЕ ПР0Т0К0ЛЫ. ЩИТ 0ПУЩЕН.";
                        case "ZeroBoss6":
                        return "ЦЕЛЬ НЕЙТРАЛИЗ0ВАНА. В0ЗВРАЩАЮСЬ НА 0РБИТУ.";
                        case "ZeroBoss7":
                        return "ЦЕЛЬ П0ТЕРЯНА. В0ЗВРАЩАЮСЬ НА 0РБИТУ.";
                        case "ZeroBoss8":
                        return "ЗАПУСКАЮ СИСТЕМУ С0ЗДАНИЯ З0НД0В.";
                        case "ZeroBoss9":
                        return "ПР0Т0К0Л ЦИКЛ0НА ЗАПУЩЕН.";
                        case "ZeroBoss10":
                        return "ПЕРЕЗАПУСКАЮ М0ДУЛИ 0РУЖИЯ";
                        case "ZeroBoss11":
                        return "ПЕРЕЗАПУСКАЮ МОДУЛИ 0РУЖИЯ";

                        case "ZeroAwakened1":
                        return "Камень рока перестает светиться. Теперь его можно добыть.";
                        case "ZeroAwakened2":
                        return "ВНИМАНИЕ. НАНЕСЕН К0Л0ССАЛЬНЫЙ УР0Н, ЕСТЬ УГР0ЗА П0РАЖЕНИЯ. ЗАПУСКАЮ ПР0Т0К0Л СУДН0ГО ДНЯ";
                        case "ZeroAwakened3":
                        return "ВНИМАНИЕ. НАНЕСЕН КО0Л0ССАЛЬНЫЙ УРОН, СН0ВА ЕСТЬ УГРОЗА ПОРАЖЕНИЯ. ЗАПУСКАЮ РЕЖИМ П0ЛНЫЙ АТАКУЮЩИЙ РЕЖИМ 0МЕГА";
                        case "ZeroAwakened4":
                        return "0БНАРУЖЕН ЧИТЕР 0БНАРУЖЕН ЧИТЕР. НИКАК0Г0 ДР0ПА ДЛЯ Т3БЯ.";
                        case "ZeroAwakened5":
                        return "ТВ0Й ИНСТРУМЕНТ 'МЯСНИК' ТЕБЯ НЕ СПАСЕТ";
                        case "ZeroAwakened6":
                        return "ЦЕЛЬ НЕЙТРАЛИЗ0ВАНА. В0ЗВРАЩАЮСЬ НА 0РБИТУ.";
                        case "ZeroAwakened7":
                        return "ЦЕЛЬ П0ТЕРЯНА. В0ЗВРАЩАЮСЬ НА 0РБИТУ.";
                        case "ZeroAwakened8":
                        return @"ЗАРЯДКА САМ00РГАНИЗАЦИЙ ЗАВЕРШЕНА...";
                        case "ZeroAwakened9":
                        return @"ЗАПУСКАЮ РЕЖИМ САМ00РГАНИЗАЦИЙ...";
                        case "ZeroAwakened10":
                        return @"ALLOW C:\TERRARIA\AAMOD\ZER0\PR0T0C0L\SELF-ORGNAZATION.EXE ? <Y/N>...";
                        case "ZeroAwakened11":
                        return @"===Р Е З Н Я===";

                        case "ZeroDeath1":
                        return "МИССИЯ ПР0ВАЛЕНА. 0ТПРАВКА СИГНАЛА БЕДСТВИЯ НА БАЗУ.";
                        case "ZeroDeath2":
                        return "МИССИЯ ПР0ВАЛЕНА. ПЫТАЮСЬ СН0ВА 0ТПРАВИТЬ СИГНАЛА БЕДСТВИЯ НА БАЗУ";
                        case "ZeroDeath3":
                        return "0ТПРАВЛЯЮ...";
                        case "ZeroDeath4":
                        return "СИГНАЛ БЕДСТВИЯ П0ЛУЧЕН.";
                        case "FuryAshe1":
                        return "Папа, НЕТ! ТЫ за это ЗАПЛАТИШЬ, ";
                        case "FuryAshe2":
                        return "АГХ! Прости, папа..! Мне надо бежать!";
                        case "WrathHaruka1":
                        return "Отец! Рргх..! В следующий раз, я спущу тебя с небес на землю!";
                        case "WrathHaruka2":
                        return "Нгх... прости, отец... Я больше не могу...";

                        case "ShenA1":
                        return "Ты даешь настоящий бой, дитя.";
                        case "ShenA2":
                        return "Посмотри, где ты, террариан. Это все ты сотворил. Ритуал, руна? Все ты.";
                        case "ShenA3":
                        return "Дерешься до самого конца.";
                        case "ShenA4":
                        return "Почему ты сражаешься? Ради силы? Ради славы?";
                        case "ShenA5":
                        return "Ты немного напоминаешь меня...";
                        case "ShenA6":
                        return "В любом случае, я лично сделаю так, что бы это был твой последний ритуал!";
                        case "ShenA7":
                        return "Ты силен. Требуется мужество, что бы сразиться с всем, что ты победил.";
                        case "ShenA8":
                        return "Сколько еще ты со мной будешь биться, что бы победить меня? Или ты, наконец, сдашься?";
                        /* 
                        case "ShenA9":
                        return "Но сегодня, мы сражаемся! Покажи все, что у тебя есть!";
                        case "ShenA10":
                        return "УМРИ, НАКОНЕЦ, ТЫ, НИКЧЕМНЫЙ МЕЛКИЙ ЧЕРВЬ!!";
                        */
                        case "ShenA11":
                        return "Хмпф..! Вот мы и снова здесь, приближаемся к концу черты. Но я не сдамся!";
                        case "ShenA12":
                        return "Хм..?! Ты все еще жив? Ну, тогда... Давай изменим это!";
                        case "ShenA13":
                        return "Конец игры-самая сложная часть, помни это, дитя!";
                        case "ShenA14":
                        return "Ты маленький..! Умри, наконец!";
                        case "ShenA15":
                        return "Ну вот и все! Конец близок, так что ты не имеешь права сдаваться!";
                        case "ShenA16":
                        return "Невозможно-- Как?!";

                        case "ShenAThorium":
                        return "Знаешь, я видел, как ты победил того бога-сферу и его 3 громил. Стоит признать, это было впечатляюще.";
                        case "ShenACalamity":
                        return "Учитывая, что ты поставил ту тоскливую ведьму на место, ты заслуживаешь уважения.";
                        case "ShenAGRealm":
                        return "Я видел, как ты раздавил того вредителя-переростка в джунглях, впечатляет.";
                        case "ShenARedemption":
                        return "В любом случае, после того, как ты избил ту космическую ханжу, даже я был шокирован.";
                        case "ShenASpirit":
                        return "Теперь, когда я задумался об этом. Ты разбил надзирателя, как яйцо? Для такого нужно много сил.";
                        case "ShenANoMod":
                        return "Все, сраженное тобой, было подобно богам и монстрам. Ты достоин уважения и признания.  ";

                        case "ShenDeath1":
                        return "Снова разделились...";
                        case "ShenDeath2":
                        return "Это ТВОЯ вина, ты, дерзкий червь..! Я знал, что нам надо было быть намного более агрессивными, но  НЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕТ..! Ты сказал, что мы раздавим их даже толком не пытаясь!";
                        case "ShenDeath3":
                        return "Войны";
                        case "ShenDeath4":
                        return ",когда-нибудь, ты снова встанешь лицом к лицу с нашей яростью... или, когда мы снова наберемся сил...";
                        case "ShenDeath5":
                        return "... или, когда ты снова решишься использовать печать..!";
                        case "ShenDeath6":
                        return "Твой выбор, дитя";
                        case "ShenDeath7":
                        return "ТЫ ИМБЕЦИЛ! МЫ ПРОЙГРАЛИ! СНООООООООООООООООООООООООООООООООООООООВА!!!";
                        case "ShenDeath8":
                        return "Ргх, моя голова...";
                        case "ShenDeath9":
                        return "КУЧКА КЛОУНОВ";
                        case "ShenDeath10":
                        return "А ТЫ, ";
                        case "ShenDeath11":
                        return "! В СЛЕДУЮЩИЙ РАЗ Я ОТОРВУ ТВОЮ ГОЛОВУ!!!";
                        case "ShenDeath12":
                        return "И поверь нам, дитя.";
                        case "ShenDeath13":
                        return "Всегда есть следующий раз";

                        case "ShenDoragon1":
                        return "ТЕРРА МАГИЯ?! НЕТ! Я ДУМАЛ, ЧТО ОНА БЫЛА СТЕРТА С ЛИЦА ЗЕМЛИ!";
                        case "ShenDoragon2":
                        return "Руки Хаоса! Помогите мне!";
                        case "ShenDoragon3":
                        return "Аши? Харука? Мне снова нужна ваша помощь..!";
                        case "ShenDoragon4":
                        return "Уже иду, папа~!";
                        case "ShenDoragon5":
                        return "Снова..?";
                        case "ShenDoragon6":
                        return "Девочки..? Помогите вашему отцу разобраться с этим никчемным смертным.";
                        case "ShenDoragon7":
                        return "С удовольствием, папа~!";
                        case "ShenDoragon8":
                        return "Конечно, отец.";
                        case "ShenDoragon9":
                        return "Ты стойкий, дитя. Мне это нравиться.";
                        case "ShenDoragon10":
                        return "Что это? Компетентный воин? Неожидал...";
                        case "ShenDoragon11":
                        return "Настоящие войны беспощадны! И я уверен, что ты в том числе..!";
                        case "ShenDoragon12":
                        return "Сдавайся, дитя. Мир всегда впадает в хаос!";
                        case "ShenDoragon13":
                        return "БУДЬ БЕСПОЩАДЕН!";
                        case "ShenDoragon14":
                        return "Что? Ты все еще сражаешься? Почему?!";
                        case "ShenDoragon15":
                        return "Победа над сверхдревним запитала каменных хранителей.";
                        case "ShenDoragon16":
                        return "Хех, ладно. Я оставлю тебя в покое, полагаю. Но если ты вернешься и будешь сильнее, то я покажу тебе силу истинного неукротимого раздора...";
                        case "ShenDoragon17":
                        return "Победа над сверхдревним запитала каменных хранителей";
                        case "ShenDoragon18":
                        return "Хорошее шоу, дитя, хорошее шоу. Твоя отвага все еще восхищает меня! Может быть, когда-нибудь, я покажу тебе свою истинную силу.";
                        case "ShenDoragon19":
                        return "Неплохие умения, дитя.";
                        case "ShenDoragon20":
                        return "Что это? Компетентный воин? Впечатляет.";
                        case "ShenDoragon21":
                        return "Ты настоящий боец, знаешь ли. А теперь стой ровно и дай мне зажарить тебя.";
                        case "ShenDoragon22":
                        return "Хаос всегда правит, дитя. Вы, террарианы, похоже, никогда это не запомните.";
                        case "ShenDoragon23":
                        return "Похоже, что ты переворачиваешь ход событий, дитя, но не на долго!";
                        case "ShenDoragon24":
                        return "Хмм... все еще сражаешься, а?";
                        


                        case "ShenSpawn1":
                        return "Не ожидал снова увидеть нас, дитя?";
                        case "ShenSpawn2":
                        return "НЬЕХЕХЕХЕХЕХЕЕХЕХЕХЕХЕХЕ..! Да..! Я уверен, что ты прям шокирован увидеть нас здесь..! Но в этот раз, у нас есть 'НЕБОЛЬШОЙ' туз в рукаве..!";
                        case "ShenSpawn3":
                        return "Та печать, что ты использовал, вернула нам все наши силы, что позволит мне и Ямате перевоплотиться в нашу истинную форму, могущественную форму..!";
                        case "ShenSpawn4":
                        return "Когда то, мы были одним существом..! Но потом террариан, по типу тебя, разделил нас на две части..! Но теперь... хехехе...";
                        case "ShenSpawn5":
                        return "МЫ СНОВА ЕДИНЫ";
                        case "ShenSpawn6":
                        return "Хех.... хехех...";
                        case "ShenSpawn7":
                        return "Ты совершил гробовую ошибку...";
                        case "ShenSpawn8":
                        return "Видишь ли...";
                        case "ShenSpawn9":
                        return "Я, ШЕН ДОРАГОН, ИМПЕРАТОР ХАОСА И АНАРХИИ!";
                        case "ShenSpawn10":
                        return "А ты, дитя мое...";
                        case "ShenSpawn11":
                        return "ПАДЕШЬ!!!";
                        case "ShenTransition1":
                        return "Хех...";
                        case "ShenTransition2":
                        return "Хехехех...";
                        case "ShenTransition3":
                        return "ХАХАХАХАХАХАХА!!!";
                        case "ShenTransition4":
                        return "Ты забыл наши последние битвы?...?";
                        case "ShenTransition5":
                        return "Всегда есть последний бой, дитя...";
                        case "ShenTransition6":
                        return "Но в этот раз ты встал у меня на пути...";
                        case "ShenTransition7":
                        return "Шен Дорагон пробудился!";
                        case "ShenTransition8":
                        return "ТЫ БУДЕШЬ ГОРЕТЬ В ОГНЯХ РАЗДОРА!!!";
                    }
                }
            else
                {
                    switch(BossInfo)
                    {
                        case "male":
                        return "him";
                        case "fimale":
                        return "her";
                        case "male2":
                        return "HIM";
                        case "fimale2":
                        return "HER";
                        case "boy":
                        return ", BOY";
                        case "girl":
                        return ", GIRL";

                        case "FatPixie":
                        return "YEET";

                        case "AHDeath1":
                        return "RRRRRRRRRGH! NOT AGAIN!!!";
                        case "AHDeath2":
                        return "You see Ashe? I told you this was a stupid idea, but YOU didn't listen...";
                        case "AHDeath3":
                        return "Why do I keep going with you..? I should honestly just let you fight them yourself.";
                        case "AHDeath4":
                        return "Shut up! I thought if we ganged up on ";
                        case "AHDeath5":
                        return ", we could just beat the daylights out of 'em!";
                        case "AHDeath6":
                        return "Rgh..! Shut up..!";
                        case "AHDeath7":
                        return "Whatever...I'm going back home. SOMEONE has to tell dad about this kid.";
                        case "AHDeath8":
                        return "Hmpf..! Fine! Be that way! I'm going back to the inferno!";

                        case "AHSpawn1":
                        return "Well hello there, what a surprise to see YOU here~!";
                        case "AHSpawn2":
                        return "Oh yes, I've heard PLENTY about you, kid...you're the little warm-blood who thrashed my mother..!";
                        case "AHSpawn3":
                        return "Oh yes, I've heard PLENTY about you, kid...you've been stirring up quite a bit of trouble in these parts...";
                        case "AHSpawn4":
                        return "And mine...";
                        case "AHSpawn5":
                        return "...and you also hurt my mom...";
                        case "AHSpawn6":
                        return "You're a pretty annoying wretch, you know...";
                        case "AHSpawn7":
                        return "So now..! Heh...";
                        case "AHSpawn8":
                        return "We're gonna give you something to absolutely SCREAM about..! Come on, Hakie, let's torch this little warm-blood~!";
                        case "AHSpawn9":
                        return "Please don't call me Hakie again...ever.";

                        case "AsheDowned":
                        return "OW..! THAT HURT, YOU MEANIE!";
                        case "HarukaDowned":
                        return "Rgh..! Ow...";

                        case "Akuma1":
                        return "Water?! ACK..! I CAN'T BREATHE!";
                        case "Akuma2":
                        return "Yaaaaaaaaawn. I'm bushed kid, I'm gonna have to take a rain check. Come back tomorrow.";
                        case "Akuma3":
                        return "I thought you terrarians put up more of a fight. Guess not.";
                        case "Akuma4":
                        return "Hey kid! Sky's fallin', watch out!";
                        case "Akuma5":
                        return "Down comes fire and fury!";
                        case "Akuma6":
                        return "Spirits of the volcano! help me crush this kid!";
                        case "Akuma7":
                        return "Hey kid! Watch out!";
                        case "Akuma8":
                        return "Incoming!";
                        case "Akuma9":
                        return "Sun's shining, and there's no shade to be seen, kid!";
                        case "Akuma10":
                        return "Getting hotter, ain't it?";
                        case "Akuma11":
                        return "The volcanoes of the inferno are finally quelled...";
                        case "Akuma12":
                        return "Hmpf...you’re pretty good kid, but not good enough. Come back once you’ve gotten a bit better.";
                        case "Akuma13":
                        return "Face the flames of despair, kid!";
                        case "Akuma14":
                        return "Heads up, kid!";

                        case "AkumaA1":
                        return "Ashe? Help your dear old dad with this kid again!";
                        case "AkumaA2":
                        return "You got it, daddy..!";
                        case "AkumaA3":
                        return "Hey! Hands off my papa!";
                        case "AkumaA4":
                        return "Atta-girl..!";
                        case "AkumaA5":
                        return "Still got it, do you? Ya got fire in your spirit! I like that about you, kid!";
                        case "AkumaA6":
                        return "What?! How have you lasted this long?! Why you little... I refuse to be bested by a terrarian again! Have at it!";
                        case "AkumaA7":
                        return "ACK..! WATER! I LOATHE WATER!!!";
                        case "AkumaA8":
                        return "Nighttime won't save you from me this time, kid! The day is born anew!";
                        case "AkumaA9":
                        return "You just got burned, kid.";
                        case "AkumaA10":
                        return "Heh, not too shabby this time kid. I'm impressed. Here. Take your prize.";
                        case "AkumaA11":
                        return "GRAH..! HOW!? HOW COULD I LOSE TO A MERE MORTAL TERRARIAN?! Hmpf...fine kid, you win, fair and square. Here's your reward.";
                        case "AkumaA12":
                        return "Nice. You cheated. Now come fight me in expert mode like a real man.";
                        case "AkumaA13":
                        return "Sky's fallin' again! On your toes!";
                        case "AkumaA14":
                        return "Down comes the flames of fury again!";
                        case "AkumaA15":
                        return "You underestimate the artillery of a dragon, kid!";
                        case "AkumaA16":
                        return "Flames don't give in till the end, kid!";
                        case "AkumaA17":
                        return "Heads up! Volcano's eruptin' kid!";
                        case "AkumaA18":
                        return "INCOMING!";
                        case "AkumaA19":
                        return "Hey Kid? Like Fireworks? No? Too Bad!";
                        case "AkumaA20":
                        return "Here comes the grand finale, kid!";
                        case "AkumaA21":
                        return "The Sun won't quit 'til the day is done, kid!";
                        case "AkumaA22":
                        return "Face the fury of the sun!";

                        case "AkumaAAshe1":
                        return "Papa, NO! HEY! YOU! I'm gonna bake you alive next time we meet..!";
                        case "AkumaAAshe2":
                        return "OW, you Jerk..! I'm out!";

                        case "AkumaTransition1":
                        return "Heh...";
                        case "AkumaTransition2":
                        return "You know, kid...";
                        case "AkumaTransition3":
                        return "fanning the flames doesn't put them out...";
                        case "AkumaTransition4":
                        return "Akuma has been Awakened!";
                        case "AkumaTransition5":
                        return "IT ONLY MAKES THEM STRONGER!";
                        case "AkumaTransition6":
                        return "The air around you begins to heat up...";

                        case "AnubisFalse":
                        return "HAH! Get hosed-- er, sanded.";

                        case "AnubisGuys":
                        return "guys";
                        case "Anubisbud":
                        return "bud";

                        case "Anubis1":
                        return "Well, ";
                        case "Anubis2":
                        return ". Here we are.";
                        case "Anubis3":
                        return "I hope you're ready for a real fight.";
                        case "Anubis4":
                        return "Especially since I'm in my superior form.";
                        case "Anubis5":
                        return "You ready? I won't hesitate to slap you silly!";
                        case "Anubis6":
                        return "Let's go!";
                        case "Anubis7":
                        return "A rematch eh? Alright, this should be fun!";

                        case "AnubisCombat1":
                        return "YEET";
                        case "AnubisCombat2":
                        return "Careful! Projectiles hurt.";
                        case "AnubisCombat3":
                        return "Let me grab some friends";
                        case "AnubisCombat4":
                        return "Catch!";
                        case "AnubisCombat5":
                        return "Smashing, Init?";

                        case "AnubisTransition1":
                        return "...hrgh...";
                        case "AnubisTransition2":
                        return "...alright.";
                        case "AnubisTransition3":
                        return "I think...it's time.";
                        case "AnubisTransition4":
                        return "No more stops being pulled.";
                        case "AnubisTransition5":
                        return "If you're gonna be taking on the dark forces of the world...";
                        case "AnubisTransition6":
                        return "I need to make sure you're ready, because...unless you're ready...";
                        case "AnubisTransition7":
                        return "...Some things should stay locked away for your own good.";

                        case "FAnubis":
                        return "...Sorry, but you aren't ready yet.";

                        case "FAnubisCombat":
                        return "No Warnings this time.";

                        case "FAnubisWin":
                        return "...You done good, bud. Let's make a game plan moving forward. Come talk to me when you're ready.";

                        case "Athena1":
                        return "Hmpf..!";
                        case "Athena2":
                        return "s";
                        case "Athena3":
                        return "You! Earthwalker";
                        case "Athena4":
                        return "My seraphs tell me you've been attacking them! Why?!";
                        case "Athena5":
                        return "I'm gonna teach you a lesson, you little brat!";
                        case "Athena6":
                        return "En Garde!";
                        case "Athena7":
                        return "Sigh...";
                        case "Athena8":
                        return "Lets just get this overwith. I don't have all day.";
                        case "Athena9":
                        return "And stay away...idiot.";
                        case "Athena10":
                        return "No more kidding around, the storms are calling, and they're coming for you!";
                        case "Athena11":
                        return "OW! Fine, fine..! I'll leave you alone! Geez, you don't let up, do you.";
                        case "Athena12":
                        return "...So. You came.";
                        case "Athena13":
                        return "It's high time I won my honor back..!";
                        case "Athena14":
                        return "En Garde!";

                        case "AthenaA1":
                        return "And stay away...idiot.";
                        case "AthenaA2":
                        return "OW! Fine, fine..! I'll leave you alone! Geez, you don't let up, do you.";

                        case "AthenaDefeat1":
                        return "...hah...hah...";
                        case "AthenaDefeat2":
                        return "...I still lost.";
                        case "AthenaDefeat3":
                        return "No.";
                        case "AthenaDefeat4":
                        return "I'm not giving up that easilly.";
                        case "AthenaDefeat5":
                        return "There's a phrase my people live by, earthwalker.";
                        case "AthenaDefeat6":
                        return "Brightest of dawn...";
                        case "AthenaDefeat7":
                        return "Darkest of night...";
                        case "AthenaDefeat8":
                        return "Even in defeat...";
                        case "AthenaDefeat9":
                        return "A VARIAN ALWAYS PUTS UP ONE LAST FIGHT!!!";

                        case "Athena2Defeat1":
                        return "...WHY?!";
                        case "Athena2Defeat2":
                        return "...Why can't I win?!";
                        case "Athena2Defeat3":
                        return "...You haven't heard the end of me, earthwalker ";
                        case "Athena2Defeat4":
                        return "s";
                        case "Athena2Defeat5":
                        return "I will win back my honor eventually...";
                        case "Athena2Defeat6":
                        return "...until then, watch your back.";
                        case "Athena2Defeat7":
                        return "Dark, chaotic forces are waking up as of late.";
                        case "Athena2Defeat8":
                        return "Hopefully HE doesn't come back...";
                        case "Athena2Defeat9":
                        return "Stay safe.";

                        case "SeraphHerald1":
                        return "HEY! EARTHWALKER! YEAH YOU, YA STUPID APE!";
                        case "SeraphHerald2":
                        return "Queen Athena requests your presence at the acropolis again immediately!";
                        case "SeraphHerald3":
                        return "She demands a rematch, and this time, she won't let you tear her down so easily!";
                        case "SeraphHerald4":
                        return "I would say break a leg, but we can do that ourselves when you show up!";
                        case "SeraphHerald5":
                        return "See ya twerp!";
                        case "SeraphHerald6":
                        return "Oh yeah, and uh, that obnoxious kleptomaniac worm wants to fight you again too or something.";

                        case "GreedFalse1":
                        return "EEEEEEEEEEEEEEEGH THE LIGHT OF THE SURFACE! TOO BRIGHT! TOO BRIGHT!";
                        case "GreedFalse2":
                        return "AND STAY AWAY FROM MY GLORIOUS RICHES YOU LITTLE THIEF!";

                        case "Greed1":
                        return "Who disturbs me from my coin-counting?! I'm busy--";
                        case "Greed2":
                        return "...Oooooh...is that...?";
                        case "Greed3":
                        return "You brought me my favorite dish..!";
                        case "Greed4":
                        return "Now, if you don't mind...";
                        case "Greed5":
                        return "BEGONE FROM MY HOARD!!!";
                        case "GreedName":
                        return "Greed";

                        case "GreedTransition1":
                        return "YOU..! YOU LITTLE--";
                        case "GreedTransition2":
                        return "WHY DO YOU REFUSE TO LEAVE ME BE?!";
                        case "GreedTransition3":
                        return "YOU TRY TO STEAL MY SHINIES, AND NOW...!";
                        case "GreedTransition4":
                        return "I WILL STEAL YOUR LIFE!";
                        case "GreedAName":
                        return "Worm King Greed";

                        case "Rajah1":
                        return "JUSTICE CANNOT BE CHEATED";
                        case "Rajah2":
                        return "Justice has been served...";
                        case "Rajah3":
                        return "Coward.";
                        case "Rajah4":
                        return "You win this time, murderer...but I will avenge those you've mercilessly slain...";
                        case "Rajah5":
                        return "THIS ISN'T THE END, ";
                        case "Rajah6":
                        return "! RIVALS CLASH TILL THE VERY END!";
                        case "Rajah7":
                        return "And stay down.";
                        case "Rajah8":
                        return "Well fought, ";
                        case "Rajah9":
                        return ". Take your reward.";

                        case "SupremeRajahChat":
                        return "Rajah glows with furious energy as he attacks, strengthening his defenses";

                        case "SupremeRajahDefeat1":
                        return "Rgh...";
                        case "SupremeRajahDefeat2":
                        return "...so...";
                        case "SupremeRajahDefeat3":
                        return "Even when I'm at my most powerful...";
                        case "SupremeRajahDefeat4":
                        return "...I still can't beat you.";
                        case "SupremeRajahDefeat5":
                        return "...Terrarian...maybe...";
                        case "SupremeRajahDefeat6":
                        return "Perhaps this is all just a sign that...maybe my time as protector...";
                        case "SupremeRajahDefeat7":
                        return "...is finally up. It might be time to pass on the baton.";
                        case "SupremeRajahDefeat8":
                        return "...I forgive you for every rabbit you've killed, but in return...I want you to take my place...";
                        case "SupremeRajahDefeat9":
                        return "...as their champion. Their protector.";
                        case "SupremeRajahDefeat10":
                        return "I only want the best for the creatures of this world...and if you're stronger than I am...";
                        case "SupremeRajahDefeat11":
                        return "Who better to take my place than you, ";
                        case "SupremeRajahDefeat12":
                        return "Be the one the innocent can look to in their time of need.";
                        case "SupremeRajahDefeat13":
                        return "Think about it.";
                        case "SupremeRajahDefeat14":
                        return "And if you ever want to spar...use one of those special carrots. I'd be glad to earn my honor back.";
                        case "SupremeRajahDefeat15":
                        return "...See ya, kiddo.";
                        case "SupremeRajahDefeat16":
                        return "Rajah Rabbit's speech warms your heart. You no longer have the will to harm rabbits. Do him proud.";

                        case "SagChat":
                        return "EXTENSIVE BODY DAMAGE DETECTED. PROCEED WITH CAUTION!";

                        case "YamataAHead":
                        return "OWIE!!!";
                        case "Yamata1":
                        return "HAH! I went easy on ya! Come back when you're actually good and we can have a real fight!";
                        case "Yamata2":
                        return "The defeat of Yamata causes the fog in the mire to lift.";
                        case "Yamata3":
                        return "THE SUN DOESN'T SHINE IN THE DEPTHS!!! NYEHEHEHEHEHEHEHEH!!!";
                        case "Yamata4":
                        return "HISSSSSSSSSSSSSSS!!! THE SUNNNNNNNNNN! I'M OUT!";
                        case "Yamata5":
                        return "YOU THINK I'M DONE YET?! I DON'T THINK SO!!!!";
                        case "Yamata6":
                        return "Oh and don't even think about flying! My ego is so massive it has a gravitational pull all of it's own! NYEHEHEHEHEHEHEHEHEHEHEHEH!!!";
                        case "Yamata7":
                        return "THERE IS NO ESCAPE FROM THE ABYSS!";
                        case "Yamata8":
                        return "Running away?! I DON'T THINK SO!";
                        case "Yamata9":
                        return "NYEHEHEHEHEHEHEH..! And don't come back!";
                        case "Yamata10":
                        return "Resistance isn't gonna save you here! Now stop being a little brat and let me destroy you!";
                        case "Yamata11":
                        return "STOP SQUIRMING AND LET ME SQUASH YOU!!!";
                        case "Yamata12":
                        return "NGAAAAAAAAAAAAAH YOU'RE REALLY ANNOYING YOU KNOW..!";
                        case "Yamata13":
                        return "I don't understand why you keep fighting me! I'm superior to you in every single way!";
                        case "Yamata14":
                        return "I'M GETTING FRUSTRATED AGAIN!";
                        case "Yamata15":
                        return "I HATE FIGHTING YOU! I HATE IT I HATE IT I HATE IT!!!";

                        case "YamataHead":
                        return "OWIE!!!";
                        case "YamataTransition1":
                        return "NYEHEHEHEHEHEHEHEH~!";
                        case "YamataTransition2":
                        return "You thought I was DONE..?!";
                        case "YamataTransition3":
                        return "HAH! AS IF!";
                        case "YamataTransition4":
                        return "The abyss hungers...";
                        case "YamataTransition5":
                        return "Yamata has been Awakened!";
                        case "YamataTransition6":
                        return "AND IT'S GOT 7 HEADS! NYEHEHEHEHEHEHEHEHEHEHEHEH!!!";
                        case "YamataTransition7":
                        return "You begin to feel as if your soul is weighing you down...";

                        case "YamataA1":
                        return "REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE";
                        case "YamataA2":
                        return "NO...! IMPOSSIBLE! EVEN IN MY AWAKENED FORM?! YOU MUST HAVE CHEATED! GYAAAAAAH..! FINE! TAKE YOUR LOOT! I'M OUTTA HERE..!";
                        case "YamataA3":
                        return "The defeat of Yamata causes the fog in the mire to lift.";
                        case "YamataA4":
                        return "NOOOOOOOOOOOOOO!!! YOU LITTLE BRAT!!! I ALMOST HAD YOU THIS TIME!!! FINE, take your stuff, See if I care!";
                        case "YamataA5":
                        return "HAH! Nice try! Come back in Expert mode when you don't have to cheat to beat me! All your loot is MINE still!";
                        case "YamataA6":
                        return "YOU'RE STILL FIGHTING?! WHAT THE HELL IS WRONG WITH YOU?!";
                        case "YamataA7":
                        return "I'VE HAD IT UP TO HERE WITH YOUR SHENANIGANS!!! EAT VENOM YOU LITTLE HELLSPAWN!!!";
                        case "YamataA8":
                        return "STOP IT STOP IT STOP IT!!! I'M NOT GONNA LET YOU WIN!!!";
                        case "YamataA9":
                        return "You're an annoying little bugger, you know!";
                        case "YamataA10":
                        return "DIE! WHY WON'T YOU JUST DIE ALREADY?!";
                        case "YamataA11":
                        return "STOP IT!!! I'M NOT GONNA LOSE AGAIN!!!";
                        case "YamataA12":
                        return "Wh-WHA?! DIE! DIE YOU LITTLE TWERP! DIEDIEDIEDIEDIEDIEDIE!!!!";
                        case "YamataA13":
                        return "NO NO NO!!! NOT AGAIN!!! THIS TIME IMMA STOMP YOU RIGHT INTO THE GROUND!!!";
                        case "YamataA14":
                        return "Looks like I gotta come in and save your rear end again, dad.";
                        case "YamataA15":
                        return "That's my girl..!";
                        case "YamataA16":
                        return "Oh, sweetie..! Care to help daddy thrash this little worm?!";
                        case "YamataA17":
                        return "Sigh...yes dad.";

                        case "HarukaY1":
                        return "Dad, you moron..! Whatever, Can't really say I didn't see it coming.";
                        case "HarukaY2":
                        return "That's it. I'm done, YOU deal with them, dad.";

                        case "YamataHead1":
                        return "TASTE ACID YOU UNBEARABLE MAGGOT!!!";
                        case "YamataHead2":
                        return "STOP MOVING AND LET ME MELT YOU!!!";
                        case "YamataHead3":
                        return "Down Down DOWN THE VENOM GOES!!! When it will it stop? WHO KNOWS?! NYEHEHEHEHEHEH!!!";
                        case "YamataHead4":
                        return "DIEDIEDIEDIEDIEDIEDIEDIIIIIIIIIIIIIIIE!!!";
                        case "YamataHead5":
                        return "BAM! BOOM! I'LL BLOW YOU INTO NEXT SUNDAY!!!";
                        case "YamataHead6":
                        return "NGAAAAAAAAAAAAAAAAAH!!!";
                        case "YamataHead7":
                        return "GET THEM! EAT THEM! JUST GET ";
                        case "YamataHead8":
                        return " OUT OF MY FACE!!!";
                        case "YamataHead9":
                        return "I HAVE EATEN RABBITS MORE INTIMIDATING THAN YOU!";
                        case "YamataHead10":
                        return "HOPE YOU BROUGHT YOUR UMBRELLA! BECAUSE IT'S RAINING PAIN!!! NYEHEHEHEHEHEHEHEH!!!";
                        case "YamataHead11":
                        return "DOWN COMES THE VENOM!!!NYEHEHEHEHEHEHEHEH!";
                        case "YamataHead12":
                        return "EAT ECTOPLASM YOU LITTLE WRETCH";
                        case "YamataHead13":
                        return "NYAAAAAAAAAAAH!!!";
                        case "YamataHead14":
                        return "WHOOPS! DROPPED ACID! Hope you're not degradable..!";
                        case "YamataHead15":
                        return "WHOOPS! DROPPED ACID AGAIN! NYEHEHEHEHEHEHEHEHEHEHEHEH";
                        case "YamataHead16":
                        return "NYAAAAAAAH! YOU WON'T LIVE THROUGH THIS ONE!";
                        case "YamataHead17":
                        return "COME ON!!! STAND STILL SO I CAN BLOW YOU TO MARS!";
                        case "YamataHead18":
                        return "NGAAAAAAAAAAAAAH STAAAAAAAHP MOOOOOOOOVIIIIIIING!!!!!";
                        case "YamataHead19":
                        return "HAVE A HEALTHY TASTE OF ACID!";
                        case "YamataHead20":
                        return "I'M GONNA RIP YOU TO PIECES YOU LITTLE WRETCH!!!";
                        case "YamataHead21":
                        return "REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!!!";

                        case "Sagittarius1":
                        return "target(s) neutralized. returning to stealth mode.";
                        case "Sagittarius2":
                        return "target(s) lost. returning to stealth mode.";
                        case "SagittariusFree1":
                        return "target(s) neutralized. returning to stealth mode.";
                        case "SagittariusFree2":
                        return "target(s) lost. returning to stealth mode.";
                        case "SagittariusFree3":
                        return "initializing repair program.";

                        case "ZeroBoss1":
                        return "PHYSICAL ZER0 UNIT IN CRITICAL C0NDITI0N. DISCARDING AND ENGAGING D00MSDAY PR0T0C0L.";
                        case "ZeroBoss2":
                        return "D00MSDAY PR0T0CALL MALFUNCTI0N. MAIN.EXPERT M0DE = FALSE.";
                        case "ZeroBoss3":
                        return "Zero's dark influence over the void has been lifted. The doomstone here is now minable.";
                        case "ZeroBoss4":
                        return "INITIALIZING BACKUP WEAPON PR0T0C0L.";
                        case "ZeroBoss5":
                        return "CRITICAL ERR0R: ARM UNITS NOT FOUND. RER0UTING RES0URCES TO OFFENSIVE PR0T0C0LS. SHIELD L0WERED.";
                        case "ZeroBoss6":
                        return "TARGET NEUTRALIZED. RETURNING T0 0RBIT.";
                        case "ZeroBoss7":
                        return "TARGET L0ST. RETURNING T0 0RBIT.";
                        case "ZeroBoss8":
                        return "INITIATING PR0BE CREATI0N SYSTEM.";
                        case "ZeroBoss9":
                        return "CYCLONE PROTOCOL ENGAGED";
                        case "ZeroBoss10":
                        return "RE-ESTABLISHING WEAP0N UNITS";
                        case "ZeroBoss11":
                        return "RE-wiogn WEAP0N UNITS";

                        case "ZeroAwakened1":
                        return "Doomstone stops glowing. You can now mine it.";
                        case "ZeroAwakened2":
                        return "WARNING. DRASTIC DAMAGE DETECTED, FAILURE IMMINENT. ENGAGE T0TAL 0FFENCE PR0T0C0L";
                        case "ZeroAwakened3":
                        return "WARNING. DRASTIC DAMAGE DETECTED, FAILURE IMMINENT AGAIN. ENGAGE T0TAL 0FFENCE PR0T0C0L 0MEGA";
                        case "ZeroAwakened4":
                        return "CHEATER ALERT CHEATER ALERT. N0 DR0PS 4 U";
                        case "ZeroAwakened5":
                        return "Y0UR CHEAT SHEET BUTCHER T00L WILL N0T SAVE Y0U HERE";
                        case "ZeroAwakened6":
                        return "TARGET NEUTRALIZED. RETURNING T0 0RBIT.";
                        case "ZeroAwakened7":
                        return "TARGET L0ST. RETURNING T0 0RBIT.";
                        case "ZeroAwakened8":
                        return @"SELF-ORGNAZATION CHARGING COMPLETED...";
                        case "ZeroAwakened9":
                        return @"BOOTING IN SELF ORGNAZATION MODE...";
                        case "ZeroAwakened10":
                        return @"ALLOW C:\TERRARIA\AAMOD\ZER0\PR0T0C0L\SELF-ORGNAZATION.EXE ? <Y/N>...";
                        case "ZeroAwakened11":
                        return @"===C A R N A G E===";

                        case "ZeroDeath1":
                        return "MISSI0N FAILED. SENDING DISTRESS SIGNAL T0 H0ME BASE.";
                        case "ZeroDeath2":
                        return "MISSI0N FAILED. ATTEMPTING DISTRESS SIGNAL AGAIN.";
                        case "ZeroDeath3":
                        return "SENDING...";
                        case "ZeroDeath4":
                        return "DISTRESS SIGNAL RECIEVED.";
                        case "FuryAshe1":
                        return "Papa, NO! You'll PAY for this, ";
                        case "FuryAshe2":
                        return "AGH! Sorry papa..! I gotta bail!";
                        case "WrathHaruka1":
                        return "Father! Rrgh..! Next time we meet, I'll strike you down!";
                        case "WrathHaruka2":
                        return "Ngh...sorry father...I can't carry on...";

                        case "ShenA1":
                        return "You put up a real fight, kid.";
                        case "ShenA2":
                        return "Look at where you are, terrarian. This is your own doing. The ritual, the rune? All you.";
                        case "ShenA3":
                        return "Brawlin' till the very end.";
                        case "ShenA4":
                        return "Why do you instigate all these fights? Power? Glory?";
                        case "ShenA5":
                        return "You remind me of myself quite a bit...";
                        case "ShenA6":
                        return "Either way, I'll personally make sure this is your last one!";
                        case "ShenA7":
                        return "You're strong, too. Takes guts to fight most of what you have.";
                        case "ShenA8":
                        return "How many times will you fight to tear me down? Or will you give up?";
                        /* 
                        case "ShenA9":
                        return "But today, we clash! Now show me what you got!";
                        case "ShenA10":
                        return "DIE ALREADY YOU INSIGNIFICANT LITTLE WORM!!";
                        */
                        case "ShenA11":
                        return "Hmpf..! Here we are again, gettin' close to the end of the line. I'm not holding back!";
                        case "ShenA12":
                        return "Hm..?! You haven't kicked the bucket yet? Well then...Let's change that!";
                        case "ShenA13":
                        return "The end of a game is the most stressful, keep that in mind, kid!";
                        case "ShenA14":
                        return "You little..! Die already!";
                        case "ShenA15":
                        return "This is it! The end is near, so don't you dare hold back!";
                        case "ShenA16":
                        return "This can't be--! How?!";

                        case "ShenAThorium":
                        return "You know, I was watching you beat down that god-sphere and its 3 goons. Gotta admit, pretty impressive.";
                        case "ShenACalamity":
                        return "Considering you put that angsty witch in her place, gotta hand it to ya.";
                        case "ShenAGRealm":
                        return "Seeing you squashed that oversided insect in the jungle, that's quite a head-turner.";
                        case "ShenARedemption":
                        return "However, after you walloped that cosmic prude, even I was taken aback by that level of skill.";
                        case "ShenASpirit":
                        return "Now that I think about it, though, you cracking open that overseer like an egg? Quite the strength that had to have needed.";
                        case "ShenANoMod":
                        return "Everything you've torn down, gods and monsters alike, I respect that.";

                        case "ShenDeath1":
                        return "Split again...";
                        case "ShenDeath2":
                        return "This is YOUR fault you insolent worm..! I knew we should have been more aggressive but NOOOOOOOOO..! YOU said we could squash them without even trying!";
                        case "ShenDeath3":
                        return "Warriors";
                        case "ShenDeath4":
                        return ", you will face our fury again one day...either when we gain enough power again...";
                        case "ShenDeath5":
                        return "...or you decide to use that Sigil again..!";
                        case "ShenDeath6":
                        return "Your choice, child.";
                        case "ShenDeath7":
                        return "YOU IMBECILE! WE LOST! AGAAAAAAAAAAAAIN!!!";
                        case "ShenDeath8":
                        return "Rgh, my head...";
                        case "ShenDeath9":
                        return "BUNCH OF CLOWNS";
                        case "ShenDeath10":
                        return "And YOU";
                        case "ShenDeath11":
                        return "! NEXT TIME I'M GONNA TEAR YOUR HEADS OFF!!!";
                        case "ShenDeath12":
                        return "And trust us, kid.";
                        case "ShenDeath13":
                        return "There's always a next time.";

                        case "ShenDoragon1":
                        return "TERRA MAGIC?! NO! I THOUGHT IT WAS WIPED FROM THE EARTH!";
                        case "ShenDoragon2":
                        return "Grips! Assist me!";
                        case "ShenDoragon3":
                        return "Ashe? Haruka? I need your assistance again..!";
                        case "ShenDoragon4":
                        return "On it, Papa~!";
                        case "ShenDoragon5":
                        return "Again..?";
                        case "ShenDoragon6":
                        return "Girls..? Help your father with this insignificant mortal.";
                        case "ShenDoragon7":
                        return "With pleasure, Papa~!";
                        case "ShenDoragon8":
                        return "Yes, father.";
                        case "ShenDoragon9":
                        return "You are quite persistent, Child. I like that.";
                        case "ShenDoragon10":
                        return "What's this? Competence? I would have never expected...";
                        case "ShenDoragon11":
                        return "True warriors don't show mercy! I won't and I doubt you will either..!";
                        case "ShenDoragon12":
                        return "Give up, child. The world will always fall into chaos!";
                        case "ShenDoragon13":
                        return "SHOW NO MERCY!";
                        case "ShenDoragon14":
                        return "What? You're still fighting? Why?!";
                        case "ShenDoragon15":
                        return "The defeat of a superancient empowers the stonekeepers.";
                        case "ShenDoragon16":
                        return "Heh, alright. I’ll leave you alone I guess. But if you come back stronger, I’ll show you the power of true unyielding chaos...";
                        case "ShenDoragon17":
                        return "The defeat of a superancient empowers the stonekeepers.";
                        case "ShenDoragon18":
                        return "Good show, child, good show. Your combat prowess still impresses me! Maybe some day I'll show you my true power.";
                        case "ShenDoragon19":
                        return "Quite the skill, kid.";
                        case "ShenDoragon20":
                        return "What's this? Competence? Impressive.";
                        case "ShenDoragon21":
                        return "You're a real fighter, you know. Now stand still and let me grill you.";
                        case "ShenDoragon22":
                        return "Chaos always reigns, kid. You terrarians never seem to learn that.";
                        case "ShenDoragon23":
                        return "Looks like you're turning tide, but not for long!";
                        case "ShenDoragon24":
                        return "Hmm...still fighting, eh?";
                        


                        case "ShenSpawn1":
                        return "Surprised to see us again, Kid?";
                        case "ShenSpawn2":
                        return "NYEHEHEHEHEH..! Yes..! Must be shocking to see us here..! But this time, we have a little tricksie up our sleeves..!";
                        case "ShenSpawn3":
                        return "That Sigil you just used gave us back our full power, which will let us reach our true, powerful form..!";
                        case "ShenSpawn4":
                        return "We used to be the same being..! But then a Terrarian wretch like you split our soul in half..! But now...heheheh...";
                        case "ShenSpawn5":
                        return "WE ARE COMPLETE AGAIN";
                        case "ShenSpawn6":
                        return "Heh....heheh...";
                        case "ShenSpawn7":
                        return "You've made a grave mistake, child...";
                        case "ShenSpawn8":
                        return "For you see....";
                        case "ShenSpawn9":
                        return "I AM SHEN DORAGON, EMPEROR OF CHAOS AND ANARCHY!";
                        case "ShenSpawn10":
                        return "And you, my child...";
                        case "ShenSpawn11":
                        return "WILL PERISH!!!";
                        case "ShenTransition1":
                        return "Heh...";
                        case "ShenTransition2":
                        return "Heheheh...";
                        case "ShenTransition3":
                        return "HAHAHAHAHAHAHA!!!";
                        case "ShenTransition4":
                        return "Have you forgotten about our last battles...?";
                        case "ShenTransition5":
                        return "There's always a last stand, kiddo...";
                        case "ShenTransition6":
                        return "But now, you stand in my path...";
                        case "ShenTransition7":
                        return "Shen Doragon has been Awakened!";
                        case "ShenTransition8":
                        return "YOU WILL BURN IN THE FLAMES OF DISCORDIAN HELL!!!";
                    }
                }
            return"";
        }
        public static string BossSummonsInfo(string BossName)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                   switch(BossName)
                    {
                        case "BossAwoken":
                        return "已经苏醒!";
                        case "GripsAwoken":
                        return "附近可以听到刮擦地面的声音...";
                        case "EquinoxWormawoken":
                        return "昼夜双虫已经苏醒!";
                        case "ChaosRuneYamataFalse":
                        return "一幅凶煞邪魔的图像在你脑海中一闪而过...";
                        case "ChaosRuneAkumaFalse":
                        return "一种七个头的恐惧在你脑海中一闪而过...";
                        case "ChaosRuneFalse":
                        return "哈!我也希望有两个我能把你干翻!";
                        case "ChaosRuneAncientsFalse":
                        return "这个符文没用...";
                        case "ChaosRuneTrue1":
                        return "上神应龙已经苏醒!";
                        case "ChaosRuneTrue2":
                        return "跳过乐趣时间? 懂了, 我欣赏你, 小子";
                        case "ChaosSigilFalse1":
                        return "只有蓝色印迹在发光...";
                        case "ChaosSigilFalse2":
                        return "只有红色印迹在发光...";
                        case "ChaosSigilFalse3":
                        return "哈!我也希望有两个我能把你干翻!";
                        case "ChaosSigilFalse4":
                        return "混沌印迹发着光， 混沌底座的图像在你的脑海中闪现";
                        case "ChaosSigilFalse5":
                        return "这个印迹没用...";
                        case "ChaosSigilTrue1":
                        return "大错特错, 小子...";
                        case "ChaosSigilTrue2":
                        return "哈? ...又来. . ? 好, 让我们把这事安排的明明白白";
                        case "ConfusingMushroomFalse":
                        return "蘑菇发着光, 它的气味让你感到毛骨悚然. ";
                        case "ConfusingMushroomTrue":
                        return "真菌帝准备攻击你. ";
                        case "CuriousClawTrue":
                        return "混沌双爪已经到来!";
                        case "CuriousClawFalse":
                        return "爪子在你手中无力的伸开. 恶心的要死. ";
                        case "CyberneticBellFalse":
                        return "铃铛在响, 什么也没发生. ";
                        case "CyberneticBellTrue":
                        return "侵入者听到了铃声, 即将开始杀戮. ";
                        case "CyberneticClawFalse":
                        return "爪子在你手中无力的伸开. ";
                        case "CyberneticClawTrue":
                        return "捕猎者正准备来狩猎你. ";
                        case "CyberneticShroomFalse":
                        return "别再像个精神病一样挥舞着那个金属蘑菇了. ";
                        case "CyberneticShroomTrue":
                        return "电子松露出现. ";
                        case "DiamondCarrotRajahText":
                        return "致 命 错 误, 泰 拉 人!";
                        case "DiamondCarrotRajahText2":
                        return "给我 康 康 你拿了什么, ";
                        case "DjinnLampDayTimeFalse":
                        return "魔灯在月光下闪烁, 什么也没发生. ";
                        case "DjinnLampDesertFalse":
                        return "你擦灯的时候, 它吐着沙子";
                        case "DjinnLampFalse":
                        return "再怎么擦也没用";
                        case "DraconianDayTimeFalse":
                        return "臭小子. 龙不睡觉吗？ 明天再来。 ";
                        case "DraconianRuneFalse1":
                        return "一幅燎狱顶端焦塔的图像在你脑海一闪而过. ";
                        case "DraconianRuneFalse2":
                        return "嘿, 小子, 你应该知道, 那符文只需要用一次就行. ";
                        case "DraconianRuneInfernoFalse":
                        return "你只能在燎狱使用那个印迹, 小子. ";
                        case "DraconianRuneInfernoFalse2":
                        return "小子， 这个符文必须用在酷煞之日祭坛上。 在燎狱的中央。 ";
                        case "DraconianRuneTrue1":
                        return "你周围的空气开始升温!";
                        case "DraconianRuneTrue2":
                        return "不浪费时间切中要害...? 好吧, 那你准备下地狱吧...!";
                        case "DraconianSigilFalse":
                        return "嘿, 小子, 你应该知道, 那印迹只需要用一次就行. ";
                        case "DraconianSigilInfernoFalse":
                        return "你只能在燎狱使用那个印迹, 小子. ";
                        case "DraconianSignalTrue1":
                        return "呵呵, 我希望你已经准备好感受烈日的愤火了, 小子. ";
                        case "DraconianSignalTrue2":
                        return "又来, 小子？ 你没有更好的事情要做吗？ 你已经打败过我一次了. 好吧, 但我可不会对你心慈手软. ";
                        case "DragonBellDayTimeFalse":
                        return "龙现在已经睡了, 听不见铃铛响. ";
                        case "DragonBellTrue":
                        return "育母炎龙已经被呼唤";
                        case "DragonBellFalse":
                        return "龙不在这, 听不见铃铛响. ";
                        case "DreadRuneTrue1":
                        return "八歧大蛇已经觉醒!";
                        case "DreadRuneTrue2":
                        return "是的, 是的, 我明白了, 我第一阶段很讨厌. 让我们用……来解决这个问题!";
                        case "DreadTimeFalse":
                        return "不！我现在不想战斗！我需要舒服的睡眠！晚上来！";
                        case "DreadFalse1":
                        return "一幅潭渊中心怪树的图像在你脑海一闪而过";
                        case "DreadFalse2":
                        return "你究竟在tm干什么? 我已经在这了!!!";
                        case "DreadMireFalse":
                        return "嘿, 小傻子！潭渊那边走！";
                        case "DreadMireFalse2":
                        return "你必须在潭渊中心的祭坛上使用! 相信我, 不会出事的!";
                        case "DreadSigilTrue1":
                        return "你 胆 敢 进入我的地盘, 泰拉人? !哦呵呵呵呵呵…!大错特错!";
                        case "DreadSigilTrue2":
                        return "再来要更多战利品……? !这一次你可没那么幸运, 你这个小鬼……!";
                        case "SistersInfo1":
                        return "又是你...!? 上次还没学乖? 哦好, 我会用一个球把你炸成碎片!";
                        case "SistersInfo2":
                        return "不管怎样, 让我们结束这一切吧. ";
                        case "SistersDownedInfo1":
                        return "咳……再来一次……";
                        case "SistersDownedInfo2":
                        return "这 次 我 不 会 输!你会感觉到失败的味道, 你这可恶的常温动物!";
                        case "HydraChowTimeFalse":
                        return "什么也没发生. 潭渊的生物在睡觉. ";
                        case "HydraChowTrue":
                        return "九头蛇想吃那个食物. ";
                        case "HydraChowFalse":
                        return "什么也没发生. 一个拿着又臭又黏糊的球的你宛如一个智障. ";
                        case "InterestingClawTrue":
                        return "混沌双爪已经到来!";
                        case "InterestingClawFalse":
                        return "爪子在你手中无力的伸开. 恶心的要死. ";
                        case "IntimidatingMushroomFalse":
                        return "蘑菇和你大眼瞪小眼, 让你浑身发冷. ";
                        case "IntimidatingMushroomTrue":
                        return "赤孢皇正准备踩扁你";
                        case "LifescannerFalse":
                        return "生命扫描仪没起作用. ";
                        case "ScrapHeapFalse":
                        return "你用这个东西会有静电刺激感. 大概它在发信号? ";
                        case "ScrapHeapTrue":
                        return "双头狗要把这东西和你都吃了";
                        case "SubzeroCrystalTimeFalse":
                        return "水晶呆在那, 正在阳光下融化";
                        case "SubzeroCrystalSnowZoneFalse":
                        return "水晶在它里面显示出一幅附近冰地生物群的图像";
                        case "SubzeroCrystalTrue":
                        return "绝零冰蛇继续攻击";
                        case "ToadstoolFalse":
                        return "蟾蜍臭菇呱呱叫...? ";
                        case "ToadstoolTrue":
                        return "松露蟾蜍呱呱叫";
                        case "ZeroFalse":
                        return "错 误。 零 单 元 已 经 激 活. 请 稍 后 重 新 尝 试. ";
                        case "ZeroVoidZoneFalse":
                        return "错 误。 读 取 代 码PLAYER. GETM0DPLAYER<AAPLAYER>(M0D). Z0NEV0ID == FALSE. 请 稍 后 重 新 尝 试. ";
                        case "ZeroRuneTrue":
                        return "末 日 协 议 手 动 激 活. 终 结 系 统 全 功 率 充 能. ";
                        case "ZeroTesseractTrue":
                        return "零 单 元 启 动. 加 载 毁 灭 者 协 议. ";
                        case "ZeroTesseractDownedTrue":
                        return "目 标 锁 定. 本 次 终 结 你 失 败 的 几 率 为 零, 泰 拉 人. ";
                        case "RajahGlobalInfo1":
                        return "杀害无辜者必须受到惩罚!";
                        case "RajahGlobalInfo2":
                        return "你犯了不可饶恕的罪!我要把你从这个凡人的国度里抹去!准备好真正的痛苦和惩罚吧, ";
                        case "RajahGlobalInfo3":
                        return "一个愤怒的生物的眼睛注视着你…";
                        case "RoyalRabbit1":
                        return "杀害无辜者必须受到惩罚!";
                        case "RoyalRabbit2":
                        return "你要为你的罪过付出代价, ";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                   switch(BossName)
                    {
                        case "BossAwoken":
                        return " пробудился!";
                        case "GripsAwoken":
                        return "Звук того, что кто-то скребется по земле, слышен рядом...";
                        case "EquinoxWormawoken":
                        return "Черви Равнодествия пробудилсь!";
                        case "ChaosRuneYamataFalse":
                        return "Картина полыхающего демона появляется на секунду у вас в голове...";
                        case "ChaosRuneAkumaFalse":
                        return "Картина 7-голового террора появляется на секунду у вас в голове...";
                        case "ChaosRuneFalse":
                        return "ХАХ! Я хотел бы, что бы здесь было 2 меня, которые тебя закопают!";
                        case "ChaosRuneAncientsFalse":
                        return "Руна ничего не делает...";
                        case "ChaosRuneTrue1":
                        return "Шен Дорагон пробудился";
                        case "ChaosRuneTrue2":
                        return "Сразу к веселой части, а? Ты мне нравишься, дитя.";
                        case "ChaosSigilFalse1":
                        return "Только синяя часть печати загорается...";
                        case "ChaosSigilFalse2":
                        return "Только красная часть печати загорается...";
                        case "ChaosSigilFalse3":
                        return "ХАХ! Я хотел бы, что бы здесь было 2 меня, которые тебя закопают!";
                        case "ChaosSigilFalse4":
                        return "Печать хаоса загорается и картина алтарей хаоса появляется на секунду у вас в голове";
                        case "ChaosSigilFalse5":
                        return "Печать ничего не делает...";
                        case "ChaosSigilTrue1":
                        return "Большая ошибка, дитя...";
                        case "ChaosSigilTrue2":
                        return "Хмпф... Снова..? Ладно, давай просто покончим с этим.";
                        case "ConfusingMushroomFalse":
                        return "Грибы светятся, и их запах заставляет вас чувствовать себя чокнутым.";
                        case "ConfusingMushroomTrue":
                        return "Феодальный Гриб продолжает атаковать вас.";
                        case "CuriousClawTrue":
                        return "Руки уже здесь!!";
                        case "CuriousClawFalse":
                        return "Коготь прихрамывает у вас в руке. Мерзко.";
                        case "CyberneticBellFalse":
                        return "Колокол звонит, но ничего не происходит.";
                        case "CyberneticBellTrue":
                        return "Рейдер слышит звон, но продолжает пытаться убить вас.";
                        case "CyberneticClawFalse":
                        return "Коготь просто хромает у вас в руке";
                        case "CyberneticClawTrue":
                        return "Ретривер все еще пытается схватить вас.";
                        case "CyberneticShroomFalse":
                        return "Хватит махать металлическим грибом как психопат.";
                        case "CyberneticShroomTrue":
                        return "Техно-Трюфель уже существует.";
                        case "DiamondCarrotRajahText":
                        return "СМЕРТЕЛЬНАЯ ОШИБКА, ТЕРРАРИАН!";
                        case "DiamondCarrotRajahText2":
                        return "Покажи, что можешь, ";
                        case "DjinnLampDayTimeFalse":
                        return "Лампа мерцает в лунном свете, но ничего не происходит";
                        case "DjinnLampDesertFalse":
                        return "Лампа льет песок наземь, когда вы ее трете.";
                        case "DjinnLampTrue":
                        return "Никакое количество трений лампы вам не поможет.";
                        case "DraconianDayTimeFalse":
                        return "Боже, сопляк. Может дракон поспать немного? Возвращайся утром.";
                        case "DraconianRuneFalse1":
                        return "Картина сгоревшей башни на вершине Инферно появляется на секунду у вас в голове";
                        case "DraconianRuneFalse2":
                        return "Эй, сопляк, эта руна работает только единожды, знаешь ли.";
                        case "DraconianRuneInfernoFalse":
                        return "Ты можешь использовать эту руну только в Инферно, сопляк.";
                        case "DraconianRuneInfernoFalse2":
                        return "Эта печать должна быть использована на алтаре солнца, сопляк. Она в центре Инферно.";
                        case "DraconianRuneTrue1":
                        return "Водух вокруг вас нагревается...";
                        case "DraconianRuneTrue2":
                        return "Сразу с места в карьер, как я вижу..? Ну ладно, приготовься к аду..!";
                        case "DraconianSigilFalse":
                        return "Эй, сопляк, эта печать работает только единожды, знаешь ли.";
                        case "DraconianSigilInfernoFalse":
                        return "Ты можешь использовать эту печать только в Инферно, сопляк.";
                        case "DraconianSignalTrue1":
                        return "Хех, я надеюсь ты готов сразиться лицом к лицу с яростью пылающего солнца";
                        case "DraconianSignalTrue2":
                        return "Вернулся за еще одной порцией, сопляк? Тебе заняться нечем? Ты уже победил меня однажды.  Ладно, но учти, я не буду с тобой мягок.";
                        case "DragonBellDayTimeFalse":
                        return "Звон колокола не доходит до нужных ушей. Драконы сейчас спят.";
                        case "DragonBellTrue":
                        return "Мать Стаи уже здесь.";
                        case "DragonBellFalse":
                        return "Звон колокола не доходит до нужных ушей. Драконы сейчас спят.";
                        case "DreadRuneTrue1":
                        return "Ямата пробудился!";
                        case "DreadRuneTrue2":
                        return "Да-да, я знаю, что моя первая фаза - говно. Давай просто покончим с этим..!";
                        case "DreadTimeFalse":
                        return "НЕТ! Я НЕ ХОЧУ СЕЙЧАС ДРАТЬСЯ С ТОБОЙ! МНЕ НУЖЕН МОЙ ПРЕКРАСНЫЙ СОН! ВОЗВРАЩАЙСЯ НОЧЬЮ!";
                        case "DreadFalse1":
                        return "Картина странного дерева в сердце Трясины появляется на секунду у вас в голове";
                        case "DreadFalse2":
                        return "ЧТО ТЫ ТВОРИШЬ?! Я УЖЕ ЗДЕСЬ!!!";
                        case "DreadMireFalse":
                        return "Эй, тупица! Трясина вообще-то там!";
                        case "DreadMireFalse2":
                        return "Тебе стоит попробовать использовать эту печать в центре Трясины! Ничего плохого не произойдет, отвечаю!";
                        case "DreadSigilTrue1":
                        return "Ты ПОСМЕЛ зайти на мою территорию, террариан?! НЬЕХЕХЕХЕХЕ..! Большая ошибка..!";
                        case "DreadSigilTrue2":
                        return "Вернулся за добавкой..?! В этот раз тебе не повезет, маленький засранец..!";
                        case "SistersDownedInfo1":
                        return "Снова..!? Разве прошлый раз не научил тебя? Ну ладно, я сейчас создам шар, который превратит тебя в пыль!!";
                        case "SistersDownedInfo2":
                        return "В любом случае, давай просто покончим с этим.";
                        case "SistersInfo1":
                        return "*Вздох*... Поехали снова...";
                        case "SistersInfo2":
                        return "В ЭТОТ РАЗ Я НЕ ПРОЙГРАЮ! Ты почувствуешь вкус поражения, отвратительный теплокровный!";
                        case "HydraChowTimeFalse":
                        return "Ничего не пройсходит. Существа Трясины спят.";
                        case "HydraChowTrue":
                        return "Гидра хочет эту еду.";
                        case "HydraChowFalse":
                        return "Ничего не пройсходит. Вы выглядите глупо, держа в воздухе этот вонючий шар.";
                        case "InterestingClawTrue":
                        return "Тиски Хаоса уже здесь!";
                        case "InterestingClawFalse":
                        return "Коготь прихрамывает у вас в руке. Мерзко.";
                        case "IntimidatingMushroomFalse":
                        return "Гриб просто пугающе смотрит на вас.";
                        case "IntimidatingMushroomTrue":
                        return "Грибной Монарх продолжает пытаться растоптать вас.";
                        case "LifescannerFalse":
                        return "Сканер Жизни ничего не делает.";
                        case "ScrapHeapFalse":
                        return "Вы чувстуете статический шок, когда используете это. Может быть оно пытается отослать сигнал?";
                        case "ScrapHeapTrue":
                        return "Орф-X хочет съесть это И вас.";
                        case "SubzeroCrystalTimeFalse":
                        return "Кристалл просто тает на солнце.";
                        case "SubzeroCrystalSnowZoneFalse":
                        return "Кристалл показывает картину ближайшей тундры.";
                        case "SubzeroCrystalTrue":
                        return "Тундровая Змея продолжает атаковать";
                        case "ToadstoolFalse":
                        return "Поганка квакает..?";
                        case "ToadstoolTrue":
                        return "Трюфельная Жаба квакает";
                        case "ZeroFalse":
                        return "0ШИБКА. БОЕВАЯ ЕДИНИЦА ЗЕР0 УЖЕ АКТИВНА. ПОЖАЛУЙСТА, ПОПРОБУЙТЕ СНОВА ПОЗЖЕ.";
                        case "ZeroVoidZoneFalse":
                        return "0ШИБКА. PLAYER.GETM0DPLAYER<AAPLAYER>(M0D).Z0NEV0ID == FALSE. ПОЖАЛУЙСТА, ПОПРОБУЙТЕ ПОЗЖЕ.";
                        case "ZeroRuneTrue":
                        return "ПР0Т0К0Л СУДН0Г0 ДНЯ АКТИВИРОВАН. СИСТЕМЫ УНИЧТ0ЖЕНИЯ РАБОТАЮТ В П0ЛНУЮ СИЛУ.";
                        case "ZeroTesseractTrue":
                        return "БОЕВАЯ ЕДИНИЦА ЗЕР0 АКТИВИР0ВАНА. ЗАПУСКАЮ.";
                        case "ZeroTesseractDownedTrue":
                        return "ЦЕЛЬ ЗАХВАЧЕНА. ПРОВАЛ В ВЫПОЛНЕНИИ МИСИИ В ЭТОТ РАЗ НЕВОЗМОЖЕН.";
                        case "RajahGlobalInfo1":
                        return "Те кто убивают невинных должны быть НАКАЗАНЫ!";
                        case "RajahGlobalInfo2":
                        return "ТЫ СОВЕРШИЛ НЕПРОСТИТЕЛЬНЫЙ ГРЕХ! Я ИЗБАВЛЮ ЭТОТ СМЕРТНЫЙ МИР ОТ ТЕБЯ! ПРИГОТОВЬСЯ К БОЛИ И НАКАЗАНИЮ, ";
                        case "RajahGlobalInfo3":
                        return "Глаз разгневанного существа смотрит на вас...";
                        case "RoyalRabbit1":
                        return "Те кто убивают невинных должны быть НАКАЗАНЫ";
                        case "RoyalRabbit2":
                        return "ТЫ ЗАПЛАТИШЬ ЗА СВОИ ГРЕХИ, ";
                    }
                }
            else
                {
                    switch(BossName)
                    {
                        case "BossAwoken":
                        return " have awoken!";
                        case "GripsAwoken":
                        return "The sound of scraping against the ground can be heard nearby...";
                        case "EquinoxWormawoken":
                        return "The Equinox Worms have awoken!";
                        case "ChaosRuneYamataFalse":
                        return "The imagery of a blazing demon flashes through your mind...";
                        case "ChaosRuneAkumaFalse":
                        return "The imagery of a 7 headed terror flashes through your mind...";
                        case "ChaosRuneFalse":
                        return "HAH! I WISH there were two of me to smash you into the ground!";
                        case "ChaosRuneAncientsFalse":
                        return "The Rune does nothing...";
                        case "ChaosRuneTrue1":
                        return "Shen Doragon has been Awakened!";
                        case "ChaosRuneTrue2":
                        return "Skipping to the fun part, I see? I like you, child.";
                        case "ChaosSigilFalse1":
                        return "Only the blue half of the sigil is lit up...";
                        case "ChaosSigilFalse2":
                        return "Only the red half of the sigil is lit up...";
                        case "ChaosSigilFalse3":
                        return "HAH! I WISH there were two of me to smash you into the ground!";
                        case "ChaosSigilFalse4":
                        return "The Chaos Sigil glows, and imagery of the chaos pedestals flash through your mind";
                        case "ChaosSigilFalse5":
                        return "The sigil does nothing...";
                        case "ChaosSigilTrue1":
                        return "Big mistake, child...";
                        case "ChaosSigilTrue2":
                        return "Hmpf...again..? Alright, let's just get this done and overwith.";
                        case "ConfusingMushroomFalse":
                        return "The mushroom glows, and the smell of it makes you feel loopy.";
                        case "ConfusingMushroomTrue":
                        return "The Feudal Fungus keeps trying to attack you";
                        case "CuriousClawTrue":
                        return "The Grips of Chaos are already here!";
                        case "CuriousClawFalse":
                        return "The claw lays limp in your hand. Nasty.";
                        case "CyberneticBellFalse":
                        return "The bell rings, but nothing happens.";
                        case "CyberneticBellTrue":
                        return "The Raider hears the bell and keeps attempting to kill you";
                        case "CyberneticClawFalse":
                        return "The claw just lays limp in your hand.";
                        case "CyberneticClawTrue":
                        return "The Retriever is still trying to grab you";
                        case "CyberneticShroomFalse":
                        return "Stop waving that metal mushroom around like a psychopath.";
                        case "CyberneticShroomTrue":
                        return "The Techno Truffle exists.";
                        case "DiamondCarrotRajahText":
                        return "GRAVE MISTAKE, TERRARIAN!";
                        case "DiamondCarrotRajahText2":
                        return "Show me what you got, ";
                        case "DjinnLampDayTimeFalse":
                        return "The lamp shimmers in the moonlight, yet does nothing";
                        case "DjinnLampDesertFalse":
                        return "The lamp spits out sand as you rub it";
                        case "DjinnLampTrue":
                        return "No ammount of rubbing the lamp will save you here";
                        case "DraconianDayTimeFalse":
                        return "Geez, kid. Can't a dragon get a little shut-eye? Come back in the morning.";
                        case "DraconianRuneFalse1":
                        return "An image of the scorched tower at the peak of the inferno flashes through your mind";
                        case "DraconianRuneFalse2":
                        return "Hey kid, that rune only works once, ya know.";
                        case "DraconianRuneInfernoFalse":
                        return "You can only use that rune in the Inferno, kid.";
                        case "DraconianRuneInfernoFalse2":
                        return "That sigil has to be used at the Altar of the Draconian Sun, kid. It's in the middle of the inferno.";
                        case "DraconianRuneTrue1":
                        return "The air around you begins to heat up...";
                        case "DraconianRuneTrue2":
                        return "Cutting right to the chase I see..? Alright then, prepare for hell..!";
                        case "DraconianSigilFalse":
                        return "Hey kid, that Sigil only works once, ya know.";
                        case "DraconianSigilInfernoFalse":
                        return "You can only use that Sigil in the Inferno, kid.";
                        case "DraconianSignalTrue1":
                        return "Heh, I hope you’re ready to feel the fury of the blazing sun kid.";
                        case "DraconianSignalTrue2":
                        return "Back for more, kid? Don’t you have better things to do? You already beat me once.  Alright, but I won’t go easy on you.";
                        case "DragonBellDayTimeFalse":
                        return "The bell rings on deaf ears. The dragons are asleep now.";
                        case "DragonBellTrue":
                        return "The Broodmother has already been called";
                        case "DragonBellFalse":
                        return "The bell rings on deaf ears. The dragons are not here.";
                        case "DreadRuneTrue1":
                        return "Yamata has been Awakened!";
                        case "DreadRuneTrue2":
                        return "Yeah, yeah I get it, my first phase is obnoxious. Let's just get this over with..!";
                        case "DreadTimeFalse":
                        return "NO! I DON'T WANNA FIGHT NOW! I NEED MY BEAUTY SLEEP! COME BACK AT NIGHT!";
                        case "DreadFalse1":
                        return "An image of the strange tree at the heart of the mire flashes through your mind";
                        case "DreadFalse2":
                        return "WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!";
                        case "DreadMireFalse":
                        return "Hey Dumbo! Mire is that way!";
                        case "DreadMireFalse2":
                        return "You NEED to use that sigil on the altar at the center of the mire! Trust me, nothing bad will happen!";
                        case "DreadSigilTrue1":
                        return "You DARE enter my territory, Terrarian?! NYEHEHEHEHEH..! Big mistake..!";
                        case "DreadSigilTrue2":
                        return "Back for more..?! This time you won't be so lucky you little whelp..!";
                        case "SistersDownedInfo1":
                        return "Again..!? Didin't you learn from last time? Oh well, I'm gonna have a ball blasting you to shreds!";
                        case "SistersDownedInfo2":
                        return "Whatever, let's just get this over with.";
                        case "SistersInfo1":
                        return "Sigh... Here we go again...";
                        case "SistersInfo2":
                        return "THIS TIME I'M NOT LOSING! You're gonna feel the taste of defeat you disgusting warm-blood!";
                        case "HydraChowTimeFalse":
                        return "Nothing is coming. The creatures of the Mire sleep.";
                        case "HydraChowTrue":
                        return "The Hydra wants that food.";
                        case "HydraChowFalse":
                        return "Nothing is coming. Now you look dumb holding out this smelly ball of gunk.";
                        case "InterestingClawTrue":
                        return "The Grips of Chaos are already here!";
                        case "InterestingClawFalse":
                        return "The claw lays limp in your hand. Nasty.";
                        case "IntimidatingMushroomFalse":
                        return "The mushroom just glares at you and gives you chills just looking at it.";
                        case "IntimidatingMushroomTrue":
                        return "The mushroom Monarch keeps trying to stomp you";
                        case "LifescannerFalse":
                        return "The Lifescanner doesn't do anything.";
                        case "ScrapHeapFalse":
                        return "You feel a static shock from using this. Maybe it's trying to send a signal?";
                        case "ScrapHeapTrue":
                        return "Orthrus wants to eat that AND you";
                        case "SubzeroCrystalTimeFalse":
                        return "The crystal just sits there, melting in the sun";
                        case "SubzeroCrystalSnowZoneFalse":
                        return "The crystal shows an image of the nearby ice biome inside of it";
                        case "SubzeroCrystalTrue":
                        return "The Subzero Serpent continues to attack";
                        case "ToadstoolFalse":
                        return "The toadstool croaks..?";
                        case "ToadstoolTrue":
                        return "The Truffle Toad croaks";
                        case "ZeroFalse":
                        return "ERR0R. ZER0 UNIT ALREADY ACTIVE. PLEASE TRY AGAIN LATER.";
                        case "ZeroVoidZoneFalse":
                        return "ERR0R. PLAYER.GETM0DPLAYER<AAPLAYER>(M0D).Z0NEV0ID == FALSE. PLEASE TRY AGAIN LATER.";
                        case "ZeroRuneTrue":
                        return "D00MSDAY PR0T0CALL ACTIVATED MANUALLY. TERMINATI0N SYSTEMS AT FULL P0WER";
                        case "ZeroTesseractTrue":
                        return "ZER0 UNIT ACTIVATED. ENGAGE D00MBRINGER PR0T0C0L.";
                        case "ZeroTesseractDownedTrue":
                        return "TARGET L0CKED. FAILURE T0 TERMINATE Y0U IS N0T A P0SSIBILITY THIS TIME, TERRARIAN.";
                        case "RajahGlobalInfo1":
                        return "Those who slaughter the innocent must be PUNISHED!";
                        case "RajahGlobalInfo2":
                        return "YOU HAVE COMMITTED AN UNFORGIVABLE SIN! I SHALL WIPE YOU FROM THIS MORTAL REALM! PREPARE FOR TRUE PAIN AND PUNISHMENT, ";
                        case "RajahGlobalInfo3":
                        return "The eyes of a wrathful creature gaze upon you...";
                        case "RoyalRabbit1":
                        return "Those who slaughter the innocent must be PUNISHED!";
                        case "RoyalRabbit2":
                        return "YOU WILL PAY FOR YOUR SINS, ";
                    }
                }
            return"";
        }
        public static string CensusMod(string npc)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(npc)
                    {
                        case "Anubis":
                        return "总是可以";
                        case "Mushman":
                        return "打败赤孢皇或者真菌帝后在红蘑菇地中建造一个房子";
                        case "Lovecraftian":
                        return "打败克苏鲁之眼";
                        case "Samurai":
                        return "打败混沌之爪";
                        case "GoblinSlayer":
                        return "打败哥布林军团";
                    }
                }
            else if (Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
            {
                switch (npc)
                    {
                        case "Anubis":
                        return "Всегда доступен";
                        case "Mushman":
                        return "После победы над Грибным Монархом или Феодальным Грибом постройте дом в красном грибном биоме.";
                        case "Lovecraftian":
                        return "Победите Глаз Ктулху";
                        case "Samurai":
                        return "Победите Тиски Хаоса";
                        case "GoblinSlayer":
                        return "Победите армию гоблинов";
                    }
                }
            else
                {
                    switch(npc)
                    {
                        case "Anubis":
                        return "Always available";
                        case "Mushman":
                        return "After defeating Mushroom Monarch or Feudal Fungus, build a house in a red mushroom biome";
                        case "Lovecraftian":
                        return "Eye of Cthulhu defeated";
                        case "Samurai":
                        return "Grips of Chaos defeated";
                        case "GoblinSlayer":
                        return "Goblin Army is defeated";
                    }
                }
            return "";
        }
        public static string dogshit(string Boss)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(Boss)
                    {
                        case "Usean":
                        return "使用 ";
                        case "Usea":
                        return "使用 ";
                        case "Usethe":
                        return "使用 ";
                        case "or":
                        return " 或者 ";
                        case "atnight":
                        return " 在晚上召唤";
                        case "MushroomMonarch":
                        return "赤孢皇";
                        case "MushroomMonarchInfo":
                        return " 在白天召唤";
                        case "MushroomMonarchInfo2":
                        return "赤孢皇逃向天空";
                        case "FeudalFungus":
                        return "真菌帝";
                        case "FeudalFungusInfo":
                        return " 在荧光蘑菇地召唤";
                        case "FeudalFungusInfo2":
                        return "真菌帝消失了";
                        case "GripsofChaos":
                        return "混沌双爪";
                        case "GripsofChaosInfo":
                        return "和你打了一架之后，爪子飞走了";
                        case "Broodmother":
                        return "育母炎龙";
                        case "BroodmotherInfo":
                        return " 白天期间在燎狱召唤, 或者破坏火山下3个龙蛋";
                        case "BroodmotherInfo2":
                        return "育母炎龙回到了火热的深处";
                        case "Hydra":
                        return "九头渊蛇";
                        case "HydraInfo":
                        return " 夜晚在潭渊召唤, 或者破坏潭渊下3个蒴荚";
                        case "HydraInfo2":
                        return "九头渊蛇偷偷溜回了她的巢穴";
                        case "SubzeroSerpent":
                        return "绝零冰蛇";
                        case "SubzeroSerpentInfo":
                        return " 在夜晚雪地召唤";
                        case "SubzeroSerpentInfo2":
                        return "绝零冰蛇在雪中钻洞逃走了";
                        case "DesertDjinn":
                        return "沙漠巨灵";
                        case "DesertDjinnInfo":
                        return " 白天在沙漠召唤";
                        case "DesertDjinnInfo2":
                        return "沙漠巨灵高兴地向你弯着手臂（表示自己是个兄贵）";
                        case "Sagittarius":
                        return "虚空人马";
                        case "SagittariusInfo":
                        return " 在虚空浮岛召唤";
                        case "SagittariusInfo2":
                        return "虚空人马走向无尽地黑暗";
                        case "TruffleToad":
                        return "松露蟾蜍";
                        case "TruffleToadInfo":
                        return " 在荧光蘑菇环境召唤";
                        case "TruffleToadInfo2":
                        return "松露蟾蜍呱呱叫着然后消失了";
                        case "Greed":
                        return "金食饕餮";
                        case "GreedInfo":
                        return " 在欲望金窟的原欲祭坛\n处召唤. 或者打开3个欲望金窟的贪欲宝箱";
                        case "Athena":
                        return "穹武鸮姬";
                        case "AthenaInfo":
                        return " 在天穹卫城的天鸮祭坛\n上召唤.";
                        case "Anubis":
                        return "阿努比斯 史诗记述者";
                        case "AnubisInfo":
                        return " 在沙漠中召唤";
                        case "AthenaA":
                        return "复仇的鸮姬";
                        case "AthenaAInfo":
                        return "月球领主后击败穹武鸮姬";
                        case "GreedA":
                        return "鎏金万蟲王";
                        case "GreedAInfo":
                        return "月球领主后击败金食饕餮";
                        case "AnubisA":
                        return "逝落断罪师";
                        case "AnubisAInfo":
                        return "月球领主后击败阿努比斯";
                        case "Retriever":
                        return "捕猎者-电子猎犬爪";
                        case "TechnoTruffle":
                        return "电子化-机械松露怪";
                        case "RaiderUltima":
                        return "侵入者-创世哺育之母";
                        case "OrthrusX":
                        return "食腐者-双头狗俄耳托斯X型";
                        case "RajahRabbit":
                        return "巨兔王公";
                        case "RajahRabbitInfo":
                        return " 或者像一个无耻混蛋 一样杀100只兔子";
                        case "NightcrawlerDaybringer":
                        return "昼夜双虫";
                        case "SistersofDiscord":
                        return "(远古者)混沌姐妹";
                        case "Yamata":
                        return "(远古者)八歧大蛇";
                        case "YamataInfo":
                        return " 在夜晚潭渊召唤";
                        case "YamataA":
                        return "(远古者)觉醒之八歧大蛇";
                        case "Akuma":
                        return "(远古者)邪鬼巨龙";
                        case "AkumaInfo":
                        return " 在白天燎狱召唤";
                        case "AkumaA":
                        return "(远古者)觉醒之邪鬼巨龙";
                        case "Zero":
                        return "(远古者)零械单元";
                        case "ZeroInfo":
                        return " 在浮岛虚空召唤";
                        case "ZeroP":
                        return "(远古者)零之协议";
                        case "ShenDoragon":
                        return "(原古者)上神应龙";
                        case "ShenDoragonA":
                        return "(原古者)觉醒之上神应龙";
                        case "RajahRabbitRevenge":
                        return "(原古者)至尊巨兔王公";
                        case "RajahRabbitRevengeInfo":
                        return " 或者总计杀死1000只 兔子后, 每杀死100只兔子";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(Boss)
                    {
                        case "Usean":
                        return "Используйте ";
                        case "Usea":
                        return "Используйте ";
                        case "Usethe":
                        return "Используйте ";
                        case "or":
                        return " или ";
                        case "atnight":
                        return " ночью";
                        case "MushroomMonarch":
                        return "Грибной Монарх";
                        case "MushroomMonarchInfo":
                        return " днем.";
                        case "MushroomMonarchInfo2":
                        return "Грибной Монарх улетает в небеса";
                        case "FeudalFungus":
                        return "Феодальный Гриб";
                        case "FeudalFungusInfo":
                        return " в святящемся грибном биоме";
                        case "FeudalFungusInfo2":
                        return "Феодальный Гриб исчезает";
                        case "GripsofChaos":
                        return "Тиски Хаоса";
                        case "GripsofChaosInfo":
                        return "Тиски разлетаются, разобравшись с вами";
                        case "Broodmother":
                        return "Мать Стаи";
                        case "BroodmotherInfo":
                        return " в Инферно днем";
                        case "BroodmotherInfo2":
                        return "Мать Стаи возвращается в огненные глубины";
                        case "Hydra":
                        return "Гидра";
                        case "HydraInfo":
                        return " в Трясине ночью";
                        case "HydraInfo2":
                        return "Гидра вернулась в ее темное логово";
                        case "SubzeroSerpent":
                        return "Тундровая Змея";
                        case "SubzeroSerpentInfo":
                        return " в биоме Тундры ночью";
                        case "SubzeroSerpentInfo2":
                        return "Тудровая Змея закапывается в снег";
                        case "DesertDjinn":
                        return "Пустынный Джинн";
                        case "DesertDjinnInfo":
                        return " в биоме Пустыни днем.";
                        case "DesertDjinnInfo2":
                        return "Пустынный Джинн триумфально флексит над вами";
                        case "Sagittarius":
                        return "Саггитариус";
                        case "SagittariusInfo":
                        return " в Пустоте";
                        case "SagittariusInfo2":
                        return "Стрелец скачет в бесконечную тьму...";
                        case "TruffleToad":
                        return "Трюфельная Жаба";
                        case "TruffleToadInfo":
                        return " в святящемся грибном биоме";
                        case "TruffleToadInfo2":
                        return "Трюфельная Жаба квакает, затем исчезает";
                        case "Greed":
                        return "Жадность";
                        case "GreedInfo":
                        return " на Алтаре Желаний в Сокровищнице, или откройте 3 Сундука Жадности в Сокровищнице.";
                        case "Athena":
                        return "Афина";
                        case "AthenaInfo":
                        return " на Алтаре Совы в Акрополе.";
                        case "Anubis":
                        return "Анубис Летописец";
                        case "AnubisInfo":
                        return " в биоме Пустыни";
                        case "AthenaA":
                        return "Реванш Афины";
                        case "AthenaAInfo":
                        return "победите Афину в пост-Мунлорде";
                        case "GreedA":
                        return "Король червей Жадность";
                        case "GreedAInfo":
                        return "победите Жадность в пост-Мунлорде";
                        case "AnubisA":
                        return "Забытый Анубис";
                        case "AnubisAInfo":
                        return "победите Анубиса в пост-Мунлорде";
                        case "Retriever":
                        return "Ретривер";
                        case "TechnoTruffle":
                        return "Техно-Трюфель";
                        case "RaiderUltima":
                        return "Рейдер Ультима";
                        case "OrthrusX":
                        return "ОрФ Х";
                        case "RajahRabbit":
                        return "Кролик Раджа";
                        case "RajahRabbitInfo":
                        return " или убейте 100 кроликов как подонок";
                        case "NightcrawlerDaybringer":
                        return "Ночной Ползун & Приносящий свет";
                        case "SistersofDiscord":
                        return "Сестры Раздора";
                        case "Yamata":
                        return "Ямата";
                        case "YamataInfo":
                        return " в Трясине ночью";
                        case "YamataA":
                        return "Ямата Но Орочи";
                        case "Akuma":
                        return "Akuma";
                        case "AkumaInfo":
                        return " в Инферно днем";
                        case "AkumaA":
                        return "Они Акума";
                        case "Zero":
                        return "Зер0";
                        case "ZeroInfo":
                        return " в Пустоте";
                        case "ZeroP":
                        return "Зир0 Протокол";
                        case "ShenDoragon":
                        return "Шен Дорагон";
                        case "ShenDoragonA":
                        return "Шен Дорагон пробужденный";
                        case "RajahRabbitRevenge":
                        return "Месть Кролика Раджи";
                        case "RajahRabbitRevengeInfo":
                        return " или каждые 100 кроликов после 1000.";
                    }
                }
            else
                {
                    switch(Boss)
                    {
                        case "Usean":
                        return "Use an ";
                        case "Usea":
                        return "Use a ";
                        case "Usethe":
                        return "Use the ";
                        case "or":
                        return " or ";
                        case "atnight":
                        return " at night";
                        case "MushroomMonarch":
                        return "The Mushroom Monarch";
                        case "MushroomMonarchInfo":
                        return " during the day";
                        case "MushroomMonarchInfo2":
                        return "The Mushroom Monarch escapes into the sky";
                        case "FeudalFungus":
                        return "The Feudal Fungus";
                        case "FeudalFungusInfo":
                        return " in a Glowing Mushroom Biome";
                        case "FeudalFungusInfo2":
                        return "The Feudal Fungus vanishes";
                        case "GripsofChaos":
                        return "Grips of Chaos";
                        case "GripsofChaosInfo":
                        return "";
                        case "Broodmother":
                        return "The Broodmother";
                        case "BroodmotherInfo":
                        return " in the Inferno during the day";
                        case "BroodmotherInfo2":
                        return "";
                        case "Hydra":
                        return "Hydra";
                        case "HydraInfo":
                        return " in the Mire at night";
                        case "HydraInfo2":
                        return "";
                        case "SubzeroSerpent":
                        return "Subzero Serpent";
                        case "SubzeroSerpentInfo":
                        return " in the Snow biome at night";
                        case "SubzeroSerpentInfo2":
                        return "";
                        case "DesertDjinn":
                        return "Desert Djinn";
                        case "DesertDjinnInfo":
                        return " in the Desert during the day";
                        case "DesertDjinnInfo2":
                        return "";
                        case "Sagittarius":
                        return "Sagittarius";
                        case "SagittariusInfo":
                        return " in the Void";
                        case "SagittariusInfo2":
                        return "";
                        case "TruffleToad":
                        return "Truffle Toad";
                        case "TruffleToadInfo":
                        return " in a glowing mushroom biome";
                        case "TruffleToadInfo2":
                        return "";
                        case "Greed":
                        return "Greed";
                        case "GreedInfo":
                        return " at the Altar of Desire in the Hoard, or open 3 Greed Chests in Hoard.";
                        case "Athena":
                        return "Athena";
                        case "AthenaInfo":
                        return " at the owl altar in the Acropolis.";
                        case "Anubis":
                        return "Anubis Legendscribe";
                        case "AnubisInfo":
                        return " in the desert";
                        case "AthenaA":
                        return "Olympian Athena";
                        case "AthenaAInfo":
                        return "Defeat Athena post-Moon Lord";
                        case "GreedA":
                        return "Worm King Greed";
                        case "GreedAInfo":
                        return "Defeat Greed post-Moon Lord";
                        case "AnubisA":
                        return "Forsaken Anubis";
                        case "AnubisAInfo":
                        return "Defeat Anubis post-Moon Lord";
                        case "Retriever":
                        return "Retriever";
                        case "TechnoTruffle":
                        return "Techno Truffle";
                        case "RaiderUltima":
                        return "Raider Ultima";
                        case "OrthrusX":
                        return "Orthrus X";
                        case "RajahRabbit":
                        return "Rajah Rabbit";
                        case "RajahRabbitInfo":
                        return " or kill 100 Rabbits like a jerk.";
                        case "NightcrawlerDaybringer":
                        return "Nightcrawler & Daybringer";
                        case "SistersofDiscord":
                        return "Sisters of Discord";
                        case "Yamata":
                        return "Yamata";
                        case "YamataInfo":
                        return " in the Mire at night";
                        case "YamataA":
                        return "Yamata Awakened";
                        case "Akuma":
                        return "Akuma";
                        case "AkumaInfo":
                        return " in the Inferno during the day";
                        case "AkumaA":
                        return "Akuma Awakened";
                        case "Zero":
                        return "Zero";
                        case "ZeroInfo":
                        return " in the Void";
                        case "ZeroP":
                        return "Zero Protocol";
                        case "ShenDoragon":
                        return "Shen Doragon";
                        case "ShenDoragonA":
                        return "Shen Doragon Awakened";
                        case "RajahRabbitRevenge":
                        return "Rajah Rabbit's Revenge";
                        case "RajahRabbitRevengeInfo":
                        return " or every 100 rabbit kills after 1000.";
                    }
                }
            return"";
        }
        public static string RajahSPTooltip(string RajahSash)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(RajahSash)
                    {
                        case "Melee":
                        return "近战";
                        case "Ranged":
                        return "远程";
                        case "Magic":
                        return "魔法";
                        case "Summoning":
                        return "召唤";
                        case "Throwing":
                        return "投掷";
                        case "DamageType":
                        return "伤害类型";
                        case "CurrentDamageBoost:+":
                        return "当前伤害提升: +";
                        case "CurrentSpeedBoost:":
                        return "当前速度增加: ";
                        case "CurrentDamageResistance:":
                        return "当前伤害减免增加: ";
                        case "Damage":
                        return "伤害";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(RajahSash)
                    {
                        case "Melee":
                        return "ближний";
                        case "Ranged":
                        return "Дальний";
                        case "Magic":
                        return "Магический";
                        case "Summoning":
                        return "Призывной";
                        case "Throwing":
                        return "Метательный";
                        case "DamageType":
                        return "Тип урона";
                        case "CurrentDamageBoost:+":
                        return "Увеличение урона на данный момент: +";
                        case "CurrentSpeedBoost:":
                        return "Увеличение скорости на данный момент: +";
                        case "CurrentDamageResistance:":
                        return "Уменьшение урона на данный момент: ";
                        case "Damage":
                        return "Урон";
                    }
                }
            else
                {
                    switch(RajahSash)
                    {
                        case "Melee":
                        return "Melee";
                        case "Ranged":
                        return "Ranged";
                        case "Magic":
                        return "Magic";
                        case "Summoning":
                        return "Summoning";
                        case "Throwing":
                        return "Throwing";
                        case "DamageType":
                        return "Damage Type";
                        case "CurrentDamageBoost:+":
                        return "Current Damage Boost: +";
                        case "CurrentSpeedBoost:":
                        return "Current Speed Boost: ";
                        case "CurrentDamageResistance:":
                        return "Current Damage Resistance: ";
                        case "Damage":
                        return " Damage";
                    }
                }
            return"";
        }
        public static string GreedTooltip(string Greed)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(Greed)
                    {
                        case "CurrentDamageBoost:+":
                        return "当前伤害提升: +";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(Greed)
                    {
                        case "CurrentDamageBoost:+":
                        return "Увеличение урона на данный момент: +";
                    }
                }
            else
                {
                    switch(Greed)
                    {
                        case "CurrentDamageBoost:+":
                        return "Current Damage Boost: +";
                    }
                }
            return"";
        }
        public static string questFish(string questFishtext)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(questFishtext)
                    {
                        case "Fishmother":
                        return "好, 那么, 我正穿过燎狱寻找一些热的东西来搞我的传奇恶作剧, 恰巧我看到了这个水下大胖鱼把它周围的水都煮沸了. 我感觉 '完美!' 但是我没法抓住它因为我没带鱼竿. 去抓住它, 我的仆人.";
                        case "FishmotherLocation":
                        return "燎狱的任何位置";
                        case "TriHeadedKoi":
                        return "我讨厌潭渊. 在我睡觉时间以外什么也看不见, 但是我知道那有一条杀人鱼, 那东西有, 三 个 头. 你能去给我抓来吗?";
                        case "TriHeadedKoiLocation":
                        return "潭渊的任何位置";
                        case "GlitchFish":
                        return "我曾经一觉醒来看见我头上的恐怖岛屿里有条奇怪的鱼，大概在西边。你去给我抓过来。你说为什么空岛上会有水？自己想办法。";
                        case "GlitchFishLocation":
                        return "虚空的任何位置";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
                {
                    switch(questFishtext)
                    {
                        case "Fishmother":
                        return "Так вот, я ходил по Инферно в поисках чего-нибудь горячего для моего пранка. Тогда я увидел толстую рыбу, кипятящую воду вокруг себя. Я подумал 'ИДЕАЛЬНО', но не мог поймать ее, так как забыл удочку. Иди, достань ее, раб.";
                        case "FishmotherLocation":
                        return "Поймано в Инферно";
                        case "TriHeadedKoi":
                        return "Я ненавижу Трясину. Там ничего не видно, а даже когда видно, я уже сплю. Но я знаю, что там есть рыба-убийца, у которой ТРИ ГОЛОВЫ. Можешь достать ее для меня?";
                        case "TriHeadedKoiLocation":
                        return "Поймано в Трясине";
                        case "GlitchFish":
                        return "Я однажды проснулся и увидел странную рыбу в водах пугающего острова, парящего в небе над моей головой, вроде на Западе. Иди и достань ее для меня. Ты спрашиваешь, как на этих островах вообще может быть вода? Подумай сам.";
                        case "GlitchFishLocation":
                        return "Поймано в Пустоте";
                    }
                }
            else
                {
                    switch(questFishtext)
                    {
                        case "Fishmother":
                        return "Okay so, I was walking through the Inferno looking for something hot for one of my epic pranks, when I saw this fat fish underwater, boiling the water around it. I thought 'PERFECT!' but I couldn't catch it because I didn't have my rod on me. Go get it, slave.";
                        case "FishmotherLocation":
                        return "Caught anywhere in the Inferno";
                        case "TriHeadedKoi":
                        return "I hate the Mire. I can't see a thing in there unless it's past my bedtime, but I know there's a killer fish in there that I want that has, get this, THREE HEADS. Can you go get it for me?";
                        case "TriHeadedKoiLocation":
                        return "Caught anywhere in the Mire";
                        case "GlitchFish":
                        return "I once woke up and saw a strange fish in the water of the scary island over my head in the sky, probably in the West. You go, get it for me. You ask why there are waters in sky island? Find your own way.";
                        case "GlitchFishLocation":
                        return "Caught anywhere in the Void";
                    }
                }
            return"";
        }
        public static string TilesInfo(string Tiles)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
                {
                    switch(Tiles)
                    {
                        case "Spawnpointremoved":
                        return "出生点移除!";
                        case "Spawnpointset":
                        return "出生点设置!";

                        case "EnderMemoryInfo": 
                        return "用于纪念我们心中的一位早已逝去的朋友"; 

                        case "DracoAltar1":
                        return "臭小子. 龙不睡觉吗? 明天再来. ";
                        case "DracoAltar2":
                        return "嘿, 小子, 你应该知道, 那印迹只用一次. ";
                        case "DracoAltar3":
                        return "呵呵, 我希望你准备好感受烈日的愤火, 小子";
                        case "DracoAltar4":
                        return "又来, 小子? 你没有更好的事情要做吗? 你已经打败过我一次了. 好吧, 但我可不会对你心慈手软. ";
                        case "DreadAltar1":
                        return "不！我现在不想战斗！我需要舒服的睡眠！晚上来！";
                        case "DreadAltar2":
                        return "你究竟在tm干什么? 我已经在这了!!!";
                        case "DreadAltar3":
                        return "你 胆 敢 进入我的地盘, 泰拉人? !哦呵呵呵呵呵…!大错特错!";
                        case "DreadAltar4":
                        return "再来要更多战利品……? !这一次你可没那么幸运, 你这个小鬼……!";
                        case "DragonEgg1":
                        return "龙蛋破碎的声音在火山中回响… ";
                        case "DragonEgg2":
                        return "你听到远处的咆哮…";
                        case "DragonEgg3":
                        return "你听见一个疲倦, 安静的吼声, 以及...鼾声..?";
                        case "HydraPod1":
                        return "有东西从破碎的蒴荚中溢出. 真恶心. ";
                        case "HydraPod2":
                        return "你听到附近传来嘶嘶声";
                        case "HydraPod3":
                        return "你听见了嘶嘶声, 但是却消失在浓雾中...";
                    }
                }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString()   )
                {
                    switch(Tiles)
                    {
                        case "Spawnpointremoved":
                        return "Точка возрождения задан!!";
                        case "Spawnpointset":
                        return "Точка возраждения задана!";

                        case "EnderMemoryInfo": 
                        return "В память о нашем друге.";

                        case "DracoAltar1":
                        return "Боже, сопляк. Может дракон поспать немного? Возвращайся утром. ";
                        case "DracoAltar2":
                        return "Эй, сопляк, эта руна работает только единожды, знаешь ли. ";
                        case "DracoAltar3":
                        return "Хех, я надеюсь, что ты готов почувствовать ярость пылающего солнца, сопляк.";
                        case "DracoAltar4":
                        return "Вернулся за еще одной порцией, сопляк? Тебе заняться нечем? Ты уже победил меня однажды... Ладно, но учти, я не буду с тобой мягок ";
                        case "DreadAltar1":
                        return "НЕТ! Я НЕ ХОЧУ СЕЙЧАС ДРАТЬСЯ С ТОБОЙ! МНЕ НУЖЕН МОЙ ПРЕКРАСНЫЙ СОН! ВОЗВРАЩАЙСЯ НОЧЬЮ!";
                        case "DreadAltar2":
                        return "ЧТО ТЫ ТВОРИШЬ?! Я УЖЕ ЗДЕСЬ!!!";
                        case "DreadAltar3":
                        return "Ты ПОСМЕЛ зайти на мою территорию, террариан?! НЬЕХЕХЕХЕХЕ..! Большая ошибка..!";
                        case "DreadAltar4":
                        return "Вернулся за добавкой..?! В этот раз тебе не повезет, маленький засранец..!";
                        case "DragonEgg1":
                        return "Звук ломания яйца эхом проходит через вулкан...";
                        case "DragonEgg2":
                        return "Вы слышите рычание вдалеке...";
                        case "DragonEgg3":
                        return "Вы слышите уставший, тихий рык, а затем...храпение..?";
                        case "HydraPod1":
                        return "Все содержимое разливается из кокона. Мерзко.";
                        case "HydraPod2":
                        return "Вы слышите шипение вдалеке...";
                        case "HydraPod3":
                        return "Вы слышите шипение, но оно пропадает в тумане";
                    }
                }
            else
                {
                    switch(Tiles)
                    {
                        case "Spawnpointremoved":
                        return Language.GetTextValue("Game.SpawnPointRemoved");
                        case "Spawnpointset":
                        return Language.GetTextValue("Game.SpawnPointSet");


                        case "EnderMemoryInfo": 
                        return "In memory of a long lost friend still in our hearts."; 

                        case "DracoAltar1":
                        return "Geez, kid. Can't a dragon get a little shut-eye? Come back in the morning.";
                        case "DracoAltar2":
                        return "Hey kid, that Sigil only works once, ya know.";
                        case "DracoAltar3":
                        return "Heh, I hope you're ready to feel the fury of the blazing sun kid.";
                        case "DracoAltar4":
                        return "Back for more, kid? Don't you have better things to do? You already beat me once.  Alright, but I won't go easy on you.";
                        case "DreadAltar1":
                        return "NO! I DON'T WANNA FIGHT NOW! I NEED MY BEAUTY SLEEP! COME BACK AT NIGHT!";
                        case "DreadAltar2":
                        return "WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!";
                        case "DreadAltar3":
                        return "You DARE enter my territory, Terrarian?! NYEHEHEHEHEH..! Big mistake..!";
                        case "DreadAltar4":
                        return "Back for more..?! This time you won't be so lucky you little whelp..!";
                        case "DragonEgg1":
                        return "The sound of the egg breaking echoes through the volcano...";
                        case "DragonEgg2":
                        return "You hear a distant roar...";
                        case "DragonEgg3":
                        return "You hear a tired, quiet roar, and then...snoring..?";
                        case "HydraPod1":
                        return "The contents spill from the broken pod. Nasty.";
                        case "HydraPod2":
                        return "You hear hissing somewhere nearby";
                        case "HydraPod3":
                        return "You hear hissing, but it scuttles away into the fog...";

                    }
                }
            return"";
        }

        public static string EnemyChat(string Info)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
            {
                switch(Info)
                {
                    case "SeraphChat1":
                    return "啊哦，你一点也不好玩! 娘娘腔..!";
                    case "SeraphChat2":
                    return "哈哈哈哈! 你真像一条没脊梁骨的臭虫!";
                    case "SeraphChat3":
                    return "我现在很无聊.";
                    case "SeraphChat4":
                    return "来呀! 我很乐意把你打的满地找牙..!";
                    case "SeraphChat5":
                    return "哎哟哟，小地面爬行动物不高兴了. 要不一边哭去?";
                    case "SeraphKillChat1":
                    return "哦呀. 死了. 爽..!";
                    case "SeraphKillChat2":
                    return "哈! 我赢了!";
                    case "SeraphKillChat3":
                    return "爽到! 等你学会断奶了再来吧! 臭弟弟!";
                    case "SeraphKillChat4":
                    return "滚远点..!";
                    case "SeraphKillChat5":
                    return "我还没用全力. 小菜一碟!";
                    case "SeraphHurtChat1":
                    return "哦啊..! 真野蛮! 雅典娜娜娜娜娜娜娜!!!";
                    case "SeraphHurtChat2":
                    return "嘿! 我就是玩玩! 你给我等着看雅典娜听到了怎么说!";
                    case "SeraphHurtChat3":
                    return "现在你出tm大事了! 哦 雅典娜娜娜娜娜娜娜!!!!";
                    case "SeraphHurtChat4":
                    return "疼疼疼..! 哇哇哇哇 雅典娜娜娜娜娜 这条地面虫子欺负我!";
                    case "SeraphHurtChat5":
                    return "*喘*! 你弄脏了我的裙子..! 我要去叫雅典娜..!";

                    case "AthenaChat1":
                    return "...啊哈...那些小笨蛋--";
                    case "AthenaChat2":
                    return "我没时间管这个..!";
                }
            }
            else if (Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
            {
                switch (Info)
                {
                    case "SeraphChat1":
                    return "Аууу! Ты скучный! Плакса..!";
                    case "SeraphChat2":
                    return "Хахаха! Беги, бесхребетный землянин!";
                    case "SeraphChat3":
                    return "Скука.";
                    case "SeraphChat4":
                    return "Вернись! Было весели избивать тебя..!!";
                    case "SeraphChat5":
                    return "Ауу, масенький землянин сваливает. Поплачешь?";
                    case "SeraphKillChat1":
                    return "Упс. Они умерли. Ах..!";
                    case "SeraphKillChat2":
                    return "Хах. Я выйграла!";
                    case "SeraphKillChat3":
                    return "Было весело! Возвращайся, когда не будешь сосать, придурок!";
                    case "SeraphKillChat4":
                    return "И ДЕРЖИСЬ ПОДАЛЬШЕ..!";
                    case "SeraphKillChat5":
                    return "Ну, это было необычно. Слишко легко!";
                    case "SeraphHurtChat1":
                    return "АУЧ..! КАК ДЕРЗКО! АФИИИИИИНААА!!!";
                    case "SeraphHurtChat2":
                    return "ЭЙ! Я ТОЛЬКО ИГРАЛАСЬ! Подожди, скоро Афина услышит об этом!";
                    case "SeraphHurtChat3":
                    return "НУ ВСЕ! ТЫ ДОИГРАЛСЯ! АФИНАААА!!!";
                    case "SeraphHurtChat4":
                    return "БОЛЬНО..! АААААААААААФИНААААА, ЗЕМЛЯНИН ИЗДЕВАЕТСЯ НАД НАМИ!";
                    case "SeraphHurtChat5":
                    return "ВЗДОХ! Ты испортил мое платье..! Я зову Афину..!";
                    case "AthenaChat1":
                    return "...Эх...эти маленькие плаксивые--";
                    case "AthenaChat2":
                    return "У меня нет времени на это..!";
                }
            }
            else
            {
                switch(Info)
                {
                    case "SeraphChat1":
                    return "Aww you're no fun! Sissy..!";
                    case "SeraphChat2":
                    return "Hahahah! Running away like the spineless earthwalker you are!";
                    case "SeraphChat3":
                    return "I'm bored now.";
                    case "SeraphChat4":
                    return "Come back! I was having fun kicking you around..!";
                    case "SeraphChat5":
                    return "Aww the wittle earthwalker is weaving. Gonna go cry?";
                    case "SeraphKillChat1":
                    return "Whoops. They died. Oh well..!";
                    case "SeraphKillChat2":
                    return "Hah! I win!";
                    case "SeraphKillChat3":
                    return "That was fun! Come back when you don't suck!, twerp!";
                    case "SeraphKillChat4":
                    return "And STAY away..!";
                    case "SeraphKillChat5":
                    return "Well that was anticlimactic. Piece of cake!";
                    case "SeraphHurtChat1":
                    return "OUCH..! How RUDE! ATHENAAAAAAAAAAAAAAA!!!";
                    case "SeraphHurtChat2":
                    return "HEY! I was only playing! Wait till Athena hears about THIS!";
                    case "SeraphHurtChat3":
                    return "NOW You've done it! Oh ATHEEEEENAAAAAAAA";
                    case "SeraphHurtChat4":
                    return "THAT HURT..! WAAAAAAAAAAH ATHENAAAAAA THE EARTHWALKER IS BEING MEAN!";
                    case "SeraphHurtChat5":
                    return "*GASP*! You scuffed up my dress..! I'm getting Athena..!";
                    case "AthenaChat1":
                    return "...ugh...those little whiny--";
                    case "AthenaChat2":
                    return "I dont have time for this..!";
                }
            }
            return"";
        }

        public static string TheEquinox(string Info)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
            {
                switch(Info)
                {
                    case "GravAltar":
                    return @"在世界洞穴的深处躺着一条蟲王, 守护者给予我们生命的土壤.";
                    case "StarAltar":
                    return @"统治者云层之上的妖鸟女王, 守护者看护我们的天空.";
                    case "WormAltar":
                    return "神像放进插槽, 不过好像没什么事情发生.";
                    case "WormAltarOK":
                    return "神像闪耀着生命的光芒, 焕发出原本的色彩";
                    case "s":
                    return "们";
                    case "heroes":
                    return "勇者们";
                    case "WormSpawn1":
                    return "看起来有人重新组合了我们的雕像, 亲爱的戴布林格.";
                    case "WormSpawn2":
                    return "确实如此. 小东西";
                    case "WormSpawn3":
                    return ", 你给我们留下了深刻的印象";
                    case "WormSpawn4":
                    return "他们是不是就是我们一直在找的那个?";
                    case "WormSpawn5":
                    return "...有可能.";
                    case "WormSpawn6":
                    return "年轻人";
                    case "WormSpawn7":
                    return ". 我们会给你一次最后的考验.";
                    case "WormSpawn8":
                    return "对我们力量的考验.";
                    case "WormSpawn9":
                    return "等你准备好了, 就拿着手上的雕像呼唤我们.";
                    case "WormSpawn10":
                    return "由于你重新激活了这些信标. 我应该可以轻易找到你";
                    case "WormSpawn11":
                    return "不过...";
                    case "WormSpawn12":
                    return "不要指望我们会对你手下留情, ";
                }
            }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
            {
                switch(Info)
                {
                    case "GravAltar":
                    return @"Глубоко в пещерах нашего мира спит король червей, защитник земли, который был создан, что бы дать нам жизнь";
                    case "StarAltar":
                    return @"Высоко в облаках правит Королева Серафимов, стражник небес, с которых на нас смотрят создатели";
                    case "WormAltar":
                    return "Идол вошел, но ничего не случилось.";
                    case "WormAltarOK":
                    return "Идол оживает, возвращая свой настоящий цвет";
                    case "s":
                    return "s";
                    case "heroes":
                    return "герои";
                    case "WormSpawn1":
                    return "Похоже, что кто-то снова собрал наш идол, Дэйбрингер";
                    case "WormSpawn2":
                    return "Похоже. Смертный";
                    case "WormSpawn3":
                    return ", ты нас сильно впечатлил.";
                    case "WormSpawn4":
                    return "Может ли этот смертный быть тем, кого мы искали?";
                    case "WormSpawn5":
                    return "...возможно.";
                    case "WormSpawn6":
                    return "Дитя.";
                    case "WormSpawn7":
                    return ". Мы хотим проверить тебя.";
                    case "WormSpawn8":
                    return "Проверить твою силу против нас.";
                    case "WormSpawn9":
                    return "Когда будешь готов, позови нас с помощью этой статуи.";
                    case "WormSpawn10":
                    return "Мы сможем найти тебя с помощью нее.";
                    case "WormSpawn11":
                    return "Но...";
                    case "WormSpawn12":
                    return "Не думай, что мы будем добры с тобой,";
                }
            }
            else
            {
                switch(Info)
                {
                    case "GravAltar":
                    return @"Deep in the caverns of the world lies the worm king, the guardian of the earth that was created to give us life.";
                    case "StarAltar":
                    return @"High in the clouds rules the Seraph Queen, guardian of the skies where the creators watch over us.";
                    case "WormAltar":
                    return "The idol goes into the slot, but nothing happens.";
                    case "WormAltarOK":
                    return "The Idol shines to life, regaining its original coloration";
                    case "s":
                    return "s";
                    case "heroes":
                    return "heroes";
                    case "WormSpawn1":
                    return "It appears someone has reassembled our idol, dearest Daybringer.";
                    case "WormSpawn2":
                    return "It appears so. Little one";
                    case "WormSpawn3":
                    return ", you have impressed us greatly";
                    case "WormSpawn4":
                    return "Could they be the one we've been searching for..?";
                    case "WormSpawn5":
                    return "...possibly.";
                    case "WormSpawn6":
                    return "Young one";
                    case "WormSpawn7":
                    return ". We would like to provide you with one last test.";
                    case "WormSpawn8":
                    return "A test of strength against us.";
                    case "WormSpawn9":
                    return "When you are ready, call us forth with the worm statue in your hand.";
                    case "WormSpawn10":
                    return "We will be able to find you because of these beacons you reactivated.";
                    case "WormSpawn11":
                    return "However...";
                    case "WormSpawn12":
                    return "Do not expect us to go easy on you, ";
                }
            }
            return"";
        }
        public static string WorldBuild(string Info)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
            {
                switch(Info)
                {
                    case "Info1":
                    return "安置混沌祭坛";
                    case "Info2":
                    return "混沌蔓延";
                    case "Info3":
                    return "把土壤焦化成燎狱";
                    case "Info4":
                    return "给潭渊蓄水";
                    case "Info5":
                    return "建筑泰拉心球";
                }
            }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
            {
                switch(Info)
                {
                    case "Info1":
                    return "Расставляет алтари хаоса";
                    case "Info2":
                    return "Распространяет хаос";
                    case "Info3":
                    return "Зажигает Инферно";
                    case "Info4":
                    return "Затапливает Трясину.";
                    case "Info5":
                    return "Строит Террариум";
                }
            }
            else
            {
                switch(Info)
                {
                    case "Info1":
                    return "Placing Chaos Altars";
                    case "Info2":
                    return "Spreading Chaos";
                    case "Info3":
                    return "Scorching the Inferno";
                    case "Info4":
                    return "Flooding the Mire";
                    case "Info5":
                    return "Constructing the Terrarium";
                }
            }
            return"";
        }

        public static string BiomiteArmor(string Info)
        {
            if(Language.ActiveCulture.Name == GameCulture.CultureName.Chinese.ToString())
            {
                switch(Info)
                {
                    case "BiomiteArmor1":
                    return "根据你所处的环境提供效果";
                    case "BiomiteArmor2":
                    return "\n+20生命上限";
                    case "BiomiteArmor3":
                    return "\n+20魔力上限";
                    case "BiomiteArmor4":
                    return "\n你可以探测到你周围的敌人";
                    case "BiomiteArmor5":
                    return "\n你免疫着火";
                    case "BiomiteArmor6":
                    return "\n你免疫中毒";
                    case "BiomiteArmor7":
                    return "\n增加5防御力";
                    case "BiomiteArmor8":
                    return "\n增加魔力再生";
                    case "BiomiteArmor9":
                    return "\n你免疫颤抖";
                    case "BiomiteArmor10":
                    return "\n你免疫强风";
                    case "BiomiteArmor11":
                    return "\n增加生命再生\n你免疫缓慢";
                    case "BiomiteArmor12":
                    return "\n增加10%移动速度";
                    case "BiomiteArmor13":
                    return "\n增加5破甲";
                }
            }
            else if(Language.ActiveCulture.Name == GameCulture.CultureName.Russian.ToString())
            {
                switch(Info)
                {
                    case "BiomiteArmor1":
                    return "Дает различные бонусы полного комплекта в зависимости от биома, в котором вы находитесь";
                    case "BiomiteArmor2":
                    return "\nIУвеличивает максимальное ХП на 20";
                    case "BiomiteArmor3":
                    return "\nУвеличивает максимальную ману на 20";
                    case "BiomiteArmor4":
                    return "\nВы можете обнаруживать врагов вокруг себя";
                    case "BiomiteArmor5":
                    return "\nИммунитет к дебаффу 'В огне!'";
                    case "BiomiteArmor6":
                    return "\nИммунитет к дебаффу 'Отравление'";
                    case "BiomiteArmor7":
                    return "\nДобавляет 5 защиты";
                    case "BiomiteArmor8":
                    return "\nУвеличивает регенерацию маны";
                    case "BiomiteArmor9":
                    return "\nИммунитет к дебаффу 'Охлаждение'";
                    case "BiomiteArmor10":
                    return "\nИммунитет к дебаффу 'Могучий ветер'";
                    case "BiomiteArmor11":
                    return "\nУвеличивает регенерацию ХП\nИммунитет к замедлению";
                    case "BiomiteArmor12":
                    return "\nНа 10% увеличивает скорость передвижения";
                    case "BiomiteArmor13":
                    return "\nАтаки игнорируют 5 единиц брони врага";
                }
            }
            else
            {
                switch(Info)
                {
                    case "BiomiteArmor1":
                    return "Provide different setbonuses based on biome you're in";
                    case "BiomiteArmor2":
                    return "\nIncreases maximum life by 20";
                    case "BiomiteArmor3":
                    return "\nIncreases maximum mana by 20";
                    case "BiomiteArmor4":
                    return "\nYou can detect the enemies around you";
                    case "BiomiteArmor5":
                    return "\nYou immune to 'On fire!' debuff";
                    case "BiomiteArmor6":
                    return "\nYou immune to 'Poisoned' debuff";
                    case "BiomiteArmor7":
                    return "\nIncrease 5 defense";
                    case "BiomiteArmor8":
                    return "\nIncrease your mana regenaration";
                    case "BiomiteArmor9":
                    return "\nYou immune to 'Chilled' debuff";
                    case "BiomiteArmor10":
                    return "\nYou immune to 'Mighty Wind' debuff";
                    case "BiomiteArmor11":
                    return "\nIncrease your life regenaration\nYou immune to Slow";
                    case "BiomiteArmor12":
                    return "\nIncrease 10% movespeed";
                    case "BiomiteArmor13":
                    return "\nIncrease 5 armor penetration";
                }
            }
            return"";
        }
    }
}