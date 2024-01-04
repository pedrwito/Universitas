namespace Universitas.Contracts.Exceptions
{
    public class NotFoundException : ExpectedException
    {
        public override int Code { get; protected set; } = 404;
    }
}
