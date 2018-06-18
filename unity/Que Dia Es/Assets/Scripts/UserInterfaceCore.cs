using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceCore : MonoBehaviour
{
  [SerializeField]
  private Canvas _canvas;

  [SerializeField]
  private Button _btnMenu;
  [SerializeField]
  private Button _btnSystem;

  [SerializeField]
  private GameObject _menuSystem;
  [SerializeField]
  private Button _btnSound;
  [SerializeField]
  private Button _btnExit;

  private bool _menuOpened = false;
  private bool _systemOpened = false;
  private bool _interactAvailable = false;
  private bool _interactOpened = false;
  private bool _sound = true;

  [SerializeField]
  private GameObject _menuMain;
  [SerializeField]
  private Button _btnPotion;
  [SerializeField]
  private Text _textPotion;
  [SerializeField]
  private Button _btnUse;
  [SerializeField]
  private Button _btnArmor;
  [SerializeField]
  private Text _textArmor;
  [SerializeField]
  private Button _btnEquip;
  [SerializeField]
  private Text _textEquip;
  [SerializeField]
  private Button _btnTegami;
  [SerializeField]
  private Button _btnSmallKey;
  [SerializeField]
  private Button _btnHammer;
  [SerializeField]
  private Button _btnSword;
  [SerializeField]
  private Button _btnBigKey;
  [SerializeField]
  private Button _btnKnob;
  [SerializeField]
  private Text _description;
  [SerializeField]
  private Camera _uiCharacterCamera;
  [SerializeField]
  private Transform _uiCharacterHolder;
  [SerializeField]
  private Button _btnInteract;
  [SerializeField]
  private float _interactButtonDistance = 1.9f;
  private RectTransform _btnInteractTransform;

  [SerializeField]
  private GameObject _announcement;
  [SerializeField]
  private Text _announcementText;
  [SerializeField]
  private float _waitTime;
  private float _currentTimer;
  private bool _showAnnouncement = false;

  [SerializeField]
  private GameObject _popup;
  [SerializeField]
  private Text _popupText;
  [SerializeField]
  private Button _btnPopupAccept;
  [SerializeField]
  private Button _btnPopupCancel;
  [SerializeField]
  private Button _btnPopupOk;
  private List<string> _textList;
  private int _textProgress = 0;

  private Vector3 _potionPos;
  private Vector3 _armorPos;

  private string _currentInspecting;
  private GameProgress _progress;
  private PlayerCore _playerCore;
  private Transform _playerTransform;
  private Vector3 _playerPosition;
  private Vector2 _viewportPosition;
  private Vector2 _canvasSize;
  private Vector2 _canvasHalfed;
  private Vector3 _interactPosition;

  void Start()
  {
    GameMaster master = GameMaster.GetInstance();
    _progress = master.gameObject.GetComponent<GameProgress>();

    GameObject player = master.GetPlayer();
    _playerTransform = player.transform;
    _playerCore = player.GetComponent<PlayerCore>();

    RectTransform _canvasRect = _canvas.GetComponent<RectTransform>();
    _canvasSize = new Vector2(_canvasRect.sizeDelta.x, _canvasRect.sizeDelta.y);
    _canvasHalfed = new Vector2(_canvasRect.sizeDelta.x / 2f, _canvasRect.sizeDelta.y / 2f);

    _interactPosition = new Vector3(0f, 0f, -40f);

    _btnInteractTransform = _btnInteract.GetComponent<RectTransform>();

    // Main buttons
    _btnSound.onClick.AddListener(ToggleSound);
    _btnExit.onClick.AddListener(ExitGame);

    // System Menu
    _btnMenu.onClick.AddListener(ToggleMenu);
    _btnSystem.onClick.AddListener(ToggleSystem);
    _btnMenu.onClick.AddListener(CheckPlayerLock);
    _btnSystem.onClick.AddListener(CheckPlayerLock);
    _menuSystem.SetActive(_systemOpened);

    // Main menu
    _menuMain.SetActive(_menuOpened);
    _btnPotion.onClick.AddListener(() => InspectKeyItem("potion"));
    _btnArmor.onClick.AddListener(() => InspectKeyItem("armor"));
    _textArmor.text = TextContent.ARMOR;
    _btnTegami.onClick.AddListener(() => InspectKeyItem("tegami"));
    _btnSmallKey.onClick.AddListener(() => InspectKeyItem("smallkey"));
    _btnHammer.onClick.AddListener(() => InspectKeyItem("hammer"));
    _btnSword.onClick.AddListener(() => InspectKeyItem("sword"));
    _btnBigKey.onClick.AddListener(() => InspectKeyItem("bigkey"));
    _btnKnob.onClick.AddListener(() => InspectKeyItem("knob"));

    _btnUse.onClick.AddListener(UsePotion);
    _btnEquip.onClick.AddListener(EquipArmor);

    _potionPos = _btnPotion.GetComponent<RectTransform>().localPosition; ;
    _armorPos = _btnArmor.GetComponent<RectTransform>().localPosition;

    _btnInteract.onClick.AddListener(Interact);
    _btnInteract.onClick.AddListener(CheckPlayerLock);
    _btnInteract.gameObject.SetActive(false);

    _btnPopupAccept.onClick.AddListener(PopupAccept);
    _btnPopupCancel.onClick.AddListener(PopupCancel);
    _btnPopupOk.onClick.AddListener(PopupOk);
    _popup.SetActive(false);

    _announcement.SetActive(false);
  }

  private void LateUpdate()
  {
    if (_playerTransform  && _interactAvailable)
    {
      _playerPosition = _playerTransform.position;
      _playerPosition.y += _interactButtonDistance;

      _viewportPosition = Camera.main.WorldToViewportPoint(_playerPosition);

      _interactPosition.x = _canvasSize.x * _viewportPosition.x - _canvasHalfed.x;
      _interactPosition.y = _canvasSize.y * _viewportPosition.y - _canvasHalfed.y;

      _btnInteractTransform.localPosition = _interactPosition;
    }

    if (_showAnnouncement)
    {
      _currentTimer += Time.deltaTime;
      if (_currentTimer >= _waitTime)
        HideAnnouncement();
    }
  }

  private void Interact()
  {
    SetInteractOpened(true);
    _playerCore.GetInteractive().SendMessage("Interact", null, SendMessageOptions.DontRequireReceiver);
  }

  public void ShowInteract()
  {
    _interactAvailable = true;
    _btnInteract.gameObject.SetActive(true);
  }

  public void HideInteract()
  {
    _interactAvailable = false;
    _btnInteract.gameObject.SetActive(false);
  }

  public void SetInteractOpened(bool value)
  {
    _interactOpened = value;
  }

  private void UsePotion()
  {
    _progress._potions--;
    RefreshItems();
  }

  private void EquipArmor()
  {
    _progress._armorEquipped = !_progress._armorEquipped;
    ClearAvatar();
    RenderAvatar();
    RefreshItems();

    _progress.EquipArmorSkin();
  }

  private void ToggleMenu()
  {
    if (!_interactOpened)
    {
      _menuOpened = !_menuOpened;
      if (_menuOpened && _systemOpened)
        ToggleSystem();

      if (_menuOpened)
      {
        RenderAvatar();
        RefreshItems();
        RefreshKeyItems();
      }
      else
      {
        ClearAvatar();
      }
      _menuMain.SetActive(_menuOpened);
    }
  }

  private void RefreshItems()
  {
    if (_progress._potions > 0)
    {
      _btnPotion.gameObject.SetActive(true);
      _textPotion.text = TextContent.POTION + " x" + _progress._potions;
    }
    else
      _btnPotion.gameObject.SetActive(false);

    if (_progress._hasArmor)
    {
      _btnArmor.gameObject.SetActive(true);
      if (_progress._potions > 0)
        _btnArmor.GetComponent<RectTransform>().localPosition = _armorPos;
      else
        _btnArmor.GetComponent<RectTransform>().localPosition = _potionPos;
    }
    else
      _btnArmor.gameObject.SetActive(false);

    _currentInspecting = "";
    _description.text = "";

    _btnUse.gameObject.SetActive(false);
    _btnEquip.gameObject.SetActive(false);
  }

  private void RefreshKeyItems()
  {
    if (_progress._hasTegami)
      _btnTegami.gameObject.SetActive(true);
    else
      _btnTegami.gameObject.SetActive(false);

    if (_progress._hasSmallKey)
      _btnSmallKey.gameObject.SetActive(true);
    else
      _btnSmallKey.gameObject.SetActive(false);

    if (_progress._hasHammer)
      _btnHammer.gameObject.SetActive(true);
    else
      _btnHammer.gameObject.SetActive(false);

    if (_progress._hasSword)
      _btnSword.gameObject.SetActive(true);
    else
      _btnSword.gameObject.SetActive(false);

    if (_progress._hasBigKey)
      _btnBigKey.gameObject.SetActive(true);
    else
      _btnBigKey.gameObject.SetActive(false);

    if (_progress._hasKnob)
      _btnKnob.gameObject.SetActive(true);
    else
      _btnKnob.gameObject.SetActive(false);
  }

  private void RenderAvatar()
  {
    GameObject avatar = Instantiate(_progress.GetCurrentSkin());

    avatar.layer = 10;
    avatar.transform.parent = _uiCharacterHolder;
    avatar.transform.localPosition = Vector3.zero;
    avatar.transform.localRotation = Quaternion.identity;
  }

  private void ClearAvatar()
  {
    foreach (Transform child in _uiCharacterHolder)
    {
      GameObject.Destroy(child.gameObject);
    }
  }

  private void InspectKeyItem(string param)
  {
    if (param != _currentInspecting)
    {
      RefreshItems();

      switch (param)
      {
        case "potion":
          _description.text = TextContent.POTION + ": " + TextContent.POTION_DESCRIPTION;
          _btnUse.gameObject.SetActive(true);
          break;
        case "armor":
          _description.text = TextContent.ARMOR + ": " + TextContent.ARMOR_DESCRIPTION;

          if (_progress._armorEquipped)
            _textEquip.text = "Quitar";
          else
            _textEquip.text = "Equipar";

          _btnEquip.gameObject.SetActive(true);
          break;
        case "tegami":
          _description.text = TextContent.TEGAMI + ": " + TextContent.TEGAMI_DESCRIPTION;
          break;
        case "smallkey":
          _description.text = TextContent.KEY + ": " + TextContent.KEY_DESCRIPTION;
          break;
        case "hammer":
          _description.text = TextContent.HAMMER + ": " + TextContent.HAMMER_DESCRIPTION;
          break;
        case "sword":
          _description.text = TextContent.SWORD + ": " + TextContent.SWORD_DESCRIPTION;
          break;
        case "bigkey":
          _description.text = TextContent.BIGKEY + ": " + TextContent.BIGKEY_DESCRIPTION;
          break;
        case "knob":
          _description.text = TextContent.KNOB + ": " + TextContent.KNOB_DESCRIPTION;
          break;
      }
      _currentInspecting = param;
    }
    else
      RefreshItems();
  }

  private void ToggleSystem()
  {
    if (!_interactOpened)
    {
      _systemOpened = !_systemOpened;
      if (_menuOpened && _systemOpened)
        ToggleMenu();

      _menuSystem.SetActive(_systemOpened);
    }
  }

  private void CheckPlayerLock()
  {
    if (_menuOpened || _systemOpened || _interactOpened)
      _playerCore.LockPlayer();
    else
      _playerCore.UnlockPlayer();
  }

  private void ToggleSound()
  {
    _sound = !_sound;

    Debug.Log("Has sound: " + _sound);
  }

  private void ExitGame()
  {
    GameMaster.GetInstance().GoToScene(0);
  }

  public void ShowPopup(string type, List<string> text)
  {
    _btnPopupAccept.gameObject.SetActive(false);
    _btnPopupCancel.gameObject.SetActive(false);
    _btnPopupOk.gameObject.SetActive(false);
    _textList = text;
    _textProgress = 0;

    // text || option
    if (type == "text")
    {
      _btnPopupOk.gameObject.SetActive(true);
    }
    else
    {
      _btnPopupAccept.gameObject.SetActive(true);
      _btnPopupCancel.gameObject.SetActive(true);
    }

    _popupText.text = _textList[_textProgress];
    _popup.SetActive(true);
  }

  private void ClosePopup(bool result = true)
  {
    _popup.SetActive(false);

    if (_playerCore.GetInteractive() != null)
    {
      _playerCore.GetInteractive().SendMessage("ClosePopup", result, SendMessageOptions.DontRequireReceiver);
    }
  }

  private void PopupAccept()
  {
    ClosePopup();
  }

  private void PopupCancel()
  {
    ClosePopup(false);
  }

  private void PopupOk()
  {
    int textLenght = _textList.Count;
    _textProgress++;
    if (_textProgress < textLenght)
    {
      _popupText.text = _textList[_textProgress];
    }
    else
    {
      ClosePopup();
    }
  }

  public void ShowAnnouncement(string textContent)
  {
    _currentTimer = 0;
    _announcement.SetActive(true);
    _announcementText.text = textContent;
    _showAnnouncement = true;
  }
  private void HideAnnouncement()
  {
    _showAnnouncement = false;
    _announcement.SetActive(false);
  }
}
