using System;

namespace DesignPatterns.Structural.Decorator
{
    public interface INotifier
    {
        void Send();
    }
    
    public class Notifier : INotifier
    {
        public void Send()
        {
            Console.WriteLine("Sent default notification");
        }
    }
    
    // base class provides the wrapping logic and concrete decorators add various functionality
    // note the interface of the wrapped object is implemented as this describes what can be done with the wrapped object
    public abstract class NotifierDecoratorBase : INotifier
    {
        private protected readonly INotifier Decorated;

        protected NotifierDecoratorBase(INotifier decorated)
        {
            Decorated = decorated;
        }

        public abstract void Send();
    }
    
    public class EmailNotifier : NotifierDecoratorBase
    {
        public EmailNotifier(INotifier decorated) : base(decorated)
        {
        }

        public override void Send()
        {
            Console.WriteLine("notified via email");
            Decorated.Send();
        }
    }
    
    public class SlackNotifier : NotifierDecoratorBase
    {
        public SlackNotifier(INotifier decorated) : base(decorated)
        {
        }

        public override void Send()
        {
            Console.WriteLine("notified via slack");
            Decorated.Send();
        }
    }
    
    public class FacebookNotifier : NotifierDecoratorBase
    {
        public FacebookNotifier(INotifier decorated) : base(decorated)
        {
        }

        public override void Send()
        {
            Console.WriteLine("notified via facebook");
            Decorated.Send();
        }
    }
    
    public class BasicExample
    {
        static void Main()
        {
            // using decorators you can wrap an object with multiple new behaviours
            INotifier stack = new Notifier();
            stack = new EmailNotifier(stack); 
            stack = new SlackNotifier(stack);
            stack = new FacebookNotifier(stack);
            
            stack.Send();
            // notified via facebook
            // notified via slack
            // notified via email
            // Sent default notification
        }
    }
}