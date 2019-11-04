namespace papuff.domain.Arguments.Users {
    public class RegisterRequest {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Nick { get; set; }
        public bool Aprove { get; set; }
        public AddressRequest Address { get; set; }
    }
}