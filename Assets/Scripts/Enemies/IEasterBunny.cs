using UnityEngine;
public interface IEasterBunny : IEnemyController
{
    GameObject slavePrefab { get; set; }
    GameObject respanPoint1 { get; set; }
    GameObject respanPoint2 { get; set; } 
    GameObject respanPoint3 { get; set; } 
    GameObject respanPoint4 { get; set; } 

    int healtBarAmount { get; set; }

    void Die();
    void healtControl();
    void movePNJ();
    void Start();
    void Update();
}