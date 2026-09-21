namespace Summary.SMS.Gateway.Controllers.API.V1
{
    using Core.Workflows.Services;
    using Microsoft.AspNetCore.Mvc;
    using Summary.SMS.Gateway.Services;
    using Summary.SMS.Gateway.Workflows.Event.SMS.Receive;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [IgnoreAntiforgeryToken]
    [Route("api/v1/sms-gateway/webhook")]
    public class WebhookController : ControllerBase
    {
        private readonly ISMSGatewayService _gateway;
        private readonly IWorkflowManager _workflow;

        public WebhookController(ISMSGatewayService gateway,
            IWorkflowManager workflow)
        {
            _gateway = gateway;
            _workflow = workflow;
        }

        [HttpPost]
        [Route(nameof(Submit))]
        public async Task<IActionResult> Submit([FromBody] SmsInboundWebhookReceiver model)
        {
            var data = new Dictionary<string, object>
            {
                { "Sms.Gateway.DeviceId", model.DeviceId },
                { "Sms.Gateway.Id", model.Id },
                { "Sms.Gateway.Webhook.Id", model.WebhookId },
                { "Sms.Gateway.Message", model.Payload.Message },
                { "Sms.Gateway.ReceivedAt", model.Payload.ReceivedAt.ToString("MM/dd/yyyy hh:mm:ss") },
                { "Sms.Gateway.Sender", model.Payload.Sender },
                { "Sms.Gateway.SIM", model.Payload.SimNumber }
            };

            await _workflow.TriggerIntoDBAsync(
                nameof(ReceiveMessageInSmsGatewayEvent),
                data
            );

            return Ok();
        }
    }
}