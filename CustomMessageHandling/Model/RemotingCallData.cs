namespace CustomMessageHandling.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RemotingCallData
    {
        [DataMember]
        public string CorrelationId { get; set; }


        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string ServiceUrl { get; set; }
    }
}