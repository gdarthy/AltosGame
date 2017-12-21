using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Toggles : MonoBehaviour
{

    List<Toggle> _toggles { get; set; }
    public Transform toggles;

    // Use this for initialization
    void Start()
    {
        _toggles = new List<Toggle>();
        foreach (Transform toggle in toggles)
        {
            _toggles.Add(toggle.GetComponent<Toggle>());
        }
    }

    public void SetActiveToggle(string toggleName)
    {
        foreach (Toggle _toggle in _toggles)
        {
            if (_toggle.transform.name == toggleName)
            {
                _toggle.GetComponent<InventoryToggle>().SetActive();
            }
        }
    }

    public void ResetFavoriteToggles()
    {
        foreach (Toggle _toggle in _toggles)
        {
            _toggle.GetComponent<InventoryToggle>().ResetFavorite(false);
        }
    }

    public void SetFavoriteToggles()
    {
        foreach (Toggle _toggle in _toggles)
        {
            _toggle.GetComponent<InventoryToggle>().ResetFavorite(_toggle.GetComponent<InventoryToggle>().inFavorites);
        }
    }
    public void ResetActive()
    {
        foreach (Toggle toggle in _toggles)
        {
            toggle.GetComponent<InventoryToggle>().SetInactive();
        }
    }

}
