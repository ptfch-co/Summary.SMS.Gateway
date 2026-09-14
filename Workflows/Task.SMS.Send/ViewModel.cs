namespace Summary.SMS.Gateway.Workflows.Task.SMS.Send
{
    using System.ComponentModel.DataAnnotations;

    public class SendSmsBySMSGatewayViewModel
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string Phone_Number { get; set; }
        [Required]
        public int Sim_Slot { get; set; }
    }
}