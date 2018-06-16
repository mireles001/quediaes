using UnityEngine;

public class PlayerCore : MonoBehaviour
{
  private GameMaster _master;
  private GameObject _mainObj;

  private void Awake()
  {
    _mainObj = GameObject.FindWithTag("LocationMaster");
  }

  private void Start()
  {
    _master = GameMaster.GetInstance();
  }

  private void OnTriggerEnter(Collider trigger)
  {
    switch (trigger.gameObject.tag)
    {
      case "CamTrigger":
        _mainObj.SendMessage("CamTriggerEnter", trigger.gameObject.transform.parent.gameObject.name,
          SendMessageOptions.DontRequireReceiver
        );
        break;
      case "Interactive":
        _master.SetInteractive(trigger.gameObject);
        break;
      default:
        Debug.Log("Trigger detected: " + trigger.gameObject.tag);
        break;
    }
  }

  private void OnTriggerExit(Collider trigger)
  {
    if (_master.GetInteractive() != null && trigger.gameObject.tag == "Interactive" && _master.GetInteractive().name == trigger.gameObject.name)
    {
      _master.ReleaseInteractive();
    }
  }
}
