using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Category : MonoBehaviour, IPointerClickHandler
{
    public Text txtCategoryName;
    public SO_Category.CategoriesShop CategoryType;

    private CatalogController catalogController;

    void Start()
    {
        catalogController = FindObjectOfType<CatalogController>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        catalogController.LoadProducts(CategoryType);
    }
}
