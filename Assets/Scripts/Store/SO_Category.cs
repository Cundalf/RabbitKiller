using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FB Store/Catalog")]
public class SO_Category : ScriptableObject
{
    public enum CategoriesShop
    {
        WEAPONS, SKINS, CONSUMABLE
    }

    public CategoriesShop category;
    public string categoryName;
    public List<SO_Product> products;
}
