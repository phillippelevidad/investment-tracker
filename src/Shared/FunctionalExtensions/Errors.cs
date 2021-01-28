using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.FunctionalExtensions
{
    public class Errors : List<KeyValuePair<string, Exception>>
    {
        public static Errors Empty() => new Errors();

        public Errors()
        {
        }

        public Errors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
                Add(new KeyValuePair<string, Exception>(error, new Exception(error)));
        }

        public Errors(IEnumerable<Exception> errors)
        {
            foreach (var error in errors)
                Add(new KeyValuePair<string, Exception>(error.Message, error));
        }

        public Errors(IEnumerable<KeyValuePair<string, Exception>> errors)
        {
            foreach (var error in errors)
                Add(error);
        }

        public bool HasErrors => Count > 0;

        public void Add(string message)
        {
            Add(message, new Exception(message));
        }

        public void Add(Exception exception)
        {
            Add(exception.Message, exception);
        }

        public void Add(string message, Exception exception)
        {
            Add(new KeyValuePair<string, Exception>(message, exception));
        }

        public string[] GetMessages() => this.Select(item => item.Key).ToArray();

        public Exception[] GetExceptions() => this.Select(item => item.Value).ToArray();
    }
}
