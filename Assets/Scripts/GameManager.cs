using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public List<Route> readyRoute = new();

    private int totalRoute;
    private int successfulParks;

    //event
    public UnityAction<Route> OnCarEnterPark;
    public UnityAction OnCarCollision;
    


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        totalRoute = transform.GetComponentsInChildren<Route>().Length;

        OnCarEnterPark += OnCarEnterParkHandler;
        OnCarCollision += OnCarCollisionHandler;
    }

    private void OnCarCollisionHandler()
    {
       
        Debug.Log("Game Over");
        
        DOVirtual.DelayedCall(2f, () =>
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
           
            SceneManager.LoadScene(currentLevel);
           
        });
    }

    private void OnCarEnterParkHandler(Route route)
    {
        route.car.StopDancingAnim();
        successfulParks++;

        if (successfulParks == totalRoute)
        {
            Debug.Log("Winer");
            UnlockLevel();
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

            DOVirtual.DelayedCall(1.3f, () =>
            {
                if (nextLevel < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextLevel);
                }
                else
                {
                    Debug.Log("No next Level to load");
                }
            });
        }
    }

    public void RegisterRoute(Route route)
    {
        readyRoute.Add(route);

        if (readyRoute.Count == totalRoute)
            MoveAllCar();
    }

    private void MoveAllCar()
    {
        foreach (var route in readyRoute)
        {
            route.car.Move(route.linePoints);
        }
    }

    void UnlockLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1)+1);
            PlayerPrefs.Save();

        }
    }
}
