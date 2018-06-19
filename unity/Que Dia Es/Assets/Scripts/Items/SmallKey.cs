public class SmallKey : Interactive
{
  private int _phase = 0;

  void Start()
  {
    if (_progress._hasSmallKey)
      Destroy(gameObject);
  }

  public override void Interact()
  {
    _ui.HideInteract();
    PlayerLookToMe();
    _player.PlayAnimation("interact_base");
  }

  public override void EndAnimation()
  {
    _ui.ShowPopup("text", TextContent.SMALLKEY_TEXT);
  }

  public override void ClosePopup(bool result)
  {
    if (_phase == 0)
    {
      _phase++;
      Invoke("Delayer", 0.5f);
    }
    else
    {
      if (result)
      {
        _progress._hasSmallKey = true;
        EndInteract();
        _ui.ShowAnnouncement("Has obtenido la " + TextContent.KEY);

        Destroy(gameObject);
      }
      else
      {
        _phase = 0;
        EndInteract();
      }
    }
  }

  private void Delayer()
  {
    _ui.ShowPopup("option", TextContent.SMALLKEY_OPTION);
  }
}
