
using System.Collections;

using System.Threading;

using UnityEngine;


public class FramesPerSecond : MonoBehaviour

{

  #region Variables
  [SerializeField] private Color textColor = Color.red;

  private float TargetRate = 30.0f;
  Rect fpsRect;
  GUIStyle style;
  float fps = 0.0f;
  float currentFrameTime;
  
  #endregion


  #region Unity callbacks
  
  //----------------------------------------------------------------------------
  void Start()

  {

    fpsRect = new Rect(50, 50, 400, 100);
    style = new GUIStyle();
    style.fontSize = 30;
    style.normal.textColor = textColor;
    
    QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = 9999;
    currentFrameTime = Time.realtimeSinceStartup;
    
    StartCoroutine(RecalculateFPS());
    StartCoroutine(WaitForNextFrame());

  }
  
  //----------------------------------------------------------------------------

  private void OnGUI()
  {
    GUI.Label(fpsRect, "FPS: " + string.Format("{0:0.0}", fps), style);
  }
  #endregion


  #region Force And [Re]Calculate FPS
  //----------------------------------------------------------------------------

  private IEnumerator RecalculateFPS()
  {
    while (true)
    {
      yield return new WaitForSeconds(1);
      fps = 1.0f / Time.deltaTime;
    }
  }

  
  //----------------------------------------------------------------------------

  private IEnumerator WaitForNextFrame()
  {
    while (true)
    {
      yield return new WaitForEndOfFrame();

      currentFrameTime += 1.0f / TargetRate;

      var t = Time.realtimeSinceStartup;
      var sleepTime = currentFrameTime - t - 0.01f;
      
      if (sleepTime > 0) Thread.Sleep((int)(sleepTime * 1000));


      while (t < currentFrameTime) t = Time.realtimeSinceStartup;
    }
  }
  
  #endregion

}



