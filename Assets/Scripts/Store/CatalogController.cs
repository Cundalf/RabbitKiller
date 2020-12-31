using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogController : MonoBehaviour
{
    public List<SO_Category> categories;
    public GameObject categoryOptionPrefab;
    public GameObject productPrefab;
    public GameObject TabPanel;
    public GameObject CardPanel;
    public Sprite categoryActiveSprite;
    public Sprite categoryInactiveSprite;

    private void Start()
    {
        LoadCategories();
    }

    public void LoadCategories()
    {
        ClearStore();

        GameObject pfCategory;
        GameObject pfProduct;
        for (int i = 0; i < categories.Count; i++)
        {
            pfCategory = Instantiate(categoryOptionPrefab, TabPanel.transform);
            pfCategory.GetComponent<Category>().txtCategoryName.text = categories[i].categoryName;


            for (int j = 0; j < categories[i].products.Count; j++)
            {
                pfProduct = Instantiate(productPrefab, CardPanel.transform);
                pfProduct.GetComponent<Product>().imgProduct.sprite = categories[i].products[j].icon;
                pfProduct.GetComponent<Product>().txtTitle.text = categories[i].products[j].title;
                pfProduct.GetComponent<Product>().txtDesc.text = categories[i].products[j].shortDescription;
                pfProduct.GetComponent<Product>().txtCost.text = categories[i].products[j].cost.ToString();
            }
        }
    }

    private void ClearStore()
    {
        Product[] products = FindObjectsOfType<Product>();
        for (int i = 0; i < products.Length; i++)
        {
            Destroy(products[i].gameObject);
        }

        Category[] categories = FindObjectsOfType<Category>();
        for (int i = 0; i < categories.Length; i++)
        {
            Destroy(categories[i].gameObject);
        }
    }
}
