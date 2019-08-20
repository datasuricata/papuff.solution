namespace papuff.domain.Arguments.Base {
    public class BaseResponse {
        private string Message { get; set; }

        public BaseResponse() => Message = "Operação realizada com sucesso.";
        public BaseResponse(string msg) => Message = msg;
    }
}
