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
                { "Mobile.SMS.DeviceId", model.DeviceId },
                { "Mobile.SMS.Id", model.Id },
                { "Mobile.SMS.Webhook.Id", model.WebhookId },
                { "Mobile.SMS.Message", model.Payload.Message },
                { "Mobile.SMS.ReceivedAt", model.Payload.ReceivedAt.ToString("MM/dd/yyyy hh:mm:ss") },
                { "Mobile.SMS.Sender", model.Payload.Sender },
                { "Mobile.SMS.SIM", model.Payload.SimNumber }
            };

            await _workflow.TriggerIntoDBAsync(
                nameof(ReceiveMessageInSmsGatewayEvent),
                data
            );

            return Ok();
        }
    }
}