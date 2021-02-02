using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class ChangeMapaTpm
    {
        private GameManager gManager;
        private EnemyRespawnController eRespawnController;
        private MainMenuManager mainMenu;

        private string[] MAPAS_TEST = new string[] {"Assets/Scenes/Stage/Stage1.unity","Assets/Scenes/Stage/Stage2.unity"};
        private int CANTIDAD_DE_ORDAS_LIMITE = 10;
        private enum GameState
        {
            MAIN_MENU, IN_GAME, PAUSE, GAME_OVER
        }
        bool sceneLoaded;
        private EditorBuildSettingsScene[] buildSettingsScenesBackup;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            buildSettingsScenesBackup = EditorBuildSettings.scenes;

            var MainMenu = new EditorBuildSettingsScene("Assets/Scenes/MainMenu.unity", true);
            var Stage1 = new EditorBuildSettingsScene("Assets/Scenes/Stage/Stage1.unity", true);
            var Stage2 = new EditorBuildSettingsScene("Assets/Scenes/Stage/Stage2.unity", true);

            EditorBuildSettings.scenes.Append(MainMenu).ToArray();
            EditorBuildSettings.scenes.Append(Stage1).ToArray();
            EditorBuildSettings.scenes = EditorBuildSettings.scenes.Append(Stage2).ToArray();

            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            EditorBuildSettings.scenes = buildSettingsScenesBackup;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            sceneLoaded = true;
        }

        [UnityTest]
        public IEnumerator dadoQueSeLlegoAUnLimiteDeRondasParaCambiarDeMapaSeVerificaQueElJuegoSePoneEnPausa()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            dadoQueTengoUnGameMangar();
            dadoQueTengoDosMapasConfiguradosEnElGameManager();

            cuandoElNumeroDeOrdasLlegaA10();

            seEsperaQueElJuegoEsteEnPausa();
            yield return null;

        }

        [UnityTest]
        public IEnumerator dadoQueSeLlegoAlLimiteDeRondaParaCambiarDeMapaElInidiceDeSeleccionNoSuperaLaCantidadDeMapaConfigurados()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            dadoQueTengoUnGameMangar();
            dadoQueTengoDosMapasConfiguradosEnElGameManager();
            dadoQueSeCargoElUltimoMapaConfigurado();

            cuandoElNumeroDeOrdasLlegaA10();

            seEsperaQueElIndiceNoSupereLaCantidadDeMapasConfigurados();
            yield return null;
        }

        [UnityTest] 
        public IEnumerator dadoQueSeMuestraElMenuCuandoSeClickeaElBotonStartSeCargaElPrimerMapa()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            dadoQueSeMuestraElMenuPrincipal();
            dadoQueTengoLosMapasCargadosAssetBundel();
            dadoQueTengoDosMapasConfiguradosEnElGameManager();

            cuandoSeClikeaElBotonStart();

            seEsperaQueSeCargeElPrimerMapa();
            
            yield return null;
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
            eRespawnController.setGameManager(gManager);
            eRespawnController.setOrdeChangMap(CANTIDAD_DE_ORDAS_LIMITE);
        }

        private void dadoQueTengoDosMapasConfiguradosEnElGameManager()
        {
            this.gManager.setMaps(MAPAS_TEST);
        }

        private void dadoQueSeCargoElUltimoMapaConfigurado()
        {
            this.gManager.setCurrentMap(2);
        }
        private void dadoQueTengoLosMapasCargadosAssetBundel()
        {
            this.mainMenu.setGameManager(this.gManager);
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
            Assert.IsTrue(gManager.nextSceneConfig != 3);
        }

        private void seEsperaQueSeCargeElPrimerMapa()
        {
            String mensage = String.Format("Se esperaba {0} y se obtuvo {1}", MAPAS_TEST[0], gManager.getActiveScene().name); 
            Assert.IsTrue(gManager.getActiveScene().name == MAPAS_TEST[0], mensage); 
        }
    }
}
