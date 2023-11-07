using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBook : MonoBehaviour
{


    private int[] result;
    private int playerATK;
    private int playerDEF;
    private int enemyATK;
    private int enemyDEF;
    private bool playerArmyAbility;
    private bool enemyArmyAbility;
    public int playerSpellCount;
    public int enemySpellCount;


    private void Start()
    {
        result = new int[2];
        playerATK = 0;
        playerDEF = 0;
        enemyATK = 0;
        enemyDEF = 0;
        playerArmyAbility = false;
        enemyArmyAbility = false;
        playerSpellCount = 0;
        enemySpellCount = 0;
    }

    public int[] GetBattleResult(Battler player, Battler enemy, int turn)
    {
        result = new int[2];
        playerATK = player.SubmitCard.Base.Atk;
        playerDEF = player.SubmitCard.Base.Def;
        enemyATK = enemy.SubmitCard.Base.Atk;
        enemyDEF = enemy.SubmitCard.Base.Def;
        playerArmyAbility = false;
        enemyArmyAbility = false;

        if (player.SubmitCard.Base.Type != Type.Monster && player.SubmitCard.Base.CardID != 200)
        {
            PlayerKingEffect(player);
        }
        if (enemy.SubmitCard.Base.Type != Type.Monster && enemy.SubmitCard.Base.CardID != 200)
        {
            EnemyKingEffect(enemy);
        }
        if (player.SubmitCard.Base.Type == Type.Army )
        {
            PlayerCommanderEffect(player);
            ArmyEffect(player);            
        }
        if (enemy.SubmitCard.Base.Type == Type.Army )
        {
            EnemyCommanderEffect(enemy);
            ArmyEffect(enemy);
        }
        if (player.SubmitCard.Base.Type == Type.Monster)
        {
            MonsterEffect(player);
        }
        if (enemy.SubmitCard.Base.Type == Type.Monster)
        {
            MonsterEffect(enemy);
        }
        if (player.SubmitCard.Base.Ability == Ability.Wizard)
        {
            WizardEffect(player);
        }
        if (enemy.SubmitCard.Base.Ability == Ability.Wizard)
        {
            WizardEffect(enemy);
        }
        if (player.SubmitCard.Base.Ability == Ability.GainMoney)
        {
            TitleManager.money += player.SubmitCard.Base.AbilityValue;
        }

        if ((player.SubmitCard.Base.Ability == Ability.Assassination && enemy.SubmitCard.Base.Type == Type.Royalty)|| (enemy.SubmitCard.Base.Ability == Ability.Assassination && player.SubmitCard.Base.Type == Type.Royalty))
        {
            result = AssassinationEffect(player, enemy);
        }
        else if ((player.SubmitCard.Base.Ability == Ability.Reflection && enemy.SubmitCard.Base.CardID != 301) || (enemy.SubmitCard.Base.Ability == Ability.Reflection && player.SubmitCard.Base.CardID != 301))
        {
            result = RefrectionEffect(player, enemy);
        }
        else if ((player.SubmitCard.Base.Ability == Ability.DirectAttack && playerArmyAbility) || (enemy.SubmitCard.Base.Ability == Ability.DirectAttack && enemyArmyAbility))
        {
            result[0] = playerATK - enemyDEF;
            result[1] = enemyATK - playerDEF;
            result = DirectAttackEffect(player, enemy);
        }
        else
        {
            result[0] = playerATK - enemyDEF;
            result[1] = enemyATK - playerDEF;
        }

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] < 0)
            {
                result[i] = 0;
            }
        }
        return result;
    }

    void WizardEffect(Battler battler)
    {
        int ID = battler.SubmitCard.Base.CardID;
        if (ID == 101 || ID == 10)
        {
            WizardATKBuff(battler);
        }
        else if(ID == 9)
        {
            GainSpell(battler);
        }
        else if (ID == 11)
        {
            WizardDEFBuff(battler);
        }
    }


    void GainSpell(Battler battler)
    {
        int abilityValue = battler.SubmitCard.Base.AbilityValue;
        if (battler.IsPlayer)
        {
            playerSpellCount += abilityValue;
        }
        else
        {
            enemySpellCount += abilityValue;
        }
    }

    void WizardATKBuff(Battler battler)
    {
        int abilityValue = battler.SubmitCard.Base.AbilityValue;
        if (battler.IsPlayer)
        {
            if (playerSpellCount > 0)
            {
                playerATK += abilityValue;
                battler.SubmitCard.BuffATK(playerATK);
                playerSpellCount--;
            }
            else
            {
                playerSpellCount++;
            }
        }
        else
        {
            if (enemySpellCount > 0)
            {
                enemyATK += abilityValue;
                battler.SubmitCard.BuffATK(enemyATK);
                enemySpellCount--;
            }
            else
            {
                enemySpellCount++;
            }
        }
    }

    void WizardDEFBuff(Battler battler)
    {
        int abilityValue = battler.SubmitCard.Base.AbilityValue;
        if (battler.IsPlayer)
        {
            if (playerSpellCount > 0)
            {
                playerDEF += abilityValue;
                battler.SubmitCard.BuffDEF(playerDEF);
                playerSpellCount--;
            }
            else
            {
                playerSpellCount++;
            }
        }
        else
        {
            if (enemySpellCount > 0)
            {
                enemyDEF += abilityValue;
                battler.SubmitCard.BuffDEF(enemyDEF);
                enemySpellCount--;
            }
            else
            {
                enemySpellCount++;
            }
        }
    }

    void MonsterEffect(Battler battler)
    {
        if (battler.Life > 10) return;

        int abilityValue = battler.SubmitCard.Base.AbilityValue;

        if (battler.SubmitCard.Base.Ability == Ability.Goblin)
        {
            GoblinEffect(battler, abilityValue);
        }
        else if (battler.SubmitCard.Base.Ability == Ability.Skeleton)
        {
            SkeletonEffect(battler, abilityValue);
        }
        else
        {
            DemonKingEffect(battler);
        }

    }

    void GoblinEffect(Battler battler, int abilityValue)
    {
        if (battler.IsPlayer)
        {
            playerATK += abilityValue;
            battler.SubmitCard.BuffATK(playerATK);
        }
        else
        {
            enemyATK += abilityValue;
            battler.SubmitCard.BuffATK(enemyATK);
        }
    }

    void SkeletonEffect(Battler battler, int abilityValue)
    {
        if (battler.IsPlayer)
        {
            playerDEF += abilityValue;
            battler.SubmitCard.BuffDEF(playerDEF);
        }
        else
        {
            enemyDEF += abilityValue;
            battler.SubmitCard.BuffDEF(enemyDEF);
        }
    }

    void DemonKingEffect(Battler battler)
    {
        if (battler.IsPlayer)
        {
            playerATK += CountCardWithType(TitleManager.playerDeck, Type.Monster);
            battler.SubmitCard.BuffATK(playerATK);
        }
        else
        {
            enemyATK += CountCardWithType(TitleManager.enemyDeck, Type.Monster);
            battler.SubmitCard.BuffATK(enemyATK);
        }
    }

    int CountCardWithID(int[] deck, int id)
    {
        int count = 0;
        for (int i = 0; i < TitleManager.deckLength; i++)
        {
            if (deck[i] == id)
            {
                count++;
            }
        }
        return count;
    }

    int CountCardWithType(int[] deck, Type type)
    {
        int count = 0;
        for (int i = 0; i < TitleManager.deckLength; i++)
        {
            if (Resources.Load<CardBase>($"Data/{deck[i]}").Type == type)
            {
                count++;
            }
        }
        return count;
    }


    void PlayerCommanderEffect(Battler battler)
    {
        int playerCommander = CountCardWithID(TitleManager.playerDeck, 202);
        if (playerCommander > 0)
        {
            playerATK += playerCommander;
            battler.SubmitCard.BuffATK(playerATK);
            playerDEF += playerCommander;
            battler.SubmitCard.BuffDEF(playerDEF);
        }
    }


    void EnemyCommanderEffect(Battler battler)
    {
        int enemyCommander = CountCardWithID(TitleManager.enemyDeck, 202);
        if (enemyCommander > 0)
        {
            enemyATK += enemyCommander;
            battler.SubmitCard.BuffATK(enemyATK);
            enemyDEF += enemyCommander;
            battler.SubmitCard.BuffDEF(enemyDEF);
        }
    }

    void PlayerKingEffect(Battler battler)
    {
        int playerKing = CountCardWithID(TitleManager.playerDeck, 300);
        if (playerKing > 0)
        {
            playerATK += playerKing;
            battler.SubmitCard.BuffATK(playerATK);
            playerDEF += playerKing * 2;
            battler.SubmitCard.BuffDEF(playerDEF);
        }
    }

    void EnemyKingEffect(Battler battler)
    {
        int enemyKing = CountCardWithID(TitleManager.enemyDeck, 300);
        if (enemyKing > 0)
        {
            enemyATK += enemyKing;
            battler.SubmitCard.BuffATK(enemyATK);
            enemyDEF += enemyKing * 2;
            battler.SubmitCard.BuffDEF(enemyDEF);
        }
    }


    void ArmyEffect(Battler battler)
    {
        if (battler.Hand.list.Count == 0) return;

        if (battler.Hand.list[0].Base.Type != Type.Army) return;

        if (battler.SubmitCard.Base.CardID == 5 || battler.SubmitCard.Base.CardID == 202)
        {
            if (battler.IsPlayer)
            {
                playerArmyAbility = true;
            }
            else
            {
                enemyArmyAbility = true;
            }
        }
        else
        {
            int abilityValue = battler.SubmitCard.Base.AbilityValue;
            if (battler.IsPlayer)
            {
                playerATK += abilityValue;
                battler.SubmitCard.BuffATK(playerATK);
            }
            else
            {
                enemyATK += abilityValue;
                battler.SubmitCard.BuffATK(enemyATK);
            }
        }
    }



    int[] AssassinationEffect(Battler player, Battler enemy)
    {
        int[] result = new int[2];
        if (player.SubmitCard.Base.Ability == Ability.Assassination)
        {
            result[0] = player.SubmitCard.Base.AbilityValue;
            result[1] = 0;
        }
        else
        {
            result[0] = 0;
            result[1] = enemy.SubmitCard.Base.AbilityValue;
        }
        return result;
    }

    int[] RefrectionEffect(Battler player, Battler enemy)
    {
        int[] result = new int[2];
        if (player.SubmitCard.Base.Ability == Ability.Reflection)
        {
            result[0] = enemyATK;
            result[1] = 0;
        }
        else
        {
            result[0] = 0;
            result[1] = playerATK;
        }
        return result;
    }

    int[] DirectAttackEffect(Battler player, Battler enemy)
    {
        int[] result = new int[2];
        if (player.SubmitCard.Base.Ability == Ability.DirectAttack)
        {
            result[0] = playerATK;
        }
        if (enemy.SubmitCard.Base.Ability == Ability.DirectAttack)
        {
            result[1] = enemyATK;
        }
        return result;
    }




}
