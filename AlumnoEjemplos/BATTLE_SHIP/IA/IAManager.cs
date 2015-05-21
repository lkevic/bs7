using AlumnoEjemplos.BATTLE_SHIP.Naves;
using AlumnoEjemplos.BATTLE_SHIP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.BATTLE_SHIP.IA
{
    public class IAManager
    {
        private List<PilotoIA> pilotos;
        private List<PilotoIA> pilotosLibres;
        private List<Nave> objetivos;
        private float distanciaDePresecucion;
        
        public IAManager() 
        {
            pilotos = new List<PilotoIA>();
            pilotosLibres = new List<PilotoIA>();
            objetivos = new List<Nave>();

            distanciaDePresecucion = 1000f;
        }

        public void AddNave(Nave nave)
        {
            PilotoIA nuevoPiloto = new PilotoIA(nave, this);
            pilotos.Add(nuevoPiloto);
            pilotosLibres.Add(nuevoPiloto);
        }

        public void AddObjetivo(Nave nave) 
        {
            objetivos.Add(nave);
        }

        private Nave ObtenerObjetivo()
        {
            return objetivos.FirstOrDefault();
        }

        public void Actualizar(float elapsedTime)
        {
            AsignarPilotosLibres();

            ActualizarTodosLosPilotos(elapsedTime);
        }

        private void ActualizarTodosLosPilotos(float elapsedTime)
        {
            foreach (var piloto in pilotos)
            {
                piloto.Actualizar(elapsedTime);
            }
        }

        private void AsignarPilotosLibres()
        {
            Nave objetivoTemporal;
            var pilotosAsignados = new List<PilotoIA>();
            
            foreach (var piloto in pilotosLibres)
            {
                objetivoTemporal = ObtenerObjetivo();
                if (objetivoTemporal != null && piloto.Activo && TgcMath.Distancia(piloto.Position, objetivoTemporal.Position) < distanciaDePresecucion)
                {
                    piloto.PerseguirYAtacar(objetivoTemporal);
                    pilotosAsignados.Add(piloto);
                }
            }

            foreach (var piloto in pilotosAsignados)
            {
                pilotosLibres.Remove(piloto);
            }
        }

        internal void NotificarFinDeAtaque(PilotoIA piloto)
        {
            pilotosLibres.Add(piloto);
        }
    }
}
