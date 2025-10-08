using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GesticApi.Models
{
    /// <summary>
    ///     Representa un rol del sistema. Cada usuario se asocia a un rol
    ///     determinado (por ejemplo: Administrador, Coordinador OTIC o
    ///     Usuario final). Los roles determinan los permisos de acceso a
    ///     ciertas funcionalidades dentro del catálogo de servicios.
    /// </summary>
    public class Role
    {
        /// <summary>
        ///     Identificador único del rol.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Nombre del rol (p.ej. "Administrador").
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     Descripción opcional del rol.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Usuarios asociados a este rol (propiedad de navegación).
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}