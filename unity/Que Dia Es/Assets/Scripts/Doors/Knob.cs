using UnityEngine;

public class Knob : MonoBehaviour
{
  [SerializeField]
  private GameObject _lock;

  public void AnimationEnded()
  {
    _lock.GetComponent<LockBasement>().KnobAnimationEnd();
  }
}
