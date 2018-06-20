using System.Collections;
using UnityEngine;

public class AudioCore : MonoBehaviour
{
  [SerializeField]
  private AudioClip _uiBeep;

  [SerializeField]
  private AudioClip _fxDoor;
  [SerializeField]
  private AudioClip _fxLock;

  [SerializeField]
  private AudioClip _fxGet;
  [SerializeField]
  private AudioClip _fxHammer;
  [SerializeField]
  private AudioClip _fxSword;
  [SerializeField]
  private AudioClip _fxPotion;

  [SerializeField]
  private AudioClip _trackIntro;
  [SerializeField]
  private AudioClip _trackMain;
  [SerializeField]
  private AudioClip _trackSecret;
  [SerializeField]
  private AudioClip _trackVictoryIntro;
  [SerializeField]
  private AudioClip _trackVictoryLoop;

  private string _themePlaying;

  [SerializeField]
  private GameObject _sound;
  private AudioSource _s;
  [SerializeField]
  private GameObject _music;
  private AudioSource _m;
  private bool _introDone = false;

  private AudioClip _themeIntro;
  private AudioClip _themeLoop;
  private IEnumerator _coroutine;

  void Awake()
  {
    _s = _sound.GetComponent<AudioSource>();
    _m = _music.GetComponent<AudioSource>();
  }

  void Start()
  {
    PlayInitTheme();
  }

  public void PlaySound(string value = "ui")
  {
    switch (value)
    {
      case "door":
        _s.PlayOneShot(_fxDoor, 0.7F);
        break;
      case "lock":
        _s.PlayOneShot(_fxLock, 0.7F);
        break;
      case "get":
        _s.PlayOneShot(_fxGet, 0.7F);
        break;
      case "potion":
        _s.PlayOneShot(_fxPotion, 0.7F);
        break;
      case "hammer":
        _s.PlayOneShot(_fxHammer, 0.7F);
        break;
      case "sword":
        _s.PlayOneShot(_fxSword, 0.7F);
        break;
      default:
        _s.PlayOneShot(_uiBeep, 0.7F);
        break;
    }
  }

  public AudioSource GetSoundSource()
  {
    return _s;
  }

  public AudioSource GetMusicSource()
  {
    return _m;
  }

  //-------------------------------------

  private void PlayInitTheme()
  {
    _coroutine = PlayFirstTrack();
    StartCoroutine(_coroutine);
  }

  IEnumerator PlayFirstTrack()
  {
    _m.clip = _trackIntro;
    _m.Play();

    yield return new WaitForSeconds(_m.clip.length);

    _introDone = true;
    PlayTrack("main");
  }

  public void PlayTrack(string track = "main")
  {
    if (_introDone && track != _themePlaying)
    {
      _themePlaying = track;
      _m.volume = 1f;
      switch (track)
      {
        case "none":
          _m.Stop();
          break;
        case "secret":
          _m.Stop();
          _m.clip = _trackSecret;
          _m.loop = false;
          _m.Play();
          break;
        case "victory":
          SetClip(_trackVictoryLoop, _trackVictoryIntro);
          break;
        default:
          _m.volume = 0.5f;
          SetClip(_trackMain);
          break;
      }
    }
  }

  private void SetClip(AudioClip loop, AudioClip intro = null)
  {
    if (_coroutine != null)
    {
      StopCoroutine(_coroutine);
      _coroutine = null;
    }
    _m.Stop();
    _m.loop = false;

    _themeIntro = intro;
    _themeLoop = loop;

    if (_themeIntro != null)
    {
      _coroutine = PlayIntroLoopTrack();
      StartCoroutine(_coroutine);
    }
    else
      PlayLoop();
  }

  IEnumerator PlayIntroLoopTrack()
  {
    _m.clip = _themeIntro;
    _m.Play();

    yield return new WaitForSeconds(_m.clip.length);

    PlayLoop();
  }

  private void PlayLoop()
  {
    _m.clip = _themeLoop;
    _m.loop = true;
    _m.Play();
  }
}
