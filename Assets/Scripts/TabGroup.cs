using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Color tabIdle;
    public Color tabActive;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;

    void Start()
    {
        for (int i=0; i<objectsToSwap.Count; i++) {
            objectsToSwap[i].SetActive(false);
        }
    }

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null && selectedTab == button)
        {
            button.background.color = tabIdle;
            objectsToSwap[button.transform.GetSiblingIndex()].SetActive(false);
            selectedTab = null;
        }
        else 
        {
            selectedTab = button;
            ResetTabs();
            button.background.color = tabActive;
            int index = button.transform.GetSiblingIndex();

            for(int i=0; i<objectsToSwap.Count; i++)
            {
                if (i == index)
                {
                    objectsToSwap[i].SetActive(true);
                }
                else 
                {
                    
                    objectsToSwap[i].SetActive(false);
                }
            }

            if (button.notificationActive == true)
            {
                ClearNotification(tabButtons.IndexOf(button));
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            button.background.color = tabIdle;
        }
    }

    public void Notify(int i)
    {
        if (!tabButtons[i].transform.GetChild (1).gameObject.active)
        {
            tabButtons[i].transform.GetChild (1).gameObject.SetActive(true);
            tabButtons[i].notificationActive = true;
        }
    }

    public void ClearNotification(int i)
    {
        tabButtons[i].transform.GetChild (1).gameObject.SetActive(false);
        tabButtons[i].notificationActive = false;
    }
}
