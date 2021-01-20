using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
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
            this.dadoQueTengoUnGameMangar();
            mainMenu = new MainMenuManager();
        }

        private void dadoQueTengoUnGameMangar()
        {
            gManager = Substitute.ForPartsOf<GameManager>();
            eRespawnController = new EnemyRespawnController();
            eRespawnController.setGameManager(this.gManager);
            eRespawnController.setOrdeChangMap(CANTIDAD_DE_ORDAS_LIMITE);
        }

        private void cuandoElNumeroDeOrdasLlegaA10()
        {
            gManager.When(x => x.nexMap()).DoNotCallBase();
            eRespawnController.setCurrentOrde(CANTIDAD_DE_ORDAS_LIMITE);
            eRespawnController.ordeControl();
        }

        private void cuandoSeClikeaElBotonStart()
        {
            mainMenu.StartGame();
        }

        private void seEsperaQueSeCambieDeNivel()
        {
            this.gManager.Received().nexMap();
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
