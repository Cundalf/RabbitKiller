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

        [Test]
        public void cuandoLaVidaDelBossLlegaACeroPorSegundaVesLaVidaLlegaACero()
        {
            this.dadoQueTengoUnBoss().conDosBarrasDeVida();

            this.cuandoLaVidaDelBossLlegaACero();
            this.cuandoLaVidaDelBossLlegaACero();
            this.cuandoLaVidaDelBossLlegaACero();

            this.seVerificaQueLaBarraDeVidaNoSeRecarga();
        }


        private void cuandoLaVidaDelBossLlegaACero() 
        {
            this.boss.healt = 0;
            this.boss.healtControl();
        }

        private void seVerificaQueLaBarraDeVidaSeRecarga() 
        {
            Assert.IsTrue(this.boss.healt != 0,"Se esperaba que la vida sea distinto de cero y esta en: " + this.boss.healt);
            Assert.IsTrue(this.boss.healtBarAmount == 1,"Se esperaba que la cantidad de vidas esta en uno y esta en: " + this.boss.healtBarAmount);
        }
        private void seVerificaQueLaBarraDeVidaNoSeRecarga()
        {
            Assert.IsTrue(this.boss.healt == 0,"Se esperaba que la vida esta en cero y esta en: "+ this.boss.healt);
            Assert.IsTrue(this.boss.healtBarAmount == 0, "Se esperaba que la cantidad de vidas esta en cero y esta en: "+this.boss.healtBarAmount);
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
