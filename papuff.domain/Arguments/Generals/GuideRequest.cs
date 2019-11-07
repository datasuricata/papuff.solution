using Newtonsoft.Json;
using papuff.domain.Core.Enums;

namespace papuff.domain.Arguments.Generals {
    public class GuideRequest {
        // owner
        public string Name { get; set; }
        public string Email { get; set; }
        public EntryType Type { get; set; }

        // receipt
        public string Agency { get; set; }
        public string Account { get; set; }
        public string CPF { get; set; }
        public int DateDue { get; set; }

        // company
        public string Registration { get; set; }
        public string SiteUri { get; set; }
        public string CNPJ { get; set; }

        // todo siege
        public VisibilityType Visibility { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public double Range { get; set; }
        public int OperationIn { get; set; }
        public int OperationTime { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
