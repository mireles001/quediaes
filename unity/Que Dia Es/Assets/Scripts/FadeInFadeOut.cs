using UnityEngine;

/*
 * Using OnGUI we draw a black rectangle that covers
 * all contents as courtines.
 * This class works along with GameMaster component.
 * Once fadeIn is done, it callsback master so it loads
 * destination scene. After loading the new scene master
 * tells this component to initiate fadeOut. Finally after the back rectangle is gone, we tell master we are done, and we destroy this gameobject. Adios!
 */
public class FadeInFadeOut : MonoBehaviour
{
  private GameMaster _master;
  private int _destinationScene;
  private float _fIntroDuration = 0.5f;
  private float _fOutroDuration = 0.5f;
  private float _fCurrentDuration;
  private float _fAlpha;
  private bool _blnTweenDone;
  private bool _blnIntroDone;
  private bool _blnOutroDone;
  private bool _blnStartAnimation;
  private Color _solidColor;
  private Texture2D _solidRect;

  void Awake()
  {
    _master = GameMaster.GetInstance();
    DontDestroyOnLoad(transform.gameObject);
    _blnTweenDone = false;
    _blnIntroDone = false;
    _blnOutroDone = false;
    _blnStartAnimation = false;
    _solidColor = Color.black;
    _solidRect = new Texture2D(1, 1);
    _solidRect.SetPixel(0, 0, Color.red);
    _solidRect.Apply();
  }

  // STEP 1: Fade in
  public void FadeIn(int sceneIndex)
  {
    _destinationScene = sceneIndex;
    _fCurrentDuration = 0f;
    _blnStartAnimation = true;
  }

  // STEP 3: After master obj loads destination scene, we start Fading out
  public void FadeOut()
  {
    _fCurrentDuration = 0f;
    _blnStartAnimation = true;
    _blnTweenDone = false;
    _blnIntroDone = true;
  }

  // Draws the fading in&out black rectangle
  void OnGUI()
  {
    if (_blnStartAnimation)
    {
      GUI.depth = -1;

      Rect rectIntroSize = new Rect(0, 0, Screen.width, Screen.height);

      _solidColor.a = _fAlpha;
      GUI.color = _solidColor;
      GUI.DrawTexture(rectIntroSize, _solidRect);
    }
  }

  void LateUpdate()
  {
    if (_blnStartAnimation)
    {
      _fCurrentDuration += Time.deltaTime;

      // Intro modificator
      if (!_blnTweenDone && !_blnIntroDone)
      {
        _fAlpha = Mathf.Lerp(0, 1, _fCurrentDuration / _fIntroDuration);
        if (_fAlpha == 1)
        {
          // STEP 2: Fade in callback to GameMaster obj
          _blnTweenDone = true;
          _blnIntroDone = true;
          // Sends callback to parent gobject telling fadein animation is done
          _master.GetSceneLoader().FadeInCallback(_destinationScene);
        }
      }

      // Outro modificator
      if (!_blnTweenDone && _blnIntroDone && !_blnOutroDone)
      {
        _fAlpha = Mathf.Lerp(1, 0, _fCurrentDuration / _fOutroDuration);
        if (_fAlpha == 0)
        {
          // STEP 4: Fade out ends
          // Sends callback to parent gobject telling fadeout animation is done
          _master.GetSceneLoader().FadeOutCallback();
        }
      }
    }
  }
}
