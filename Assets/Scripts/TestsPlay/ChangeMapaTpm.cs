using System;
using System.Collections;
using NSubstitute;
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
        private EnemyRespawnController eRespawnController;
        private MainMenuManager mainMenu;

        private string[] MAPAS_TEST = new string[] { "Assets/Scenes/Stage/Stage1.unity", "Assets/Scenes/Stage/Stage2.unity" };
        private int CANTIDAD_DE_ORDAS_LIMITE = 10;
        private GameObject tutorialPanel;
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
            var Stage1Test = new EditorBuildSettingsScene("Assets/Scenes/Stage/Stage1.unity", true);
            var Stage2Test = new EditorBuildSettingsScene("Assets/Scenes/Stage/Stage3.unity", true);

            EditorBuildSettings.scenes.Append(MainMenu).ToArray();
            EditorBuildSettings.scenes.Append(Stage1Test).ToArray();
            EditorBuildSettings.scenes = EditorBuildSettings.scenes.Append(Stage2Test).ToArray();
            loadSceneTest("MainMenu");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            EditorBuildSettings.scenes = buildSettingsScenesBackup;
        }

        void loadSceneTest(string sceneName)
        {
            sceneLoaded = false;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            sceneLoaded = true;
        }

        [UnityTest]
        public IEnumerator dadoQueSeLlegoAUnLimiteDeRondasParaCambiarDeMapaSeVerificaQueElJuegoSePoneEnPausa()
        {
            loadSceneTest("Stage1");
            yield return new WaitWhile(() => sceneLoaded == false);

            dadoQueTengoUnEnemyRespawnController();
            dadoQueTengoDosMapasConfiguradosEnElGameManager();

            cuandoElNumeroDeOrdasLlegaA10();

            seEsperaQueElJuegoEsteEnPausa();
            yield return null;

        }

        [UnityTest]
        public IEnumerator dadoQueSeLlegoAlLimiteDeRondaParaCambiarDeMapaElInidiceDeSeleccionNoSuperaLaCantidadDeMapaConfigurados()
        {
            loadSceneTest("Stage1");
            yield return new WaitWhile(() => sceneLoaded == false);

            dadoQueTengoUnEnemyRespawnController();
            dadoQueTengoDosMapasConfiguradosEnElGameManager();
            dadoQueSeCargoElUltimoMapaConfigurado();

            cuandoElNumeroDeOrdasLlegaA10();

            seEsperaQueElIndiceNoSupereLaCantidadDeMapasConfigurados();
            yield return null;
        }

        [UnityTest]
        public IEnumerator dadoQueSeMuestraElMenuCuandoSeClickeaElBotonStartSeCargaElPrimerMapa()
        {
            loadSceneTest("MainMenu");
            yield return new WaitWhile(() => sceneLoaded == false);

            dadoQueSeMuestraElMenuPrincipal();
            dadoQueTengoDosMapasConfiguradosEnElGameManager();

            cuandoSeClikeaElBotonStart();
            SceneManager.sceneLoaded += OnSceneLoaded;
            yield return new WaitWhile(() => sceneLoaded == false);

            seEsperaQueSeCargeElPrimerMapa();
            seEsperaQueElCanvanDesaparesaca();
            yield return null;
        }

        private void dadoQueSeMuestraElMenuPrincipal()
        {
            mainMenu = GameObject.Find("CanvasMainMenu").GetComponent<MainMenuManager>();
        }

        private void dadoQueTengoUnEnemyRespawnController()
        {
            eRespawnController = new EnemyRespawnController();

            eRespawnController.setOrdeChangMap(CANTIDAD_DE_ORDAS_LIMITE);
        }

        private void dadoQueTengoDosMapasConfiguradosEnElGameManager()
        {
            GameManager.SharedInstance.setMaps(MAPAS_TEST);
        }

        private void dadoQueSeCargoElUltimoMapaConfigurado()
        {
            GameManager.SharedInstance.setCurrentMap(2);
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
            String message = String.Format("Se esperaba {0} y se obtuvo {1}", GameState.PAUSE.ToString(), GameManager.SharedInstance.ActualGameState.ToString());
            Assert.IsTrue(GameManager.SharedInstance.ActualGameState == GameManager.GameState.PAUSE,message);
        }

        private void seEsperaQueElIndiceNoSupereLaCantidadDeMapasConfigurados()
        {
            Assert.IsTrue(GameManager.SharedInstance.nextSceneConfig != 3);
        }

        private void seEsperaQueSeCargeElPrimerMapa()
        {
            String mapName = "Stage1";
            String message = String.Format("Se esperaba {0} y se obtuvo {1}", mapName, GameManager.SharedInstance.getActiveScene().name); 
            Assert.IsTrue(GameManager.SharedInstance.getActiveScene().name == mapName, message); 
        }
        private void seEsperaQueElCanvanDesaparesaca()
        {
            GameObject canvas = GameObject.Find("CanvasMainMenu");
            String message = String.Format("Se esperaba null y se obtuvo {0}", canvas);
            Assert.IsTrue(canvas == null, message);
        }

    }
}
