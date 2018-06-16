using UnityEngine;

public class Item
{
  private bool _isConsumable = false;
  private bool _isEquippable = false;
  private string _name;

  public virtual object Use()
  {
    Debug.Log("Using " + _name + "!");

    return new { name = _name, consumable = _isConsumable, equippable = _isEquippable };
  }

  public void SetName(string value)
  {
    _name = value;
  }

  public void SetConsumable(bool value)
  {
    _isConsumable = value;
  }

  public void SetEquippable(bool value)
  {
    _isEquippable = value;
  }

  public string GetName()
  {
    return _name;
  }
}

public class Potion : Item
{
  private int _power = 0;

  public Item Create(string name, int power)
  {
    Debug.Log("Creating potion");
    SetName(name);
    SetConsumable(true);
    SetEquippable(false);
    _power = power;

    return this;
  }

  public override object Use()
  {
    object data = base.Use();

    return data;
  }
}

public class Armor : Item
{
  private int _defense = 0;
  private GameObject _asset;

  public Item Create(string name, int defense, GameObject asset)
  {
    Debug.Log("Creating armor");
    SetName(name);
    SetEquippable(true);
    SetConsumable(false);

    _defense = defense;
    _asset = asset;

    return this;
  }

  public override object Use()
  {
    object data = base.Use();

    return data;
  }
}

public class Weapon : Item
{
  private int _attack = 0;
  private GameObject _asset;

  public Item Create(string name, int attack, GameObject asset)
  {
    Debug.Log("Creating weapon");
    SetName(name);
    SetEquippable(true);
    SetConsumable(false);

    _attack = attack;
    _asset = asset;

    return this;
  }

  public override object Use()
  {
    object data = base.Use();

    return data;
  }
}

public enum ItemTypes
{
  Consumable,
  Equippable
}

public enum EquippableTypes
{
  Weapon,
  Armor
}
