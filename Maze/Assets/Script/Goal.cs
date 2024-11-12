using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;
    private Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the Goal
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found on Goal object.");
        }
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        // Check if the player collided with the Goal
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player has touched the Goal");
            // Play the audio clip attached to the AudioSource
            if (audioSource != null)
            {
                audioSource.Play();
                Debug.Log("Playing audio.");
            }
            else
            {
                Debug.LogError("No AudioSource attached or found.");
            }

            // Load the next scene after a slight delay to allow the audio to play
            StartCoroutine(LoadNextSceneAfterDelay());
        }
        else
        {
            Debug.Log("Collision with non-player object.");
        }
    }

    // Coroutine to load the next scene after the audio plays
    IEnumerator LoadNextSceneAfterDelay()
    {
        if (audioSource != null)
        {
            yield return new WaitForSeconds(audioSource.clip.length);  // Wait for the audio to finish
        }
        else
        {
            yield return new WaitForSeconds(1);  // Small fallback delay if no audio is found
        }

        Debug.Log("Current Scene:" + currentScene.name);
        if (currentScene.name.Equals("Main"))
        {
            Debug.Log("Loading next scene: Second.");
            SceneManager.LoadScene("Second");
        }
        else if (currentScene.name.Equals("Second"))
        {
            Debug.Log("Loading next scene: Third.");
            SceneManager.LoadScene("Third");
        }
        else
        {
            Debug.Log("Loading default scene: Main.");
            SceneManager.LoadScene("Main");
        }

    }

}
