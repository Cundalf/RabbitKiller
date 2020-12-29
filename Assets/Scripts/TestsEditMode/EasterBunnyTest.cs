using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class EasterBunnyTest 
    {
        private EasterBunny boss;
        private static int DOS_BARRAS_DE_VIDA = 2;
        private static int UNA_BARRA_DE_VIDA = 1;

        [Test]
        public void cuandoLaVidaDelBossLlegaACeroPorPrimeraVesNoMuereSinoQueSeRecarga()
        {
            this.dadoQueTengoUnBoss().con(DOS_BARRAS_DE_VIDA);

            this.cuandoLaVidaDelBossLlegaACero();

            this.seVerificaQueLaBarraDeVidaSeRecarga();
        }

        [Test]
        public void cuandoLaVidaDelBossLlegaACeroPorSegundaVesLaVidaQuedaEnCero()
        {
            this.dadoQueTengoUnBoss().con(DOS_BARRAS_DE_VIDA);
            this.dadoQueSeVaAEjecutarElMetodoDie();

            this.cuandoLaVidaDelBossLlegaACero();
            this.cuandoLaVidaDelBossLlegaACero();
            this.cuandoLaVidaDelBossLlegaACero();

            this.seVerificaQueLaBarraDeVidaNoSeRecarga();
        }

        [Test]
        public void cuandoLaVidaDelBossLlegaACeroYNoTieneMasBarrasDeVidaElMismoSeMuere() 
        {
            this.dadoQueTengoUnBoss().con(UNA_BARRA_DE_VIDA);
            this.dadoQueSeVaAEjecutarElMetodoDie();

            this.cuandoLaVidaDelBossLlegaACero();
            this.cuandoLaVidaDelBossLlegaACero();

            this.seVerificaQueSeEjecutaElMetodoDieUnaVes();
        }

        [Test]
        public void cuandoElBossTieneVidaYNoTieneMasBarrasDeVidaElMismoElMetodoDieNoSeEjecuta()
        {
            this.dadoQueTengoUnBoss().con(UNA_BARRA_DE_VIDA);
            
            this.cuandoElBossSeQuedaSinBarraDeVidaExtrasPeroTieneVida();

            this.seVerificaQueNoSeEjecutaElMetodoDieUnaVes();
        }

        private void dadoQueSeVaAEjecutarElMetodoDie()
        {
            this.boss.When(x => x.Die()).DoNotCallBase();
        }

        private void seVerificaQueNoSeEjecutaElMetodoDieUnaVes()
        {
            this.boss.Received(0).Die();
        }

        private void seVerificaQueSeEjecutaElMetodoDieUnaVes()
        {
            this.boss.Received(1).Die();
        }

        private void seVerificaQueLaBarraDeVidaSeRecarga() 
        {
            Assert.IsTrue(this.boss.healt != 0,"Se esperaba que la vida sea distinto de cero y esta en: " + this.boss.healt);
            Assert.IsTrue(this.boss.healtBarAmount == 1,"Se esperaba que la cantidad de vidas esta en uno y esta en: " + this.boss.healtBarAmount);
        }
        private void seVerificaQueLaBarraDeVidaNoSeRecarga()
        {
            Assert.IsTrue(this.boss.healt <= 0,"Se esperaba que la vida esta en cero y esta en: "+ this.boss.healt);
            Assert.IsTrue(this.boss.healtBarAmount == 0, "Se esperaba que la cantidad de vidas esta en cero y esta en: "+this.boss.healtBarAmount);
        }

        private EasterBunnyTest cuandoLaVidaDelBossLlegaACero()
        {
            this.boss.healt = 0;
            this.boss.healtControl(1);
            return this;
        }

        private void cuandoElBossSeQuedaSinBarraDeVidaExtrasPeroTieneVida() 
        {
            this.boss.healt = 10;
            this.boss.healtBarAmount = 0;
            this.boss.healtControl(1);
        }

        private EasterBunnyTest con( int barras)
        {
            this.boss.healtBarAmount = barras; 
            return this;
        }

        private EasterBunnyTest dadoQueTengoUnBoss()
        {
            this.boss = Substitute.ForPartsOf<EasterBunny>();
            return this;
        }

    }
}
