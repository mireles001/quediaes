using System.Collections.Generic;
using UnityEngine;

public class InteractiveNotes : Interactive
{
  [SerializeField]
  private string _note;

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
    _ui.ShowPopup("text", noteText);
  }
}
