using UnityEngine;

public class Tegami : Interactive
{
  [SerializeField]
  private GameObject _d1;

  void Start()
  {
    if (_progress._hasTegami)
    {
      Destroy(gameObject);
    }
    else
    {
      _d1.GetComponent<SphereCollider>().enabled = false;
    }
  }

  public override void Interact()
  {
    _player.StopAvatar();
    _master.GetAudio().PlaySound();
    _ui.HideInteract();
    PlayerLookToMe();
    _player.PlayAnimation("interact_low");
  }

  public override void EndAnimation()
  {
    _ui.ShowPopup("text", TextContent.TEGAMI_TEXT);
  }

  public override void ClosePopup(bool result)
  {
    _progress._hasTegami = true;
    _d1.GetComponent<SphereCollider>().enabled = true;
    EndInteract();
    _ui.ShowAnnouncement(TextContent.TEGAMI + " guardada en tu inventorio");

    Destroy(gameObject);
  }
}
