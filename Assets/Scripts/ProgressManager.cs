using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }

    private List<IResettable> resettableObjects = new List<IResettable>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        FindAllResettables();
    }

    private void FindAllResettables()
    {
        resettableObjects.Clear();
        resettableObjects.AddRange(FindObjectsOfType<MonoBehaviour>().Where(obj => obj is IResettable).Cast<IResettable>());
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        foreach (var resettable in resettableObjects)
        {
            resettable.ResetProgress();
        }

        Debug.Log("Игровой прогресс сброшен!");
    }
}

