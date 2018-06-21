using UnityEngine;

public class AnimatedCallback : MonoBehaviour
{
  [SerializeField]
  private GameObject _go;

  public void SetGo(GameObject go)
  {
    _go = go;
  }

  public void AnimatedCompleted()
  {
    _go.SendMessage("AnimatedCompleted", null, SendMessageOptions.DontRequireReceiver);
  }

}
