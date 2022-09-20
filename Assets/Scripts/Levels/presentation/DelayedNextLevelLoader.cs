using System.Collections;
using Levels.domain.repositories;
using Levels.presentation;
using UnityEngine;
using Zenject;

public class DelayedNextLevelLoader : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [Inject] private ICurrentLevelRepository currentLevelRepository;
    [Inject] private LevelLoadingController levelLoadingController;

    public void LoadDelayed()
    {
        StopAllCoroutines();
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(delay);
        var currentLevel = currentLevelRepository.GetCurrentLevel();
        levelLoadingController.LoadLevel(currentLevel.ID);
    }
}
