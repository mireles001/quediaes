using UnityEngine;

public class InteractiveKeyItem : MonoBehaviour
{
  [SerializeField]
  private string _keyItemName;
  private InteractiveCore _core;
  private GameMaster _master;

  private void Awake()
  {
    _core = GetComponent<InteractiveCore>();
    _master = GameMaster.GetInstance();

    if (_master.IsItemPicked(_keyItemName))
    {
      _core.RemoveInteractive();
    }
  }

  public void Use()
  {
    if (_keyItemName != "" && _master.HasRequirements(_keyItemName))
    {
      _master.ItemPicked(_keyItemName);
      _core.RemoveInteractive();
    }
  }
}
