using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryToggle : MonoBehaviour, IPointerClickHandler
{
    public bool inFavorites { get; private set; }

    // TODO saving favorites toggles

    void Awake()
    {
        inFavorites = false;
        ResetFavorite(false);
    }

    public void SetActive()
    {
        GetComponent<Toggle>().isOn = true;
    }

    public void SetInactive()
    {
        GetComponent<Toggle>().isOn = false;
    }

    public void ResetFavorite(bool favorie)
    {
        transform.GetChild(2).GetComponent<Image>().enabled = favorie;
        if(favorie) SetActive();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
        {
            inFavorites = !inFavorites;
            if (inFavorites)
            {
                SetActive();
                transform.GetChild(2).GetComponent<Image>().enabled = true;
            }
            else
            {
                transform.GetChild(2).GetComponent<Image>().enabled = false;
            }
        }
    }
}
