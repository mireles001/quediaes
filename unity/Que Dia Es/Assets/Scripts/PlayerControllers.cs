using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
  [SerializeField]
  private float _speedMove = 5f;
  [SerializeField]
  private float _speedRotation = 10f;
  [SerializeField]
  private float _repositionTolerance = 0.1f;

  private bool _inputLocked = false;
  private bool _translateActive = false;
  private bool _validInput = true;
  private int _layerMask;
  private Vector3 _endPosition;
  private Quaternion _targetRotation;
  private Ray _ray;
  private RaycastHit _hit;
  private Transform _avatarHolder;
  private Rigidbody _rb;

  void Awake()
  {
    _layerMask = 1 << LayerMask.NameToLayer("Ground");
    _avatarHolder = GetComponent<PlayerCore>().GetAvatarHolder();
    _rb = GetComponent<Rigidbody>();
  }

  void Update()
  {
    if (!_inputLocked)
    {
      bool validPosition = false;
      if (Input.GetMouseButtonUp(0))
      {
        validPosition = CheckPosition(Input.mousePosition);
      }
      else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
      {
        validPosition = CheckPosition(Input.GetTouch(0).position);
      }

      if (validPosition && _validInput)
      {
        _translateActive = true;
      }

      _validInput = true;
    }
    else
    {
      if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
      {
        _validInput = false;
      }
    }

    if (_translateActive)
    {
      if (Vector3.Distance(_endPosition, transform.position) < _repositionTolerance)
      {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _translateActive = false;
      }
      else
      {
        transform.position += (_endPosition - transform.position).normalized * _speedMove * Time.deltaTime;
        LookTowards(_endPosition);
      }
    }
  }

  private bool CheckPosition(Vector2 pos)
  {
    bool isValid = false;
    _ray = Camera.main.ScreenPointToRay(pos);
    if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _layerMask))
    {
      _rb.useGravity = false;
      _endPosition = _hit.point;
      isValid = true;
    }

    return isValid;
  }

  public void LockPlayer()
  {
    _inputLocked = true;
  }

  public void UnlockPlayer()
  {
    _inputLocked = false;
  }

  public void LookTowards(Vector3 looAt)
  {
    _targetRotation = Quaternion.LookRotation((looAt - _avatarHolder.position).normalized);
    _targetRotation.x = 0f;
    _targetRotation.z = 0f;
    _avatarHolder.rotation = Quaternion.Lerp(_avatarHolder.rotation, _targetRotation, Time.deltaTime * _speedRotation);
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.layer != 9)
    {
      _translateActive = false;
    }
  }
}
