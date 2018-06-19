using UnityEngine;

public class GameProgress : MonoBehaviour
{
  public bool _hasTegami = false;
  public bool _hasSmallKey = false;
  public bool _hasHammer = false;
  public bool _hasSword = false;
  public bool _hasBigKey = false;
  public bool _hasKnob = false;
  public bool _hasArmor = false;
  public int _potions = 0;
  public bool _armorEquipped = false;

  public bool _atticLock = true;
  public bool _stairsDown = false;
  public bool _basementLock = true;
  public bool _basementPhase = false;

  public bool _hasPotionPaty = false;
  public bool _hasPotionFridge = false;
  public bool _hasPotionAttic = false;

  public string _usedDoorName = "";

  private PlayerCore playerCore;

  public GameObject GetCurrentSkin()
  {
    GameObject skin;

    if (!playerCore)
      playerCore = GetComponent<GameMaster>().GetPlayer().GetComponent<PlayerCore>();

    if (_hasArmor && _armorEquipped)
    {
      skin = playerCore.GetSkin("armored");
    }
    else
    {
      skin = playerCore.GetSkin("base");
    }

    return skin;
  }

  public void EquipArmorSkin()
  {
    if (!playerCore)
      playerCore = GetComponent<GameMaster>().GetPlayer().GetComponent<PlayerCore>();

    playerCore.EquipArmorSkin(_armorEquipped);
  }
}
