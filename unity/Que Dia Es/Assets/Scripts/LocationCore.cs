using UnityEngine;

public class LocationCore : MonoBehaviour
{
  private GameObject _camSpotsHolder;
  private GameObject _currentCameraHolder;
  private GameMaster _master;

  void Awake()
  {
    _camSpotsHolder = GameObject.FindWithTag("CamspotsHolder");
  }

  void Start()
  {
    _master = GameMaster.GetInstance();
  }

  public void SwapCamera(string cameraName)
  {
    if (_camSpotsHolder)
    {
      if (_currentCameraHolder)
      {
        _currentCameraHolder.SendMessage("CameraOff");
      }

      _currentCameraHolder = GameObject.Find(_camSpotsHolder.name + "/" + cameraName) as GameObject;
      _currentCameraHolder.SendMessage("CameraOn");
    }
  }

  public void CamTriggerEnter(string cameraName)
  {
    if (_currentCameraHolder && _currentCameraHolder.name != cameraName)
    {
      SwapCamera(cameraName);
    }
    else if (!_currentCameraHolder)
    {
      SwapCamera(cameraName);
    }
  }
}
