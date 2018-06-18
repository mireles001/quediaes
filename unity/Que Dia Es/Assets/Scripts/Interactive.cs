using UnityEngine;

public class Interactive : MonoBehaviour
{
  protected PlayerCore _player;
  protected GameProgress _progress;
  protected UserInterfaceCore _ui;

  void Awake()
  {
    GameMaster master = GameMaster.GetInstance();
    _player = master.GetPlayer().GetComponent<PlayerCore>();
    _progress = master.gameObject.GetComponent<GameProgress>();
    _ui = master.GetUI().GetComponent<UserInterfaceCore>();
  }

  public virtual void Interact()
  {
    _ui.HideInteract();
    PlayerLookToMe();
    _player.PlayAnimation("interact_base");
  }

  protected void PlayerLookToMe()
  {
    _player.GetAvatarHolder().LookAt(transform);
    Vector3 rot = _player.GetAvatarHolder().localEulerAngles;
    rot.x = 0f;
    rot.z = 0f;

    _player.GetAvatarHolder().localEulerAngles = rot;
  }

  public virtual void EndAnimation()
  {
    EndInteract();
  }

  protected void EndInteract()
  {
    _player.UnlockPlayer();
    _ui.SetInteractOpened(false);
  }

  public virtual void ClosePopup(bool result)
  {
    EndInteract();
  }
}
