using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    [SerializeField] public GameObject FinishScreen;
    [SerializeField] public GameObject DieScreen;


    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private ParticleSystem _gameOverParticle;
    [SerializeField] private ParticleSystem _gameOverParticle2;

    public bool IsStart;
    public bool IsDie;


    public static UI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        _particle.Play();
        IsStart = true;
        _startScreen.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void DieScreenActive()
    {
        DieScreen.SetActive(true);
        FinishSystem.Instance.GetComponent<BoxCollider>().isTrigger = false;
    }

    public IEnumerator CO_PlayParticle()
    {
        yield return new WaitForSeconds(2);

        _gameOverParticle.Play();
        _gameOverParticle2.Play();
    }
}
