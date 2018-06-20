using UnityEngine;

public class LocationCore : MonoBehaviour
{
  [SerializeField]
  private string _musicTheme;

  private void Awake()
  {
    GameMaster.GetInstance().ActivatePlayerAndUI();
  }

  private void Start()
  {
    GameMaster master = GameMaster.GetInstance();
    GameObject player = master.GetPlayer();
    string usedDoor = master.gameObject.GetComponent<GameProgress>()._usedDoorName;

    master.GetAudio().PlayTrack(_musicTheme);

    if (player)
    {
      PlayerCore playerCore = player.GetComponent<PlayerCore>();
      playerCore.LocationLoaded();
      playerCore.UnlockPlayer();

      if (usedDoor != "")
      {
        GameObject door = GameObject.Find(usedDoor);

        master.gameObject.GetComponent<GameProgress>()._usedDoorName = "";

        player.transform.parent = door.transform;
        player.transform.localPosition = new Vector3(0f, 0f, 0.75f);
        player.transform.parent = master.gameObject.transform;
        player.transform.localRotation = Quaternion.identity;
        player.transform.localScale = new Vector3(1f, 1f, 1f);

        Transform avatar = player.GetComponent<PlayerCore>().GetAvatarHolder();

        avatar.parent = door.transform;
        avatar.localRotation = Quaternion.identity;

        avatar.parent = player.transform;
        avatar.localPosition = Vector3.zero;
        avatar.localScale = new Vector3(1f, 1f, 1f);
      }
    }
  }
}
