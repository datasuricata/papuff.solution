using papuff.domain.Core.Enums;
using papuff.domain.Core.Generals;
using System;

namespace papuff.domain.Arguments.Generals {
    public class GeneralResponse {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CurrentStage Stage { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserId { get; set; }

        public static explicit operator GeneralResponse(General v) {
            return v == null ? null : new GeneralResponse {
                Id = v.Id,
                BirthDate = v.BirthDate,
                Description= v.Description,
                Name = v.Name,
                Stage = v.Stage,
                UserId= v.UserId,
            };
        }
    }
}
