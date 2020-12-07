﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionUIScript : MonoBehaviour
{
    public GameObject[] pages;
    public int currentPage = 0;
    public GameObject nextButton;
    public GameObject prevButton;

    // Start is called before the first frame update
    void Start()
    {
        checkPage();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void nextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            checkPage();
            pages[currentPage].SetActive(true);
            print("next page " + currentPage);
        }
    }

    public void prevPage()
    {
        if (currentPage != 0)
        {
            pages[currentPage].SetActive(false);
            currentPage--;
            checkPage();
            pages[currentPage].SetActive(true);
            print("prev page " + currentPage);

        }
    }

    void checkPage()
    {
        if (currentPage == 0)
        {
            prevButton.SetActive(false);
        }
        else
        {
            prevButton.SetActive(true);
        }
        if (currentPage == pages.Length - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }
}
