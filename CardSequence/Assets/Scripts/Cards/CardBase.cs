using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBase : ScriptableObject
{
    //カードリスト
    [SerializeField] int cardID;
    [SerializeField] new string name;
    [SerializeField] Rarity rarity;
    [SerializeField] int price;
    [SerializeField] Ability ability;
    [SerializeField] int abilityCondition;
    [SerializeField] int abilityValue;
    [SerializeField] Type type;
    [SerializeField] int atk;
    [SerializeField] int def;
    [SerializeField] Sprite icon;
    [TextArea]
    [SerializeField] string description;
    [SerializeField] AudioClip clip;

    public int CardID { get => cardID; }
    public string Name { get => name; }
    public Rarity Rarity { get => rarity; }
    public int Price { get => price; }
    public Ability Ability { get => ability;  }
    public int AbilityCondition { get => abilityCondition; }
    public int AbilityValue { get => abilityValue; }
    public Type Type { get => type; }
    public int Atk { get => atk; }
    public int Def { get => def; }
    public Sprite Icon { get => icon; }
    public string Description { get => description; }
    public AudioClip Clip { get => clip; }
}


public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary,
}

public enum Ability
{
    None,
    Reflection,
    GainMoney,
    Assassination,
    DirectAttack,
    Skeleton,
    Goblin,
    Wizard,
}

public enum Type
{
    None,
    Army,
    Royalty,
    Monster,
}

