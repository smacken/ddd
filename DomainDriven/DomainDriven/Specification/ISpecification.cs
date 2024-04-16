namespace DomainDriven.Specification
{
    using System.Linq.Expressions;

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }

        bool IsSatisfiedBy(T obj);
    }

    public abstract class Specification<T> : ISpecification<T>
    {
        private Func<T, bool>? compiledExpression;

        private Func<T, bool> CompiledExpression =>
            this.compiledExpression ??= this.SpecExpression.Compile();

        /// <inheritdoc/>
        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        /// <inheritdoc/>
        public bool IsSatisfiedBy(T obj) => this.CompiledExpression.Invoke(obj);
    }

    public static class SpecExtensions
    {
        public static ISpecification<T> And<T>(
            this ISpecification<T> left,
            ISpecification<T> right
        ) => new And<T>(left, right);

        public static ISpecification<T> Or<T>(
            this ISpecification<T> left,
            ISpecification<T> right
        ) => new Or<T>(left, right);

        public static ISpecification<T> Not<T>(this ISpecification<T> inner) => new Not<T>(inner);
    }
}
