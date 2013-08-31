using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {

	public string itemName;
	public string description;
	public int cost;
	public GameObject placeableObject;
	public RoomType roomType;
	
    public UILabel itemNameLabel;
	public UILabel itemCostLabel;
	public UILabel itemDescLabel;
	
	private Vector2 _position;
	private int _sellPrice;
	
	void Start()
	{
		itemNameLabel.text = itemName;	
		itemCostLabel.text = cost.ToString();
		itemDescLabel.text = description;
	}
}
