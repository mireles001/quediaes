using UnityEngine;
using UnityEngine.UI;

public class TitleCore : MonoBehaviour
{
  [SerializeField]
  private Button _btn;
  [SerializeField]
  private GameObject _player;
  [SerializeField]
  private GameObject _UI;
  [SerializeField]
  private GameObject _audio;

  private void Start()
  {
    GameObject master = GameMaster.GetInstance().gameObject;
    GameObject fader = GameObject.Find("FadeInFadeOut");

    if (master) Destroy(master);
    if (fader) Destroy(fader);

    _btn.onClick.AddListener(StartGame);
  }
  private void StartGame()
  {
    Destroy(_btn);
    GameMaster master = GameMaster.GetInstance();
    master.CreatePlayerAndUI(_player, _UI, _audio);
    master.GetAudio().PlaySound();
    master.GoToScene(1);
  }
}
