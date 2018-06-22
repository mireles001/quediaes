using UnityEngine;

public class Chest : Interactive
{
  [SerializeField]
  private Transform _chestTop;
  private Vector3 _endRotation = new Vector3(-160f, 0f, 0f);

  void Start()
  {
    if (_progress._hasBigKey)
    {
      GetComponent<SphereCollider>().enabled = false;
      _chestTop.transform.localEulerAngles = _endRotation;
    }
  }

  public override void Interact()
  {
    _player.StopAvatar();
    _master.GetAudio().PlaySound();
    _ui.HideInteract();
    PlayerLookToMe();
    if (_progress._hasSword)
    {
      _ui.ShowPopup("option", TextContent.BIGKEY_OPTION);
    }
    else
    {
      _player.PlayAnimation("interact_low");
    }
  }

  public override void EndAnimation()
  {
    if (!_progress._hasSword)
    {
      _ui.ShowPopup("text", TextContent.BIGKEY_TEXT);
    }
    else
    {
      _chestTop.transform.localEulerAngles = _endRotation;
      Invoke("Delayer", 1f);
    }
  }

  public override void ClosePopup(bool result)
  {
    if (_progress._hasSword && result)
    {
      if (_progress._hasBigKey)
      {
        _ui.ShowAnnouncement("Por fin! " + TextContent.BIGKEY + " a la bolsa");
        GetComponent<SphereCollider>().enabled = false;
        EndInteract();
      }
      else
      {
        _master.GetAudio().PlaySound("sword");
        _player.PlayAnimation("interact_sword");
      }
    }
    else
    {
      EndInteract();
    }
  }

  private void Delayer()
  {
    _ui.ShowPopup("text", TextContent.GETBIGKEY_TEXT);
    _progress._hasBigKey = true;
  }
}
