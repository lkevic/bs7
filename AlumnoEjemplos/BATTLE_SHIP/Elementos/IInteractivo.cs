using AlumnoEjemplos.BATTLE_SHIP.Naves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Elementos
{
    public interface IInteractivo : IRenderObject
    {
        void Actualizar(float elapsedTime);

        ElementosManager ManagerTGC { get; set; }
    }
}
