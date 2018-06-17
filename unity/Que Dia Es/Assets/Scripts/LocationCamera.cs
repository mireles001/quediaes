using UnityEngine;

public class LocationCamera : MonoBehaviour
{
  private GameObject _camSpotsHolder;
  private GameObject _currentCameraHolder;

  private void Awake()
  {
    _camSpotsHolder = GameObject.FindWithTag("CamspotsHolder");
  }

  public void SwapCamera(string cameraName)
  {
    if (_camSpotsHolder)
    {
      if (_currentCameraHolder)
        _currentCameraHolder.SendMessage("CameraOff");

      _currentCameraHolder = GameObject.Find(_camSpotsHolder.name + "/" + cameraName) as GameObject;
      _currentCameraHolder.SendMessage("CameraOn");
    }
  }

  public void CamTriggerEnter(string cameraName)
  {
    if (_currentCameraHolder && _currentCameraHolder.name != cameraName)
      SwapCamera(cameraName);
    else if (!_currentCameraHolder)
      SwapCamera(cameraName);
  }
}
