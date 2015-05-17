using AlumnoEjemplos.BATTLE_SHIP.Naves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Elementos
{
    public class ElementosManager
    {
        private List<IRenderObject> objetosEstatico { get; set; }
        private List<IInteractivo> objetosInteractivos { get; set; }
        private List<Nave> navesEnemigas { get; set; }
        
        private List<IInteractivo> objetosAEliminar { get; set; }
        private List<IInteractivo> objetosAAgregar { get; set; }

        public List<Nave> NavesEnemigas { get { return navesEnemigas; } }
        public List<IInteractivo> ObjetosInteractivos { get { return objetosInteractivos; } }

        public Nave NavePrincipal { get; set; }

        public ElementosManager() 
        { 
            objetosEstatico = new List<IRenderObject>();
            objetosInteractivos = new List<IInteractivo>();
            navesEnemigas = new List<Nave>();
            objetosAEliminar = new List<IInteractivo>();
            objetosAAgregar = new List<IInteractivo>();
        }

        #region Add
        public void Add(IRenderObject elemento)
        {
            objetosEstatico.Add(elemento);
        }

        public void Add(IInteractivo elemento)
        {
            objetosEstatico.Add(elemento);
            objetosAAgregar.Add(elemento);
            //objetosInteractivos.Add(elemento);
            elemento.ManagerTGC = this;
        }

        public void Add(Nave elemento)
        {
            objetosEstatico.Add(elemento);
            objetosAAgregar.Add(elemento);
            //objetosInteractivos.Add(elemento);
            navesEnemigas.Add(elemento);
            elemento.ManagerTGC = this;
        }

        public void AddNavePrincipal(Nave naveJugador) 
        {
            objetosEstatico.Add(naveJugador);
            objetosAAgregar.Add(naveJugador);
            naveJugador.ManagerTGC = this;
            NavePrincipal = naveJugador;
        }

        #endregion

        #region Remove
        public void Remove(IInteractivo obj)
        {
            objetosAEliminar.Add(obj);
        }

        public void Remove(Nave obj)
        {
            navesEnemigas.Remove(obj);
            objetosAEliminar.Add(obj);
        }
        #endregion

        #region Actualizacion y Render
        public void Actualizar(float elapsedTime)
        {
            foreach (var elemento in objetosInteractivos)
            {
                elemento.Actualizar(elapsedTime);
            }
        }

        public void RenderAll()
        {
            foreach (var elemento in objetosEstatico)
            {
                elemento.render();
            }
        }

        public void ActualizarListaDeObjetosInteractivos()
        {
            foreach (var item in objetosAEliminar)
            {
                objetosEstatico.Remove(item);
                objetosInteractivos.Remove(item);
                // TODO: Falta hacer el dispose
                //item.dispose();
            }
            objetosAEliminar = new List<IInteractivo>();

            foreach (var item in objetosAAgregar)
            {
                objetosInteractivos.Add(item);
            }
            objetosAAgregar = new List<IInteractivo>();
        }
        #endregion
    }
}
