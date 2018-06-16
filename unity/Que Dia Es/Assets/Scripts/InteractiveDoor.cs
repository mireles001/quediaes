using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{
  [SerializeField]
  private int _goToSceneIndex = 0;
  [SerializeField]
  private string _spawnPointName;
  [SerializeField]
  private bool _locked = false;
  [SerializeField]
  private GameObject _doorAsset;
  private GameMaster _master;

  private void Awake()
  {
    _master = GameMaster.GetInstance();
  }

  private void Start()
  {
    if (_master.GetPhase() != 0 && _master.GetSpawnPoint() != "" && _master.GetSpawnPoint() == _spawnPointName)
    {
      if (_doorAsset)
      {
        CloseDoor();
      }
    }
  }

  public void Use()
  {
    if (!_locked)
    {
      _master.SetSpawnPoint(_spawnPointName);

      if (_doorAsset)
      {
        OpenDoorFinished();
      }
      else
      {
        _master.GoToScene(_goToSceneIndex);
      }
    }
    else
    {

    }
  }

  private void OpenDoorFinished()
  {
    _master.GoToScene(_goToSceneIndex);
  }

  public void CloseDoor()
  {

  }
}
