﻿using UnityEngine;

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
  private UserInterfaceCore _UI;

  void Awake()
  {
    _controller = GetComponent<PlayerControllers>();
  }

  public void StartUp()
  {
    GameMaster master = GameMaster.GetInstance();
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

    _controller.RefreshAnimator();
  }

  public Animator GetAnimator()
  {
    return _controller.GetAnimator();
  }

  public void StopAvatar()
  {
    _controller.ForceStop();
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
    _UI.HideInteract();
    _currentInteractive = null;
  }

  public void PlayAnimation(string animationName)
  {
    float t = 0f;

    switch (animationName)
    {
      case "interact_low":
        t = 1.5f;
        break;
      case "interact_sword":
        t = 2f;
        break;
      case "interact_reach":
        t = 1.7f;
        break;
      case "interact_hammer":
        t = 1.5f;
        break;
      default:
        t = 1f;
        break;
    }

    GetAnimator().Play(animationName);

    Invoke("EndAnimation", t);
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
