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

        private void dadoQueSeMuestraElMenuPrincipal()
        {
            this.dadoQueTengoUnGameMangar();
            mainMenu = new MainMenuManager();
        }

        private void dadoQueTengoUnGameMangar()
        {
            gManager = Substitute.ForPartsOf<GameManager>();
            eRespawnController = new EnemyRespawnController();
            eRespawnController.setOrdeChangMap(CANTIDAD_DE_ORDAS_LIMITE);
        }

        private void cuandoElNumeroDeOrdasLlegaA10()
        {
            gManager.When(x => x.changeMap()).DoNotCallBase();
            eRespawnController.setCurrentHorde(CANTIDAD_DE_ORDAS_LIMITE);
            eRespawnController.hordeControl();
        }

        private void seEsperaQueSeCambieDeNivel()
        {
            this.gManager.Received().changeMap();
        }

        //[UnityTest]
        public IEnumerator ChangeMapTestWithEnumeratorPasses()
        {
            yield return null;
        }
    }
}
