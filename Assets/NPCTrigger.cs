using UnityEngine;
using UnityEngine.Playables;

public class NPCTrigger : MonoBehaviour
{
    public PlayableDirector timelineDirector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the Timeline when the player enters the trigger zone
            timelineDirector.Play();
        }
    }
}
