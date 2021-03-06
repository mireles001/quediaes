﻿using System.Collections.Generic;
using UnityEngine;

public class InteractiveNotes : Interactive
{
  [SerializeField]
  private string _note;

  [SerializeField]
  private GameObject _character;
  private Animator _anim;

  public override void Interact()
  {
    _player.StopAvatar();
    _master.GetAudio().PlaySound();
    _ui.HideInteract();
    PlayerLookToMe();
    if (IsCharacter())
    {
      _anim = _character.GetComponent<Animator>();
      _anim.SetBool("isCheering", true);
      EndAnimation();
    }
    else
    {
      _player.PlayAnimation("interact_base");
    }
  }

  public override void EndAnimation()
  {
    List<string> noteText = new List<string>();
    switch (_note)
    {
      case "attic_0":
        noteText = TextContent.ATTIC0_TEXT;
        break;
      case "attic_1":
        noteText = TextContent.ATTIC1_TEXT;
        break;
      case "attic_2":
        noteText = TextContent.ATTIC2_TEXT;
        break;
      case "main_0":
        noteText = TextContent.MAIN0_TEXT;
        break;
      case "main_1":
        noteText = TextContent.MAIN1_TEXT;
        break;
      case "tvroom_0":
        noteText = TextContent.TVROOM0_TEXT;
        break;
      case "basement_0":
        noteText = TextContent.BASEMENT0_TEXT;
        break;
      case "basement_1":
        noteText = TextContent.BASEMENT1_TEXT;
        break;
      case "basement_2":
        noteText = TextContent.BASEMENT2_TEXT;
        break;
      case "basement_3":
        noteText = TextContent.BASEMENT3_TEXT;
        break;
      case "basement_4":
        noteText = TextContent.BASEMENT4_TEXT;
        break;
    }

    if (_note != "basement_2")
    {
      _ui.ShowPopup("text", noteText);
    }
    else
    {
      _ui.ShowPopup("option", noteText, true);
    }
  }

  public override void ClosePopup(bool result)
  {
    if (IsCharacter())
    {
      _anim.SetBool("isCheering", false);
    }

    if (_note == "basement_2" && result)
    {
      Invoke("Delayer", 2f);
    }
    else
    {
      EndInteract();
    }
  }

  private bool IsCharacter()
  {
    bool value = false;

    if ((_note == "basement_0" || _note == "basement_1" || _note == "basement_2" || _note == "basement_3" || _note == "basement_4") && _character != null)
    {
      value = true;
    }

    return value;
  }

  private void Delayer()
  {
    GameMaster.GetInstance().GoToScene(15);
  }
}
