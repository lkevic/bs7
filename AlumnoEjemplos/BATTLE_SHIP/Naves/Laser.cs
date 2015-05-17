using AlumnoEjemplos.BATTLE_SHIP.Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.BATTLE_SHIP.Naves
{
    public class Laser : IInteractivo
    {
        private Nave nave;
        private Nave objetivoLaser;
        private int potenciaDeLaser;
        private int duracionMs;
        private TgcLine dibujo;
        public DateTime tiempoInicial { get; set; }
        public ElementosManager ManagerTGC { get; set; }
        public bool AlphaBlendEnable
        {
            get { return dibujo.AlphaBlendEnable; }
            set { dibujo.AlphaBlendEnable = value; }
        }

        public Laser(Nave nave, Nave objetivoLaser, int potenciaDeLaser)
        {
            this.tiempoInicial = DateTime.Now;
            this.duracionMs = 1000;
            this.nave = nave;
            this.objetivoLaser = objetivoLaser;
            this.potenciaDeLaser = potenciaDeLaser;
            this.dibujo = TgcLine.fromExtremes(nave.Position, objetivoLaser.Position);
        }

        public void Actualizar(float elapsedTime)
        {
            if ((DateTime.Now - tiempoInicial).TotalMilliseconds > duracionMs)
            {
                objetivoLaser.RecibirDisparo(potenciaDeLaser);
                ManagerTGC.Remove(this);
            }
            else
            {
                this.dibujo.PStart = nave.Position;
                this.dibujo.PEnd = objetivoLaser.Position;
                this.dibujo.updateValues();
            }
        }

        public void render()
        {
            dibujo.render();
        }

        public void dispose()
        {
            this.dibujo.dispose();
        }
    }
}
