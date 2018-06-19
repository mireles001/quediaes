using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : Interactive
{
  private bool _timeToDecide = false;

  public override void Interact()
  {
    _timeToDecide = false;
    _ui.HideInteract();
    PlayerLookToMe();
     _player.PlayAnimation("interact_base");
  }

  public override void EndAnimation()
  {
    if (_timeToDecide)
    {
      _progress._basementPhase = true;
      Invoke("EndScene", 2f);
    }
    else
    {
      _ui.ShowPopup("text", TextContent.BASEMENT_TEXT);
    }
  }

  public override void ClosePopup(bool result)
  {
    if (result)
    {
      if (_timeToDecide)
      {
        _player.PlayAnimation("interact_base");
      }
      else
      {
        Invoke("Delayer", 1f);
      }
    }
    else
    {
      EndInteract();
    }
  }

  private void Delayer()
  {
    _timeToDecide = true;
    _ui.ShowPopup("option", TextContent.BASEMENT_OPTION);
  }

  private void EndScene()
  {
    EndInteract();
    GameMaster.GetInstance().GoToScene(14);
  }
}
