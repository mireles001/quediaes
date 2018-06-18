using UnityEngine;

public class PlayerCore : MonoBehaviour
{
  [SerializeField]
  private Transform _avatarHolder;
  [SerializeField]
  private Transform _cameraTarget;
  [SerializeField]
  private GameObject _baseSkin;
  [SerializeField]
  private GameObject _armoredSkin;

  private GameObject _mainObj;
  private GameObject _currentInteractive;
  private PlayerControllers _controller;
  private GameProgress _core;
  private UserInterfaceCore _UI;

  void Awake()
  {
    _controller = GetComponent<PlayerControllers>();
  }

  public void StartUp()
  {
    GameMaster master = GameMaster.GetInstance();
    _core = master.gameObject.GetComponent<GameProgress>();
    EquipArmorSkin();
    _UI = master.GetUI().GetComponent<UserInterfaceCore>();
  }

  public void LocationLoaded()
  {
    _mainObj = GameObject.FindWithTag("LocationMaster");
  }

  public void EquipArmorSkin(bool putOn = false)
  {
    foreach (Transform child in _avatarHolder.transform)
    {
      GameObject.Destroy(child.gameObject);
    }

    GameObject newSkin;

    if (putOn)
    {
      newSkin = Instantiate(_armoredSkin) as GameObject;
    }
    else
    {
      newSkin = Instantiate(_baseSkin) as GameObject;
    }

    newSkin.transform.parent = _avatarHolder.transform;
    newSkin.transform.localPosition = Vector3.zero;
    newSkin.transform.localRotation = Quaternion.identity;
    newSkin.name = "avatar";
  }

  public GameObject GetSkin(string name)
  {
    if (name == "base")
    {
      return _baseSkin;
    }
    else
    {
      return _armoredSkin;
    }
  }

  public Transform GetAvatarHolder()
  {
    return _avatarHolder;
  }

  public Transform GetCameraTarget()
  {
    return _cameraTarget;
  }

  public void LockPlayer()
  {
    _controller.LockPlayer();
  }

  public void UnlockPlayer()
  {
    _controller.UnlockPlayer();
  }

  public GameObject GetInteractive()
  {
    return _currentInteractive;
  }

  private void InteractiveOn(GameObject go)
  {
    _currentInteractive = go;
    _UI.ShowInteract();
  }

  private void InteractiveOff(GameObject go)
  {
    if (_currentInteractive && _currentInteractive.name == go.name)
    {
      _UI.HideInteract();
      _currentInteractive = null;
    }
  }

  public void PlayAnimation(string animationName)
  {
    Debug.Log("Play: " + animationName + " animation");
    Invoke("EndAnimation", 1);
  }

  public void EndAnimation()
  {
    _currentInteractive.SendMessage("EndAnimation", null, SendMessageOptions.DontRequireReceiver);
  }

  private void OnTriggerEnter(Collider trigger)
  {
    if ("Interactive" == trigger.gameObject.tag)
    {
      InteractiveOn(trigger.gameObject);
    }
    else if ("CamTrigger" == trigger.gameObject.tag)
    {
      _mainObj.SendMessage("CamTriggerEnter", trigger.gameObject.transform.parent.gameObject.name, SendMessageOptions.DontRequireReceiver);
    }
  }

  private void OnTriggerExit(Collider trigger)
  {
    if (trigger.gameObject.tag == "Interactive")
    {
      InteractiveOff(trigger.gameObject);
    }
  }
}
