using System;
using System.ComponentModel.DataAnnotations;

namespace GesticApi.Models
{
    /// <summary>
    ///     Representa un registro histórico de cambios en un servicio. Cada
    ///     vez que se modifica un servicio se puede generar un registro
    ///     indicando la fecha, el usuario que realizó el cambio y el valor
    ///     anterior y nuevo del campo modificado. Esto permite auditar
    ///     fácilmente las modificaciones.
    /// </summary>
    public class ServiceHistory
    {
        /// <summary>
        ///     Identificador único del registro histórico.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Identificador del servicio al que pertenece este registro.
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        ///     Servicio asociado (propiedad de navegación).
        /// </summary>
        public Service? Service { get; set; }

        /// <summary>
        ///     Fecha en la que se produjo el cambio.
        /// </summary>
        public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        ///     Identificador del usuario que realizó el cambio. Puede ser
        ///     nulo si el cambio se realizó automáticamente.
        /// </summary>
        public int? ChangedBy { get; set; }

        /// <summary>
        ///     Usuario que realizó el cambio (propiedad de navegación).
        /// </summary>
        public User? ChangedByUser { get; set; }

        /// <summary>
        ///     Valor anterior del campo modificado. Se almacena como una
        ///     cadena JSON para soportar cambios en diferentes atributos.
        /// </summary>
        public string? OldValue { get; set; }

        /// <summary>
        ///     Valor nuevo del campo modificado. También se almacena en
        ///     formato JSON.
        /// </summary>
        public string? NewValue { get; set; }
    }
}