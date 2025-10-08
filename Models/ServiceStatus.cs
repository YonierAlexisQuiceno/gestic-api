using System;

namespace GesticApi.Models
{
    /// <summary>
    ///     Enumeración que define los posibles estados de un servicio TIC. En la
    ///     base de datos estos valores se almacenan como texto dentro del
    ///     tipo enumerado PostgreSQL <c>service_status</c> y siguen las
    ///     definiciones de la OTIC: un servicio puede estar activo, retirado
    ///     o planificado para el futuro.
    /// </summary>
    public enum ServiceStatus
    {
        /// <summary>
        ///     El servicio está disponible y se presta normalmente.
        /// </summary>
        ACTIVO,

        /// <summary>
        ///     El servicio ha sido retirado y ya no se presta.
        /// </summary>
        RETIRADO,

        /// <summary>
        ///     El servicio está planificado pero aún no disponible.
        /// </summary>
        PLANIFICADO
    }
}