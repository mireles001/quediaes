public class Armor : Interactive
{
  private int _phase = 0;
  private bool _checkForOption = false;

  public override void Interact()
  {
    _checkForOption = false;
    _ui.HideInteract();
    PlayerLookToMe();
    _player.PlayAnimation("interact_base");
  }

  public override void EndAnimation()
  {
    if (!_progress._hasArmor)
    {
      if (_progress._hasSmallKey)
      {
        _checkForOption = true;
        _ui.ShowPopup("option", TextContent.ARMOR_OPTION);
      }
      else
      {
        _ui.ShowPopup("text", TextContent.ARMOR_TEXT);
      }
    }
    else
    {
      _ui.ShowPopup("text", TextContent.HASARMOR_TEXT);
    }
  }

  public override void ClosePopup(bool result)
  {
    if (_phase == 0)
    {
      if (_checkForOption && result)
      {
        _phase++;
        Invoke("Delayer", 0.5f);
      }
      else
      {
        EndInteract();
      }
    }
    else
    {
      if (!_progress._hasArmor)
      {
        _progress._hasArmor = true;
        _ui.ShowAnnouncement("YES! " + TextContent.ARMOR + " it's in the bag!");
      }
      EndInteract();
    }
  }

  private void Delayer()
  {
    _ui.ShowPopup("text", TextContent.GETARMOR_TEXT);
  }
}
