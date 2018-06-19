using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBasement : Interactive
{
  [SerializeField]
  private GameObject _door;
  [SerializeField]
  private GameObject _knob;
  private Animator _anim;

  void Start()
  {
    if (!_progress._basementLock)
    {
      Destroy(gameObject);
    }
    else
    {
      _door.GetComponent<SphereCollider>().enabled = false;
    }
  }

  public override void Interact()
  {
    _ui.HideInteract();
    PlayerLookToMe();
    if (_progress._hasBigKey)
    {
      _ui.ShowPopup("option", TextContent.BASEMENTDOOR_OPTION);
    }
    else
    {
      _player.PlayAnimation("interact_base");
    }
  }

  public override void EndAnimation()
  {
    if (!_progress._hasBigKey)
    {
      _ui.ShowPopup("text", TextContent.BASEMENTDOOR_TEXT);
    }
  }

  public override void ClosePopup(bool result)
  {
    if (_progress._hasBigKey && result)
    {
      if (!_progress._basementLock)
      {
        _progress._hasKnob = true;
        _ui.ShowAnnouncement("Manija adquirida y sótano Unlocked");
        _door.GetComponent<SphereCollider>().enabled = true;
        EndInteract();

        Destroy(gameObject);
      }
      else
      {
        _anim = _knob.GetComponent<Animator>();
        _anim.SetBool("knob_drop", true);
      }
    }
    else
    {
      EndInteract();
    }
  }

  public void KnobAnimationEnd()
  {
    _progress._basementLock = false;
    _ui.ShowPopup("text", TextContent.UNLOCKEDBASEMENTDOOR_TEXT);
  }
}
