using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class RhythmManager : MonoBehaviour
{
    public bool hasStarted;
    public static RhythmManager Instance;
    public EventReference Song;
    public EventInstance songInstance;
    private float intensity = 0f;
    [FMODUnity.ParamRef] public string param1 = "Parameter 1";
    
    void Awake()
    {
        songInstance = RuntimeManager.CreateInstance(Song);
        Instance = this;
    }

    void Start()
    {
        songInstance.setParameterByName("Parameter 1", intensity);
    }

    void Update()
    {
        //Debug.Log(Time());
        if (!hasStarted && GameManager.Instance.getState() == GameManager.State.Battling)
        {
            //CameraManager.Instance.moveCameraTo(500, 0);
            hasStarted = true;
            songInstance.start();
        }
        if (hasStarted && GameManager.Instance.getState() == GameManager.State.Roaming)
        {
            hasStarted = false;
            songInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        if (BattleManager.Instance.state == BattleManager.battleState.ATTACKING)
        {
            intensity = Mathf.MoveTowards(intensity, 1f, Time.deltaTime);
            songInstance.setParameterByName("Parameter 1", intensity);
        }
        else
        {
            intensity = Mathf.MoveTowards(intensity, 0f, Time.deltaTime);
            songInstance.setParameterByName("Parameter 1", intensity);            
        }
    }

    public float rhythmTime()
    {
        songInstance.getTimelinePosition(out int ms);
        return ms/1000f;
    }
}