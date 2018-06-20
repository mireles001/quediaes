using UnityEngine;

public class Door : Interactive
{
  [SerializeField]
  private int _gotoSceneIndex;
  [SerializeField]
  private GameObject _animatedGameObject;
  private Animator _anim;

  void Start()
  {
    if (_animatedGameObject)
      _anim = _animatedGameObject.GetComponent<Animator>();
  }

  public override void Interact()
  {
    _ui.HideInteract();
    PlayerLookToMe();
    _player.PlayAnimation("interact_base");

    if (_animatedGameObject)
    {
      _anim.SetBool("open", true);
      _master.GetAudio().PlaySound("door");
    }
    else
    {
      _master.GetAudio().PlaySound();
    }
  }

  public override void EndAnimation()
  {
    GameMaster master = GameMaster.GetInstance();
    master.gameObject.GetComponent<GameProgress>()._usedDoorName = gameObject.name;

    EndInteract();
    _player.LockPlayer();
    master.GoToScene(_gotoSceneIndex);
  }
}
