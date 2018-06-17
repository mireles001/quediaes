using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/* 
 * GameMaster additional component with the only mission of working along FadeInFadeOut gameObject to achieve transitioning between scenes smoothly
 */
public class SceneLoader : MonoBehaviour
{
  private GameObject _fadeInFadeOut;

  // STEP 1: It all starts here
  public void GoToScene(int sceneIndex)
  {
    if (_fadeInFadeOut == null)
    {
      _fadeInFadeOut = ObjectFactory.CreateFadeInFadeOut();
      _fadeInFadeOut.GetComponent<FadeInFadeOut>().FadeIn(sceneIndex);
    }
  }

  // STEP 2: After fadein is ready, we load next scene
  public void FadeInCallback(int sceneIndex)
  {
    StartCoroutine(LoadNewScene(sceneIndex));
  }

  /* 
   * STEP 4: Last stop, here fadeout is ready. We let know all root gameobjects in new scene that they are free to do their things.
   */
  public void FadeOutCallback()
  {
    Destroy(_fadeInFadeOut);
    _fadeInFadeOut = null;

    /*
    Sends a broadcast to all gameobjects in ROOT level, just to let them know fadeIn cycle is done.
    If you want something to happens at the begging of a scene, add 'fadeOutDone' function
    in a root gameobject.
    */
    GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
    foreach (GameObject go in gos)
    {
      if (go && go.transform.parent == null)
      {
        go.gameObject.SendMessage("FadeOutFinished", null, SendMessageOptions.DontRequireReceiver);
      }
    }
  }

  // STEP 3: We load next scene, and afterwards we let know fadeInFadeOut to start fading out!
  IEnumerator LoadNewScene(int sceneIndex)
  {

    AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

    while (!async.isDone)
    {
      yield return null;
    }

    if (async.isDone)
      _fadeInFadeOut.GetComponent<FadeInFadeOut>().FadeOut();
  }
}
