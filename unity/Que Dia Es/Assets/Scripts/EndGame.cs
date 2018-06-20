using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
  [SerializeField]
  private Button _btn;
  private GameMaster _master;

  void Awake()
  {
    _master = GameMaster.GetInstance();
    Destroy(_master.GetPlayer());
    Destroy(_master.GetUI());
  }

  void Start()
  {

    _btn.onClick.AddListener(TerminateGame);
  }

  private void TerminateGame()
  {
    _master.GetAudio().PlaySound("potion");
    _master.GoToScene(0);
  }
}
