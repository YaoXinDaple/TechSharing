namespace LangExt.Kernel
{
    public class Option<T> where T : class
    {
        private readonly T? _value;

        private Option(T? value) => _value = value;

        public bool IsSome => _value is not null;

        public static Option<T> Some(T value) => new Option<T>(value);

        public static Option<T> None() => new Option<T>(null);

        public Option<TOut> Map<TOut>(Func<T, TOut> map) where TOut : class =>
            _value is not null ? Option<TOut>.Some(map(_value)) : Option<TOut>.None();

        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> bind) where TOut : class =>
            _value is not null ? bind(_value) : Option<TOut>.None();

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none) =>
            _value is not null ? some(_value) : none();

        public Option<T> Filter(Func<T, bool> filter) =>
            _value is not null && filter(_value) ? Some(_value) : None();

        public T ValueOrThrow() => _value ?? throw new InvalidOperationException("Option is None");

        public T ValueOr(Func<T> valueProvider) => _value ?? valueProvider();
    }

    public readonly struct ValueOption<T> where T : struct
    {
        private readonly T? _value;

        private ValueOption(T? value) => _value = value;

        public bool IsSome => _value is not null;

        public static ValueOption<T> Some(T value) => new ValueOption<T>(value);

        public static ValueOption<T> None() => new ValueOption<T>(null);

        public ValueOption<TOut> Map<TOut>(Func<T, TOut> map) where TOut : struct =>
            _value is not null ? ValueOption<TOut>.Some(map(_value.Value)) : ValueOption<TOut>.None();

        public ValueOption<TOut> Bind<TOut>(Func<T, ValueOption<TOut>> bind) where TOut : struct =>

            _value is not null ? bind(_value.Value) : ValueOption<TOut>.None();

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none) =>
            _value is not null ? some(_value.Value) : none();

        public ValueOption<T> Filter(Func<T, bool> filter) =>
                _value is not null && filter(_value.Value) ? Some(_value.Value) : None();

        public T ValueOrThrow() => _value ?? throw new InvalidOperationException("Option is None");
    }
}
