using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Product : MonoBehaviour, IPointerClickHandler
{
    public Image imgProduct;
    public Text txtTitle;
    public Text txtDesc;
    public Text txtCost;
    private SO_Product product;
    
    public void LoadProduct(SO_Product myProduct)
    {
        product = myProduct;
        imgProduct.sprite = product.icon;
        txtTitle.text = product.title;
        txtDesc.text = product.shortDescription;
        txtCost.text = product.cost.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<CatalogController>().OpenProductDetail(product);
    }
}
