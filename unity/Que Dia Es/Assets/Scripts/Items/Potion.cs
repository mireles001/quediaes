using UnityEngine;

public class Potion : Interactive
{
  void Start()
  {
    bool destroy = false;
    switch (gameObject.name)
    {
      case "p_0":
        destroy = _progress._hasPotionPaty;
        break;
      case "p_1":
        destroy = _progress._hasPotionFridge;
        break;
      case "p_2":
        destroy = _progress._hasPotionAttic;
        break;
    }

    if (destroy)
    {
      Destroy(gameObject);
    }
  }

  public override void EndAnimation()
  {
    _ui.ShowPopup("text", TextContent.POTION_TEXT);
  }

  public override void ClosePopup(bool result)
  {
    switch (gameObject.name)
    {
      case "p_0":
        _progress._hasPotionPaty = true;
        break;
      case "p_1":
        _progress._hasPotionFridge = true;
        break;
      case "p_2":
        _progress._hasPotionAttic = true;
        break;
    }
    _progress._potions++;
    EndInteract();
    _ui.ShowAnnouncement("Adquiriste una " + TextContent.POTION + "!");

    Destroy(gameObject);
  }
}
