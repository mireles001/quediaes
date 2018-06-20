using UnityEngine;

public class Interactive : MonoBehaviour
{
  protected PlayerCore _player;
  protected GameProgress _progress;
  protected UserInterfaceCore _ui;
  protected GameMaster _master;

  void Awake()
  {
    _master = GameMaster.GetInstance();
    _player = _master.GetPlayer().GetComponent<PlayerCore>();
    _progress = _master.gameObject.GetComponent<GameProgress>();
    _ui = _master.GetUI().GetComponent<UserInterfaceCore>();
  }

  public virtual void Interact()
  {
    _master.GetAudio().PlaySound();
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
