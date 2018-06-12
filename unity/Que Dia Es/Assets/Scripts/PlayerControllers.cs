using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
  public Transform _playerAvatar;
  [SerializeField]
  private bool _inputLocked = false;
  [SerializeField]
  private float _speedMove = 5f;
  [SerializeField]
  private float _speedRotation = 5f;
  [SerializeField]
  private float _repositionTolerance = 0.1f;

  private Vector3 _endPosition;
  private Quaternion _targetRotation;
  private bool _translateActive = false;
  private Ray _ray;
  private RaycastHit _hit;
  private int _layerMask;
  private Rigidbody _rb;

  // Use this for initialization
  void Start()
  {
    _layerMask = 1 << LayerMask.NameToLayer("Ground");
    _rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void LateUpdate()
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

      if (validPosition)
      {
        _translateActive = true;
      }
    }

    if (_translateActive)
    {
      if (Vector3.Distance(_endPosition, transform.position) < _repositionTolerance)
      {
        _rb.useGravity = false;
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
      _endPosition = _hit.point;
      _rb.useGravity = true;
      isValid = true;
    }

    return isValid;
  }

  public void LookTowards(Vector3 looAt)
  {
    _targetRotation = Quaternion.LookRotation((looAt - _playerAvatar.position).normalized);
    _targetRotation.x = 0f;
    _targetRotation.z = 0f;
    _playerAvatar.rotation = Quaternion.Lerp(_playerAvatar.rotation, _targetRotation, Time.deltaTime * _speedRotation);
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.layer != 9)
    {
      _translateActive = false;
    }
  }
}
