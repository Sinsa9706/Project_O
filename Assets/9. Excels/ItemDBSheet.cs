using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ItemDBSheet : ScriptableObject
{
	public List<ItemData> Items; // Replace 'EntityType' to an actual type that is serializable.
}
