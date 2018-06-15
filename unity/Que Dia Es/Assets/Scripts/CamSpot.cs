using UnityEngine;
using System.Collections.Generic;

public class CamSpot : MonoBehaviour
{
  [SerializeField]
  private CameraTypes _type;
  [SerializeField]
  private LookAtTypes _lookAt;
  private bool _lookToPlayer = false;
  private bool _lookToPivot = false;
  [SerializeField]
  private float _cameraSpeed = 5F;
  private GameObject _pivotHolder;

  // Free Camera
  private bool _freeCamera = false;
  [SerializeField]
  private float _freeCameraDistance;
  private float _freeCameraDistanceDefault = 4F;
  [SerializeField]
  private float _freeCameraHeight;
  private float _freeCameraHeightDefault = 2F;

  //Rail Camera
  private bool _hasRail = false;
  private float _totalRailDistance;
  private GameObject _railDot;
  private BezierSpline _railPath;
  private List<float> _railSegments = new List<float>();
  [SerializeField]
  private List<Vector3> _railTriggers = new List<Vector3>();

  [SerializeField]
  private bool _smoothCameraSwap = true;
  private bool _cameraAtZero = false;
  private bool _blnGotoValidPoint = false;
  private bool _itsOn = false;
  private Camera _mainCamera;
  private Transform _player;
  private Transform _playerCamTarget;
  private Transform _cam;

  // Use this for initialization
  void Awake()
  {
    GameObject playerObj = GameObject.FindWithTag("Player");
    _mainCamera = Camera.main;

    if (playerObj)
    {
      _player = playerObj.transform;

      GameObject goPlayerCamTarget = GameObject.Find(playerObj.name + "/CameraTarget");

      if (goPlayerCamTarget)
      {
        _playerCamTarget = goPlayerCamTarget.transform;
      }
      else
      {
        _playerCamTarget = _player;
      }
    }
    else
    {
      Debug.Log("No player detected at " + gameObject.name + "!");
    }

    switch (_type.GetHashCode())
    {
      case 1:
        _freeCamera = true;
        break;
      case 2:
        _hasRail = true;
        break;
    }

    switch (_lookAt.GetHashCode())
    {
      case 1:
        _lookToPlayer = true;
        break;
      case 2:
        _lookToPivot = true;
        break;
    }

    if (_freeCamera && _lookToPlayer)
    {
      _lookToPlayer = false;
      Debug.Log("Cannot have Free Camera & Look to Player at Camspot: " + gameObject.name);
    }
  }

  void Start()
  {
    string camHolder = Helpers.GetGameObjectPath(transform) + "/cam_holder";
    GameObject cameraHolder = GameObject.Find(camHolder);
    _cam = cameraHolder.transform;

    Destroy(GameObject.Find(camHolder + "/cam_marker"));

    // Check if we will follow a pivot and if we have one
    // assigned for...
    string pathPivot = Helpers.GetGameObjectPath(transform) + "/pivot_holder";
    _pivotHolder = GameObject.Find(pathPivot);

    if (!_lookToPivot && _pivotHolder)
    {
      Destroy(_pivotHolder);
    }
    else if (_lookToPivot)
    {
      if (_pivotHolder)
      {
        Destroy(GameObject.Find(pathPivot + "/pivot_marker"));
      }
      else
      {
        _lookToPivot = false;
      }
    }

    string pathRail = Helpers.GetGameObjectPath(transform) + "/rail_holder";
    GameObject railHolder = GameObject.Find(pathRail);
    if (_hasRail)
    {
      _railDot = new GameObject("rail_dot");
      _railDot.transform.parent = transform;

      if (railHolder && _railTriggers.Count > 1)
      {
        _railPath = railHolder.GetComponent<BezierSpline>();
        GetTotalDistance();
      }
      else
      {
        _hasRail = false;
      }
    }
    else
    {
      Destroy(railHolder);
    }
  }

