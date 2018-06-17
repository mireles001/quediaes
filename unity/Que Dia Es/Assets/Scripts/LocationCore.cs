using UnityEngine;

public class LocationCore : MonoBehaviour
{
  private void Awake()
  {
    GameMaster.GetInstance().ActivatePlayerAndUI();
  }

  private void Start()
  {
    GameObject player = GameObject.FindWithTag("Player");

    if (player)
    {
      player.GetComponent<PlayerCore>().LocationLoaded();
    }
  }
}
