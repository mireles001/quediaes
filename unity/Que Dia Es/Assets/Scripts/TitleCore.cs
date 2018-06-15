using UnityEngine;
using UnityEngine.UI;

public class TitleCore : MonoBehaviour
{
  [SerializeField]
  private Button _btn;

  private void Start()
  {
    _btn.onClick.AddListener(StartGame);
  }
  private void StartGame()
  {
    Destroy(_btn);
    GameMaster.GetInstance().GoToScene(1);
  }
}
