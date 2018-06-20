using UnityEngine;

public class LockAttic : Interactive
{
  [SerializeField]
  private GameObject _door;
  [SerializeField]
  private GameObject _knob;

  // Use this for initialization
  void Start()
  {
    if (_progress._atticLock)
    {
      _knob.SetActive(false);
      _door.GetComponent<SphereCollider>().enabled = false;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public override void Interact()
  {
    _master.GetAudio().PlaySound();
    _ui.HideInteract();
    PlayerLookToMe();
    if (_progress._hasKnob)
    {
      _ui.ShowPopup("option", TextContent.ATTICDOOR_OPTION);
    }
    else
    {
      _player.PlayAnimation("interact_base");
    }
  }

  public override void EndAnimation()
  {
    if (_progress._hasKnob)
    {
      _knob.SetActive(true);
      _master.GetAudio().PlaySound("lock");
      Invoke("Delayer", 1f);
    }
    else
    {
      _ui.ShowPopup("text", TextContent.ATTICDOOR_TEXT);
    }
  }

  public override void ClosePopup(bool result)
  {
    if (_progress._hasKnob && result)
    {
      if (!_progress._atticLock)
      {
        _progress._hasKnob = false;
        _ui.ShowAnnouncement("Has dejado la Manija en la puerta");
        _door.GetComponent<SphereCollider>().enabled = true;
        EndInteract();

        Destroy(gameObject);
      }
      else
      {
        _player.PlayAnimation("interact_base");
      }
    }
    else
    {
      EndInteract();
    }
  }

  private void Delayer()
  {
    _progress._atticLock = false;
    _ui.ShowPopup("text", TextContent.UNLOCKEDATTICDOOR_TEXT);
  }
}
