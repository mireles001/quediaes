using UnityEngine;

public class LockLadder : Interactive
{
  [SerializeField]
  private GameObject _ladder;
  [SerializeField]
  private GameObject _stringGo;
  private Animator _stringAnim;
  [SerializeField]
  private GameObject _ladderGo;
  private Animator _ladderAnim;

  void Start()
  {
    if (!_progress._stairsDown)
    {
      _ladder.GetComponent<SphereCollider>().enabled = false;
      _ladderGo.SetActive(false);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public override void Interact()
  {
    _player.StopAvatar();
    _master.GetAudio().PlaySound();
    _ui.HideInteract();
    PlayerLookToMe();
    _ui.ShowPopup("option", TextContent.LADDER_OPTION);
  }

  public override void EndAnimation()
  {
    _stringAnim = _stringGo.GetComponent<Animator>();
    _stringAnim.Play("string_pull");
  }

  public override void ClosePopup(bool result)
  {
    if (result)
    {
      if (!_progress._stairsDown)
      {
        _master.GetAudio().PlaySound("lock");
        _player.PlayAnimation("interact_reach");
      }
      else
      {
        _ladder.GetComponent<SphereCollider>().enabled = true;
        EndInteract();

        Destroy(gameObject);
      }
    }
    else
    {
      EndInteract();
    }
  }

  public void AnimatedCompleted()
  {
    if (!_progress._stairsDown)
    {
      _progress._stairsDown = true;
      _ladderGo.SetActive(true);
      _ladderAnim = _ladderGo.GetComponent<Animator>();
      _master.GetAudio().PlaySound("door");
      _ladderAnim.Play("ladder_anim");
    }
    else
    {
      Invoke("Delayer", 1f);
    }
  }

  private void Delayer()
  {
    _ui.ShowPopup("text", TextContent.LADDER_TEXT);
  }
}
