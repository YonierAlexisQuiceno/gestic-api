using System;

namespace GesticApi.Models
{
    /// <summary>
    ///     Enumeración que define los estados que puede tener una solicitud
    ///     realizada por un usuario. Estos valores se almacenan en la
    ///     base de datos mediante el tipo enumerado PostgreSQL
    ///     <c>request_status</c>. Se utilizan términos en español para
    ///     mantener coherencia con el resto del modelo y las interfaces.
    /// </summary>
    public enum RequestStatus
    {
        /// <summary>
        ///     La solicitud ha sido creada pero aún no se ha empezado a
        ///     procesar.
        /// </summary>
        PENDIENTE,

        /// <summary>
        ///     La solicitud está en proceso de atención.
        /// </summary>
        EN_PROCESO,

        /// <summary>
        ///     La solicitud ha sido atendida y cerrada satisfactoriamente.
        /// </summary>
        COMPLETADA,

        /// <summary>
        ///     La solicitud ha sido cancelada y no se seguirá atendiendo.
        /// </summary>
        CANCELADA
    }
}