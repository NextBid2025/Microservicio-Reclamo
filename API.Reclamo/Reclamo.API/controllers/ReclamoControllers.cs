using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reclamo.Application.Commands;
using Reclamo.Application.DTOs;
using Reclamo.Application.Queries;
using Reclamo.Domain.ValueObjects;

namespace Reclamo.API.Controllers
{
    /// <summary>
    /// Controlador principal para la gestión de reclamos.
    /// Expone endpoints para crear, resolver, listar reclamos, subir evidencias y verificar el estado del servicio.
    /// </summary>
    [ApiController]
    [Route("api/reclamos")]
    public class ReclamoController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="ReclamoController"/>.
        /// </summary>
        /// <param name="mediator">Instancia de IMediator para manejar los comandos y consultas.</param>
        public ReclamoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Crea un nuevo reclamo.
        /// </summary>
        /// <param name="reclamoDto">Datos del reclamo a crear.</param>
        /// <returns>ID del reclamo creado.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateReclamo([FromBody] CreateReclamoDto reclamoDto)
        {
            var reclamoId = await _mediator.Send(new CreateReclamoCommand(
                new UsuarioId(reclamoDto.UsuarioId),
                new SubastaId(reclamoDto.SubastaId),
                reclamoDto.Motivo,
                reclamoDto.Descripcion,
                reclamoDto.EvidenciaUrl
            ));

            return CreatedAtAction(nameof(CreateReclamo), new { id = reclamoId });
        }

        /// <summary>
        /// Resuelve un reclamo existente.
        /// </summary>
        /// <param name="id">ID del reclamo a resolver.</param>
        /// <param name="command">Comando con los datos de resolución.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("{id}/resolver")]
        public async Task<IActionResult> ResolverReclamo([FromRoute] string id, [FromBody] ResolverReclamoCommand command)
        {
            if (id != command.ReclamoId)
                return BadRequest("El ID de la ruta no coincide con el del comando.");

            await _mediator.Send(command);
            return Ok("Reclamo resuelto exitosamente.");
        }

        /// <summary>
        /// Obtiene la lista de todos los reclamos.
        /// </summary>
        /// <returns>Lista de reclamos.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllReclamos()
        {
            var reclamos = await _mediator.Send(new GetAllReclamosQuery());
            return Ok(reclamos);
        }

        /// <summary>
        /// Sube un archivo de evidencia (imagen o PDF) y retorna la URL de acceso.
        /// </summary>
        /// <param name="archivo">Archivo enviado como multipart/form-data.</param>
        /// <returns>URL del archivo subido.</returns>
        [HttpPost("evidencia")]
        public async Task<IActionResult> SubirEvidencia(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo no válido.");

            var ruta = Path.Combine("wwwroot/evidencias", archivo.FileName);
            using (var stream = new FileStream(ruta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var url = $"{Request.Scheme}://{Request.Host}/evidencias/{archivo.FileName}";
            return Ok(new { url });
        }

        /// <summary>
        /// Verifica el estado de salud del servicio.
        /// </summary>
        /// <returns>Mensaje de estado.</returns>
        [HttpGet("/Reclamos/health")]
        public IActionResult Health()
        {
            return Ok("Healthy");
        }
    }
}
        
 