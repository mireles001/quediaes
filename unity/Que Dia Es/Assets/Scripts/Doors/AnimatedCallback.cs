using UnityEngine;

public class AnimatedCallback : MonoBehaviour
{
  [SerializeField]
  private GameObject _go;

  public void AnimatedCompleted()
  {
    _go.SendMessage("AnimatedCompleted", null, SendMessageOptions.DontRequireReceiver);
  }

}
