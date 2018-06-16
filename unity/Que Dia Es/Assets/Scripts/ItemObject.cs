using UnityEngine;

public class ItemObject : MonoBehaviour
{
  [SerializeField]
  private string _name;
  [SerializeField]
  private ItemTypes _itemType;
  [SerializeField]
  private EquippableTypes _equippableType;
  [SerializeField]
  private int _attribute;
  [SerializeField]
  private GameObject _equippableAsset;

  private string _systemName;
  private GameMaster _master;
  private InteractiveCore _core;

  private void Awake()
  {
    _core = GetComponent<InteractiveCore>();
    _master = GameMaster.GetInstance();
    _systemName = _master.GetSceneIndex() + "_" + gameObject.name;

    if (_master.IsItemPicked(_systemName))
    {
      _core.RemoveInteractive();
    }
  }

  public void Use()
  {
    switch (_itemType.GetHashCode())
    {
      case 0:
        _master.ItemPicked(_systemName, new Potion().Create(_name, _attribute));
        break;
      case 1:
        switch (_equippableType.GetHashCode())
        {
          case 0:
            _master.ItemPicked(_systemName, new Weapon().Create(_name, _attribute, _equippableAsset));
            break;
          case 1:
            _master.ItemPicked(_systemName, new Armor().Create(_name, _attribute, _equippableAsset));
            break;
        }
        break;
    }

    _core.RemoveInteractive();
  }
}