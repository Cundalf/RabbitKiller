using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EasterBunnyTest
    {

        private EasterBunny boss;

        [Test]
        public void cuandoLaVidaDelBossLlegaACeroPorPrimeraVesNoMuereSinoQueSeRecarga()
        {
            this.dadoQueTengoUnBoss().conDosBarrasDeVida();

            this.cuandoLaVidaDelBossLlegaACero();

            this.seVerificaQueLaBarraDeVidaSeRecarga();
        }

        private void cuandoLaVidaDelBossLlegaACero() 
        {
            this.boss.healt = 0;
            this.boss.healtControl();
        }

        private void seVerificaQueLaBarraDeVidaSeRecarga() 
        {
            Assert.IsTrue(this.boss.healt != 0);
            Assert.IsTrue(this.boss.healtBarAmount == 1);
        }

        private EasterBunnyTest conDosBarrasDeVida()
        {
            this.boss.healtBarAmount = 2; 
            return this;
        }

        private EasterBunnyTest dadoQueTengoUnBoss()
        {
            this.boss = new EasterBunny();
            return this;
        }
    }
}
