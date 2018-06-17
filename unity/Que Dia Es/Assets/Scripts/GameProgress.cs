using UnityEngine;

public class GameProgress : MonoBehaviour
{
  public bool _hasTegami = true;
  public bool _hasSmallKey = true;
  public bool _hasHammer = true;
  public bool _hasSword = true;
  public bool _hasBigKey = true;
  public bool _hasKnob = true;
  public bool _hasArmor = true;
  public int _potions = 0;
  public bool _armorEquipped = false;
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
