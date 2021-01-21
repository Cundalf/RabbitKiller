using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ChangeMapaTpm
    {
        private GameManager gManager;
        private EnemyRespawnController eRespawnController;
        private MainMenuManager mainMenu;

        private int CANTIDAD_DE_ORDAS_LIMITE = 10;
        private enum GameState
        {
            MAIN_MENU, IN_GAME, PAUSE, GAME_OVER
        }

        [Test]
        public void dadoQueSeLlegoAUnLimiteDeRondasParaCambiarDeMapaSeVerificaQueElJuegoSePoneEnPausa()
        {
            dadoQueTengoUnGameMangar();
            dadoQueTengoDosMapasConfiguradosEnElGameManager();

            cuandoElNumeroDeOrdasLlegaA10();

            seEsperaQueElJuegoEsteEnPausa();

        }

        [Test]
        public void dadoQueSeLlegoAlLimiteDeRondaParaCambiarDeMapaElInidiceDeSeleccionNoSuperaLaCantidadDeMapaConfigurados()
        {
            dadoQueTengoUnGameMangar();
            dadoQueTengoDosMapasConfiguradosEnElGameManager();
            dadoQueSeCargoElUltimoMapaConfigurado();

            cuandoElNumeroDeOrdasLlegaA10();

            seEsperaQueElIndiceNoSupereLaCantidadDeMapasConfigurados();
        }

        [Test] 
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
            gManager = new GameManager();
            eRespawnController = new EnemyRespawnController();
            eRespawnController.setGameManager(this.gManager);
            eRespawnController.setOrdeChangMap(CANTIDAD_DE_ORDAS_LIMITE);
        }
        private void dadoQueTengoDosMapasConfiguradosEnElGameManager()
        {
            this.gManager.setMaps( new string[]{"Stage1","Stage3" });
        }
        private void dadoQueSeCargoElUltimoMapaConfigurado()
        {
            this.gManager.setCurrentMap(2);
        }

        private void cuandoElNumeroDeOrdasLlegaA10()
        {
            eRespawnController.setCurrentOrde(CANTIDAD_DE_ORDAS_LIMITE);
            eRespawnController.ordeControl();
        }

        private void cuandoSeClikeaElBotonStart()
        {
            mainMenu.StartGame();
        }

        private void seEsperaQueElJuegoEsteEnPausa()
        {
            Assert.IsTrue(gManager.ActualGameState.ToString() == GameState.PAUSE.ToString());
            
        }

        private void seEsperaQueElIndiceNoSupereLaCantidadDeMapasConfigurados()
        {
            Assert.IsTrue(gManager.currentMap != 3);
        }


        private void seEsperaQueSeCargeElPrimerMapa()
        {

        }

        //[UnityTest]
        public IEnumerator ChangeMapaTpmWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
