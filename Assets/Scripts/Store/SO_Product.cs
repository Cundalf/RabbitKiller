﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FB Store/Product")]
public class SO_Product : ScriptableObject
{
    public SO_Category.CategoriesShop category;
    public Sprite icon;
    public string title;
    public string shortDescription;
    public int cost;
}
