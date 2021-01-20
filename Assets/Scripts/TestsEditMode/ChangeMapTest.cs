using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ChangeMapTest
    {

        private GameManager gManager;
        private EnemyRespawnController eRespawnController;
        private MainMenuManager mainMenu;

        private int CANTIDAD_DE_ORDAS_LIMITE = 10;

        [Test]
        public void dadoQueLaCantidadDeOrdasSuperadasFue10SeTieneQueCargarElSiguienteMapa()
        {
            dadoQueTengoUnGameMangar();

            cuandoElNumeroDeOrdasLlegaA10();

            seEsperaQueSeCambieDeNivel();
        }

        //[Test] cambiar a play mode 
        public void dadoQueSeMuestraElMenuCuandoSeClickeaElBotonStartSeCargaElPrimerMapa()
        {

            dadoQueSeMuestraElMenuPrincipal();

            cuandoSeClikeaElBotonStart();

            seEsperaQueSeCargeElPrimerMapa();

        }

        private void dadoQueSeMuestraElMenuPrincipal()
        {
            gManager = new GameManager();
            eRespawnController = new EnemyRespawnController();
            mainMenu = new MainMenuManager();
        }

        private void dadoQueTengoUnGameMangar()
        {
            gManager = new GameManager();
            eRespawnController = new EnemyRespawnController();
        }

        private void cuandoElNumeroDeOrdasLlegaA10()
        {
            eRespawnController.setCurrentOrde(CANTIDAD_DE_ORDAS_LIMITE);
        }

        private void cuandoSeClikeaElBotonStart()
        {
            mainMenu.StartGame();
        }

        private void seEsperaQueSeCambieDeNivel()
        {
            
        }

        private void seEsperaQueSeCargeElPrimerMapa()
        {
            
        }

        //[UnityTest]
        public IEnumerator ChangeMapTestWithEnumeratorPasses()
        {
            yield return null;
        }
    }
}
