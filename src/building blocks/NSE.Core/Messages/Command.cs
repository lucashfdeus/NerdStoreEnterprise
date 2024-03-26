using FluentValidation.Results;
using MediatR;
using System;

namespace NSE.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            //O virtual utilizado nesse caso é que se quiser usar o 
            // override é possível porem não é obrigatório.
            // E se chamar o método sem usar o override irá lançar uma exeção de método não implementado.
            throw new NotImplementedException();
        }
    }
}
