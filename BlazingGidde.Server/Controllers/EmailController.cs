using BlazingGidde.Server.Services;
using BlazingGidde.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
//using BlazingGidde.Shared.EmailTemplates;
using SendGrid;
using SendGrid.Helpers.Mail;


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
            //string emailHtml = await _templateRenderer.RenderTemplateAsync<WelcomeEmail, EmailViewModel>(model);

            //await SendEmailAsync("pintajarto@@gmail.com", model.Subject, emailHtml);

            return Ok("¡Correo enviado!");
        }

        private async Task SendEmailAsync(string toEmailAddress, string subject, string body)
        {
             var client = new SendGridClient("YOUR_SENDGRID_API_KEY");
             var from = new EmailAddress("pintajarto@gmail.com", "David Tortosa");
             var to = new EmailAddress(toEmailAddress);
             var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await client.SendEmailAsync(msg);

            await Task.CompletedTask;

        }
    }
}



