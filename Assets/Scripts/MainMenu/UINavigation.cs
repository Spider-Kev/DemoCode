using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigation : MonoBehaviour
{
    public Canvas panel_Main;
    public Canvas panel_Store;
    public Canvas panel_Selector;

    public void ShowStore()
    {
        panel_Main.enabled = false;
        panel_Store.enabled = true;
    }

    public void ShowMainMenu()
    {
        panel_Main.enabled = true;
        panel_Store.enabled = false;
        panel_Selector.enabled = false;
    }

    public void ShowSelectorMenu()
    {
        panel_Main.enabled = false;
        panel_Selector.enabled = true;
    }
}
