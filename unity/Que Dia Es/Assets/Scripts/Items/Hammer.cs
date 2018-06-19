public class Hammer : Interactive
{
  void Start()
  {
    if (_progress._hasHammer)
    {
      Destroy(gameObject);
    }
  }

  public override void EndAnimation()
  {
    _ui.ShowPopup("text", TextContent.HAMMER_TEXT);
  }

  public override void ClosePopup(bool result)
  {
    _progress._hasHammer = true;
    _ui.ShowAnnouncement("El " + TextContent.HAMMER + " de tu apá a la bolsa");
    EndInteract();

    Destroy(gameObject);
  }
}
