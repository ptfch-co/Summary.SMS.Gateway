namespace Summary.SMS.Gateway.Workflows.Event.SMS.Receive
{
    using Core.Workflows.Abstractions.Models;
    using Core.Workflows.Activities;
    using Core.Workflows.Models;
    using Microsoft.Extensions.Localization;
    using System.Collections.Generic;

    public class ReceiveMessageInSmsGatewayEvent : EventActivity
    {
        private readonly IStringLocalizer<ReceiveMessageInSmsGatewayEvent> T;

        public ReceiveMessageInSmsGatewayEvent(IStringLocalizer<ReceiveMessageInSmsGatewayEvent> t)
        {
            T = t;
        }

        public override string Name => nameof(ReceiveMessageInSmsGatewayEvent);

        public override LocalizedString DisplayText => T[SMSGateway.Localize.SOfReceiveWebhookMessage];

        public override LocalizedString Category => T[SMSGateway.Public.Category];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            return Outcomes(T[SMSGateway.Workflows.Done]);
        }

        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            return Outcomes(SMSGateway.Workflows.Done);
        }
    }
}