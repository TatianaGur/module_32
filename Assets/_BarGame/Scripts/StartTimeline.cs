using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{
    private PlayableDirector _plDirector;

    private void Start()
    {
        _plDirector = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _plDirector.Play();
        }
    }
}
