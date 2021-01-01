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

    private void Start()
    {
        if (categories.Count > 0) {
            LoadCategories();
            LoadProducts(categories[0].category);
        }
    }

    public void LoadCategories()
    {
        ClearStore();

        GameObject pfCategory;
        for (int i = 0; i < categories.Count; i++)
        {
            pfCategory = Instantiate(categoryOptionPrefab, TabPanel.transform);
            pfCategory.GetComponent<Category>().txtCategoryName.text = categories[i].categoryName;
            pfCategory.GetComponent<Category>().CategoryType = categories[i].category;
            // Se marca como inactiva
            //pfCategory.GetComponent<Image>().color = new Color(255, 255, 255, 100);
        }
    }

    public void LoadProducts(SO_Category.CategoriesShop categoryType)
    {
        ClearProducts();
        UpdateCategoryActive(categoryType);

        GameObject pfProduct;

        for (int i = 0; i < categories.Count; i++)
        {
            if (categories[i].category == categoryType)
            {
                for (int j = 0; j < categories[i].products.Count; j++)
                {
                    pfProduct = Instantiate(productPrefab, CardPanel.transform);
                    pfProduct.GetComponent<Product>().imgProduct.sprite = categories[i].products[j].icon;
                    pfProduct.GetComponent<Product>().txtTitle.text = categories[i].products[j].title;
                    pfProduct.GetComponent<Product>().txtDesc.text = categories[i].products[j].shortDescription;
                    pfProduct.GetComponent<Product>().txtCost.text = categories[i].products[j].cost.ToString();
                    pfProduct.GetComponent<Product>().category = categories[i].products[j].category;
                }
            }
        }
    }

    private void UpdateCategoryActive(SO_Category.CategoriesShop categoryActive)
    {
        Category[] categories = FindObjectsOfType<Category>();
        for (int i = 0; i < categories.Length; i++)
        {
            if (categories[i].GetComponent<Category>().CategoryType == categoryActive)
            {
                categories[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                categories[i].GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            }
        }
    }

    private void ClearProducts()
    {
        Product[] products = FindObjectsOfType<Product>();
        for (int i = 0; i < products.Length; i++)
        {
            Destroy(products[i].gameObject);
        }
    }

    private void ClearStore()
    {
        ClearProducts();

        Category[] categories = FindObjectsOfType<Category>();
        for (int i = 0; i < categories.Length; i++)
        {
            Destroy(categories[i].gameObject);
        }
    }
}
