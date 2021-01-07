using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogController : MonoBehaviour
{
    public List<SO_Category> categories;
    public GameObject categoryOptionPrefab;
    public GameObject productPrefab;
    public GameObject ShopPanel;
    public GameObject TabPanel;
    public GameObject ProductsPanel;
    public GameObject ProductDetailGO;
    public GameObject CloseCatalogButtonGO;

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
                    pfProduct = Instantiate(productPrefab, ProductsPanel.transform);
                    pfProduct.GetComponent<Product>().LoadProduct(categories[i].products[i]);
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

    public void CloseProductDetail()
    {
        ShopPanel.SetActive(true);
        ProductDetailGO.SetActive(false);
        CloseCatalogButtonGO.SetActive(true);
    }

    public void OpenProductDetail(SO_Product product)
    {
        CloseCatalogButtonGO.SetActive(false);
        ShopPanel.SetActive(false);
        ProductDetailGO.SetActive(true);

        Transform productContent = ProductDetailGO.transform.Find("ProductContentPanel");
        productContent.Find("txtProduct").GetComponent<Text>().text = product.title;
        productContent.Find("txtDescription").GetComponent<Text>().text = product.description;
        productContent.Find("imgProduct").GetComponent<Image>().sprite = product.icon;
        productContent.Find("CostPanel").Find("txtCost").GetComponent<Text>().text = product.cost.ToString();
    }

    public void BuyProduct()
    {
        Debug.Log("Comprar!");
    }
}
