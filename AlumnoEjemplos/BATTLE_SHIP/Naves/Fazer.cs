using AlumnoEjemplos.BATTLE_SHIP.Elementos;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.BATTLE_SHIP.Naves
{
    public class Fazer : IInteractivo
    {
        private Nave nave;
        private Nave objetivoFazer;
        private int potenciaDeFazer;
        private TgcBox dibujo;
        public float velocidad { get; set; }
        public DateTime tiempoInicial { get; set; }
        public ElementosManager ManagerTGC { get; set; }
        public bool AlphaBlendEnable
        {
            get { return dibujo.AlphaBlendEnable; }
            set { dibujo.AlphaBlendEnable = value; }
        }

        public Fazer(Nave nave, Nave objetivoFazer, int potenciaDeLaser)
        {
            this.tiempoInicial = DateTime.Now;
            this.nave = nave;
            this.objetivoFazer = objetivoFazer;
            this.potenciaDeFazer = potenciaDeLaser;
            //this.dibujo = new TgcSphere(20f, Color.Green, nave.Position);
            this.dibujo = TgcBox.fromSize(nave.Position, new Vector3(5f, 5f, 5f), Color.Red);
            this.dibujo.updateValues();
            this.velocidad = 500f;
        }

        public void Actualizar(float elapsedTime)
        {
            var dir = new Vector3(objetivoFazer.Position.X - this.dibujo.Position.X, objetivoFazer.Position.Y - this.dibujo.Position.Y, objetivoFazer.Position.Z - this.dibujo.Position.Z);
            var modulo = dir.Length();
            var versor = new Vector3(dir.X / modulo, dir.Y / modulo, dir.Z / modulo);
            if (modulo <= 40f)
            {
                objetivoFazer.RecibirDisparo(potenciaDeFazer);
                ManagerTGC.Remove(this);
            }
            else
            {
                float z = versor.Z * elapsedTime * velocidad;
                float x = versor.X * elapsedTime * velocidad;
                float y = versor.Y * elapsedTime * velocidad;

                this.dibujo.move(x, y, z);
                this.dibujo.updateValues();
            }
        }

        public void render()
        {
            this.dibujo.render();
        }

        public void dispose()
        {
           this.dibujo.dispose();
        }
    }
}
