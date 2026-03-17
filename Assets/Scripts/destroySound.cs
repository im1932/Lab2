using System.Collections.Generic;
using UnityEngine;

public class PrefabSound : MonoBehaviour
{
    [SerializeField] private GameObject[] watchedPrefabs;
    [SerializeField] private AudioSource audioSource;

    private readonly HashSet<int> knownObjects = new HashSet<int>();

    private void Start()
    {
        CacheCurrentObjects();
    }

    private void Update()
    {
        CheckDestroyedObjects();
    }

    private void CacheCurrentObjects()
    {
        knownObjects.Clear();

        Transform[] allTransforms = FindObjectsOfType<Transform>(true);
        foreach (Transform t in allTransforms)
        {
            if (IsWatchedObject(t.gameObject))
            {
                knownObjects.Add(t.gameObject.GetInstanceID());
            }
        }
    }

    private void CheckDestroyedObjects()
    {
        HashSet<int> currentObjects = new HashSet<int>();

        Transform[] allTransforms = FindObjectsOfType<Transform>(true);
        foreach (Transform t in allTransforms)
        {
            GameObject obj = t.gameObject;
            if (IsWatchedObject(obj))
            {
                currentObjects.Add(obj.GetInstanceID());
            }
        }

        foreach (int id in knownObjects)
        {
            if (!currentObjects.Contains(id))
            {
                PlayDestroySound();
            }
        }

        knownObjects.Clear();
        foreach (int id in currentObjects)
        {
            knownObjects.Add(id);
        }
    }

    private bool IsWatchedObject(GameObject obj)
    {
        if (watchedPrefabs == null || watchedPrefabs.Length == 0)
            return false;

        string objectName = obj.name;

        for (int i = 0; i < watchedPrefabs.Length; i++)
        {
            if (watchedPrefabs[i] == null)
                continue;

            string prefabName = watchedPrefabs[i].name;

            if (objectName == prefabName || objectName == prefabName + "(Clone)")
                return true;
        }

        return false;
    }

    private void PlayDestroySound()
    {
        if (audioSource == null || audioSource.clip == null)
            return;

        audioSource.PlayOneShot(audioSource.clip);
    }
}