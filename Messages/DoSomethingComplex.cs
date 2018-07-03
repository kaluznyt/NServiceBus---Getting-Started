namespace Messages
{
    using System.Collections.Generic;

    using NServiceBus;

    public class DoSomethingComplex :
        ICommand
    {
        public int SomeId { get; set; }
        public ChildClass ChildStuff { get; set; }
        public List<ChildClass> ListOfStuff { get; set; } = new List<ChildClass>();
    }
}