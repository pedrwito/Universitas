namespace Universitas.Contracts.Exceptions
{
    public class BadRequestException : ExpectedException
    {
        public override int Code { get; protected set; } = 400;
    }
}
