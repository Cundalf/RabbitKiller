using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BulletTest
    {

        private GameObject bullet;
        private GameObject boss;

        [Test]
        public void dadoQueSeTieneUnaObjetoBulletYColicionaConUnGameObjectConElTagBossSeEjecutaElMetodoHelatControl()
        {
            this.dadoQueTengoUnBullet();
            this.dadoQueTengoBoss().conElTag("Boss").conVida(100).yUnaContidadDeBarrasDeVidaIgual(1);

            this.cuandoColicionaConElObjecotBullet();

            this.seVerificaQueSeEjecutaElHealtControlDeEasterBunny();

        }

        private void cuandoColicionaConElObjecotBullet() 
        {
            this.bullet.GetComponent<Bullet>().OnTriggerEnter(this.boss.GetComponent<Collider>());
        }

        private BulletTest conElTag(string tag) 
        {
            this.boss.tag = tag;
            return this;
        }
        private BulletTest conVida(int vida)
        {
            this.boss.AddComponent<EasterBunny>().healt = vida;
            return this;
        }
        private BulletTest yUnaContidadDeBarrasDeVidaIgual(int barraDeVida)
        {
            this.boss.AddComponent<EasterBunny>().healtBarAmount = barraDeVida;
            return this;
        }

        private BulletTest dadoQueTengoUnBullet()
        {
            this.bullet = new GameObject();
            this.bullet.AddComponent<Bullet>();
            this.bullet.AddComponent<BoxCollider>();
            return this;
        }

        private BulletTest dadoQueTengoBoss()
        {
            this.boss = new GameObject("Boss");
            this.boss.AddComponent<BoxCollider>();
            return this;
        }

        private void seVerificaQueSeEjecutaElHealtControlDeEasterBunny() 
        {
            Assert.IsTrue(this.boss.GetComponent<EasterBunny>().healt != 100,"Se esoeraba que la vida del jugador sea menor ya que se ejecuta el metodo heart control");
        }
    }
}
