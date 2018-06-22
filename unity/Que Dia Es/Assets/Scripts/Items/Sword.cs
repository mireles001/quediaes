using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Interactive
{
  [SerializeField]
  private GameObject _container;
  [SerializeField]
  private GameObject _sword;
  private bool _useHammer = false;

  void Start()
  {
    if (_progress._hasSword)
    {
      GetComponent<SphereCollider>().enabled = false;
      _container.SetActive(false);
      _sword.SetActive(false);
    }
  }

  public override void Interact()
  {
    _player.StopAvatar();
    _master.GetAudio().PlaySound();
    _ui.HideInteract();
    PlayerLookToMe();
    if (_progress._hasHammer)
    {
      _ui.ShowPopup("option", TextContent.SWORD_OPTION);
    }
    else
    {
      _player.PlayAnimation("interact_base");
    }
  }

  public override void EndAnimation()
  {
    if (_progress._hasHammer && _useHammer)
    {
      _container.SetActive(false);
      Invoke("Delayer", 1f);
    }
    else
    {
      _ui.ShowPopup("text", TextContent.SWORD_TEXT);
    }
  }

  public override void ClosePopup(bool result)
  {
    if (_progress._hasSword)
    {
      _ui.ShowAnnouncement("Obtienes " + TextContent.SWORD + "! :D");
      _sword.SetActive(false);
      GetComponent<SphereCollider>().enabled = false;
      EndInteract();
    }
    else
    {
      if (_progress._hasHammer && result)
      {
        _useHammer = true;
        _master.GetAudio().PlaySound("hammer");
        _player.PlayAnimation("interact_hammer");
      }
      else
      {
        EndInteract();
      }
    }
  }

  private void Delayer()
  {
    _ui.ShowPopup("text", TextContent.GETSWORD_TEXT);
    _progress._hasSword = true;
  }
}
