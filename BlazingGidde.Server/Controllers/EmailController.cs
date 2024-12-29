
using BlazingGidde.Server.Services;
using BlazingGidde.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using BlazingGidde.Server.Components;




namespace BlazingGidde.Server.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly TemplateRenderer _templateRenderer;

        public EmailController(TemplateRenderer templateRenderer)
        {
            _templateRenderer = templateRenderer;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail()
        {
            var model = new EmailViewModel
            {
                Subject = "¡Bienvenido!",
                Greeting = "Hola,",
                Body = "Gracias por registrarte.",
                Signature = "El equipo sin nombre"
            };

            //esta line de bajo no la he conseguido arreglar
            string emailHtml = await _templateRenderer.RenderTemplateAsync<EmailTemplate>(model);

            //await SendEmailAsync("pintajarto@@gmail.com", model.Subject, emailHtml);

            return Ok("¡Correo enviado!");
        }

        private async Task SendEmailAsync(string toEmailAddress, string subject, string body)
        {
            

            await Task.CompletedTask;

        }
    }
}



