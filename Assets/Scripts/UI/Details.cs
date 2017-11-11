using UnityEngine.UI;
using UnityEngine;

public class Details : MonoBehaviour {

    Text itemName;
    Text itemDescription;
    Text itemStats;

    string defaultName, defaultDescription, defaultStats;

    void Start()
    {
        itemName = transform.GetChild(0).GetComponent<Text>();
        itemDescription = transform.GetChild(1).GetComponent<Text>();
        itemStats = transform.GetChild(2).GetComponent<Text>();
        defaultName = itemName.text;
        defaultDescription = itemDescription.text;
        defaultStats = itemStats.text;
    }

    public void SetDetails(string itemName, string itemDescription, string itemStats)
    {
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
        this.itemStats.text = itemStats;
    }
	
    public void SetDefault()
    {
        itemName.text = defaultName;
        itemDescription.text = defaultDescription;
        itemStats.text = defaultStats;
    }

}
