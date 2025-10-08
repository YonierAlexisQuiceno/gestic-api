using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GesticApi.Models
{
    /// <summary>
    ///     Representa a un usuario del sistema, ya sea administrador,
    ///     coordinador OTIC o usuario final. Incluye credenciales básicas
    ///     (nombre de usuario, correo electrónico y una contraseña hasheada)
    ///     así como la relación con su rol y las entidades que crea o
    ///     solicita.
    /// </summary>
    public class User
    {
        /// <summary>
        ///     Identificador único del usuario.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Nombre de usuario para iniciar sesión.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        ///     Contraseña hasheada del usuario. Nunca se almacena en texto
        ///     plano.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        ///     Dirección de correo electrónico del usuario.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        ///     Identificador del rol asociado a este usuario.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        ///     Rol asociado (propiedad de navegación).
        /// </summary>
        public Role? Role { get; set; }

        /// <summary>
        ///     Fecha de creación del usuario.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        ///     Servicios creados por este usuario (propiedad de navegación).
        /// </summary>
        public List<Service> ServicesCreated { get; set; } = new();

        /// <summary>
        ///     Solicitudes realizadas por este usuario.
        /// </summary>
        public List<Request> Requests { get; set; } = new();
    }
}