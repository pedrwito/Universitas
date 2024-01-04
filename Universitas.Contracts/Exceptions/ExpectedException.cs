namespace Universitas.Contracts.Exceptions
{
    public abstract class ExpectedException : Exception
    {
        public abstract int Code { get; protected set; }
        //TODO cambiar als excepciones que tengo por las custom que cree.
    }
}
