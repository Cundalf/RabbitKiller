using NSubstitute;
using NUnit.Framework;

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

        [Test]
        public void cuandoLaVidaDelBossLlegaACeroPeroLeQuedaOtraBarraDeVidaYSeEjecutaElMetodoDieElBossNoSeEliminaDelMapa()
        {
            this.dadoQueTengoUnBoss().conUnaBarrasDeVida();

            this.cuandoLaVidaDelBossLlegaACero();
            this.cuandoLaVidaDelBossLlegaACero().seEjecutaElMetoDie();

            this.seVerificaQueElBossNoSeDestruye();

        }

        private void seVerificaQueElBossNoSeDestruye()
        {
            Assert.IsTrue(this.boss.bloodPSPoint == null, "Se espera que el punto de particulas sea null ya que el metodo die no lo tendira que instancaiar y es:" + this.boss.bloodPSPoint.ToString());
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

        private EasterBunnyTest cuandoLaVidaDelBossLlegaACero()
        {
            this.boss.healt = 0;
            this.boss. healtControl();
            return this;
        }

        private EasterBunnyTest conDosBarrasDeVida()
        {
            this.boss.healtBarAmount = 2; 
            return this;
        }

        private EasterBunnyTest dadoQueTengoUnBoss()
        {
            this.boss = Substitute.ForPartsOf<EasterBunny>();

            return this;
        }

        private EasterBunnyTest conUnaBarrasDeVida()
        {
            this.boss.healtBarAmount = 1;
            return this;
        }

        private EasterBunnyTest seEjecutaElMetoDie()
        {
            this.boss.Configure().Die();
            return this;
        }
    }
}
