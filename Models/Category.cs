using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GesticApi.Models
{
    /// <summary>
    ///     Representa la categoría a la que pertenece un servicio. Permite
    ///     agrupar los servicios por tipo (p.ej. Infraestructura, Acceso,
    ///     Aplicaciones, Soporte) según el catálogo de servicios oficial.
    /// </summary>
    public class Category
    {
        /// <summary>
        ///     Identificador único de la categoría.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Nombre de la categoría.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     Descripción opcional de la categoría.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Servicios que pertenecen a esta categoría (propiedad de
        ///     navegación).
        /// </summary>
        public List<Service> Services { get; set; } = new();
    }
}