  private void LateUpdate()
  {
    if (_itsOn && _player)
    {
      if (_freeCamera || _hasRail)
      {
        Vector3 newPosition = Vector3.zero;
        if (_freeCamera)
        {
          if (_freeCameraDistance == 0F) _freeCameraDistance = _freeCameraDistanceDefault;

          if (_freeCameraHeight == 0F) _freeCameraHeight = _freeCameraHeightDefault;

          newPosition = _player.position + _cam.TransformDirection(new Vector3(0, _freeCameraHeight, -_freeCameraDistance));
        }
        else if (_hasRail)
        {
          newPosition = _railPath.GetPoint(GetRailDistance(_player.position));
        }

        if (!_blnGotoValidPoint)
        {
          _cam.position = newPosition;
          _blnGotoValidPoint = true;
        }
        else
        {
          _cam.position = Vector3.Lerp(_cam.position, newPosition, Time.deltaTime * _cameraSpeed);
        }
      }

      if (_lookToPlayer)
      {
        _cam.LookAt(_playerCamTarget);
      }
      else if (_lookToPivot)
      {
        _cam.LookAt(_pivotHolder.transform);
      }
    }

    if (_smoothCameraSwap && !_cameraAtZero)
    {
      _mainCamera.transform.localPosition = Vector3.Lerp(_mainCamera.transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * _cameraSpeed);

      _mainCamera.transform.localRotation = Quaternion.Lerp(_mainCamera.transform.localRotation, Quaternion.identity, Time.deltaTime * _cameraSpeed);

      if (Mathf.Approximately(_mainCamera.transform.localPosition.magnitude, Vector3.zero.magnitude))
      {
        _cameraAtZero = true;
      }
    }
  }

  public void CameraOn()
  {
    _mainCamera.transform.parent = _cam;

    if (!_smoothCameraSwap)
    {
      _mainCamera.transform.localPosition = new Vector3(0, 0, 0);
      _mainCamera.transform.localRotation = Quaternion.identity;
      _cameraAtZero = true;
    }

    _itsOn = true;
  }

  public void CameraOff()
  {
    _cameraAtZero = false;
    _blnGotoValidPoint = false;
    _itsOn = false;
  }

  // Used in Rail Camera --------------- INI
  private Vector3 ClosestPoint(Vector3 limit1, Vector3 limit2, Vector3 point)
  {
    Vector3 lineVector = limit2 - limit1;
    float lineVectorSqrMag = lineVector.sqrMagnitude;

    if (Mathf.Approximately(lineVectorSqrMag, 0F)) return limit1;

    float dotProduct = Vector3.Dot(lineVector, limit1 - point);
    float t = -dotProduct / lineVectorSqrMag;

    return limit1 + Mathf.Clamp01(t) * lineVector;
  }

  private void GetTotalDistance()
  {
    _totalRailDistance = 0F;
    for (int i = 0; i < _railTriggers.Count - 1; i++)
    {
      float localDistance = Vector3.Distance(_railTriggers[i] + transform.position, _railTriggers[i + 1] + transform.position);
      _railSegments.Add(localDistance);
      _totalRailDistance += localDistance;
    }
  }

  private float GetRailDistance(Vector3 playerPosition)
  {
    float currentRailDistance = 0F;
    int closestSegment = 0;
    Vector3 closestSegmentPoint = Vector3.zero;
    float closestDistance = Mathf.Infinity;

    for (int i = 0; i < _railTriggers.Count - 1; i++)
    {
      Vector3 segmentPoint = ClosestPoint(_railTriggers[i] + transform.position, _railTriggers[i + 1] + transform.position, playerPosition);
      float segmentDistance = Vector3.Distance(playerPosition, segmentPoint);
      if (segmentDistance < closestDistance)
      {
        closestDistance = segmentDistance;
        closestSegmentPoint = segmentPoint;
        closestSegment = i;
      }
    }

    for (int i = 0; i < closestSegment; i++)
      currentRailDistance += _railSegments[i];

    currentRailDistance += Vector3.Distance(_railTriggers[closestSegment] + transform.position, closestSegmentPoint);

    _railDot.transform.position = closestSegmentPoint;

    return Mathf.Clamp(currentRailDistance / _totalRailDistance, 0F, 1F);
  }
  // Used in Rail Camera --------------- END

  private void OnDrawGizmos()
  {
    if (_railTriggers.Count > 1)
    {
      Gizmos.color = Color.blue;
      if (_itsOn) Gizmos.color = Color.green;

      for (int i = 0; i < _railTriggers.Count - 1; i++)
        Gizmos.DrawLine(_railTriggers[i] + transform.position, _railTriggers[i + 1] + transform.position);

      if (_hasRail && _player && _itsOn)
      {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_player.position, _railDot.transform.position);
      }
    }
  }
}

public enum CameraTypes
{
  Fixed,
  Free,
  Rail
}

public enum LookAtTypes
{
  Nothing,
  Player,
  Pivot
}
